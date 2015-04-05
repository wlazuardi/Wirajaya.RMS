using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using WirajayaRMS.Business.Entities;
using WirajayaRMS.Business.ApplicationFacade;
using WirajayaRMS.CrossCutting.Security;
using WirajayaRMS.Web.UserControl;

namespace WirajayaRMS.Web.Transaksi
{
    public partial class Rekrutmen : SecurePage
    {
        List<UserAccessData> ListUserAccess 
        {
            set
            {
                ViewState["ListUserAccess"] = value;
            }
            get
            {
                if (ViewState["ListUserAccess"] == null)
                    return new List<UserAccessData>();
                else
                    return (List<UserAccessData>)ViewState["ListUserAccess"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Recruitment request list page";

            if (!IsPostBack)
            {
                lbAddNewRecruitment.PostBackUrl = "~/Transaksi/AddEditRekrutmen.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));

                List<UserAccessData> _listUserAccess = new UserAccessSystem().GetUserAccessListByKdUser(UserDataSession.KdUser);

                bool _isCanCreateRequest = false;
                foreach (UserAccessData _userAccessData in _listUserAccess) 
                {
                    if (_userAccessData.LevelApproval.KdLevelApproval == "1") 
                    {
                        _isCanCreateRequest = true;
                        break;
                    }
                }

                if (_isCanCreateRequest == true || UserDataSession.IsAdmin == 1)
                {
                    lbAddNewRecruitment.Visible = true;
                }
                else 
                {
                    lbAddNewRecruitment.Visible = false;
                }

                List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);
                ddlDivisi.DataSource = _listDivisi;
                ddlDivisi.DataTextField = "KdNmDivisi";
                ddlDivisi.DataValueField = "KdDivisi";
                ddlDivisi.DataBind();
                ddlDivisi.Items.Insert(0, new ListItem("-- Select Division --","0"));
                ddlDivisi.SelectedIndex = 0;

                ddlDivisiHistory.DataSource = _listDivisi;
                ddlDivisiHistory.DataTextField = "KdNmDivisi";
                ddlDivisiHistory.DataValueField = "KdDivisi";
                ddlDivisiHistory.DataBind();
                ddlDivisiHistory.Items.Insert(0, new ListItem("-- Select Division --", "0"));
                ddlDivisiHistory.SelectedIndex = 0;

                ListUserAccess = new UserAccessSystem().GetUserAccessListByKdUser(UserDataSession.KdUser);

                //List<RecruitmentData> _listRecruitment = new RecruitmentSystem().GetRecruitmentList(Convert.ToInt32(ddlDivisi.SelectedValue));
                List<RecruitmentData> _listRecruitment = new RecruitmentSystem().GetRecruitmentListOnHold(UserDataSession.KdUser, Convert.ToInt32(ddlDivisi.SelectedValue), 0, 0);
                rptRecruitment.DataSource = _listRecruitment;
                rptRecruitment.DataBind();

                List<RecruitmentData> _listRecruitmentHistory = new RecruitmentSystem().GetRecruitmentHistoryList(UserDataSession.KdUser, Convert.ToInt32(ddlDivisiHistory.SelectedValue), 0, 0);                
                rptRecruitmentHistory.DataSource = _listRecruitmentHistory;
                rptRecruitmentHistory.DataBind();                
            }
        }

        protected void ddlDivisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<RecruitmentData> _listRecruitment = new RecruitmentSystem().GetRecruitmentListOnHold(UserDataSession.KdUser, Convert.ToInt32(ddlDivisi.SelectedValue), 0, 0);            
            rptRecruitment.DataSource = _listRecruitment;
            rptRecruitment.DataBind();
        }

        protected void ddlDivisiHistory_SelectedIndexChanged(object sender, EventArgs e) 
        {
            List<RecruitmentData> _listRecruitmentHistory = new RecruitmentSystem().GetRecruitmentHistoryList(UserDataSession.KdUser, Convert.ToInt32(ddlDivisiHistory.SelectedValue), 0, 0);            
            rptRecruitmentHistory.DataSource = _listRecruitmentHistory;
            rptRecruitmentHistory.DataBind();            
        }

        protected void rptRecruitment_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litNo = (Literal)e.Item.FindControl("litNo");
                HyperLink linkNoRequest = (HyperLink)e.Item.FindControl("linkNoRequest");
                Literal litStrukturOrganisasi = (Literal)e.Item.FindControl("litStrukturOrganisasi");
                Literal litJabatan = (Literal)e.Item.FindControl("litJabatan");
                Literal litJmlOrang = (Literal)e.Item.FindControl("litJmlOrang");
                Literal litAlasan = (Literal)e.Item.FindControl("litAlasan");
                Literal litStatus = (Literal)e.Item.FindControl("litStatus");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                HyperLink btnPrint = (HyperLink)e.Item.FindControl("btnPrint");

                RecruitmentData _data = (RecruitmentData)e.Item.DataItem;

                litNo.Text = (e.Item.ItemIndex + 1).ToString();
                linkNoRequest.Text = _data.NoRequest;
                linkNoRequest.NavigateUrl = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_data.NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));
                btnEdit.PostBackUrl = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_data.NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));
                litStrukturOrganisasi.Text = _data.StrukturOrganisasi.NmStrukturOrganisasi;
                litJabatan.Text = _data.Jabatan.NmJabatan;
                litJmlOrang.Text = _data.JmlOrang.ToString();
                if (_data.KdAlasan == 1)
                {
                    litAlasan.Text = "New Employee Allocation";
                }
                else
                {
                    litAlasan.Text = "Employee Replacement";
                }
                litStatus.Text = _data.StatusDokumen;

                btnPrint.NavigateUrl = "~/Transaksi/RequestReport.ashx?no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_data.NoRequest));
                btnPrint.Visible = false;
                bool isCanPrint = false;
                foreach (UserAccessData _userAccessItem in ListUserAccess)
                {
                    // Cari apakah User merupakan bagian rekrutmen pada struktur organisasi-divisi terkait dan
                    // apakah status dokumen saat ini tengah dalam proses rekrutmen (termasuk closed)
                    int _position = _data.StrukturOrganisasi.KdSO.IndexOf(_userAccessItem.StrukturOrganisasi.KdSO);
                    if (_position == 0 && _data.KdDivisi == _userAccessItem.Divisi.KdDivisi && _userAccessItem.LevelApproval.KdLevelApproval == "5")
                    {
                        isCanPrint = true;
                        break;
                    }
                }
                if ((isCanPrint == true || UserDataSession.IsAdmin == 1) &&
                    (_data.CurrLevelApproval.Substring(0, 1) == "5" || _data.CurrLevelApproval.Substring(0, 1) == "6"))
                {
                    btnPrint.Visible = true;
                }
            }
        }

        protected void rptRecruitmentHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Literal litNo = (Literal)e.Item.FindControl("litNo");
                HyperLink linkNoRequest = (HyperLink)e.Item.FindControl("linkNoRequest");
                Literal litStrukturOrganisasi = (Literal)e.Item.FindControl("litStrukturOrganisasi");
                Literal litJabatan = (Literal)e.Item.FindControl("litJabatan");
                Literal litJmlOrang = (Literal)e.Item.FindControl("litJmlOrang");
                Literal litAlasan = (Literal)e.Item.FindControl("litAlasan");
                Literal litStatus = (Literal)e.Item.FindControl("litStatus");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                HyperLink btnPrint = (HyperLink)e.Item.FindControl("btnPrint");

                RecruitmentData _data = (RecruitmentData)e.Item.DataItem;

                litNo.Text = (e.Item.ItemIndex + 1).ToString();
                linkNoRequest.Text = _data.NoRequest;
                linkNoRequest.NavigateUrl = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_data.NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));
                btnEdit.PostBackUrl = "~/Transaksi/AddEditRekrutmen.aspx?e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_data.NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt("11"));
                litStrukturOrganisasi.Text = _data.StrukturOrganisasi.NmStrukturOrganisasi;
                litJabatan.Text = _data.Jabatan.NmJabatan;
                litJmlOrang.Text = _data.JmlOrang.ToString();
                if (_data.KdAlasan == 1)
                {
                    litAlasan.Text = "New Employee Allocation";
                }
                else
                {
                    litAlasan.Text = "Employee Replacement";
                }
                litStatus.Text = _data.StatusDokumen;

                btnPrint.NavigateUrl = "~/Transaksi/RequestReport.ashx?no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_data.NoRequest));
                btnPrint.Visible = false;
                bool isCanPrint = false;
                foreach (UserAccessData _userAccessItem in ListUserAccess)
                {
                    // Cari apakah User merupakan bagian rekrutmen pada struktur organisasi-divisi terkait dan
                    // apakah status dokumen saat ini tengah dalam proses rekrutmen (termasuk closed)
                    int _position = _data.StrukturOrganisasi.KdSO.IndexOf(_userAccessItem.StrukturOrganisasi.KdSO);
                    if (_position == 0 && _data.KdDivisi == _userAccessItem.Divisi.KdDivisi &&_userAccessItem.LevelApproval.KdLevelApproval == "5")
                    {
                        isCanPrint = true;
                        break;
                    }
                }
                if ((isCanPrint == true || UserDataSession.IsAdmin == 1) && 
                    (_data.CurrLevelApproval.Substring(0, 1) == "5" || _data.CurrLevelApproval.Substring(0, 1) == "6"))
                {
                    btnPrint.Visible = true;
                }
            }
        }
    }
}
