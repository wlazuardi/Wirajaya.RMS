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
using Geekees.Common.Controls;
using System.Web.Services;
using WirajayaRMS.Web.UserControl;

namespace WirajayaRMS.Web.Pendataan
{
    public partial class StrukturOrganisasi : SecurePage
    {
        List<StrukturOrganisasiData> _listStrukturOrganisasi
        {
            get
            {
                if (ViewState["_listStrukturOrganisasi"] == null)
                    return new List<StrukturOrganisasiData>();
                else
                    return (List<StrukturOrganisasiData>)ViewState["_listStrukturOrganisasi"];
            }
            set
            {
                ViewState["_listStrukturOrganisasi"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Organizational structure data-logging page";
            BuildStrukturOrganisasiTree();

            if (UserDataSession.IsAdmin != 1) 
            {
                txtMaxJmlKaryawan.Enabled = false;
            }
        }

        //protected void ddlDivisi_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BuildStrukturOrganisasiTree();
        //}

        protected void BuildStrukturOrganisasiTree()
        {
            tvStrukturOrganisasi.ContextMenu.MenuItems.Clear();
            tvStrukturOrganisasi.ContextMenu.MenuItems.Add(new ASContextMenuItem("Add", "return onAddContextMenu(" + tvStrukturOrganisasi.ContextMenuClientID + ")"));
            tvStrukturOrganisasi.ContextMenu.MenuItems.Add(new ASContextMenuItem("Edit", "return onEditContextMenu(" + tvStrukturOrganisasi.ContextMenuClientID + ")"));
            tvStrukturOrganisasi.ContextMenu.MenuItems.Add(new ASContextMenuItem("Delete", "return onDeleteContextMenu(" + tvStrukturOrganisasi.ContextMenuClientID + ")"));

            tvStrukturOrganisasi.RootNode.Clear();

            List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);

            foreach (DivisiData _itemDivisi in _listDivisi)
            {
                string _rootText = _itemDivisi.NmDivisi;
                string _rootValue = _itemDivisi.KdDivisi.ToString();

                ASTreeViewNode _rootNode = new ASTreeViewNode(_rootText, _rootValue);

                tvStrukturOrganisasi.RootNode.AppendChild(_rootNode);

                _listStrukturOrganisasi = new StrukturOrganisasiSystem().GetAllListStrukturOrganisasi(_itemDivisi.KdDivisi);

                foreach (StrukturOrganisasiData _itemSO in _listStrukturOrganisasi)
                {
                    string _value = _itemSO.KdSO;
                    string _text = _itemSO.NmStrukturOrganisasi;

                    ASTreeViewNode _node = new ASTreeViewNode(_value + ". "  + _text, _value);

                    if (_itemSO.ChildNode.Count > 0)
                    {
                        BuildStrukturOrganisasiChildNode(_itemSO, _node.ChildNodes);
                    }

                    _rootNode.AppendChild(_node);
                }
            }
        }

        protected void BuildStrukturOrganisasiChildNode(StrukturOrganisasiData _node, List<ASTreeViewNode> _nodeList)
        {
            foreach (StrukturOrganisasiData _item in _node.ChildNode)
            {
                string _value = _item.KdSO;
                string _text = _item.NmStrukturOrganisasi;

                ASTreeViewNode _newNode = new ASTreeViewNode(_value + ". " + _text, _value);

                if (_item.ChildNode.Count > 0)
                {
                    BuildStrukturOrganisasiChildNode(_item, _newNode.ChildNodes);
                }

                _nodeList.Add(_newNode);
            }
        }

        //protected void BuildUnitDropdownTree()
        //{
        //    ddlUnit.Items.Clear();

        //    List<UnitData> _listJabatan = new UnitSystem().GetAllListUnit(Convert.ToInt32(ddlDivisi.SelectedValue));

        //    foreach (UnitData _itemJabatan in _listJabatan)
        //    {
        //        string _value = _itemJabatan.KdUnit;
        //        string _text = _itemJabatan.NmUnit;

        //        ddlUnit.Items.Add(new ListItem(_text, _value));

        //        if (_itemJabatan.ChildNode.Count > 0)
        //        {
        //            BuildUnitChildNode(_itemJabatan, 1);
        //        }
        //    }

        //    ddlUnit.Items.Insert(0, new ListItem("-- Pilih Unit --", "0"));
        //    ddlUnit.SelectedIndex = 0;
        //}

        //protected void BuildUnitChildNode(UnitData _node, int _depth)
        //{
        //    foreach (UnitData _item in _node.ChildNode)
        //    {
        //        string _value = _item.KdUnit;
        //        string _text = _item.NmUnit;

        //        ASTreeViewNode _newNode = new ASTreeViewNode(_text, _value);
        //        string _space = "";
        //        for (int i = 0; i < _depth; i++)
        //        {
        //            _space += "&nbsp;&nbsp;&nbsp;";
        //        }

        //        ddlUnit.Items.Add(new ListItem(HttpUtility.HtmlDecode(_space) + _text, _value));

        //        if (_item.ChildNode.Count > 0)
        //        {
        //            BuildUnitChildNode(_item, _depth + 1);
        //        }
        //    }
        //}

        [WebMethod]
        public static List<UnitData> GetUnitList(int kdDivisi) 
        {
            List<UnitData> _listJabatan = new UnitSystem().GetAllListUnit(kdDivisi);
            return _listJabatan;
        }

        [WebMethod]
        public static StrukturOrganisasiData GetStrukturOrganisasiData(int kdDivisi, string kdSO)
        {
            StrukturOrganisasiData _data = new StrukturOrganisasiSystem().GetStrukturOrganisasiData(kdDivisi, kdSO);
            return _data;
        }

        protected void btnAddSO_Click(object sender, EventArgs e)
        {
            StrukturOrganisasiData _data = new StrukturOrganisasiData();
            //_data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            _data.KdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            //_data.KdUnit = ddlDivisi.SelectedValue;
            _data.KdUnit = hidKdUnit.Value;
            _data.NmStrukturOrganisasi = txtNamaSO.Text;
            _data.ParentKdSO = hidParentKdSO.Value;
            _data.JmlKaryawan = Convert.ToInt32(txtJmlKaryawan.Text);
            _data.MaxJmlKaryawan = Convert.ToInt32(txtMaxJmlKaryawan.Text);
            _data.IsActive = 1;

            int success = new StrukturOrganisasiSystem().InsertStrukturOrganisasi(_data);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Organizational structure data saved successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }

            BuildStrukturOrganisasiTree();
            popUpAddEditSO.Hide();
        }

        protected void btnEditSO_Click(object sender, EventArgs e)
        {
            StrukturOrganisasiData _data = new StrukturOrganisasiData();
            //_data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            _data.KdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            //_data.KdUnit = ddlDivisi.SelectedValue;
            _data.KdUnit = hidKdUnit.Value;
            _data.KdSO = hidKdSO.Value;
            _data.NmStrukturOrganisasi = txtNamaSO.Text;
            _data.ParentKdSO = hidParentKdSO.Value;
            _data.JmlKaryawan = Convert.ToInt32(txtJmlKaryawan.Text);
            _data.MaxJmlKaryawan = Convert.ToInt32(txtMaxJmlKaryawan.Text);
            _data.IsActive = 1;

            int success = new StrukturOrganisasiSystem().UpdateStrukturOrganisasi(_data);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Organizational structure data updated successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }

            BuildStrukturOrganisasiTree();
            popUpAddEditSO.Hide();
        }

        protected void btnYesConfirm_Click(object sender, EventArgs e)
        {
            //int kdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            int kdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            string kdSO = hidKdSO.Value;

            int success = new StrukturOrganisasiSystem().DeleteStrukturOrganisasi(kdDivisi, kdSO);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Organizational structure data deleted successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to delete the data", AlertType.Danger);
            }

            BuildStrukturOrganisasiTree();
            popUpConfirm.Hide();
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e)
        {
            BuildStrukturOrganisasiTree();
            popUpConfirm.Hide();
        }
    }
}
