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
using WirajayaRMS.Web.UserControl;

namespace WirajayaRMS.Web.Pendataan
{
    public partial class JobDesc : SecurePage
    {
        #region Private Method
        private void BuildJobDescRepeater()
        {
            List<JobDescData> _listJobDesc = new JobDescSystem().GetJobDescList(Convert.ToInt32(ddlDivisi.SelectedValue), ddlStrukturOrganisasi.SelectedValue, ddlJabatan.SelectedValue);

            if (_listJobDesc.Count == 0)
            {
                lblMessage.Text = "No job decription data found";
            }
            else
            {
                lblMessage.Text = "";
            }

            rptJobDesc.DataSource = _listJobDesc;
            rptJobDesc.DataBind();
        }

        private void BuildSODropdownTree()
        {
            ddlStrukturOrganisasi.Items.Clear();

            List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiSystem().GetAllListStrukturOrganisasi(Convert.ToInt32(ddlDivisi.SelectedValue));

            foreach (StrukturOrganisasiData _itemSO in _listSO)
            {
                string _value = _itemSO.KdSO;
                string _text = _itemSO.NmStrukturOrganisasi;

                ddlStrukturOrganisasi.Items.Add(new ListItem(_value + ". "  + _text, _value));

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
                string _text = _item.NmStrukturOrganisasi;

                string _space = "";
                for (int i = 0; i < _depth; i++)
                {
                    _space += "&nbsp;&nbsp;&nbsp;";
                }

                ddlStrukturOrganisasi.Items.Add(new ListItem(HttpUtility.HtmlDecode(_space) + _value + ". "  + _text, _value));

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
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Organizational structure job description data-logging page";

            if (!IsPostBack)
            {
                List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);
                ddlDivisi.DataSource = _listDivisi;
                ddlDivisi.DataTextField = "NmDivisi";
                ddlDivisi.DataValueField = "KdDivisi";
                ddlDivisi.DataBind();

                BuildSODropdownTree();
            }
        }

        protected void ddlDivisi_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildSODropdownTree();
        }

        protected void ddlStrukturOrganisasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildJabatanDropdown();
        }

        protected void ddlJabatan_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildJobDescRepeater();
        }

        protected void rptJobDesc_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                JobDescData jobDescItem = (JobDescData)e.Item.DataItem;

                Literal litJobDesc = (Literal)e.Item.FindControl("litJobDesc");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
                RequiredFieldValidator reqEditJobDesc = (RequiredFieldValidator)e.Item.FindControl("reqEditJobDesc");

                litJobDesc.Text = jobDescItem.JobDesc;
                btnEdit.CommandName = "EDIT";
                btnDelete.CommandName = "DELETE";
                btnSave.CommandName = "SAVE";
                btnCancel.CommandName = "CANCEL";
                btnEdit.CommandArgument = jobDescItem.KdJobDesc.ToString();
                btnDelete.CommandArgument = jobDescItem.KdJobDesc.ToString();
                btnSave.CommandArgument = jobDescItem.KdJobDesc.ToString();
                reqEditJobDesc.ValidationGroup = jobDescItem.KdJobDesc.ToString();
                btnSave.ValidationGroup = jobDescItem.KdJobDesc.ToString();
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
                Literal litJobDesc = (Literal)e.Item.FindControl("litJobDesc");

                if (e.CommandName == "EDIT")
                {
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;

                    txtEditJobDesc.Text = litJobDesc.Text;
                    txtEditJobDesc.Visible = true;
                    litJobDesc.Visible = false;
                }
                else if (e.CommandName == "SAVE")
                {
                    JobDescData _jobDescData = new JobDescData();
                    _jobDescData.KdJobDesc = Convert.ToInt32(e.CommandArgument);
                    _jobDescData.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                    _jobDescData.KdJabatan = ddlJabatan.SelectedValue;
                    _jobDescData.KdSO = ddlStrukturOrganisasi.SelectedValue;
                    _jobDescData.JobDesc = txtEditJobDesc.Text;

                    int result = new JobDescSystem().UpdateJobDesc(_jobDescData);
                    if (result > 0)
                    {
                        BuildJobDescRepeater();
                    }

                }
                else if (e.CommandName == "CANCEL")
                {
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;

                    txtEditJobDesc.Visible = false;
                    litJobDesc.Visible = true;
                }
                else if (e.CommandName == "DELETE")
                {
                    int kdJobDesc = Convert.ToInt32(e.CommandArgument);
                    int result = new JobDescSystem().DeleteJobDesc(kdJobDesc);
                    if (result > 0)
                    {
                        BuildJobDescRepeater();
                    }
                }
            }
        }

        protected void btnAddJobDesc_Click(object sender, EventArgs e)
        {
            JobDescData _jobDescData = new JobDescData();
            _jobDescData.JobDesc = txtJobDesc.Text;
            _jobDescData.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            _jobDescData.KdSO = ddlStrukturOrganisasi.SelectedValue;
            _jobDescData.KdJabatan = ddlJabatan.SelectedValue;

            alertNotification.Hide();

            int success = new JobDescSystem().AddJobDesc(_jobDescData);

            if (success > 0)
            {
                alertNotification.Show("Job description added successfully", AlertType.Success);
                BuildJobDescRepeater();
                txtJobDesc.Text = "";
            }
            else
            {
                alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }
        }
    }
}
