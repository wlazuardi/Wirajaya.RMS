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
    public partial class Unit : SecurePage
    {
        List<UnitData> _listUnit
        {
            get
            {
                if (ViewState["_listUnit"] == null)
                    return new List<UnitData>();
                else
                    return (List<UnitData>)ViewState["_listUnit"];
            }
            set
            {
                ViewState["_listUnit"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Organizational unit data-logging page";
            BuildUnitTree();
        }

        //protected void ddlDivisi_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    BuildUnitTree();
        //    //BuildJabatanDropdownTree();
        //}

        protected void BuildUnitTree()
        {
            tvUnit.ContextMenu.MenuItems.Clear();
            tvUnit.ContextMenu.MenuItems.Add(new ASContextMenuItem("Add", "return onAddContextMenu(" + tvUnit.ContextMenuClientID + ")"));
            tvUnit.ContextMenu.MenuItems.Add(new ASContextMenuItem("Edit", "return onEditContextMenu(" + tvUnit.ContextMenuClientID + ")"));
            tvUnit.ContextMenu.MenuItems.Add(new ASContextMenuItem("Delete", "return onDeleteContextMenu(" + tvUnit.ContextMenuClientID + ")"));

            tvUnit.RootNode.Clear();

            List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);

            foreach (DivisiData _itemDivisi in _listDivisi)
            {
                string _rootText = _itemDivisi.NmDivisi;
                string _rootValue = _itemDivisi.KdDivisi.ToString();

                ASTreeViewNode _rootNode = new ASTreeViewNode(_rootText, _rootValue);

                tvUnit.RootNode.AppendChild(_rootNode);

                //_listStrukturOrganisasi = new JabatanSystem().GetAllListJabatan(Convert.ToInt32(ddlDivisi.SelectedValue));
                _listUnit = new UnitSystem().GetAllListUnit(Convert.ToInt32(_itemDivisi.KdDivisi));

                foreach (UnitData _itemUnit in _listUnit)
                {
                    string _value = _itemUnit.KdUnit;
                    string _text = _itemUnit.KdUnit + ". " + _itemUnit.NmUnit;

                    ASTreeViewNode _node;
                    if (_itemUnit.IsActive == 0)
                        _node = new ASTreeViewNode(HttpUtility.HtmlDecode("<span style='color:red;'>" + _text + "</span>"), _value);
                    else
                        _node = new ASTreeViewNode(_text, _value);

                    if (_itemUnit.ChildNode.Count > 0)
                    {
                        BuildUnitChildNode(_itemUnit, _node.ChildNodes);
                    }

                    _rootNode.AppendChild(_node);
                }
            }
        }

        protected void BuildUnitChildNode(UnitData _node, List<ASTreeViewNode> _nodeList)
        {
            foreach (UnitData _item in _node.ChildNode)
            {
                string _value = _item.KdUnit;
                string _text = _item.KdUnit + ". " + _item.NmUnit;

                ASTreeViewNode _newNode;
                if (_item.IsActive == 0)
                    _newNode = new ASTreeViewNode(HttpUtility.HtmlDecode("<span style='color:red;'>" + _text + "</span>"), _value);
                else
                    _newNode = new ASTreeViewNode(_text, _value);

                if (_item.ChildNode.Count > 0)
                {
                    BuildUnitChildNode(_item, _newNode.ChildNodes);
                }

                _nodeList.Add(_newNode);
            }
        }

        //protected void BuildJabatanDropdownTree()
        //{
        //    ddlMaxJabatan.Items.Clear();

        //    //List<JabatanData> _listJabatan = new JabatanSystem().GetAllListJabatan(Convert.ToInt32(ddlDivisi.SelectedValue));
        //    List<JabatanData> _listJabatan = new JabatanSystem().GetAllListJabatan(Convert.ToInt32(hidKdDivisi.Value));

        //    foreach (JabatanData _itemJabatan in _listJabatan)
        //    {
        //        string _value = _itemJabatan.KdJabatan;
        //        string _text = _itemJabatan.NmJabatan;

        //        ddlMaxJabatan.Items.Add(new ListItem(_text, _value));

        //        if (_itemJabatan.ChildNode.Count > 0)
        //        {
        //            BuildJabatanChildNode(_itemJabatan, 1);
        //        }
        //    }

        //    ddlMaxJabatan.Items.Insert(0, new ListItem("-- Pilih jabatan --", "0"));
        //    ddlMaxJabatan.SelectedIndex = 0;
        //}

        //protected void BuildJabatanChildNode(JabatanData _node, int _depth)
        //{
        //    foreach (JabatanData _item in _node.ChildNode)
        //    {
        //        string _value = _item.KdJabatan;
        //        string _text = _item.NmJabatan;

        //        string _space = "";
        //        for (int i = 0; i < _depth; i++) 
        //        {
        //            _space += "&nbsp;&nbsp;&nbsp;";
        //        }

        //        ddlMaxJabatan.Items.Add(new ListItem(HttpUtility.HtmlDecode(_space) + _text, _value));

        //        if (_item.ChildNode.Count > 0)
        //        {
        //            BuildJabatanChildNode(_item, _depth + 1);
        //        }

        //    }
        //}

        [WebMethod]
        public static List<JabatanData> GetJabatanList(int kdDivisi)
        {
            List<JabatanData> _listJabatan = new JabatanSystem().GetAllListJabatan(kdDivisi);
            return _listJabatan;
        }

        [WebMethod]
        public static UnitData GetUnitData(int kdDivisi, string kdUnit)
        {
            UnitData _data = new UnitSystem().GetUnitData(kdDivisi, kdUnit);
            return _data;
        }

        protected void btnAddUnit_Click(object sender, EventArgs e)
        {
            //string _kdJabatanMax = ddlMaxJabatan.SelectedValue;
            string _kdJabatanMax = hidMaxJabatan.Value;

            if (_kdJabatanMax != "0")
            {
                UnitData _data = new UnitData();
                //_data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
                _data.KdDivisi = Convert.ToInt32(hidKdDivisi.Value);
                _data.NmUnit = txtNamaUnit.Text;
                _data.MaxKdJabatan = _kdJabatanMax;
                _data.ParentKdUnit = hidParentKdUnit.Value;
                _data.IsActive = 1;

                int success = new UnitSystem().InsertUnit(_data);

                alertNotification.Visible = false;

                if (success > 0)
                {
                    alertNotification.Show("Unit data saved successfully", AlertType.Success);
                }
                else
                {
                    alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
                }

                BuildUnitTree();
                popUpAddEditUnit.Hide();
            }
        }

        protected void btnEditUnit_Click(object sender, EventArgs e)
        {
            UnitData _data = new UnitData();
            //_data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            _data.KdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            _data.KdUnit = hidKdUnit.Value;
            _data.NmUnit = txtNamaUnit.Text;
            _data.ParentKdUnit = hidParentKdUnit.Value;
            //_data.MaxKdJabatan = ddlMaxJabatan.SelectedValue;
            _data.MaxKdJabatan = hidMaxJabatan.Value;
            _data.IsActive = 1;

            int success = new UnitSystem().UpdateUnit(_data);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Unit data updated successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to update the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }

            BuildUnitTree();
            popUpAddEditUnit.Hide();
        }

        protected void btnYesConfirm_Click(object sender, EventArgs e)
        {
            //int kdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            int kdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            string kdUnit = hidKdUnit.Value;

            int success = new UnitSystem().DeleteUnit(kdDivisi, kdUnit);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Unit data deleted successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to delete the data", AlertType.Danger);
            }

            BuildUnitTree();
            popUpConfirm.Hide();
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e)
        {
            BuildUnitTree();
            popUpConfirm.Hide();
        }

    }
}
