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
using WirajayaRMS.Business.Entities;
using WirajayaRMS.Business.ApplicationFacade;
using System.Collections.Generic;
using WirajayaRMS.CrossCutting.Security;
using WirajayaRMS.CrossCutting.OptManagement;
using AjaxControlToolkit;
using System.IO;
using WirajayaRMS.Web.UserControl;

namespace WirajayaRMS.Web.Kandidat
{
    public partial class Kandidat : SecurePage
    {
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

        public List<PositionData> ListPosisi
        {
            set
            {
                ViewState["ListPosisi"] = value;
            }
            get
            {
                if (ViewState["ListPosisi"] == null)
                    return new List<PositionData>();
                else
                    return (List<PositionData>)ViewState["ListPosisi"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Master candidate data-logging page";

            if (Request.QueryString["error"] != null && Request.QueryString["error"] == "1")
                alertNotification.Show("Failed to save the data, please re-check your input", AlertType.Danger);

            if (Request.QueryString["success"] != null && Request.QueryString["success"] == "1")
                alertNotification.Show("Success to save data", AlertType.Success);

            // Code agar bisa upload file saat di pop up
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnAddKandidat);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(btnEditKandidat);
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            if (!IsPostBack)
            {
                List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);
                ddlDivisi.DataSource = _listDivisi;
                ddlDivisi.DataTextField = "KdNmDivisi";
                ddlDivisi.DataValueField = "KdDivisi";
                ddlDivisi.DataBind();
                ddlDivisi.Items.Insert(0, new ListItem("-- Select Division --", "0"));
                ddlDivisi.SelectedIndex = 0;

                BuildSODropdownTree(ddlStrukturOrganisasi, ddlDivisi);
                BuildJabatanDropdown(ddlJabatan, ddlDivisi, ddlStrukturOrganisasi);
                BuildKandidatRepeater();
            }
        }

        private void BuildSODropdownTree(DropDownList dropdown, DropDownList ddlDivisi)
        {
            dropdown.Items.Clear();

            List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiSystem().GetListStrukturOrganisasiByKdUser(Convert.ToInt32(ddlDivisi.SelectedValue), UserDataSession.KdUser);

            foreach (StrukturOrganisasiData _itemSO in _listSO)
            {
                string _value = _itemSO.KdSO;
                string _text = _itemSO.ParentNmStrukturOrganisasi + _itemSO.NmStrukturOrganisasi;

                dropdown.Items.Add(new ListItem(_value + ". " + _text, _value));

                if (_itemSO.ChildNode.Count > 0)
                {
                    BuildSOChildNode(dropdown, _itemSO, 1);
                }
            }

            dropdown.Items.Insert(0, new ListItem("-- Select Organizational Structure --", "0"));
            dropdown.SelectedIndex = 0;
        }

        private void BuildSOChildNode(DropDownList dropdown, StrukturOrganisasiData _node, int _depth)
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

                dropdown.Items.Add(new ListItem(HttpUtility.HtmlDecode(_space) + _value + ". " + _text, _value));

                if (_item.ChildNode.Count > 0)
                {
                    BuildSOChildNode(dropdown, _item, _depth + 1);
                }

            }
        }

        private void BuildJabatanDropdown(DropDownList dropdown, DropDownList ddlDivisi, DropDownList ddlStrukturOrganisasi)
        {
            dropdown.Items.Clear();

            StrukturOrganisasiData _soData = new StrukturOrganisasiSystem().GetStrukturOrganisasiData(Convert.ToInt32(ddlDivisi.SelectedValue), ddlStrukturOrganisasi.SelectedValue);

            if (_soData.KdUnit != null)
            {
                List<JabatanData> _listJabatan = new JabatanSystem().GetUnitMaxJabatanList(Convert.ToInt32(ddlDivisi.SelectedValue), _soData.KdUnit);
                dropdown.DataSource = _listJabatan;
                dropdown.DataTextField = "KdNmJabatan";
                dropdown.DataValueField = "KdJabatan";
                dropdown.DataBind();
            }

            dropdown.Items.Insert(0, new ListItem("-- Select Position --", "0"));
            dropdown.SelectedIndex = 0;
        }


        private void BuildKandidatRepeater()
        {
            int kdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            string kdSO = ddlStrukturOrganisasi.SelectedValue;
            string kdJabatan = ddlJabatan.SelectedValue;
            List<KandidatData> _listKandidat = new KandidatSystem().GetKandidatList(kdDivisi, kdSO, kdJabatan);
            rptKandidat.DataSource = _listKandidat;
            rptKandidat.DataBind();
        }

        protected void ddlDivisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildSODropdownTree(ddlStrukturOrganisasi, ddlDivisi);
            BuildJabatanDropdown(ddlJabatan, ddlDivisi, ddlStrukturOrganisasi);
            BuildKandidatRepeater();
        }

        protected void ddlStrukturOrganisasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildJabatanDropdown(ddlJabatan, ddlDivisi, ddlStrukturOrganisasi);
            BuildKandidatRepeater();
        }

        protected void ddlJabatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildKandidatRepeater();
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
                //Repeater rptCVFiles = (Repeater)e.Item.FindControl("rptCVFiles");
                Label lblDivisi = (Label)e.Item.FindControl("lblDivisi");
                Label lblSO = (Label)e.Item.FindControl("lblSO");
                Label lblJabatan = (Label)e.Item.FindControl("lblJabatan");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");

                KandidatData kandidatData = (KandidatData)e.Item.DataItem;
                lblNo.Text = (e.Item.ItemIndex + 1).ToString();
                lblNama.Text = kandidatData.NmKandidat;
                lblGender.Text = kandidatData.Gender == 'L' ? "Male" : "Female";
                lblNoIdentitas.Text = kandidatData.NoIdentitas;
                lblNoHandphone.Text = kandidatData.NoHandphone;
                lblEmail.Text = kandidatData.Email;

                //rptCVFiles.DataSource = kandidatData.CVFiles;
                //rptCVFiles.DataBind();

                lblDivisi.Text = kandidatData.Divisi.NmDivisi;
                lblSO.Text = kandidatData.StrukturOrganisasi.NmStrukturOrganisasi;
                lblJabatan.Text = kandidatData.Jabatan.NmJabatan;
                btnEdit.CommandName = "EDIT";
                btnEdit.CommandArgument = kandidatData.KdKandidat.ToString();
                btnDelete.CommandName = "DELETE";
                btnDelete.CommandArgument = kandidatData.KdKandidat.ToString() + "-" + 
                                            kandidatData.Divisi.KdDivisi.ToString() + "-" + 
                                            kandidatData.StrukturOrganisasi.KdSO + "-" + 
                                            kandidatData.Jabatan.KdJabatan;
            }
        }

        protected void rptKandidat_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "EDIT")
                {
                    int kdKandidat = Convert.ToInt32(e.CommandArgument);
                    KandidatData kandidatData = new KandidatSystem().GetKandidatData(kdKandidat);

                    hidKdKandidatEdit.Text = kdKandidat.ToString();
                    txtNmKandidatEdit.Text = kandidatData.NmKandidat;
                    txtNoIdentitasKandidatEdit.Text = kandidatData.NoIdentitas;
                    txtNoHandphoneKandidatEdit.Text = kandidatData.NoHandphone;
                    txtEmailKandidatEdit.Text = kandidatData.Email;
                    rblGenderKandidatEdit.SelectedValue = kandidatData.Gender.ToString();
                    ListExistingCVFile = kandidatData.CVFiles;
                    rptExistingCVFilesEdit.DataSource = kandidatData.CVFiles;
                    rptExistingCVFilesEdit.DataBind();

                    ListPosisi = new PositionSystem().GetPosisiKandidat(kdKandidat);
                    rptPosisiKandidatEdit.DataSource = ListPosisi;
                    rptPosisiKandidatEdit.DataBind();

                    ListCVFile = new List<FileUpload>();
                    rptCVFilesEdit.DataSource = ListCVFile;
                    rptCVFilesEdit.DataBind();
                    popUpEditKandidat.Show();
                }
                else if (e.CommandName == "DELETE")
                {
                    string[] args = e.CommandArgument.ToString().Split('-');
                    int kdKandidat = Convert.ToInt32(args[0]);
                    int kdDivisi = Convert.ToInt32(args[1]);
                    string kdSO = args[2];
                    string kdJabatan = args[3];
                    KandidatData _kandidatData = new KandidatSystem().GetKandidatData(kdKandidat);
                    DivisiData _divisiData = new DivisiSystem().GetDivisiData(kdDivisi);
                    StrukturOrganisasiData _soData = new StrukturOrganisasiSystem().GetStrukturOrganisasiData(kdDivisi, kdSO);
                    JabatanData _jabatanData = new JabatanSystem().GetJabatanData(kdDivisi, kdJabatan);
                    litConfirm.Text = "Are you sure want to delete this candidate: <strong>" + _kandidatData.NmKandidat + 
                        "</strong> on position: <strong>" + _divisiData.NmDivisi + " - " + _soData.NmStrukturOrganisasi + 
                        " - " + _jabatanData.NmJabatan + "</strong>?";

                    hidCommandArgument.Value = e.CommandArgument.ToString();
                    popUpConfirm.Show();

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

        protected void btnAddKandidatFileEdit_Click(object sender, EventArgs e)
        {
            ListCVFile.Add(new FileUpload());
            rptCVFilesEdit.DataSource = ListCVFile;
            rptCVFilesEdit.DataBind();
        }

        protected void btnAddKandidatFile_Click(object sender, EventArgs e)
        {
            ListCVFile.Add(new FileUpload());
            rptCVFiles.DataSource = ListCVFile;
            rptCVFiles.DataBind();
        }

        #region EditKandidat
        protected void rptPosisiKandidatEdit_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlDivisiEditKandidat = (DropDownList)e.Item.FindControl("ddlDivisiEditKandidat");
                DropDownList ddlSOEditKandidat = (DropDownList)e.Item.FindControl("ddlSOEditKandidat");
                DropDownList ddlJabatanEditKandidat = (DropDownList)e.Item.FindControl("ddlJabatanEditKandidat");

                Rating ratingEditKandidat = (Rating)e.Item.FindControl("ratingEditKandidat");
                HiddenField hidRating = (HiddenField)e.Item.FindControl("hidRating");
                Button btnTriggerRatingEdit = (Button)e.Item.FindControl("btnTriggerRatingEdit");

                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                TextBox txtNoRequest = (TextBox)e.Item.FindControl("txtNoRequest");

                PositionData _positionData = (PositionData)e.Item.DataItem;

                List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);
                ddlDivisiEditKandidat.DataSource = _listDivisi;
                ddlDivisiEditKandidat.DataTextField = "KdNmDivisi";
                ddlDivisiEditKandidat.DataValueField = "KdDivisi";
                ddlDivisiEditKandidat.DataBind();
                ddlDivisiEditKandidat.Items.Insert(0, new ListItem("-- Select Division --", "0"));
                ddlDivisiEditKandidat.SelectedIndex = 0;
                ddlDivisiEditKandidat.SelectedValue = _positionData.Divisi.KdDivisi.ToString();

                BuildSODropdownTree(ddlSOEditKandidat, ddlDivisiEditKandidat);
                ddlSOEditKandidat.SelectedValue = _positionData.StrukturOrganisasi.KdSO;
                BuildJabatanDropdown(ddlJabatanEditKandidat, ddlDivisiEditKandidat, ddlSOEditKandidat);
                ddlJabatanEditKandidat.SelectedValue = _positionData.Jabatan.KdJabatan;

                hidRating.Value = _positionData.Rate.ToString();
                ratingEditKandidat.CurrentRating = Convert.ToInt32(hidRating.Value);
                ratingEditKandidat.BehaviorID = "ratingEditKandidat" + e.Item.ItemIndex.ToString();
                btnTriggerRatingEdit.CommandName = "RATE";
                ratingEditKandidat.Attributes.Add("onClick", "storeRating('" + ratingEditKandidat.BehaviorID + "', '" +
                        hidRating.ClientID + "', '" + btnTriggerRatingEdit.ClientID + "')");

                txtNoRequest.Text = _positionData.NoRequest;

                if (ListPosisi.Count > 1)
                {
                    btnDelete.CommandName = "DELETE";
                    btnDelete.CommandArgument = e.Item.ItemIndex.ToString();
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
        }

        protected virtual void rptPosisiKandidatEdit_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlDivisiEditKandidat = (DropDownList)e.Item.FindControl("ddlDivisiEditKandidat");
                DropDownList ddlSOEditKandidat = (DropDownList)e.Item.FindControl("ddlSOEditKandidat");
                DropDownList ddlJabatanEditKandidat = (DropDownList)e.Item.FindControl("ddlJabatanEditKandidat");

                ddlDivisiEditKandidat.SelectedIndexChanged += new EventHandler(ddlDivisiEditKandidat_SelectedIndexChanged);
                ddlSOEditKandidat.SelectedIndexChanged += new EventHandler(ddlSOEditKandidat_SelectedIndexChanged);
            }
        }

        protected void ddlSOEditKandidat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSOEditKandidat = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddlSOEditKandidat.Parent;
            DropDownList ddlDivisiEditKandidat = (DropDownList)item.FindControl("ddlDivisiEditKandidat");
            DropDownList ddlJabatanEditKandidat = (DropDownList)item.FindControl("ddlJabatanEditKandidat");

            foreach (RepeaterItem i in rptPosisiKandidatEdit.Items)
            {
                HiddenField hidRating = (HiddenField)i.FindControl("hidRating");
                Rating ratingEditKandidat = (Rating)i.FindControl("ratingEditKandidat");
                ratingEditKandidat.CurrentRating = Convert.ToInt32(hidRating.Value);
            }

            BuildJabatanDropdown(ddlJabatanEditKandidat, ddlDivisiEditKandidat, ddlSOEditKandidat);
        }

        protected void ddlDivisiEditKandidat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlDivisiEditKandidat = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddlDivisiEditKandidat.Parent;
            DropDownList ddlSOEditKandidat = (DropDownList)item.FindControl("ddlSOEditKandidat");
            DropDownList ddlJabatanEditKandidat = (DropDownList)item.FindControl("ddlJabatanEditKandidat");

            foreach (RepeaterItem i in rptPosisiKandidatEdit.Items)
            {
                HiddenField hidRating = (HiddenField)i.FindControl("hidRating");
                Rating ratingEditKandidat = (Rating)i.FindControl("ratingEditKandidat");
                ratingEditKandidat.CurrentRating = Convert.ToInt32(hidRating.Value);
            }

            BuildSODropdownTree(ddlSOEditKandidat, ddlDivisiEditKandidat);
            BuildJabatanDropdown(ddlJabatanEditKandidat, ddlDivisiEditKandidat, ddlSOEditKandidat);
        }

        protected void rptPosisiKandidatEdit_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "DELETE")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    ListPosisi.RemoveAt(index);
                    rptPosisiKandidatEdit.DataSource = ListPosisi;
                    rptPosisiKandidatEdit.DataBind();
                }
                else if (e.CommandName == "RATE")
                {
                    HiddenField hidRating = (HiddenField)e.Item.FindControl("hidRating");
                    ListPosisi[e.Item.ItemIndex].Rate = Convert.ToInt32(hidRating.Value);

                    foreach (RepeaterItem i in rptPosisiKandidatEdit.Items)
                    {
                        HiddenField _hidRating = (HiddenField)i.FindControl("hidRating");
                        Rating ratingEditKandidat = (Rating)i.FindControl("ratingEditKandidat");
                        ratingEditKandidat.CurrentRating = Convert.ToInt32(_hidRating.Value);
                    }
                }
            }
        }

        protected void btnTambahPosisiEdit_Click(object sender, EventArgs e)
        {
            ListPosisi.Add(new PositionData());
            rptPosisiKandidatEdit.DataSource = ListPosisi;
            rptPosisiKandidatEdit.DataBind();
        }
        #endregion

        #region AddKandidat
        protected void rptPosisiKandidat_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlDivisiKandidat = (DropDownList)e.Item.FindControl("ddlDivisiKandidat");
                DropDownList ddlSOKandidat = (DropDownList)e.Item.FindControl("ddlSOKandidat");
                DropDownList ddlJabatanKandidat = (DropDownList)e.Item.FindControl("ddlJabatanKandidat");

                Rating ratingKandidat = (Rating)e.Item.FindControl("ratingKandidat");
                HiddenField hidRating = (HiddenField)e.Item.FindControl("hidRating");
                Button btnTriggerRating = (Button)e.Item.FindControl("btnTriggerRating");

                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");

                PositionData _positionData = (PositionData)e.Item.DataItem;

                List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);
                ddlDivisiKandidat.DataSource = _listDivisi;
                ddlDivisiKandidat.DataTextField = "KdNmDivisi";
                ddlDivisiKandidat.DataValueField = "KdDivisi";
                ddlDivisiKandidat.DataBind();
                ddlDivisiKandidat.Items.Insert(0, new ListItem("-- Select Division --", "0"));
                ddlDivisiKandidat.SelectedIndex = 0;
                ddlDivisiKandidat.SelectedValue = _positionData.Divisi.KdDivisi.ToString();

                BuildSODropdownTree(ddlSOKandidat, ddlDivisiKandidat);
                ddlSOKandidat.SelectedValue = _positionData.StrukturOrganisasi.KdSO;
                BuildJabatanDropdown(ddlJabatanKandidat, ddlDivisiKandidat, ddlSOKandidat);
                ddlJabatanKandidat.SelectedValue = _positionData.Jabatan.KdJabatan;

                hidRating.Value = _positionData.Rate.ToString();
                ratingKandidat.CurrentRating = Convert.ToInt32(hidRating.Value);
                ratingKandidat.BehaviorID = "ratingKandidat" + e.Item.ItemIndex.ToString();
                btnTriggerRating.CommandName = "RATE";
                ratingKandidat.Attributes.Add("onClick", "storeRating('" + ratingKandidat.BehaviorID + "', '" +
                        hidRating.ClientID + "', '" + btnTriggerRating.ClientID + "')");

                if (ListPosisi.Count > 1)
                {
                    btnDelete.CommandName = "DELETE";
                    btnDelete.CommandArgument = e.Item.ItemIndex.ToString();
                }
                else
                {
                    btnDelete.Visible = false;
                }
            }
        }

        protected virtual void rptPosisiKandidat_ItemCreated(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlDivisiKandidat = (DropDownList)e.Item.FindControl("ddlDivisiKandidat");
                DropDownList ddlSOKandidat = (DropDownList)e.Item.FindControl("ddlSOKandidat");
                DropDownList ddlJabatanKandidat = (DropDownList)e.Item.FindControl("ddlJabatanKandidat");

                ddlDivisiKandidat.SelectedIndexChanged += new EventHandler(ddlDivisiKandidat_SelectedIndexChanged);
                ddlSOKandidat.SelectedIndexChanged += new EventHandler(ddlSOKandidat_SelectedIndexChanged);
            }
        }

        protected void ddlSOKandidat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlSOKandidat = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddlSOKandidat.Parent;
            DropDownList ddlDivisiKandidat = (DropDownList)item.FindControl("ddlDivisiKandidat");
            DropDownList ddlJabatanKandidat = (DropDownList)item.FindControl("ddlJabatanKandidat");

            foreach (RepeaterItem i in rptPosisiKandidat.Items)
            {
                HiddenField hidRating = (HiddenField)i.FindControl("hidRating");
                Rating ratingKandidat = (Rating)i.FindControl("ratingKandidat");
                ratingKandidat.CurrentRating = Convert.ToInt32(hidRating.Value);
            }

            BuildJabatanDropdown(ddlJabatanKandidat, ddlDivisiKandidat, ddlSOKandidat);
        }

        protected void ddlDivisiKandidat_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlDivisiKandidat = (DropDownList)sender;
            RepeaterItem item = (RepeaterItem)ddlDivisiKandidat.Parent;
            DropDownList ddlSOKandidat = (DropDownList)item.FindControl("ddlSOKandidat");
            DropDownList ddlJabatanKandidat = (DropDownList)item.FindControl("ddlJabatanKandidat");

            foreach (RepeaterItem i in rptPosisiKandidat.Items)
            {
                HiddenField hidRating = (HiddenField)i.FindControl("hidRating");
                Rating ratingKandidat = (Rating)i.FindControl("ratingKandidat");
                ratingKandidat.CurrentRating = Convert.ToInt32(hidRating.Value);
            }

            BuildSODropdownTree(ddlSOKandidat, ddlDivisiKandidat);
            BuildJabatanDropdown(ddlJabatanKandidat, ddlDivisiKandidat, ddlSOKandidat);
        }

        protected void rptPosisiKandidat_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "DELETE")
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    ListPosisi.RemoveAt(index);
                    rptPosisiKandidatEdit.DataSource = ListPosisi;
                    rptPosisiKandidatEdit.DataBind();
                }
                else if (e.CommandName == "RATE")
                {
                    HiddenField hidRating = (HiddenField)e.Item.FindControl("hidRating");
                    ListPosisi[e.Item.ItemIndex].Rate = Convert.ToInt32(hidRating.Value);

                    foreach (RepeaterItem i in rptPosisiKandidat.Items)
                    {
                        HiddenField _hidRating = (HiddenField)i.FindControl("hidRating");
                        Rating ratingKandidat = (Rating)i.FindControl("ratingKandidat");
                        ratingKandidat.CurrentRating = Convert.ToInt32(_hidRating.Value);
                    }
                }
            }
        }

        protected void btnTambahPosisi_Click(object sender, EventArgs e)
        {
            ListPosisi.Add(new PositionData());
            rptPosisiKandidat.DataSource = ListPosisi;
            rptPosisiKandidat.DataBind();
        }
        #endregion


        protected void btnEditKandidat_Click(object sender, EventArgs e)
        {
            try
            {
                if (hidKdKandidatEdit.Text != "" && hidKdKandidatEdit.Text != null)
                {
                    KandidatData _dataKandidat = new KandidatData();
                    _dataKandidat.KdKandidat = Convert.ToInt32(hidKdKandidatEdit.Text);
                    _dataKandidat.NmKandidat = txtNmKandidatEdit.Text;
                    _dataKandidat.NoIdentitas = txtNoIdentitasKandidatEdit.Text;
                    _dataKandidat.Gender = Convert.ToChar(rblGenderKandidatEdit.SelectedValue);
                    _dataKandidat.NoHandphone = txtNoHandphoneKandidatEdit.Text;
                    _dataKandidat.Email = txtEmailKandidatEdit.Text;
                    _dataKandidat.UserIn = new UserData();
                    _dataKandidat.UserIn.KdUser = UserDataSession.KdUser;
                    _dataKandidat.CVFiles = new List<FileData>();

                    // Cek existing file, apakah ada perubahan (dihapus)
                    foreach (RepeaterItem item in rptExistingCVFilesEdit.Items)
                    {
                        // Untuk existing file, cukup ambil KdFilenya saja
                        TextBox hidKdFile = (TextBox)item.FindControl("hidKdFile");
                        FileData _fileData = new FileData();
                        _fileData.KdFile = Convert.ToInt32(hidKdFile.Text);
                        _dataKandidat.CVFiles.Add(_fileData);
                    }

                    // Cek file baru
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

                    // Cek data posisi
                    List<PositionData> _listPosisi = new List<PositionData>();
                    foreach (RepeaterItem item in rptPosisiKandidatEdit.Items)
                    {
                        PositionData _positionData = new PositionData();
                        DropDownList ddlDivisiEditKandidat = (DropDownList)item.FindControl("ddlDivisiEditKandidat");
                        DropDownList ddlSOEditKandidat = (DropDownList)item.FindControl("ddlSOEditKandidat");
                        DropDownList ddlJabatanEditKandidat = (DropDownList)item.FindControl("ddlJabatanEditKandidat");
                        HiddenField hidRating = (HiddenField)item.FindControl("hidRating");
                        TextBox txtNoRequest = (TextBox)item.FindControl("txtNoRequest");

                        _positionData.Divisi.KdDivisi = Convert.ToInt32(ddlDivisiEditKandidat.SelectedValue);
                        _positionData.StrukturOrganisasi.KdSO = ddlSOEditKandidat.SelectedValue;
                        _positionData.Jabatan.KdJabatan = ddlJabatanEditKandidat.SelectedValue;
                        _positionData.Rate = Convert.ToInt32(hidRating.Value);
                        _positionData.NoRequest = txtNoRequest.Text;
                        _positionData.UserIn.KdUser = UserDataSession.KdUser;
                        _listPosisi.Add(_positionData);
                    }

                    // Return kdKandidat
                    int result = new KandidatSystem().EditKandidat(_dataKandidat, _listPosisi);

                    // Jika sukses add kandidat, edit juga daftar posisinya
                    if (result > 0)
                    {
                        Response.Redirect("~/Kandidat/Kandidat.aspx?success=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())) , false);
                    }
                    else
                    {
                        Response.Redirect("~/Kandidat/Kandidat.aspx?error=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Kandidat/Kandidat.aspx?error=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
            }
        }

        protected void btnAddKandidat_Click(object sender, EventArgs e)
        {
            try
            {
                KandidatData _dataKandidat = new KandidatData();
                _dataKandidat.NmKandidat = txtNmKandidat.Text;
                _dataKandidat.NoIdentitas = txtNoIdentitasKandidat.Text;
                _dataKandidat.Gender = Convert.ToChar(rblGenderKandidat.SelectedValue);
                _dataKandidat.NoHandphone = txtNoHandphoneKandidat.Text;
                _dataKandidat.Email = txtEmailKandidat.Text;
                _dataKandidat.UserIn = new UserData();
                _dataKandidat.UserIn.KdUser = UserDataSession.KdUser;
                _dataKandidat.CVFiles = new List<FileData>();

                // Cek file baru
                foreach (RepeaterItem item in rptCVFiles.Items)
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

                // Cek data posisi
                List<PositionData> _listPosisi = new List<PositionData>();
                foreach (RepeaterItem item in rptPosisiKandidat.Items)
                {
                    PositionData _positionData = new PositionData();
                    DropDownList ddlDivisiKandidat = (DropDownList)item.FindControl("ddlDivisiKandidat");
                    DropDownList ddlSOKandidat = (DropDownList)item.FindControl("ddlSOKandidat");
                    DropDownList ddlJabatanKandidat = (DropDownList)item.FindControl("ddlJabatanKandidat");
                    HiddenField hidRating = (HiddenField)item.FindControl("hidRating");

                    _positionData.Divisi.KdDivisi = Convert.ToInt32(ddlDivisiKandidat.SelectedValue);
                    _positionData.StrukturOrganisasi.KdSO = ddlSOKandidat.SelectedValue;
                    _positionData.Jabatan.KdJabatan = ddlJabatanKandidat.SelectedValue;
                    _positionData.Rate = Convert.ToInt32(hidRating.Value);
                    _positionData.UserIn.KdUser = UserDataSession.KdUser;
                    _listPosisi.Add(_positionData);
                }

                // Return kdKandidat
                int result = new KandidatSystem().AddKandidat(_dataKandidat, _listPosisi);

                // Jika sukses add kandidat, edit juga daftar posisinya
                if (result > 0)
                {
                    Response.Redirect("~/Kandidat/Kandidat.aspx?success=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                }
                else
                {
                    Response.Redirect("~/Kandidat/Kandidat.aspx?error=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Kandidat/Kandidat.aspx?error=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
            }
        }

        protected void btnShowAddKandidat_Click(object sender, EventArgs e)
        {
            ListPosisi = new List<PositionData>();
            ListPosisi.Add(new PositionData());
            rptPosisiKandidat.DataSource = ListPosisi;
            rptPosisiKandidat.DataBind();

            ListCVFile = new List<FileUpload>();
            ListCVFile.Add(new FileUpload());
            rptCVFiles.DataSource = ListCVFile;
            rptCVFiles.DataBind();
            popUpAddKandidat.Show();
        }

        protected void btnYesConfirm_Click(object sender, EventArgs e) 
        {
            try
            {
                if (hidCommandArgument.Value != null && hidCommandArgument.Value != "")
                {
                    string[] args = hidCommandArgument.Value.ToString().Split('-');
                    int kdKandidat = Convert.ToInt32(args[0]);
                    int kdDivisi = Convert.ToInt32(args[1]);
                    string kdSO = args[2];
                    string kdJabatan = args[3];

                    int deleteResult = new PositionSystem().DeletePosisi(kdKandidat, kdDivisi, kdSO, kdJabatan);

                    if (deleteResult > 0)
                    {
                        Response.Redirect("~/Kandidat/Kandidat.aspx?success=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                    }
                    else
                    {
                        Response.Redirect("~/Kandidat/Kandidat.aspx?error=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())), false);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Kandidat/Kandidat.aspx?error=1&mid=" + HttpUtility.UrlEncode(Rijndael.Encrypt(MenuId.ToString())));
            }
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e)
        {
            popUpConfirm.Hide();
        }

    }
}