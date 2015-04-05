﻿using System;
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
using WirajayaRMS.Web.UserControl;
using WirajayaRMS.CrossCutting.Security;
using System.Globalization;
using System.Drawing;
using WirajayaRMS.CrossCutting.OptManagement;
using System.IO;
using AjaxControlToolkit;
using iTextSharp.text.pdf;
using System.Net;
using iTextSharp.text.html.simpleparser;
using System.Diagnostics;

namespace WirajayaRMS.Web.Transaksi
{
    public partial class AddEditRekrutmen : SecurePage
    {
        #region View State
        public bool IsCanEdit
        {
            set
            {
                ViewState["IsCanEdit"] = value;
            }
            get
            {
                if (ViewState["IsCanEdit"] == null)
                    return true;
                return (bool)ViewState["IsCanEdit"];
            }
        }

        public bool IsCanPrint
        {
            set
            {
                ViewState["IsCanPrint"] = value;
            }
            get
            {
                if (ViewState["IsCanPrint"] == null)
                    return true;
                return (bool)ViewState["IsCanPrint"];
            }
        }

        public List<JobDescData> ListJobDesc
        {
            set
            {
                ViewState["ListJobDesc"] = value;
            }
            get
            {
                if (ViewState["ListJobDesc"] == null)
                    return new List<JobDescData>();
                else
                    return (List<JobDescData>)ViewState["ListJobDesc"];
            }
        }

        public List<QualificationData> ListQualification
        {
            set
            {
                ViewState["ListQualification"] = value;
            }
            get
            {
                if (ViewState["ListQualification"] == null)
                    return new List<QualificationData>();
                else
                    return (List<QualificationData>)ViewState["ListQualification"];
            }
        }

        public string NoRequest
        {
            set
            {
                ViewState["NoRequest"] = value;
            }
            get
            {
                if (ViewState["NoRequest"] == null)
                    return String.Empty;
                else
                    return ViewState["NoRequest"].ToString();
            }
        }

        public List<QualificationMatchingData> ListAdditionalQualification
        {
            set
            {
                ViewState["ListAdditionalQualification"] = value;
            }
            get
            {
                if (ViewState["ListAdditionalQualification"] == null)
                    return new List<QualificationMatchingData>();
                else
                    return (List<QualificationMatchingData>)ViewState["ListAdditionalQualification"];
            }
        }

        public List<FileUpload> ListCVFile
        {
            set
            {
                Session["ListCVFile"] = value;
            }
            get
            {
                if (Session["ListCVFile"] == null)
                    return new List<FileUpload>();
                else
                    return (List<FileUpload>)Session["ListCVFile"];
            }
        }

        public List<FileData> ListExistingCVFile
        {
            set
            {
                ViewState["ListExistingCVFile"] = value;
            }
            get
            {
                if (ViewState["ListExistingCVFile"] == null)
                    return new List<FileData>();
                else
                    return (List<FileData>)ViewState["ListExistingCVFile"];
            }
        }
        #endregion

        #region Private Method
        private void BuildSODropdownTree()
        {
            divJmlKaryawan.Visible = true;
            divMaxJmlKaryawan.Visible = true;

            ddlStrukturOrganisasi.Items.Clear();

            List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiSystem().GetListStrukturOrganisasiByKdUser(Convert.ToInt32(ddlDivisi.SelectedValue), UserDataSession.KdUser);

            foreach (StrukturOrganisasiData _itemSO in _listSO)
            {
                string _value = _itemSO.KdSO;
                string _text = _itemSO.ParentNmStrukturOrganisasi + _itemSO.NmStrukturOrganisasi;

                ddlStrukturOrganisasi.Items.Add(new ListItem(_value + ". " + _text, _value));

                if (_itemSO.ChildNode.Count > 0)
                {
                    BuildSOChildNode(_itemSO, 1);
                }
            }

            ddlStrukturOrganisasi.Items.Insert(0, new ListItem("-- Select Organizational Structure --", "0"));
            ddlStrukturOrganisasi.SelectedIndex = 0;

            BuildJabatanDropdown();
        }

        private void BuildSOChildNode(StrukturOrganisasiData _node, int _depth)
        {
            foreach (StrukturOrganisasiData _item in _node.ChildNode)
            {
                string _value = _item.KdSO;
                string _text = _item.ParentNmStrukturOrganisasi + _item.NmStrukturOrganisasi;

                string _space = "";
                for (int i = 0; i < _depth; i++)
                {
                    _space += "&nbsp;&nbsp;&nbsp;";
                }

                ddlStrukturOrganisasi.Items.Add(new ListItem(HttpUtility.HtmlDecode(_space) + _value + ". " + _text, _value));

                if (_item.ChildNode.Count > 0)
                {
                    BuildSOChildNode(_item, _depth + 1);
                }

            }
        }

        private void BuildJabatanDropdown()
        {
            ddlJabatan.Items.Clear();

            StrukturOrganisasiData _soData = new StrukturOrganisasiSystem().GetStrukturOrganisasiData(Convert.ToInt32(ddlDivisi.SelectedValue), ddlStrukturOrganisasi.SelectedValue);

            if (_soData.KdUnit != null)
            {
                List<JabatanData> _listJabatan = new JabatanSystem().GetUnitMaxJabatanList(Convert.ToInt32(ddlDivisi.SelectedValue), _soData.KdUnit);
                ddlJabatan.DataSource = _listJabatan;
                ddlJabatan.DataTextField = "KdNmJabatan";
                ddlJabatan.DataValueField = "KdJabatan";
                ddlJabatan.DataBind();
            }

            ddlJabatan.Items.Insert(0, new ListItem("-- Select Position --", "0"));
            ddlJabatan.SelectedIndex = 0;

            BuildJobDescRepeater();
            BuildQualificationRepeater();
        }

        private void BuildJobDescRepeater()
        {
            ListJobDesc = new JobDescSystem().GetJobDescList(Convert.ToInt32(ddlDivisi.SelectedValue), ddlStrukturOrganisasi.SelectedValue, ddlJabatan.SelectedValue);

            // Reset KdJobDesc 
            foreach (JobDescData _data in ListJobDesc)
            {
                _data.KdJobDesc = 0;
            }

            rptJobDesc.DataSource = ListJobDesc;
            rptJobDesc.DataBind();
        }

        private void BuildQualificationRepeater()
        {
            ListQualification = new QualificationSystem().GetQualificationList(Convert.ToInt32(ddlDivisi.SelectedValue), ddlStrukturOrganisasi.SelectedValue, ddlJabatan.SelectedValue);

            // Reset KdQualification
            foreach (QualificationData _data in ListQualification)
            {
                _data.KdQualification = 0;
            }

            rptQualification.DataSource = ListQualification;
            rptQualification.DataBind();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Recruitment request add/edit page";

            // Code agar bisa upload file saat di pop up
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnAddKandidat);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnEditKandidat);
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!IsPostBack)
            {
                lbBack.PostBackUrl = "~/Transaksi/Rekrutmen.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString()));

                List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);
                ddlDivisi.DataSource = _listDivisi;
                ddlDivisi.DataTextField = "KdNmDivisi";
                ddlDivisi.DataValueField = "KdDivisi";
                ddlDivisi.DataBind();
                ddlDivisi.Items.Insert(0, new ListItem("-- Select Division --", "0"));
                ddlDivisi.SelectedIndex = 0;

                // Tentukan apakah edit atau tambah baru
                if (Request.QueryString["e"] != null && Request.QueryString["e"] == "1" &&
                    Request.QueryString["no"] != null && Request.QueryString["no"] != "")
                {
                    LoadRecruitmentRequestData();
                }
                else
                {
                    BuildSODropdownTree();
                    BuildJabatanDropdown();
                    BuildJobDescRepeater();
                    BuildQualificationRepeater();

                    divNoRequest.Visible = false;
                    divStatusRequest.Visible = false;
                    divJmlKaryawan.Visible = false;
                    divMaxJmlKaryawan.Visible = false;
                    divFasilitas.Visible = false;
                    divGaji.Visible = false;
                    divRequestCreator.Visible = false;
                }


                ratingEditKandidat.CssClass += "ratingControl";
                ratingEditKandidat.Attributes.Add("data-behaviorid", "ratingEditKandidat");

                ratingAddKandidat.CssClass += "ratingControl";
                ratingAddKandidat.Attributes.Add("data-behaviorid", "ratingAddKandidat");
            }
        }

        private bool IsRequestClosed()
        {
            RecruitmentData _recData = new RecruitmentSystem().GetRecruitmentData(NoRequest);
            if (_recData.CurrLevelApproval == "6")
                return true;
            else return false;
        }

        private void CheckPrivileges()
        {
            RecruitmentData _recData = new RecruitmentSystem().GetRecruitmentData(NoRequest);
            CheckPrivileges(_recData);
        }

        private void CheckPrivileges(RecruitmentData _recData)
        {
            if (UserDataSession.IsAdmin == 1)
            {
                IsCanEdit = false;
                if (_recData.CurrLevelApproval != "6")
                {
                    IsCanEdit = true;
                }

                IsCanPrint = false;
                if ((_recData.CurrLevelApproval.Substring(0, 1) == "5" || _recData.CurrLevelApproval.Substring(0, 1) == "6"))
                {
                    IsCanPrint = true;
                }
            }
            else
            {
                // Cari apakah user memiliki akses terhadap request dilihat dari current lv approval
                // Contoh: Saat current lv approval = 2, hanya GM yang boleh edit, Manager tidak boleh            
                bool _isAnySameSO = false;
                IsCanEdit = false;
                IsCanPrint = false;

                List<UserAccessData> _listUserAccess = new UserAccessSystem().GetUserAccessListByKdUser(UserDataSession.KdUser);
                foreach (UserAccessData _userAccessItem in _listUserAccess)
                {
                    bool _inTheSameSO = false;

                    int _position = _recData.StrukturOrganisasi.KdSO.IndexOf(_userAccessItem.StrukturOrganisasi.KdSO);
                    //if (_position == 0)
                    //{
                    //    _inTheSameSO = true;
                    //    _isAnySameSO = true;
                    //}

                    if (_recData.KdDivisi == _userAccessItem.Divisi.KdDivisi && _position == 0)
                    {
                        _inTheSameSO = true;
                        _isAnySameSO = true;
                    }

                    // Hanya boleh print kalau sudah sampai recruitment
                    if (_inTheSameSO &&
                        (_recData.CurrLevelApproval.Substring(0, 1) == "5" || _recData.CurrLevelApproval.Substring(0, 1) == "6") &&
                        _userAccessItem.LevelApproval.KdLevelApproval == "5")
                    {
                        IsCanPrint = true;
                    }

                    // Periksa juga untuk current lv approval 5.1, 5.2, dst... (oleh karena itu Substring 1 karakter dari kiri saja)
                    if (_inTheSameSO && _recData.CurrLevelApproval.Substring(0, 1) == _userAccessItem.LevelApproval.KdLevelApproval)
                    {
                        IsCanEdit = true;
                        break;
                    }
                }

                if (!_isAnySameSO)
                {
                    Response.Redirect("~/NotAuthorized.aspx");
                }
            }
        }

        private void LoadRecruitmentRequestData()
        {
            if (Request.QueryString["notif"] != null && Request.QueryString["notif"] != "")
            {
                int kdNotification = Convert.ToInt32(Rijndael.Decrypt(HttpUtility.HtmlDecode(Request.QueryString["notif"])));
                int readResult = new NotificationSystem().MarkNotifRead(kdNotification);
            }

            txtQualification.Text = "";
            txtJobDesc.Text = "";

            if (Request.QueryString["success"] != null && Request.QueryString["success"] == "1")
                alertNotification.Show("Data saved successfully", AlertType.Success);

            if (Request.QueryString["error"] != null && Request.QueryString["error"] == "1")
                alertNotification.Show("Failed to save the data", AlertType.Danger);

            NoRequest = Rijndael.Decrypt(HttpUtility.UrlDecode(Request.QueryString["no"]));

            btnPrint.NavigateUrl = "~/Transaksi/RequestReport.ashx?no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest));

            lblNoRequest.Text = NoRequest;

            RecruitmentData _recData = new RecruitmentSystem().GetRecruitmentData(NoRequest);

            UserData creatorData = new UserSystem().GetUserData(_recData.Creator.KdUser);
            divRequestCreator.Visible = true;
            lblRequestCreator.Text = creatorData.FullName;

            ddlDivisi.SelectedValue = _recData.KdDivisi.ToString();
            BuildSODropdownTree();
            ddlStrukturOrganisasi.SelectedValue = _recData.StrukturOrganisasi.KdSO;
            BuildJabatanDropdown();
            ddlJabatan.SelectedValue = _recData.Jabatan.KdJabatan;

            txtJmlKebutuhanKaryawan.Text = _recData.JmlOrang.ToString();
            txtTglButuh.Text = _recData.TglButuh.ToString("dd/MM/yyyy");
            rblAlasan.SelectedValue = _recData.KdAlasan.ToString();

            StrukturOrganisasiData _soData = new StrukturOrganisasiSystem().GetStrukturOrganisasiData(_recData.KdDivisi, _recData.StrukturOrganisasi.KdSO);
            lblJmlKaryawan.Text = _soData.JmlKaryawan.ToString();
            lblMaxJmlKaryawan.Text = _soData.MaxJmlKaryawan.ToString();

            JabatanData _jabatanData = new JabatanSystem().GetJabatanData(_recData.KdDivisi, _recData.Jabatan.KdJabatan);
            if (_jabatanData.Fasilitas == "" || _jabatanData.Fasilitas == null)
            {
                lblFasilitas.Text = "N/A";
            }
            else
            {
                lblFasilitas.Text = _jabatanData.Fasilitas;
            }

            divNoRequest.Visible = true;
            divJmlKaryawan.Visible = true;
            divMaxJmlKaryawan.Visible = true;
            divFasilitas.Visible = true;

            if (UserDataSession.ShowSalary == 1 || UserDataSession.IsAdmin == 1)
            {
                divGaji.Visible = true;
                lblMinSalary.Text = _jabatanData.MinSalary.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
                lblMaxSalary.Text = _jabatanData.MaxSalary.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
            }
            else
            {
                divGaji.Visible = false;
            }

            CheckPrivileges(_recData);

            if (!IsCanEdit)
            {
                txtJmlKebutuhanKaryawan.Enabled = false;
                txtTglButuh.Enabled = false;
                rblAlasan.Enabled = false;
                txtJobDesc.Enabled = false;
                btnAddJobDesc.Enabled = false;
                txtQualification.Enabled = false;
                btnAddQualification.Enabled = false;
                txtComment.Enabled = false;
                btnSaveRecruitment.Enabled = false;
                btnProcess.Enabled = false;
                btnDeclineRecruitment.Enabled = false;
                btnShowAddKandidat.Enabled = false;
            }

            if (IsCanPrint == true)
            {
                btnPrint.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
            }

            LevelApprovalData _lvAppData = new LevelApprovalSystem().GetLevelApprovalData(Convert.ToInt32(ddlDivisi.SelectedValue), _recData.CurrLevelApproval);
            divStatusRequest.Visible = true;
            lblStatuRequest.Text = _lvAppData.StatusDokumen;

            // Jika status request sudah tidak lagi di manager, 
            // tombol "Proses Permintaan" diubah jadi "Setujui Permintaan"
            if (_recData.CurrLevelApproval != "1")
            {
                pnlHistoryApproval.Visible = true;
                btnDeclineRecruitment.Visible = true;
                btnProcess.ToolTip = "Approve Request";

                HtmlGenericControl html = new HtmlGenericControl();
                html.InnerHtml = "<i class=\"glyphicon glyphicon-play\"></i>&nbsp;Approve Request";
                btnProcess.Controls.Clear();
                btnProcess.Controls.Add(html);

                // Jika status dokumen berada di rekrutmen,
                // munculkan panel kandidat (seleksi & interview kandidat)
                if (_recData.CurrLevelApproval.Equals("5.1") ||
                    _recData.CurrLevelApproval.Equals("5.2") ||
                    _recData.CurrLevelApproval.Equals("5.3") ||
                    _recData.CurrLevelApproval.Equals("5.4") ||
                    _recData.CurrLevelApproval.Equals("6"))
                {
                    pnlKandidat.Visible = true;

                    if (_recData.CurrLevelApproval.Contains("5"))
                    {
                        pnlDetailStatusRecruitment.Visible = true;
                        rblDetailStatusRecruitment.SelectedValue = _recData.CurrLevelApproval;
                    }

                    if (_recData.CurrLevelApproval.Equals("6"))
                    {
                        divInfoKandidatTerpilih.Visible = true;
                    }
                    else
                    {
                        divInfoKandidatTerpilih.Visible = false;
                    }

                    List<KandidatData> _listKandidat = new RecruitmentSystem().GetRecruitmentKandidatList(NoRequest);
                    rptKandidat.DataSource = _listKandidat;
                    rptKandidat.DataBind();


                    if (_recData.CurrLevelApproval.Equals("6") && _listKandidat.Count <= 0)
                    {
                        pnlKandidat.Visible = false;
                    }

                    if (!IsCanEdit)
                    {
                        txtAdditionalQualification.Enabled = false;
                        txtKomentarAdditionalQualification.Enabled = false;
                        btnTambahAdditionalQualification.Enabled = false;
                        btnSaveQualificationMatching.Enabled = false;
                        txtTglInterview.Enabled = false;
                        txtInterviewer.Enabled = false;
                        txtHasilInterview.Enabled = false;
                        btnShowTglInterview.Enabled = false;
                        lbAddInterview.Enabled = false;
                    }
                }
            }

            ListJobDesc = new RecruitmentSystem().GetRecruitmentJobDescList(NoRequest);
            rptJobDesc.DataSource = ListJobDesc;
            rptJobDesc.DataBind();

            ListQualification = new RecruitmentSystem().GetRecruitmentQualification(NoRequest);
            rptQualification.DataSource = ListQualification;
            rptQualification.DataBind();

            ddlDivisi.Enabled = false;
            ddlStrukturOrganisasi.Enabled = false;
            ddlJabatan.Enabled = false;

            RecruitmentApprovalData _recAppData = new RecruitmentSystem().GetRecruitmentApprovalComment(NoRequest, UserDataSession.KdUser, _recData.CurrLevelApproval);
            txtComment.Text = _recAppData.Komentar;

            List<RecruitmentApprovalData> _recAppHistoryList = new RecruitmentSystem().GetRequestApprovalHistory(NoRequest);
            rptApprovalHistory.DataSource = _recAppHistoryList;
            rptApprovalHistory.DataBind();
        }

        protected void ddlDivisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildSODropdownTree();
            divJmlKaryawan.Visible = false;
            divMaxJmlKaryawan.Visible = false;
            divFasilitas.Visible = false;
            divGaji.Visible = false;
        }

        protected void ddlStrukturOrganisasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            StrukturOrganisasiData _soData = new StrukturOrganisasiSystem().GetStrukturOrganisasiData(Convert.ToInt32(ddlDivisi.SelectedValue), ddlStrukturOrganisasi.SelectedValue);
            lblJmlKaryawan.Text = _soData.JmlKaryawan.ToString();
            lblMaxJmlKaryawan.Text = _soData.MaxJmlKaryawan.ToString();
            divJmlKaryawan.Visible = true;
            divMaxJmlKaryawan.Visible = true;
            divFasilitas.Visible = false;
            divGaji.Visible = false;
            BuildJabatanDropdown();
        }

        protected void ddlJabatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            JabatanData _jabatanData = new JabatanSystem().GetJabatanData(Convert.ToInt32(ddlDivisi.SelectedValue), ddlJabatan.SelectedValue);
            divFasilitas.Visible = true;
            if (_jabatanData.Fasilitas == "" || _jabatanData.Fasilitas == null)
            {
                lblFasilitas.Text = "N/A";
            }
            else
            {
                lblFasilitas.Text = _jabatanData.Fasilitas;
            }

            if (UserDataSession.ShowSalary == 1 || UserDataSession.IsAdmin == 1)
            {
                divGaji.Visible = true;
                lblMinSalary.Text = _jabatanData.MinSalary.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
                lblMaxSalary.Text = _jabatanData.MaxSalary.ToString("C", CultureInfo.CreateSpecificCulture("id-ID"));
            }

            BuildJobDescRepeater();
            BuildQualificationRepeater();
        }

        protected void rptJobDesc_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobDescData jobDescItem = (JobDescData)e.Item.DataItem;

                Label lblJobDesc = (Label)e.Item.FindControl("lblJobDesc");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
                RequiredFieldValidator reqEditJobDesc = (RequiredFieldValidator)e.Item.FindControl("reqEditJobDesc");

                lblJobDesc.Text = jobDescItem.JobDesc;
                btnEdit.CommandName = "EDIT";
                btnDelete.CommandName = "DELETE";
                btnSave.CommandName = "SAVE";
                btnCancel.CommandName = "CANCEL";
                btnEdit.CommandArgument = e.Item.ItemIndex.ToString();
                btnDelete.CommandArgument = e.Item.ItemIndex.ToString();
                btnSave.CommandArgument = e.Item.ItemIndex.ToString();
                reqEditJobDesc.ValidationGroup = e.Item.ItemIndex.ToString() + "-JOBDESC";
                btnSave.ValidationGroup = e.Item.ItemIndex.ToString() + "-JOBDESC";

                if (!IsCanEdit)
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        protected void rptJobDesc_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
                TextBox txtEditJobDesc = (TextBox)e.Item.FindControl("txtEditJobDesc");
                Label lblJobDesc = (Label)e.Item.FindControl("lblJobDesc");

                if (e.CommandName == "EDIT")
                {
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;

                    txtEditJobDesc.Text = lblJobDesc.Text;
                    txtEditJobDesc.Visible = true;
                    lblJobDesc.Visible = false;
                }
                else if (e.CommandName == "SAVE")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    ListJobDesc[index].JobDesc = txtEditJobDesc.Text;
                    rptJobDesc.DataSource = ListJobDesc;
                    rptJobDesc.DataBind();
                }
                else if (e.CommandName == "CANCEL")
                {
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;

                    txtEditJobDesc.Visible = false;
                    lblJobDesc.Visible = true;
                }
                else if (e.CommandName == "DELETE")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    ListJobDesc.RemoveAt(index);
                    rptJobDesc.DataSource = ListJobDesc;
                    rptJobDesc.DataBind();
                }
            }
        }

        protected void rptQualification_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                QualificationData QualificationItem = (QualificationData)e.Item.DataItem;

                HiddenField hidKdQualification = (HiddenField)e.Item.FindControl("hidKdQualification");
                Label lblQualification = (Label)e.Item.FindControl("lblQualification");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
                RequiredFieldValidator reqEditQualification = (RequiredFieldValidator)e.Item.FindControl("reqEditQualification");

                lblQualification.Text = QualificationItem.Qualification;
                btnEdit.CommandName = "EDIT";
                btnDelete.CommandName = "DELETE";
                btnSave.CommandName = "SAVE";
                btnCancel.CommandName = "CANCEL";

                btnEdit.CommandArgument = e.Item.ItemIndex.ToString();
                btnDelete.CommandArgument = e.Item.ItemIndex.ToString();
                btnSave.CommandArgument = e.Item.ItemIndex.ToString();
                reqEditQualification.ValidationGroup = e.Item.ItemIndex.ToString() + "-QUALIFICATION";
                btnSave.ValidationGroup = e.Item.ItemIndex.ToString() + "-QUALIFICATION";

                if (!IsCanEdit)
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        protected void rptQualification_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
                TextBox txtEditQualification = (TextBox)e.Item.FindControl("txtEditQualification");
                Label lblQualification = (Label)e.Item.FindControl("lblQualification");

                if (e.CommandName == "EDIT")
                {
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;

                    txtEditQualification.Text = lblQualification.Text;
                    txtEditQualification.Visible = true;
                    lblQualification.Visible = false;
                }
                else if (e.CommandName == "SAVE")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    ListQualification[index].Qualification = txtEditQualification.Text;
                    rptQualification.DataSource = ListQualification;
                    rptQualification.DataBind();
                }
                else if (e.CommandName == "CANCEL")
                {
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;

                    txtEditQualification.Visible = false;
                    lblQualification.Visible = true;
                }
                else if (e.CommandName == "DELETE")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    ListQualification.RemoveAt(index);
                    rptQualification.DataSource = ListQualification;
                    rptQualification.DataBind();
                }
            }
        }

        protected void btnAddJobDesc_Click(object sender, EventArgs e)
        {
            JobDescData _data = new JobDescData();
            _data.JobDesc = txtJobDesc.Text;
            ListJobDesc.Add(_data);
            rptJobDesc.DataSource = ListJobDesc;
            rptJobDesc.DataBind();

            txtJobDesc.Text = "";
        }

        protected void btnAddQualification_Click(object sender, EventArgs e)
        {
            QualificationData _data = new QualificationData();
            _data.Qualification = txtQualification.Text;
            ListQualification.Add(_data);
            rptQualification.DataSource = ListQualification;
            rptQualification.DataBind();

            txtQualification.Text = "";
        }

        protected void btnSaveRecruitment_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "";

                // Tambah baru
                RecruitmentData _data = new RecruitmentData();
                _data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                _data.StrukturOrganisasi = new StrukturOrganisasiData();
                _data.StrukturOrganisasi.KdSO = ddlStrukturOrganisasi.SelectedValue;
                _data.Jabatan = new JabatanData();
                _data.Jabatan.KdJabatan = ddlJabatan.SelectedValue;
                _data.TglButuh = DateTime.ParseExact(txtTglButuh.Text, "dd/MM/yyyy", null);
                _data.KdAlasan = Convert.ToInt32(rblAlasan.SelectedValue);
                _data.JmlOrang = Convert.ToInt32(txtJmlKebutuhanKaryawan.Text);
                _data.JobDescList = ListJobDesc;
                _data.QualificationList = ListQualification;

                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();
                _recAppData.Komentar = txtComment.Text;
                _recAppData.IsFinalize = 0;
                _recAppData.User = new UserData();
                _recAppData.User.KdUser = UserDataSession.KdUser;
                _data.ApprovalDataList = new List<RecruitmentApprovalData>();
                _data.ApprovalDataList.Add(_recAppData);

                // Tentukan apakah edit atau tambah baru
                if (Request.QueryString["e"] != null && Request.QueryString["e"] == "1" && Request.QueryString["no"] != null && Request.QueryString["no"] != "")
                {
                    // Edit
                    _data.NoRequest = NoRequest;

                    RecruitmentData _recData = new RecruitmentSystem().GetRecruitmentData(NoRequest);

                    /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
                    CheckPrivileges(_recData);
                    if (!IsCanEdit)
                    {
                        Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
                    }
                    /*EOF: 4 April 2015*/

                    _data.Creator.KdUser = _recData.Creator.KdUser;
                    _data.UserUp.KdUser = UserDataSession.KdUser;

                    if (_recData.CurrLevelApproval.Equals("5.1") || _recData.CurrLevelApproval.Equals("5.2") || _recData.CurrLevelApproval.Equals("5.3") || _recData.CurrLevelApproval.Equals("5.4"))
                    {
                        _data.CurrLevelApproval = rblDetailStatusRecruitment.SelectedValue;
                        /*
                         * Commented 4 April 2015
                        _data.ApprovalDataList = new List<RecruitmentApprovalData>(); // Reset
                         */
                        _recAppData.LvApproval = new LevelApprovalData();
                        _recAppData.LvApproval.KdLevelApproval = _data.CurrLevelApproval; //Tambahan yang masih diragukan, gunanya untuk menyimpan komentar saat lagi diedit
                    }
                    else
                    {
                        _data.CurrLevelApproval = _recData.CurrLevelApproval;
                        _recAppData.LvApproval = new LevelApprovalData();
                        _recAppData.LvApproval.KdLevelApproval = _data.CurrLevelApproval;
                    }

                    result = new RecruitmentSystem().UpdateRequest(_data);
                }
                else
                {
                    _data.Creator.KdUser = UserDataSession.KdUser;
                    _data.UserUp.KdUser = UserDataSession.KdUser;

                    _recAppData.LvApproval = new LevelApprovalData();
                    _data.CurrLevelApproval = "1";
                    _recAppData.LvApproval = new LevelApprovalData();
                    _recAppData.LvApproval.KdLevelApproval = "1";
                    result = new RecruitmentSystem().AddNewRequest(_data);
                }

                if (result != "")
                {
                    Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?success=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(result)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                }
                else
                {
                    alertNotification.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
                }
            }
            catch (Exception)
            {
                alertNotification.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
            }

        }

        protected void btnProcessRecruitment_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "";

                RecruitmentData _data = new RecruitmentData();
                _data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                _data.StrukturOrganisasi = new StrukturOrganisasiData();
                _data.StrukturOrganisasi.KdSO = ddlStrukturOrganisasi.SelectedValue;
                _data.Jabatan = new JabatanData();
                _data.Jabatan.KdJabatan = ddlJabatan.SelectedValue;
                _data.TglButuh = DateTime.ParseExact(txtTglButuh.Text, "dd/MM/yyyy", null);
                _data.KdAlasan = Convert.ToInt32(rblAlasan.SelectedValue);
                _data.JmlOrang = Convert.ToInt32(txtJmlKebutuhanKaryawan.Text);
                _data.JobDescList = ListJobDesc;
                _data.QualificationList = ListQualification;

                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();
                _recAppData.Komentar = txtComment.Text;
                _recAppData.IsFinalize = 1;
                _recAppData.User = new UserData();
                _recAppData.User.KdUser = UserDataSession.KdUser;
                _data.ApprovalDataList = new List<RecruitmentApprovalData>();
                _data.ApprovalDataList.Add(_recAppData);

                // Ambil template email untuk kiriim notifikasi
                StreamReader sr = new StreamReader(Server.MapPath("~/MailTemplate/NotificationMailTemplate.htm"));
                string template = sr.ReadToEnd();
                sr.Close();

                // Ambil template email untuk kiriim notifikasi
                StreamReader sr2 = new StreamReader(Server.MapPath("~/MailTemplate/StatusChangedNotificationMailTemplate.htm"));
                string statusChangedTemplate = sr2.ReadToEnd();
                sr2.Close();

                // Tentukan apakah edit atau tambah baru
                if (Request.QueryString["e"] != null && Request.QueryString["e"] == "1" &&
                    Request.QueryString["no"] != null && Request.QueryString["no"] != "")
                {
                    // Edit Existing Request
                    _data.NoRequest = NoRequest;

                    RecruitmentData _recData = new RecruitmentSystem().GetRecruitmentData(NoRequest);

                    /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
                    CheckPrivileges(_recData);
                    if (!IsCanEdit)
                    {
                        Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
                    }
                    /*EOF: 4 April 2015*/

                    _data.Creator.KdUser = _recData.Creator.KdUser;
                    _data.UserUp.KdUser = UserDataSession.KdUser;

                    _recAppData.LvApproval = new LevelApprovalData();
                    _recAppData.LvApproval.KdLevelApproval = _recData.CurrLevelApproval;

                    if (_recData.CurrLevelApproval.Equals("5.1") || _recData.CurrLevelApproval.Equals("5.2") || _recData.CurrLevelApproval.Equals("5.3") || _recData.CurrLevelApproval.Equals("5.4"))
                    {
                        _data.CurrLevelApproval = "6";
                        List<KandidatData> _listKandidat = new RecruitmentSystem().GetRecruitmentKandidatList(NoRequest);
                        rptKandidatFinal.DataSource = _listKandidat;
                        rptKandidatFinal.DataBind();
                        popUpKandidatFinal.Show();
                    }
                    else
                    {
                        LevelApprovalData _nextLevelApprovalData = new RecruitmentSystem().GetNextLevelApproval(_data.KdDivisi, _recData.CurrLevelApproval);
                        _data.CurrLevelApproval = _nextLevelApprovalData.KdLevelApproval;

                        int notifyResult = new NotificationSystem().NotifyNextPIC(_data, template, UserDataSession.KdUser);
                        int notifyStatusChangeR = new NotificationSystem().NotifyStatusChanged(_data, statusChangedTemplate, UserDataSession.KdUser);

                        result = new RecruitmentSystem().UpdateRequest(_data);

                        if (result != "")
                        {
                            Response.Redirect("~/Transaksi/Rekrutmen.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
                        }
                        else
                        {
                            alertNotification.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
                        }
                    }

                }
                else
                {
                    // New Request
                    _data.CurrLevelApproval = "2";
                    _recAppData.LvApproval = new LevelApprovalData();
                    _recAppData.LvApproval.KdLevelApproval = "1";

                    _data.Creator.KdUser = UserDataSession.KdUser;
                    _data.UserUp.KdUser = UserDataSession.KdUser;

                    result = new RecruitmentSystem().AddNewRequest(_data);

                    _data.NoRequest = result;

                    int notifyResult = new NotificationSystem().NotifyNextPIC(_data, template, UserDataSession.KdUser);
                    int notifyStatusChangeR = new NotificationSystem().NotifyStatusChanged(_data, statusChangedTemplate, UserDataSession.KdUser);

                    if (result != "")
                    {
                        Response.Redirect("~/Transaksi/Rekrutmen.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
                    }
                    else
                    {
                        alertNotification.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
                    }
                }
            }
            catch (Exception ex)
            {
                alertNotification.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
            }
        }

        protected void rptApprovalHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblUser = (Label)e.Item.FindControl("lblUser");
                Label lblLevelApproval = (Label)e.Item.FindControl("lblLevelApproval");
                Label lblComment = (Label)e.Item.FindControl("lblComment");
                Label lblTglProses = (Label)e.Item.FindControl("lblTglProses");

                RecruitmentApprovalData _data = (RecruitmentApprovalData)e.Item.DataItem;

                lblUser.Text = _data.User.FullName;
                lblLevelApproval.Text = _data.LvApproval.KdLevelApproval + ". " + _data.LvApproval.NmLevelApproval;
                lblComment.Text = _data.Komentar;
                lblTglProses.Text = _data.TglProses.ToString("dd MMM yyyy - HH:mm");
            }
        }

        protected void btnDeclineRecruitment_Click(object sender, EventArgs e)
        {
            try
            {
                /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
                CheckPrivileges();
                if (!IsCanEdit)
                {
                    Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
                }
                /*EOF: 4 April 2015*/

                string result = "";

                RecruitmentData _data = new RecruitmentData();
                _data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                _data.StrukturOrganisasi = new StrukturOrganisasiData();
                _data.StrukturOrganisasi.KdSO = ddlStrukturOrganisasi.SelectedValue;
                _data.Jabatan = new JabatanData();
                _data.Jabatan.KdJabatan = ddlJabatan.SelectedValue;
                _data.TglButuh = DateTime.ParseExact(txtTglButuh.Text, "dd/MM/yyyy", null);
                _data.KdAlasan = Convert.ToInt32(rblAlasan.SelectedValue);
                _data.JmlOrang = Convert.ToInt32(txtJmlKebutuhanKaryawan.Text);
                _data.JobDescList = ListJobDesc;
                _data.QualificationList = ListQualification;

                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();
                _recAppData.Komentar = txtComment.Text;
                _recAppData.IsFinalize = 1;
                _recAppData.User = new UserData();
                _recAppData.User.KdUser = UserDataSession.KdUser;
                _data.ApprovalDataList = new List<RecruitmentApprovalData>();
                _data.ApprovalDataList.Add(_recAppData);

                // Tentukan apakah edit atau tambah baru
                if (Request.QueryString["e"] != null && Request.QueryString["e"] == "1" &&
                    Request.QueryString["no"] != null && Request.QueryString["no"] != "")
                {
                    // Edit Existing Request
                    _data.NoRequest = NoRequest;

                    RecruitmentData _recData = new RecruitmentSystem().GetRecruitmentData(NoRequest);

                    _data.Creator.KdUser = _recData.Creator.KdUser;
                    _data.UserUp.KdUser = UserDataSession.KdUser;

                    _recAppData.LvApproval = new LevelApprovalData();
                    _recAppData.LvApproval.KdLevelApproval = _recData.CurrLevelApproval;

                    // Lansgung Close karena di decline
                    _data.CurrLevelApproval = "6";

                    result = new RecruitmentSystem().UpdateRequest(_data);

                    // Ambil template email untuk kiriim notifikasi
                    StreamReader sr = new StreamReader(Server.MapPath("~/MailTemplate/DeclineNotificationMailTemplate.htm"));
                    string template = sr.ReadToEnd();
                    sr.Close();

                    int notifyResult = new NotificationSystem().NotifyRequestDeclined(_data, _recAppData, template, UserDataSession.KdUser);

                    if (result != "")
                    {
                        Response.Redirect("~/Transaksi/Rekrutmen.aspx?mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
                    }
                    else
                    {
                        alertNotification.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
                    }
                }
            }
            catch (Exception ex)
            {
                alertNotification.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
            }
        }

        protected void btnShowAddKandidat_Click(object sender, EventArgs e)
        {
            ListCVFile = new List<FileUpload>();
            txtNamaKandidat.Text = "";
            txtNoIdentitasKandidat.Text = "";
            rblGenderKandidat.ClearSelection();
            txtNoHandphoneKandidat.Text = "";
            txtEmailKandidat.Text = "";

            rptCVFiles.DataSource = ListCVFile;
            rptCVFiles.DataBind();

            int kdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            string kdSO = ddlStrukturOrganisasi.SelectedValue;
            string kdJabatan = ddlJabatan.SelectedValue;
            List<KandidatData> listMasterKandidat = new KandidatSystem().GetKandidatNotInRequest(kdDivisi, kdSO, kdJabatan, NoRequest);
            if (listMasterKandidat.Count > 0)
            {
                rowNoMasterKandidat.Visible = false;
                rptMasterKandidat.DataSource = listMasterKandidat;
                rptMasterKandidat.DataBind();
            }
            else
            {
                rowNoMasterKandidat.Visible = true;
            }

            popUpAddKandidat.Show();

            ratingAddKandidat.Attributes.Add("onclick", "storeRating('ratingAddKandidat', '" + hidRatingAddKandidat.ClientID + "')");
        }

        protected void btnAddKandidatFile_Click(object sender, EventArgs e)
        {
            ListCVFile.Add(new FileUpload());
            rptCVFiles.DataSource = ListCVFile;
            rptCVFiles.DataBind();
        }

        protected void btnAddKandidatFileEdit_Click(object sender, EventArgs e)
        {
            ListCVFile.Add(new FileUpload());
            rptCVFilesEdit.DataSource = ListCVFile;
            rptCVFilesEdit.DataBind();
        }

        protected void btnAddKandidat_Click(object sender, EventArgs e)
        {
            try
            {
                /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
                CheckPrivileges();
                if (!IsCanEdit)
                {
                    Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
                }
                /*EOF: 4 April 2015*/

                if (Request.QueryString["no"] != null && Request.QueryString["no"] != "")
                {
                    KandidatData _dataKandidat = new KandidatData();
                    _dataKandidat.NoRequest = NoRequest;
                    _dataKandidat.NmKandidat = txtNamaKandidat.Text;
                    _dataKandidat.NoIdentitas = txtNoIdentitasKandidat.Text;
                    _dataKandidat.Gender = Convert.ToChar(rblGenderKandidat.SelectedValue);
                    _dataKandidat.NoHandphone = txtNoHandphoneKandidat.Text;
                    _dataKandidat.Email = txtEmailKandidat.Text;
                    _dataKandidat.UserIn = new UserData();
                    _dataKandidat.UserIn.KdUser = UserDataSession.KdUser;
                    _dataKandidat.CVFiles = new List<FileData>();

                    foreach (RepeaterItem item in rptCVFiles.Items)
                    {
                        FileUpload fuCVFile = (FileUpload)item.FindControl("fuCVFile");
                        if (fuCVFile.HasFile)
                        {
                            FileData fileData = new FileData();
                            string ext = Path.GetExtension(fuCVFile.FileName);
                            string formatedFileName = Path.GetFileNameWithoutExtension(fuCVFile.FileName).Replace(' ', '_') + DateTime.Now.ToString("ddMMyyyyhhmmss") + ext;
                            fuCVFile.SaveAs(SystemConfiguration.UploadDirectory + formatedFileName);
                            fileData.NmFile = formatedFileName;
                            fileData.NmFileAsli = fuCVFile.FileName;
                            fileData.FileSize = fuCVFile.PostedFile.ContentLength;
                            _dataKandidat.CVFiles.Add(fileData);
                        }
                    }

                    _dataKandidat.Divisi = new DivisiData();
                    _dataKandidat.Divisi.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                    _dataKandidat.StrukturOrganisasi = new StrukturOrganisasiData();
                    _dataKandidat.StrukturOrganisasi.KdSO = ddlStrukturOrganisasi.SelectedValue;
                    _dataKandidat.Jabatan = new JabatanData();
                    _dataKandidat.Jabatan.KdJabatan = ddlJabatan.SelectedValue;
                    _dataKandidat.Rate = Convert.ToInt32(hidRatingAddKandidat.Text);

                    int result = new RecruitmentSystem().AddRecruitmentKandidat(_dataKandidat);

                    if (result > 0)
                    {
                        Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?success=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                    }
                    else
                    {
                        Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
            }
        }

        protected void rptCVFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                FileUpload fuCVFile = (FileUpload)e.Item.FindControl("fuCVFile");

                FileUpload item = (FileUpload)e.Item.DataItem;
                fuCVFile = item;

                btnDelete.CommandName = "DELETE";
                btnDelete.CommandArgument = e.Item.ItemIndex.ToString();
            }
        }

        protected void rptCVFiles_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DELETE")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ListCVFile.RemoveAt(index);
                rptCVFiles.DataSource = ListCVFile;
                rptCVFiles.DataBind();
            }
        }

        protected void rptCVFilesEdit_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DELETE")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ListCVFile.RemoveAt(index);
                rptCVFilesEdit.DataSource = ListCVFile;
                rptCVFilesEdit.DataBind();
            }
        }

        protected void rptKandidat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblNo = (Label)e.Item.FindControl("lblNo");
                Label lblNama = (Label)e.Item.FindControl("lblNama");
                Label lblGender = (Label)e.Item.FindControl("lblGender");
                Label lblNoIdentitas = (Label)e.Item.FindControl("lblNoIdentitas");
                Label lblNoHandphone = (Label)e.Item.FindControl("lblNoHandphone");
                Label lblEmail = (Label)e.Item.FindControl("lblEmail");
                Repeater rptKandidatCVFiles = (Repeater)e.Item.FindControl("rptKandidatCVFiles");
                LinkButton lbMatching = (LinkButton)e.Item.FindControl("lbMatching");
                LinkButton lbInterview = (LinkButton)e.Item.FindControl("lbInterview");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                TextBox hidRatingKandidat = (TextBox)e.Item.FindControl("hidRatingKandidat");
                Rating ratingKandidat = (Rating)e.Item.FindControl("ratingKandidat");
                HtmlTableRow rowTblKandidat = (HtmlTableRow)e.Item.FindControl("rowTblKandidat");

                KandidatData kandidatData = (KandidatData)e.Item.DataItem;

                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                lblNama.Text = kandidatData.NmKandidat;
                lblGender.Text = kandidatData.Gender == 'L' ? "Male" : "Female";
                lblNoIdentitas.Text = kandidatData.NoIdentitas;
                lblNoHandphone.Text = kandidatData.NoHandphone;
                lblEmail.Text = kandidatData.Email;
                rptKandidatCVFiles.DataSource = kandidatData.CVFiles;
                rptKandidatCVFiles.DataBind();
                lbMatching.CommandName = "MATCHING";
                lbMatching.CommandArgument = kandidatData.KdKandidat.ToString();
                lbInterview.CommandName = "INTERVIEW";
                lbInterview.CommandArgument = kandidatData.KdKandidat.ToString();
                btnEdit.CommandName = "EDIT";
                btnEdit.CommandArgument = kandidatData.KdKandidat.ToString();
                btnDelete.CommandName = "DELETE";
                btnDelete.CommandArgument = kandidatData.KdKandidat.ToString();

                hidRatingKandidat.Text = kandidatData.Rate.ToString();
                ratingKandidat.CurrentRating = kandidatData.Rate;
                ratingKandidat.BehaviorID = "ratingRecruitmentKandidat" + e.Item.ItemIndex.ToString();
                ratingKandidat.CssClass += "ratingControl";
                ratingKandidat.Attributes.Add("data-behaviorid", "ratingRecruitmentKandidat" + e.Item.ItemIndex.ToString());

                if (kandidatData.IsPassed == 1)
                {
                    lblNama.Text += " <small class=\"badge pull-right bg-green\"><i class=\"fa fa-check-circle\"></i> &nbsp;selected</small>";
                }

                if (!IsCanEdit)
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        protected void rptKandidat_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
            CheckPrivileges();
            if (!IsCanEdit)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
            }
            /*EOF: 4 April 2015*/

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "MATCHING" || e.CommandName == "INTERVIEW" || e.CommandName == "EDIT" || e.CommandName == "DELETE")
                {
                    int kdKandidat = Convert.ToInt32(e.CommandArgument);

                    if (e.CommandName == "MATCHING")
                    {
                        KandidatData _kandidatData = new KandidatSystem().GetKandidatData(kdKandidat);
                        hidKdKandidatMatching.Value = e.CommandArgument.ToString();
                        lblNmKandidatMatching.Text = _kandidatData.NmKandidat;
                        lblGenderMatching.Text = _kandidatData.Gender == 'L' ? "Male" : "Female";
                        lblNoIdentitasMatching.Text = _kandidatData.NoIdentitas;

                        if (_kandidatData.NoHandphone != "" || _kandidatData.NoHandphone != null)
                        {
                            lblNoHandphoneMatching.Text = _kandidatData.NoHandphone;
                        }
                        else
                        {
                            lblNoHandphoneMatching.Text = "N/A";
                        }

                        if (_kandidatData.Email != "" || _kandidatData.Email != null)
                        {
                            lblEmailMatching.Text = _kandidatData.Email;
                        }
                        else
                        {
                            lblEmailMatching.Text = "N/A";
                        }

                        rptKandidatMatchingCVFiles.DataSource = _kandidatData.CVFiles;
                        rptKandidatMatchingCVFiles.DataBind();

                        List<QualificationMatchingData> _qualificationMatchingList = new KandidatSystem().GetQualificationMatchingList(_kandidatData.KdKandidat, NoRequest);
                        rptQualificationMatching.DataSource = _qualificationMatchingList;
                        rptQualificationMatching.DataBind();

                        ListAdditionalQualification = new KandidatSystem().GetAdditionalQualificationList(_kandidatData.KdKandidat);
                        rptAdditionalQualification.DataSource = ListAdditionalQualification;
                        rptAdditionalQualification.DataBind();

                        popUpMatchingKandidat.Show();
                    }
                    else if (e.CommandName == "INTERVIEW")
                    {
                        KandidatData _kandidatData = new KandidatSystem().GetKandidatData(kdKandidat);
                        hidKdKandidatInterview.Value = e.CommandArgument.ToString();
                        lblNmKandidatInterview.Text = _kandidatData.NmKandidat;
                        lblGenderKandidatInterview.Text = _kandidatData.Gender == 'L' ? "Male" : "Female";
                        lblNoIdentitasInterview.Text = _kandidatData.NoIdentitas;

                        if (_kandidatData.NoHandphone != "" || _kandidatData.NoHandphone != null)
                        {
                            lblNoHandphoneInterview.Text = _kandidatData.NoHandphone;
                        }
                        else
                        {
                            lblNoHandphoneInterview.Text = "N/A";
                        }

                        if (_kandidatData.Email != "" || _kandidatData.Email != null)
                        {
                            lblEmailInterview.Text = _kandidatData.Email;
                        }
                        else
                        {
                            lblEmailInterview.Text = "N/A";
                        }

                        rptKandidatInterviewCVFiles.DataSource = _kandidatData.CVFiles;
                        rptKandidatInterviewCVFiles.DataBind();

                        List<InterviewData> _kandidatInterviewResultList = new KandidatSystem().GetInterviewResultList(_kandidatData.KdKandidat);
                        rptKandidatInterview.DataSource = _kandidatInterviewResultList;
                        rptKandidatInterview.DataBind();

                        txtTglInterview.Text = "";
                        txtInterviewer.Text = "";
                        txtHasilInterview.Text = "";

                        popUpInterviewKandidat.Show();
                    }
                    else if (e.CommandName == "EDIT")
                    {
                        KandidatData _kandidatData = new KandidatSystem().GetKandidatData(kdKandidat, NoRequest);
                        hidKdKandidatEdit.Text = e.CommandArgument.ToString();
                        txtNmKandidatEdit.Text = _kandidatData.NmKandidat;
                        rblGenderKandidatEdit.SelectedValue = _kandidatData.Gender.ToString();
                        txtNoIdentitasKandidatEdit.Text = _kandidatData.NoIdentitas;
                        txtNoHandphoneKandidatEdit.Text = _kandidatData.NoHandphone;
                        txtEmailKandidatEdit.Text = _kandidatData.Email;
                        hidRatingEditKandidat.Text = _kandidatData.Rate.ToString();
                        ratingEditKandidat.CurrentRating = _kandidatData.Rate;
                        ListCVFile = new List<FileUpload>();
                        ListExistingCVFile = new List<FileData>();
                        ListExistingCVFile = _kandidatData.CVFiles;
                        rptExistingCVFilesEdit.DataSource = ListExistingCVFile;
                        rptExistingCVFilesEdit.DataBind();

                        popUpEditKandidat.Show();

                        ratingEditKandidat.Attributes.Add("onclick", "storeRating('ratingEditKandidat', '" + hidRatingEditKandidat.ClientID + "')");
                    }
                    else if (e.CommandName == "DELETE")
                    {
                        KandidatData _kandidatData = new KandidatSystem().GetKandidatData(kdKandidat);
                        hidKdKandidatConfirm.Value = kdKandidat.ToString();
                        litConfirm.Text = "Are you sure want to delete this candidate: <b>" + _kandidatData.NmKandidat + "</b>?";
                        popUpConfirm.Show();
                    }
                }
            }
        }

        protected void rptExistingCVFilesEdit_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox hidKdFile = (TextBox)e.Item.FindControl("hidKdFile");
                HyperLink linkDownloadCV = (HyperLink)e.Item.FindControl("linkDownloadCV");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");

                FileData _fileData = (FileData)e.Item.DataItem;

                hidKdFile.Text = _fileData.KdFile.ToString();
                linkDownloadCV.Text = _fileData.NmFileAsli + " (" + FileUnitHelper.SizeSuffix(_fileData.FileSize) + ")";
                string[] _parts = _fileData.NmFileAsli.Split('.');
                string _ext = _parts[_parts.Length - 1];
                linkDownloadCV.NavigateUrl = "~/DownloadFile.ashx?name=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_fileData.NmFile)) + "#." + _ext;
                btnDelete.CommandName = "DELETE";
                btnDelete.CommandArgument = e.Item.ItemIndex.ToString();
            }
        }

        protected void rptExistingCVFilesEdit_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "DELETE")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    ListExistingCVFile.RemoveAt(index);
                    rptExistingCVFilesEdit.DataSource = ListExistingCVFile;
                    rptExistingCVFilesEdit.DataBind();
                }
            }
        }

        protected void btnEditKandidat_Click(object sender, EventArgs e)
        {
            /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
            CheckPrivileges();
            if (!IsCanEdit)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
            }
            /*EOF: 4 April 2015*/

            try
            {
                if (hidKdKandidatEdit.Text != "" && hidKdKandidatEdit.Text != null)
                {
                    KandidatData _dataKandidat = new KandidatData();
                    _dataKandidat.KdKandidat = Convert.ToInt32(hidKdKandidatEdit.Text);
                    _dataKandidat.NoRequest = NoRequest;
                    _dataKandidat.NmKandidat = txtNmKandidatEdit.Text;
                    _dataKandidat.NoIdentitas = txtNoIdentitasKandidatEdit.Text;
                    _dataKandidat.Gender = Convert.ToChar(rblGenderKandidatEdit.SelectedValue);
                    _dataKandidat.NoHandphone = txtNoHandphoneKandidatEdit.Text;
                    _dataKandidat.Email = txtEmailKandidatEdit.Text;
                    _dataKandidat.UserIn = new UserData();
                    _dataKandidat.UserIn.KdUser = UserDataSession.KdUser;
                    _dataKandidat.CVFiles = new List<FileData>();

                    foreach (RepeaterItem item in rptExistingCVFilesEdit.Items)
                    {
                        // Untuk existing file, cukup ambil KdFilenya saja
                        TextBox hidKdFile = (TextBox)item.FindControl("hidKdFile");
                        FileData _fileData = new FileData();
                        _fileData.KdFile = Convert.ToInt32(hidKdFile.Text);
                        _dataKandidat.CVFiles.Add(_fileData);
                    }

                    foreach (RepeaterItem item in rptCVFilesEdit.Items)
                    {
                        FileUpload fuCVFile = (FileUpload)item.FindControl("fuCVFile");
                        if (fuCVFile.HasFile)
                        {
                            FileData _fileData = new FileData();
                            string ext = Path.GetExtension(fuCVFile.FileName);
                            string formatedFileName = Path.GetFileNameWithoutExtension(fuCVFile.FileName).Replace(' ', '_') + DateTime.Now.ToString("ddMMyyyyhhmmss") + ext;
                            fuCVFile.SaveAs(SystemConfiguration.UploadDirectory + formatedFileName);
                            _fileData.NmFile = formatedFileName;
                            _fileData.NmFileAsli = fuCVFile.FileName;
                            _fileData.FileSize = fuCVFile.PostedFile.ContentLength;
                            _dataKandidat.CVFiles.Add(_fileData);
                        }
                    }

                    _dataKandidat.Divisi = new DivisiData();
                    _dataKandidat.Divisi.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                    _dataKandidat.StrukturOrganisasi = new StrukturOrganisasiData();
                    _dataKandidat.StrukturOrganisasi.KdSO = ddlStrukturOrganisasi.SelectedValue;
                    _dataKandidat.Jabatan = new JabatanData();
                    _dataKandidat.Jabatan.KdJabatan = ddlJabatan.SelectedValue;

                    _dataKandidat.Rate = Convert.ToInt32(hidRatingEditKandidat.Text);

                    int result = new KandidatSystem().EditKandidat(_dataKandidat);

                    if (result > 0)
                    {
                        Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?success=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                    }
                    else
                    {
                        Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
                throw;
            }
        }

        protected void rptKandidatCVFiles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HyperLink linkDownloadCV = (HyperLink)e.Item.FindControl("linkDownloadCV");
                FileData _fileData = (FileData)e.Item.DataItem;
                linkDownloadCV.Text = _fileData.NmFileAsli;
                string[] _parts = _fileData.NmFileAsli.Split('.');
                string _ext = _parts[_parts.Length - 1];
                linkDownloadCV.NavigateUrl = "~/DownloadFile.ashx?name=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_fileData.NmFile)) + "#." + _ext;
            }
        }

        protected void rptQualificationMatching_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox hidKdQualification = (TextBox)e.Item.FindControl("hidKdQualification");
                Label lblQualification = (Label)e.Item.FindControl("lblQualification");
                CheckBox chkMatch = (CheckBox)e.Item.FindControl("chkMatch");
                TextBox txtComment = (TextBox)e.Item.FindControl("txtComment");
                Label lblNo = (Label)e.Item.FindControl("lblNo");

                QualificationMatchingData qualificationMatchingData = (QualificationMatchingData)e.Item.DataItem;
                hidKdQualification.Text = qualificationMatchingData.Qualification.KdQualification.ToString();
                lblQualification.Text = qualificationMatchingData.Qualification.Qualification;
                chkMatch.Checked = qualificationMatchingData.IsMatch;
                txtComment.Text = qualificationMatchingData.Komentar;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();

                if (!IsCanEdit)
                {
                    chkMatch.Enabled = false;
                    txtComment.Enabled = false;
                }
            }
        }

        protected void rptAdditionalQualification_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox hidKdQualification = (TextBox)e.Item.FindControl("hidKdQualification");
                Label lblNo = (Label)e.Item.FindControl("lblNo");
                Label lblQualification = (Label)e.Item.FindControl("lblQualification");
                Label lblComment = (Label)e.Item.FindControl("lblComment");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
                RequiredFieldValidator reqAdditionalQualificationRepeater = (RequiredFieldValidator)e.Item.FindControl("reqAdditionalQualificationRepeater");

                QualificationMatchingData qualificationMatchingData = (QualificationMatchingData)e.Item.DataItem;
                hidKdQualification.Text = qualificationMatchingData.Qualification.KdQualification.ToString();
                lblQualification.Text = qualificationMatchingData.Qualification.Qualification;
                lblComment.Text = qualificationMatchingData.Komentar;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                btnEdit.CommandName = "EDIT";
                btnDelete.CommandName = "DELETE";
                btnSave.CommandName = "SAVE";
                btnCancel.CommandName = "CANCEL";
                btnSave.CommandArgument = hidKdQualification.Text;
                btnDelete.CommandArgument = hidKdQualification.Text;

                reqAdditionalQualificationRepeater.ValidationGroup = hidKdQualification.Text + "-ADDITIONAL_QUALIFICATION";
                btnSave.ValidationGroup = hidKdQualification.Text + "-ADDITIONAL_QUALIFICATION";

                if (!IsCanEdit)
                {
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                }
            }
        }

        protected void rptAdditionalQualification_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
                CheckPrivileges();
                if (!IsCanEdit)
                {
                    Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
                }
                /*EOF: 4 April 2015*/

                if (e.CommandName == "EDIT")
                {
                    Label lblQualification = (Label)e.Item.FindControl("lblQualification");
                    Label lblComment = (Label)e.Item.FindControl("lblComment");
                    TextBox txtAdditionalQualificationRepeater = (TextBox)e.Item.FindControl("txtAdditionalQualificationRepeater");
                    TextBox txtAdditionalQualificationCommentRepeater = (TextBox)e.Item.FindControl("txtAdditionalQualificationCommentRepeater");

                    LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                    LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                    LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                    LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");

                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;

                    lblQualification.Visible = false;
                    lblComment.Visible = false;

                    txtAdditionalQualificationRepeater.Visible = true;
                    txtAdditionalQualificationCommentRepeater.Visible = true;
                    txtAdditionalQualificationRepeater.Text = lblQualification.Text;
                    txtAdditionalQualificationCommentRepeater.Text = lblComment.Text;
                }
                else if (e.CommandName == "DELETE")
                {
                    ListAdditionalQualification.RemoveAt(e.Item.ItemIndex);
                    rptAdditionalQualification.DataSource = ListAdditionalQualification;
                    rptAdditionalQualification.DataBind();
                }
                else if (e.CommandName == "SAVE")
                {
                    TextBox hidKdQualification = (TextBox)e.Item.FindControl("hidKdQualification");
                    Label lblQualification = (Label)e.Item.FindControl("lblQualification");
                    Label lblComment = (Label)e.Item.FindControl("lblComment");
                    TextBox txtAdditionalQualificationRepeater = (TextBox)e.Item.FindControl("txtAdditionalQualificationRepeater");
                    TextBox txtAdditionalQualificationCommentRepeater = (TextBox)e.Item.FindControl("txtAdditionalQualificationCommentRepeater");

                    LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                    LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                    LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                    LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;

                    ListAdditionalQualification[e.Item.ItemIndex].Qualification.KdQualification = Convert.ToInt32(hidKdQualification.Text);
                    ListAdditionalQualification[e.Item.ItemIndex].Qualification.Qualification = txtAdditionalQualificationRepeater.Text;
                    ListAdditionalQualification[e.Item.ItemIndex].Komentar = txtAdditionalQualificationCommentRepeater.Text;

                    lblQualification.Text = txtAdditionalQualificationRepeater.Text;
                    lblComment.Text = txtAdditionalQualificationCommentRepeater.Text;

                    lblQualification.Visible = true;
                    lblComment.Visible = true;

                    txtAdditionalQualificationRepeater.Visible = false;
                    txtAdditionalQualificationCommentRepeater.Visible = false;
                }
                else if (e.CommandName == "CANCEL")
                {
                    Label lblQualification = (Label)e.Item.FindControl("lblQualification");
                    Label lblComment = (Label)e.Item.FindControl("lblComment");
                    TextBox txtAdditionalQualificationRepeater = (TextBox)e.Item.FindControl("txtAdditionalQualificationRepeater");
                    TextBox txtAdditionalQualificationCommentRepeater = (TextBox)e.Item.FindControl("txtAdditionalQualificationCommentRepeater");

                    LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                    LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                    LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                    LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;

                    lblQualification.Visible = true;
                    lblComment.Visible = true;

                    txtAdditionalQualificationRepeater.Visible = false;
                    txtAdditionalQualificationCommentRepeater.Visible = false;
                }
            }
        }

        protected void btnTambahAdditionalQualification_Click(object sender, EventArgs e)
        {
            QualificationMatchingData _qualificationData = new QualificationMatchingData();
            _qualificationData.Qualification = new QualificationData();
            _qualificationData.Qualification.Qualification = txtAdditionalQualification.Text;
            _qualificationData.Komentar = txtKomentarAdditionalQualification.Text;
            _qualificationData.KdKandidat = Convert.ToInt32(hidKdKandidatMatching.Value);
            ListAdditionalQualification.Add(_qualificationData);

            txtAdditionalQualification.Text = "";
            txtKomentarAdditionalQualification.Text = "";

            rptAdditionalQualification.DataSource = ListAdditionalQualification;
            rptAdditionalQualification.DataBind();
        }

        protected void btnSaveQualificationMatching_Click(object sender, EventArgs e)
        {
            /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
            CheckPrivileges();
            if (!IsCanEdit)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
            }
            /*EOF: 4 April 2015*/

            List<QualificationMatchingData> _listQualificationMatching = new List<QualificationMatchingData>();

            foreach (RepeaterItem item in rptQualificationMatching.Items)
            {
                TextBox hidKdQualification = (TextBox)item.FindControl("hidKdQualification");
                Label lblQualification = (Label)item.FindControl("lblQualification");
                CheckBox chkMatch = (CheckBox)item.FindControl("chkMatch");
                TextBox txtComment = (TextBox)item.FindControl("txtComment");

                QualificationMatchingData qualificationMatchingItem = new QualificationMatchingData();
                qualificationMatchingItem.Qualification = new QualificationData();
                qualificationMatchingItem.Qualification.KdQualification = Convert.ToInt32(hidKdQualification.Text);
                qualificationMatchingItem.IsMatch = chkMatch.Checked;
                qualificationMatchingItem.Komentar = txtComment.Text;
                qualificationMatchingItem.KdKandidat = Convert.ToInt32(hidKdKandidatMatching.Value);

                _listQualificationMatching.Add(qualificationMatchingItem);
            }

            int result = new KandidatSystem().AddQualificationMatchingList(_listQualificationMatching);

            int additionalResult = new KandidatSystem().AddAdditionalQualificationList(ListAdditionalQualification);

            popUpMatchingKandidat.Hide();
        }

        protected void rptKandidatInterview_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox hidKdInterview = (TextBox)e.Item.FindControl("hidKdInterview");
                Label lblTglInterview = (Label)e.Item.FindControl("lblTglInterview");
                Label lblInterviewer = (Label)e.Item.FindControl("lblInterviewer");
                Label lblHasilInterview = (Label)e.Item.FindControl("lblHasilInterview");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
                CustomValidator cvTglInterviewRepeater = (CustomValidator)e.Item.FindControl("cvTglInterviewRepeater");
                RequiredFieldValidator reqTglInterviewRepeater = (RequiredFieldValidator)e.Item.FindControl("reqTglInterviewRepeater");
                RequiredFieldValidator reqInterviewRepeater = (RequiredFieldValidator)e.Item.FindControl("reqInterviewRepeater");
                RequiredFieldValidator reqHasilInterviewRepeater = (RequiredFieldValidator)e.Item.FindControl("reqHasilInterviewRepeater");

                if (!IsCanEdit)
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }

                InterviewData interviewData = (InterviewData)e.Item.DataItem;
                hidKdInterview.Text = interviewData.KdInterview.ToString();
                lblTglInterview.Text = interviewData.TglInterview.ToString("dd/MM/yyyy");
                lblInterviewer.Text = interviewData.Interviewer;
                lblHasilInterview.Text = interviewData.Hasil;
                btnEdit.CommandName = "EDIT";
                btnDelete.CommandName = "DELETE";
                btnSave.CommandName = "SAVE";
                btnCancel.CommandName = "CANCEL";
                btnSave.CommandArgument = interviewData.KdInterview.ToString();
                btnDelete.CommandArgument = interviewData.KdInterview.ToString();

                cvTglInterviewRepeater.ValidationGroup = interviewData.KdInterview.ToString() + "-INTERVIEW";
                reqTglInterviewRepeater.ValidationGroup = interviewData.KdInterview.ToString() + "-INTERVIEW";
                reqHasilInterviewRepeater.ValidationGroup = interviewData.KdInterview.ToString() + "-INTERVIEW";
                reqInterviewRepeater.ValidationGroup = interviewData.KdInterview.ToString() + "-INTERVIEW";
                btnSave.ValidationGroup = interviewData.KdInterview.ToString() + "-INTERVIEW";
            }
        }

        protected void rptKandidatInterview_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
                CheckPrivileges();
                if (!IsCanEdit)
                {
                    Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
                }
                /*EOF: 4 April 2015*/

                if (e.CommandName == "EDIT")
                {
                    Label lblTglInterview = (Label)e.Item.FindControl("lblTglInterview");
                    Label lblInterviewer = (Label)e.Item.FindControl("lblInterviewer");
                    Label lblHasilInterview = (Label)e.Item.FindControl("lblHasilInterview");
                    TextBox txtTglInterviewRepeater = (TextBox)e.Item.FindControl("txtTglInterviewRepeater");
                    TextBox txtInterviewerRepeater = (TextBox)e.Item.FindControl("txtInterviewerRepeater");
                    TextBox txtHasilInterviewRepeater = (TextBox)e.Item.FindControl("txtHasilInterviewRepeater");
                    LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                    LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                    LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                    LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");

                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;

                    lblTglInterview.Visible = false;
                    lblInterviewer.Visible = false;
                    lblHasilInterview.Visible = false;

                    txtTglInterviewRepeater.Text = lblTglInterview.Text;
                    txtInterviewerRepeater.Text = lblInterviewer.Text;
                    txtHasilInterviewRepeater.Text = lblHasilInterview.Text;

                    txtTglInterviewRepeater.Visible = true;
                    txtInterviewerRepeater.Visible = true;
                    txtHasilInterviewRepeater.Visible = true;
                }
                else if (e.CommandName == "DELETE")
                {
                    int kdInterview = Convert.ToInt32(e.CommandArgument);
                    int result = new KandidatSystem().DeleteInterviewData(kdInterview);

                    List<InterviewData> _kandidatInterviewResultList = new KandidatSystem().GetInterviewResultList(Convert.ToInt32(hidKdKandidatInterview.Value));
                    rptKandidatInterview.DataSource = _kandidatInterviewResultList;
                    rptKandidatInterview.DataBind();
                }
                else if (e.CommandName == "SAVE")
                {
                    Label lblTglInterview = (Label)e.Item.FindControl("lblTglInterview");
                    Label lblInterviewer = (Label)e.Item.FindControl("lblInterviewer");
                    Label lblHasilInterview = (Label)e.Item.FindControl("lblHasilInterview");

                    TextBox txtTglInterviewRepeater = (TextBox)e.Item.FindControl("txtTglInterviewRepeater");
                    TextBox txtInterviewerRepeater = (TextBox)e.Item.FindControl("txtInterviewerRepeater");
                    TextBox txtHasilInterviewRepeater = (TextBox)e.Item.FindControl("txtHasilInterviewRepeater");

                    LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                    LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                    LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                    LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;

                    lblTglInterview.Text = txtTglInterviewRepeater.Text;
                    lblInterviewer.Text = txtInterviewerRepeater.Text;
                    lblHasilInterview.Text = txtHasilInterviewRepeater.Text;

                    InterviewData interviewData = new InterviewData();
                    interviewData.KdInterview = Convert.ToInt32(e.CommandArgument);
                    interviewData.TglInterview = DateTime.ParseExact(txtTglInterviewRepeater.Text, "dd/MM/yyyy", null);
                    interviewData.Interviewer = txtInterviewerRepeater.Text;
                    interviewData.Hasil = txtHasilInterviewRepeater.Text;
                    interviewData.KdKandidat = Convert.ToInt32(hidKdKandidatInterview.Value);

                    int result = new KandidatSystem().UpdateInterviewData(interviewData);
                    if (result > 0)
                    {
                        lblTglInterview.Visible = true;
                        lblInterviewer.Visible = true;
                        lblHasilInterview.Visible = true;

                        txtTglInterviewRepeater.Visible = false;
                        txtInterviewerRepeater.Visible = false;
                        txtHasilInterviewRepeater.Visible = false;
                    }

                }
                else if (e.CommandName == "CANCEL")
                {
                    Label lblTglInterview = (Label)e.Item.FindControl("lblTglInterview");
                    Label lblInterviewer = (Label)e.Item.FindControl("lblInterviewer");
                    Label lblHasilInterview = (Label)e.Item.FindControl("lblHasilInterview");
                    TextBox txtTglInterviewRepeater = (TextBox)e.Item.FindControl("txtTglInterviewRepeater");
                    TextBox txtInterviewerRepeater = (TextBox)e.Item.FindControl("txtInterviewerRepeater");
                    TextBox txtHasilInterviewRepeater = (TextBox)e.Item.FindControl("txtHasilInterviewRepeater");

                    LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                    LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                    LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                    LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");

                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;

                    lblTglInterview.Visible = true;
                    lblInterviewer.Visible = true;
                    lblHasilInterview.Visible = true;

                    txtTglInterviewRepeater.Visible = false;
                    txtInterviewerRepeater.Visible = false;
                    txtHasilInterviewRepeater.Visible = false;
                }
            }
        }

        protected void lbAddInterview_Click(object sender, EventArgs e)
        {
            /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
            CheckPrivileges();
            if (!IsCanEdit)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
            }
            /*EOF: 4 April 2015*/

            InterviewData _interviewData = new InterviewData();
            _interviewData.KdKandidat = Convert.ToInt32(hidKdKandidatInterview.Value);
            _interviewData.TglInterview = DateTime.ParseExact(txtTglInterview.Text, "dd/MM/yyyy", null);
            _interviewData.Interviewer = txtInterviewer.Text;
            _interviewData.Hasil = txtHasilInterview.Text;

            txtTglInterview.Text = "";
            txtInterviewer.Text = "";
            txtHasilInterview.Text = "";

            int result = new KandidatSystem().AddInterviewData(_interviewData);
            if (result > 0)
            {
                List<InterviewData> _kandidatInterviewResultList = new KandidatSystem().GetInterviewResultList(Convert.ToInt32(hidKdKandidatInterview.Value));
                rptKandidatInterview.DataSource = _kandidatInterviewResultList;
                rptKandidatInterview.DataBind();
            }
        }

        protected void rptKandidatFinal_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox hidKdKandidat = (TextBox)e.Item.FindControl("hidKdKandidat");
                Label lblNo = (Label)e.Item.FindControl("lblNo");
                Label lblNama = (Label)e.Item.FindControl("lblNama");
                Label lblGender = (Label)e.Item.FindControl("lblGender");
                Label lblNoIdentitas = (Label)e.Item.FindControl("lblNoIdentitas");
                Label lblNoHandphone = (Label)e.Item.FindControl("lblNoHandphone");
                Label lblEmail = (Label)e.Item.FindControl("lblEmail");
                Repeater rptKandidatFinalCVFiles = (Repeater)e.Item.FindControl("rptKandidatFinalCVFiles");

                KandidatData kandidatData = (KandidatData)e.Item.DataItem;

                hidKdKandidat.Text = kandidatData.KdKandidat.ToString();
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                lblNama.Text = kandidatData.NmKandidat;
                lblGender.Text = kandidatData.Gender == 'L' ? "Male" : "Female";
                lblNoIdentitas.Text = kandidatData.NoIdentitas;
                lblNoHandphone.Text = kandidatData.NoHandphone;
                lblEmail.Text = kandidatData.Email;
                rptKandidatFinalCVFiles.DataSource = kandidatData.CVFiles;
                rptKandidatFinalCVFiles.DataBind();
            }
        }

        protected void btnFinalisasi_Click(object sender, EventArgs e)
        {
            try
            {
                RecruitmentData _data = new RecruitmentData();
                _data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                _data.StrukturOrganisasi = new StrukturOrganisasiData();
                _data.StrukturOrganisasi.KdSO = ddlStrukturOrganisasi.SelectedValue;
                _data.Jabatan = new JabatanData();
                _data.Jabatan.KdJabatan = ddlJabatan.SelectedValue;
                _data.TglButuh = DateTime.ParseExact(txtTglButuh.Text, "dd/MM/yyyy", null);
                _data.KdAlasan = Convert.ToInt32(rblAlasan.SelectedValue);
                _data.JmlOrang = Convert.ToInt32(txtJmlKebutuhanKaryawan.Text);
                _data.JobDescList = ListJobDesc;
                _data.QualificationList = ListQualification;
                _data.UserUp.KdUser = UserDataSession.KdUser;

                RecruitmentApprovalData _recAppData = new RecruitmentApprovalData();
                _recAppData.Komentar = txtComment.Text;
                _recAppData.IsFinalize = 1;
                _recAppData.User = new UserData();
                _recAppData.User.KdUser = UserDataSession.KdUser;
                _data.ApprovalDataList = new List<RecruitmentApprovalData>();
                _data.ApprovalDataList.Add(_recAppData);
                _data.NoRequest = NoRequest;

                RecruitmentData _recData = new RecruitmentSystem().GetRecruitmentData(NoRequest);
                _data.Creator.KdUser = _recData.Creator.KdUser;

                _recAppData.LvApproval = new LevelApprovalData();
                _recAppData.LvApproval.KdLevelApproval = _recData.CurrLevelApproval;

                _data.CurrLevelApproval = "6";

                List<KandidatData> _listKandidatTerpilih = new List<KandidatData>();
                KandidatData _kandidatTerpilih = new KandidatData();

                foreach (RepeaterItem item in rptKandidatFinal.Items)
                {
                    TextBox hidKdKandidat = (TextBox)item.FindControl("hidKdKandidat");
                    CheckBox chkPilih = (CheckBox)item.FindControl("chkPilih");
                    Label lblNama = (Label)item.FindControl("lblNama");
                    Label lblGender = (Label)item.FindControl("lblGender");
                    Label lblEmail = (Label)item.FindControl("lblEmail");

                    if (chkPilih.Checked)
                    {
                        _kandidatTerpilih = new KandidatData();
                        _kandidatTerpilih.KdKandidat = Convert.ToInt32(hidKdKandidat.Text);
                        _kandidatTerpilih.NoRequest = NoRequest;
                        _kandidatTerpilih.NmKandidat = lblNama.Text;
                        _kandidatTerpilih.Gender = lblGender.Text == "Male" ? 'L' : 'P';
                        _kandidatTerpilih.Email = lblEmail.Text;
                        _listKandidatTerpilih.Add(_kandidatTerpilih);
                    }
                }

                if (_listKandidatTerpilih.Count == 0)
                {
                    alertFinalisasi.Show("Please choose at least one candidate", AlertType.Danger);
                    return;
                }

                if (_listKandidatTerpilih.Count <= _data.JmlOrang && _listKandidatTerpilih.Count > 0)
                {
                    int resultKandidat = new RecruitmentSystem().UpdateKandidatTerpilih(_listKandidatTerpilih);
                    string result = new RecruitmentSystem().UpdateRequest(_data);

                    // Ambil template email untuk kiriim notifikasi
                    StreamReader sr = new StreamReader(Server.MapPath("~/MailTemplate/ClosingNotificationMailTemplate.htm"));
                    string template = sr.ReadToEnd();
                    sr.Close();

                    int notifyResult = new NotificationSystem().NotifyRequestClosed(_data, template, _listKandidatTerpilih, UserDataSession.KdUser);

                    if (result != "" && resultKandidat > 0)
                    {
                        Response.Redirect("~/Transaksi/Rekrutmen.aspx?=mid" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
                    }
                    else
                    {
                        alertFinalisasi.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
                    }
                }
                else
                {
                    alertFinalisasi.Show("Maximum number of choosen candidate is " + _data.JmlOrang + " person(s)", AlertType.Danger);
                }
            }
            catch (Exception ex)
            {
                alertFinalisasi.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
            }
        }

        protected void btnYesConfirm_Click(object sender, EventArgs e)
        {
            int kdKandidat = Convert.ToInt32(hidKdKandidatConfirm.Value);
            int result = new RecruitmentSystem().DeleteKandidatFromRequest(kdKandidat, NoRequest);
            if (result > 0)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?success=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
            }
            else
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
            }
        }

        protected void btnYesPermanentConfirm_Click(object sender, EventArgs e)
        {
            int kdKandidat = Convert.ToInt32(hidKdKandidatConfirm.Value);
            int result = new RecruitmentSystem().DeleteRecruitmentKandidat(kdKandidat, NoRequest);
            if (result > 0)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?success=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
            }
            else
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
            }
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e)
        {
            popUpConfirm.Hide();
        }

        protected void btnAddKandidatFromMaster_Click(object sender, EventArgs e)
        {
            /*BOF: 4 April 2015: Penambahan logika penyimpanan request multi-tab*/
            CheckPrivileges();
            if (!IsCanEdit)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?error=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), true);
            }
            /*EOF: 4 April 2015*/

            List<KandidatData> _listSelectedKandidat = new List<KandidatData>();

            foreach (RepeaterItem item in rptMasterKandidat.Items)
            {
                CheckBox chkPilih = (CheckBox)item.FindControl("chkPilih");
                if (chkPilih.Checked == true)
                {
                    KandidatData _kandidatData = new KandidatData();
                    TextBox hidKdKandidat = (TextBox)item.FindControl("hidKdKandidat");
                    TextBox hidRatingKandidat = (TextBox)item.FindControl("hidRatingKandidat");

                    int kdKandidat = Convert.ToInt32(hidKdKandidat.Text);
                    _kandidatData.KdKandidat = Convert.ToInt32(hidKdKandidat.Text);
                    _kandidatData.Divisi.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                    _kandidatData.StrukturOrganisasi.KdSO = ddlStrukturOrganisasi.SelectedValue;
                    _kandidatData.Jabatan.KdJabatan = ddlJabatan.SelectedValue;
                    _kandidatData.NoRequest = NoRequest;
                    _kandidatData.UserIn.KdUser = UserDataSession.KdUser;
                    _kandidatData.IsPassed = 0;
                    _kandidatData.Rate = Convert.ToInt32(hidRatingKandidat.Text);
                    _listSelectedKandidat.Add(_kandidatData);
                }
            }

            int result = new RecruitmentSystem().AddRecruitmentKandidatFromExistingList(_listSelectedKandidat);

            if (result > 0)
            {
                Response.Redirect("~/Transaksi/AddEditRekrutmen.aspx?success=1&e=1&no=" + HttpUtility.UrlEncode(Rijndael.Encrypt(NoRequest)) + "&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
            }
            else
            {
                alertAddExistingKandidat.Show("Failed to save the data, please re-check the data you input", AlertType.Danger);
            }
        }

        protected void rptMasterKandidat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox hidKdKandidat = (TextBox)e.Item.FindControl("hidKdKandidat");
                Label lblNama = (Label)e.Item.FindControl("lblNama");
                Label lblGender = (Label)e.Item.FindControl("lblGender");
                Label lblNoIdentitas = (Label)e.Item.FindControl("lblNoIdentitas");
                Label lblNoHP = (Label)e.Item.FindControl("lblNoHP");
                TextBox hidRatingKandidat = (TextBox)e.Item.FindControl("hidRatingKandidat");
                Rating ratingKandidat = (Rating)e.Item.FindControl("ratingKandidat");

                KandidatData kandidatData = (KandidatData)e.Item.DataItem;

                hidKdKandidat.Text = kandidatData.KdKandidat.ToString();
                lblNama.Text = kandidatData.NmKandidat;
                lblGender.Text = kandidatData.Gender == 'L' ? "Male" : "Female";
                lblNoIdentitas.Text = kandidatData.NoIdentitas;
                lblNoHP.Text = kandidatData.NoHandphone;
                ratingKandidat.CurrentRating = kandidatData.Rate;
                hidRatingKandidat.Text = kandidatData.Rate.ToString();
                ratingKandidat.BehaviorID = "ratingMasterKandidat" + e.Item.ItemIndex.ToString();
                ratingKandidat.CssClass += "ratingControl";
                ratingKandidat.Attributes.Add("data-behaviorid", "ratingMasterKandidat" + e.Item.ItemIndex.ToString());
            }
        }

        static void HtmlToPdf(string pageLocation, string destinationFile)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "wkhtmltopdf.exe";
            startInfo.Arguments = pageLocation + " " + destinationFile;
            Process.Start(startInfo);
        }
    }
}
