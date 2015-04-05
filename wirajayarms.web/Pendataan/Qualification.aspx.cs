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
using WirajayaRMS.Business.Entities;
using WirajayaRMS.Business.ApplicationFacade;
using System.Collections.Generic;
using WirajayaRMS.Web.UserControl;

namespace WirajayaRMS.Web.Pendataan
{
    public partial class Qualification : SecurePage
    {
        private void BuildQualificationRepeater()
        {
            List<QualificationData> _listQualification = new QualificationSystem().GetQualificationList(Convert.ToInt32(ddlDivisi.SelectedValue), ddlStrukturOrganisasi.SelectedValue, ddlJabatan.SelectedValue);

            if (_listQualification.Count == 0)
            {
                lblMessage.Text = "No qualification data found";
            }
            else
            {
                lblMessage.Text = "";
            }

            rptQualification.DataSource = _listQualification;
            rptQualification.DataBind();
        }

        private void BuildSODropdownTree()
        {
            ddlStrukturOrganisasi.Items.Clear();

            List<StrukturOrganisasiData> _listSO = new StrukturOrganisasiSystem().GetAllListStrukturOrganisasi(Convert.ToInt32(ddlDivisi.SelectedValue));

            foreach (StrukturOrganisasiData _itemSO in _listSO)
            {
                string _value = _itemSO.KdSO;
                string _text = _itemSO.NmStrukturOrganisasi;

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
                string _text = _item.NmStrukturOrganisasi;

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

            BuildQualificationRepeater();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Organizational structure qualification data-logging page";

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
            BuildQualificationRepeater();
        }

        protected void rptQualification_ItemDataBound(object source, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                QualificationData QualificationItem = (QualificationData)e.Item.DataItem;

                Literal litQualification = (Literal)e.Item.FindControl("litQualification");
                LinkButton btnEdit = (LinkButton)e.Item.FindControl("btnEdit");
                LinkButton btnDelete = (LinkButton)e.Item.FindControl("btnDelete");
                LinkButton btnSave = (LinkButton)e.Item.FindControl("btnSave");
                LinkButton btnCancel = (LinkButton)e.Item.FindControl("btnCancel");
                RequiredFieldValidator reqEditQualification = (RequiredFieldValidator)e.Item.FindControl("reqEditQualification");

                litQualification.Text = QualificationItem.Qualification;
                btnEdit.CommandName = "EDIT";
                btnDelete.CommandName = "DELETE";
                btnSave.CommandName = "SAVE";
                btnCancel.CommandName = "CANCEL";
                btnEdit.CommandArgument = QualificationItem.KdQualification.ToString();
                btnDelete.CommandArgument = QualificationItem.KdQualification.ToString();
                btnSave.CommandArgument = QualificationItem.KdQualification.ToString();
                reqEditQualification.ValidationGroup = QualificationItem.KdQualification.ToString();
                btnSave.ValidationGroup = QualificationItem.KdQualification.ToString();
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
                Literal litQualification = (Literal)e.Item.FindControl("litQualification");

                if (e.CommandName == "EDIT")
                {
                    btnEdit.Visible = false;
                    btnDelete.Visible = false;
                    btnSave.Visible = true;
                    btnCancel.Visible = true;

                    txtEditQualification.Text = litQualification.Text;
                    txtEditQualification.Visible = true;
                    litQualification.Visible = false;
                }
                else if (e.CommandName == "SAVE")
                {
                    QualificationData _QualificationData = new QualificationData();
                    _QualificationData.KdQualification = Convert.ToInt32(e.CommandArgument);
                    _QualificationData.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                    _QualificationData.KdJabatan = ddlJabatan.SelectedValue;
                    _QualificationData.KdSO = ddlStrukturOrganisasi.SelectedValue;
                    _QualificationData.Qualification = txtEditQualification.Text;

                    int result = new QualificationSystem().UpdateQualification(_QualificationData);
                    if (result > 0)
                    {
                        BuildQualificationRepeater();
                    }

                }
                else if (e.CommandName == "CANCEL")
                {
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                    btnSave.Visible = false;
                    btnCancel.Visible = false;

                    txtEditQualification.Visible = false;
                    litQualification.Visible = true;
                }
                else if (e.CommandName == "DELETE")
                {
                    int kdQualification = Convert.ToInt32(e.CommandArgument);
                    int result = new QualificationSystem().DeleteQualification(kdQualification);
                    if (result > 0)
                    {
                        BuildQualificationRepeater();
                    }
                }
            }
        }

        protected void btnAddQualification_Click(object sender, EventArgs e)
        {
            QualificationData _QualificationData = new QualificationData();
            _QualificationData.Qualification = txtQualification.Text;
            _QualificationData.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            _QualificationData.KdSO = ddlStrukturOrganisasi.SelectedValue;
            _QualificationData.KdJabatan = ddlJabatan.SelectedValue;

            alertNotification.Hide();

            int success = new QualificationSystem().AddQualification(_QualificationData);

            if (success > 0)
            {
                alertNotification.Show("Qualification data added successfully", AlertType.Success);
                BuildQualificationRepeater();
                txtQualification.Text = "";
            }
            else
            {
                alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }
        }
    }
}
