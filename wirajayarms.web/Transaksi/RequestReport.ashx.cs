using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using WirajayaRMS.CrossCutting.Security;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.Business.ApplicationFacade;
using System.Collections.Generic;
using System.Globalization;
using WirajayaRMS.CrossCutting.OptManagement;
using System.Web.SessionState;

namespace WirajayaRMS.Web.Transaksi
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class RequestReport : IHttpHandler, IReadOnlySessionState 
    {
        public UserData userData
        {
            set;
            get;
        }

        public void ProcessRequest(HttpContext context)
        {
            userData = (UserData)context.Session[SessionNameFactory.UserData];

            if (context.Request.QueryString["no"] != null && context.Request.QueryString["no"] != "" && userData != null)
            {
                string noRequest = Rijndael.Decrypt(HttpUtility.UrlDecode(context.Request.QueryString["no"]));
                byte[] b = WritePdf(context, noRequest);

                using (PdfReader reader = new PdfReader(b))
                {
                    context.Response.ContentType = "application/pdf";
                    context.Response.AddHeader("content-disposition", "attachment;filename=Request Report " + noRequest.Replace("/", "_") + ".pdf");
                    context.Response.Cache.SetCacheability(HttpCacheability.NoCache);

                    using (PdfStamper stamper = new PdfStamper(reader, context.Response.OutputStream))
                    {
                        int PageCount = reader.NumberOfPages;
                        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);

                        BaseFont arial = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        Font titleFont = new Font(arial, 12, Font.NORMAL);

                        RecruitmentData recData = new RecruitmentSystem().GetRecruitmentData(noRequest);
                        DivisiData divisiData = new DivisiSystem().GetDivisiData(recData.KdDivisi);

                        for (int i = 1; i <= PageCount; i++)
                        {
                            string pageNumber = String.Format("Page {0} of {1}", i, PageCount);
                            PdfContentByte over = stamper.GetOverContent(i);
                            over.BeginText();
                            over.SetTextMatrix(reader.GetPageSize(i).Width - 70, 36);
                            over.SetFontAndSize(bf, 8);
                            over.ShowText(pageNumber);
                            over.EndText();

                            Image logo = Image.GetInstance(context.Server.MapPath("~/img/wirajaya_packindo.png"));
                            logo.ScaleAbsolute(100f, 60f);
                            logo.SetAbsolutePosition(reader.GetPageSize(i).Left + 40, reader.GetPageSize(i).Top - 90);
                            over.AddImage(logo);

                            PdfPTable tabHeader = new PdfPTable(new float[] { 1f });
                            tabHeader.TotalWidth = reader.GetPageSize(i).Width;
                            tabHeader.HorizontalAlignment = Rectangle.ALIGN_CENTER;

                            PdfPCell footerCell = new PdfPCell(new Paragraph("PT WIRAJAYA PACKINDO", titleFont));
                            footerCell.Border = Rectangle.NO_BORDER;
                            footerCell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            tabHeader.AddCell(footerCell);

                            footerCell = new PdfPCell(new Phrase("DIVISI: " + divisiData.NmDivisi, titleFont));
                            footerCell.Border = Rectangle.NO_BORDER;
                            footerCell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            tabHeader.AddCell(footerCell);

                            footerCell = new PdfPCell(new Phrase("Employee Recruitment Request Report\n\n", titleFont));
                            footerCell.Border = Rectangle.NO_BORDER;
                            footerCell.HorizontalAlignment = Rectangle.ALIGN_CENTER;
                            footerCell.PaddingBottom = 30;
                            tabHeader.AddCell(footerCell);

                            tabHeader.WriteSelectedRows(0, -1, reader.GetPageSize(i).Right - tabHeader.TotalWidth, reader.GetPageSize(i).Top - 36, over);

                            over.MoveTo(reader.GetPageSize(i).Left + 36, reader.GetPageSize(i).Top - 12 - tabHeader.TotalHeight);
                            over.LineTo(reader.GetPageSize(i).Right - 36, reader.GetPageSize(i).Top - 12 - tabHeader.TotalHeight);
                            //Set a color
                            over.SetColorStroke(BaseColor.BLACK);
                            //Draw the rectangle
                            over.Stroke();
                        }
                    }

                    context.Response.End();
                }
            }
            else 
            {
                context.Response.Redirect("~/Login.aspx");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private byte[] WritePdf(HttpContext context, string noRequest)
        {
            using (System.IO.MemoryStream output = new System.IO.MemoryStream())
            {
                RecruitmentData recData = new RecruitmentSystem().GetRecruitmentData(noRequest);
                DivisiData divisiData = new DivisiSystem().GetDivisiData(recData.KdDivisi);
                StrukturOrganisasiData soData = new StrukturOrganisasiSystem().GetStrukturOrganisasiData(recData.KdDivisi, recData.StrukturOrganisasi.KdSO);
                JabatanData jabatanData = new JabatanSystem().GetJabatanData(recData.KdDivisi, recData.Jabatan.KdJabatan);
                List<JobDescData> jobDescList = new RecruitmentSystem().GetRecruitmentJobDescList(noRequest);
                List<QualificationData> qualificationList = new RecruitmentSystem().GetRecruitmentQualification(noRequest);
                LevelApprovalData lvAppData = new LevelApprovalSystem().GetLevelApprovalData(divisiData.KdDivisi, recData.CurrLevelApproval);

                Document doc = new Document(iTextSharp.text.PageSize.A4, 36, 36, 110, 130);
                PdfWriter writer = PdfWriter.GetInstance(doc, output);

                // calling PDFFooter class to Include in document
                writer.PageEvent = new PdfPage() { PrintedBy = userData.FullName };
                doc.Open();

                //BaseFont baseFont = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\ariblk.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                BaseFont arial = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                Font titleFont = new Font(arial, 12, Font.NORMAL);
                Font sectionHeaderFont = new Font(arial, 11, Font.BOLD);
                Font sectionSubHeaderFont = new Font(arial, 10, Font.ITALIC);
                Font fieldHeaderFont = new Font(arial, 8, Font.NORMAL);
                Font fieldSubHeaderFont = new Font(arial, 8, Font.ITALIC);
                Font fieldValueFont = new Font(arial, 10, Font.NORMAL);
                Font signatureFont = new Font(arial, 10, Font.BOLD);

                PdfPTable formTable = new PdfPTable(new float[] { 
                    (doc.PageSize.Width * 0.25f), 
                    (doc.PageSize.Width * 0.25f),
                    (doc.PageSize.Width * 0.25f),
                    (doc.PageSize.Width * 0.25f)
                });

                formTable.WidthPercentage = 100;
                formTable.SplitLate = false;
                formTable.DefaultCell.Border = Rectangle.NO_BORDER;

                PdfPCell cell = new PdfPCell();
                cell.AddElement(new Phrase("Request Data", sectionHeaderFont));
                cell.AddElement(new Phrase("Data Permohonan", sectionSubHeaderFont));
                cell.Colspan = 4;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.NO_BORDER | Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                // Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("NO. REQUEST", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("NUMBER OF PERSON", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("REQUEST DATE", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("REQUIRED DATE", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.NO_BORDER;
                formTable.AddCell(cell);

                // Sub Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("No. Request", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("Jumlah Orang", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("Tanggal Permohonan", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.PaddingTop = 0f;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("Tanggal Dibutuhkan", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.PaddingTop = 0f;
                cell.Border = Rectangle.NO_BORDER;
                formTable.AddCell(cell);

                // Field Value
                cell = new PdfPCell();
                cell.AddElement(new Phrase(noRequest, fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase(recData.JmlOrang.ToString() + " person(s)", fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase(recData.TglRequest.ToString("dd-MMMM-yyyy"), fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase(recData.TglButuh.ToString("dd-MMMM-yyyy") ,fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.NO_BORDER | Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                // Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("REQUESTED BY", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("REASON", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("REQUEST STATUS", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                // Sub Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("Diajukan Oleh", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("Alasan", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("Status Permohonan", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                // Field Value
                UserData creatorData = new UserSystem().GetUserData(recData.Creator.KdUser);
                cell = new PdfPCell();
                cell.AddElement(new Phrase(creatorData.FullName, fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase((recData.KdAlasan == 1) ? "New Employee Allocation" : "Employee Replacement", fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase(lvAppData.StatusDokumen, fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                /*
                 * Second Section
                 * Position Data
                 */
                cell = new PdfPCell();
                cell.AddElement(new Phrase("Position Data", sectionHeaderFont));
                cell.AddElement(new Phrase("Data Posisi", sectionSubHeaderFont));
                cell.Colspan = 4;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                // Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("ORGANIZATION STRUCTURE", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("NUM. OF CURRENT EMPLOYEES", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("MAX NUMBER OF EMPLOYEES", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.NO_BORDER;
                formTable.AddCell(cell);

                // Sub Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("Struktur Organisasi", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.RIGHT_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("Jumlah Karyawan Saat Ini", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase("Jumlah Karyawan Maksimal", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.PaddingTop = 0f;
                cell.Border = Rectangle.NO_BORDER;
                formTable.AddCell(cell);

                // Field Value
                cell = new PdfPCell();
                //Cek apakah struktur organisasi mempunyai parent?
                string namaSO = soData.NmStrukturOrganisasi;
                if (recData.StrukturOrganisasi.KdSO.Count(i => i == '.') > 1)
                {
                    int position = recData.StrukturOrganisasi.KdSO.IndexOf('.', soData.KdSO.IndexOf('.') + 1);
                    string kdDept = recData.StrukturOrganisasi.KdSO.Substring(0, position);
                    StrukturOrganisasiData parentSO = new StrukturOrganisasiSystem().GetStrukturOrganisasiData(recData.KdDivisi, kdDept);
                    namaSO = parentSO.NmStrukturOrganisasi + " - " + namaSO;
                }
                cell.AddElement(new Phrase(namaSO, fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase(soData.JmlKaryawan.ToString() + " person(s)", fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.AddElement(new Phrase(soData.MaxJmlKaryawan.ToString() + " person(s)", fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                // Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("POSITION", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                // Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("RANGE OF SALARY", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                // Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("FACILITY", fieldHeaderFont));
                cell.PaddingTop = 3f;
                cell.Border = Rectangle.NO_BORDER;
                formTable.AddCell(cell);

                // Sub Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("Posisi", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.RIGHT_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                // Sub Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("Kisaran Gaji", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.NO_BORDER | Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                // Sub Field Header
                cell = new PdfPCell();
                cell.AddElement(new Phrase("Fasilitas", fieldSubHeaderFont));
                cell.UseAscender = true;
                cell.Border = Rectangle.NO_BORDER;
                formTable.AddCell(cell);

                // Field Value
                cell = new PdfPCell();
                cell.AddElement(new Phrase(jabatanData.NmJabatan, fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                cell.Colspan = 2;
                formTable.AddCell(cell);

                // Field Value
                cell = new PdfPCell();
                cell.AddElement(new Phrase(jabatanData.MinSalary.ToString("C", CultureInfo.CreateSpecificCulture("id-ID")) + " - " + jabatanData.MaxSalary.ToString("C", CultureInfo.CreateSpecificCulture("id-ID")), fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                formTable.AddCell(cell);

                // Field Value
                cell = new PdfPCell();
                cell.AddElement(new Phrase((jabatanData.Fasilitas == null || jabatanData.Fasilitas == "" ? "N/A" : jabatanData.Fasilitas), fieldValueFont));
                cell.PaddingTop = 0f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                /*
                 * Third Section
                 * Job Description
                 */
                cell = new PdfPCell();
                cell.AddElement(new Phrase("Job Description", sectionHeaderFont));
                cell.AddElement(new Phrase("Deskripsi Pekerjaan", sectionSubHeaderFont));
                cell.Colspan = 4;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.Colspan = 4;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                iTextSharp.text.List jobDescUL = new List(List.UNORDERED, 10f);
                jobDescUL.SetListSymbol("\u2022");
                foreach (JobDescData item in jobDescList)
                {
                    jobDescUL.Add(new iTextSharp.text.ListItem(char.ToUpper(item.JobDesc[0]) + item.JobDesc.Substring(1),fieldValueFont));
                }
                cell.AddElement(jobDescUL);
                formTable.AddCell(cell);

                /*
                 * Fourth Section
                 * Qualification
                 */
                cell = new PdfPCell();
                cell.AddElement(new Phrase("Qualification", sectionHeaderFont));
                cell.AddElement(new Phrase("Kualifikasi", sectionSubHeaderFont));
                cell.Colspan = 4;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                formTable.AddCell(cell);

                cell = new PdfPCell();
                cell.Colspan = 4;
                cell.PaddingTop = 5f;
                cell.PaddingBottom = 5f;
                cell.Border = Rectangle.BOTTOM_BORDER;
                iTextSharp.text.List qualificationUL = new List(List.UNORDERED, 10f);
                qualificationUL.SetListSymbol("\u2022");
                foreach (QualificationData item in qualificationList)
                {
                    qualificationUL.Add(new iTextSharp.text.ListItem(char.ToUpper(item.Qualification[0]) + item.Qualification.Substring(1), fieldValueFont));
                }
                cell.AddElement(qualificationUL);
                formTable.AddCell(cell);

                doc.Add(formTable);

                PdfPTable tabFooter = new PdfPTable(new float[] { 1f, 1f });
                tabFooter.TotalWidth = 300f;

                PdfPCell footerCell = new PdfPCell(new Phrase("Direktur", signatureFont));
                footerCell.CellEvent = new CellSpacingEvent(10);
                footerCell.Border = Rectangle.NO_BORDER;
                tabFooter.AddCell(footerCell);
                footerCell = new PdfPCell(new Phrase("Recruitment", signatureFont));
                footerCell.CellEvent = new CellSpacingEvent(10);
                footerCell.Border = Rectangle.NO_BORDER;
                tabFooter.AddCell(footerCell);
                footerCell = new PdfPCell(new Phrase("Sign here/Tanda tangan di sini", sectionSubHeaderFont));
                footerCell.Border = Rectangle.NO_BORDER;
                tabFooter.AddCell(footerCell);
                footerCell = new PdfPCell(new Phrase("Sign here/Tanda tangan di sini", sectionSubHeaderFont));
                footerCell.Border = Rectangle.NO_BORDER;
                tabFooter.AddCell(footerCell);
                footerCell = new PdfPCell(new Phrase("Date/Tgl.: ________________", sectionSubHeaderFont));
                footerCell.Border = Rectangle.NO_BORDER;
                tabFooter.AddCell(footerCell);
                footerCell = new PdfPCell(new Phrase("Date/Tgl.: ________________", sectionSubHeaderFont));
                footerCell.Border = Rectangle.NO_BORDER;
                tabFooter.AddCell(footerCell);

                tabFooter.WriteSelectedRows(0, -1, doc.Left, 28 + tabFooter.TotalHeight, writer.DirectContent);

                doc.Close();

                return output.ToArray();
            }
        }
    }

    public class PdfPage : PdfPageEventHelper 
    {
        public string PrintedBy { get; set; }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            BaseFont arial = BaseFont.CreateFont("C:\\WINDOWS\\Fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font fieldHeaderFont = new Font(arial, 8, Font.NORMAL);
            PdfPTable tabHeader = new PdfPTable(new float[] { 1F });
            tabHeader.TotalWidth = 150F;
            PdfPCell cell = new PdfPCell(new Phrase("Creation Date: " + DateTime.Now.ToString("dd/MMM/yyyy"), fieldHeaderFont));
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            tabHeader.AddCell(cell);
            cell = new PdfPCell(new Phrase("Generated By: " + PrintedBy, fieldHeaderFont));
            cell.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
            cell.Border = Rectangle.NO_BORDER;
            tabHeader.AddCell(cell);
            tabHeader.WriteSelectedRows(0, -1, document.Right - tabHeader.TotalWidth, document.Top + 110 - 36, writer.DirectContent);
        }
    }

    public class CellSpacingEvent : IPdfPCellEvent
    {
        private int cellSpacing;

        public CellSpacingEvent(int cellSpacing)
        {
            this.cellSpacing = cellSpacing;
        }

        void IPdfPCellEvent.CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
        {
            //Grab the line canvas for drawing lines on
            PdfContentByte cb = canvases[PdfPTable.LINECANVAS];
            cb.MoveTo(position.Left, position.Top);
            cb.LineTo(position.Right - this.cellSpacing, position.Top);
            //Set a color
            cb.SetColorStroke(BaseColor.BLACK);
            //Draw the rectangle
            cb.Stroke();
        }
    }
}
