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
    public partial class Jabatan : SecurePage
    {
        List<JabatanData> _listJabatan
        {
            get
            {
                if (ViewState["_listJabatan"] == null)
                    return new List<JabatanData>();
                else
                    return (List<JabatanData>)ViewState["_listJabatan"];
            }
            set
            {
                ViewState["_listJabatan"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Organization positions data-logging page";
            BuildJabatanTree();
        }

        protected void BuildJabatanTree()
        {
            List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList(UserDataSession.KdUser);

            tvJabatan.ContextMenu.MenuItems.Clear();
            tvJabatan.ContextMenu.MenuItems.Add(new ASContextMenuItem("Add", "return onAddContextMenu(" + tvJabatan.ContextMenuClientID + ")"));
            tvJabatan.ContextMenu.MenuItems.Add(new ASContextMenuItem("Edit", "return onEditContextMenu(" + tvJabatan.ContextMenuClientID + ")"));
            tvJabatan.ContextMenu.MenuItems.Add(new ASContextMenuItem("Delete", "return onDeleteContextMenu(" + tvJabatan.ContextMenuClientID + ")"));

            tvJabatan.RootNode.Clear();

            foreach (DivisiData _itemDivisi in _listDivisi)
            {
                string _rootText = _itemDivisi.NmDivisi;
                string _rootValue = _itemDivisi.KdDivisi.ToString();
                
                ASTreeViewNode _rootNode = new ASTreeViewNode(_rootText, _rootValue);

                tvJabatan.RootNode.AppendChild(_rootNode);

                _listJabatan = new JabatanSystem().GetAllListJabatan(_itemDivisi.KdDivisi);

                foreach (JabatanData _itemJabatan in _listJabatan)
                {
                    string _value = _itemJabatan.KdJabatan;
                    string _text = _itemJabatan.KdJabatan + ". " + _itemJabatan.NmJabatan;

                    ASTreeViewNode _node;
                    
                    if(_itemJabatan.IsActive == 0)
                        _node = new ASTreeViewNode(HttpUtility.HtmlDecode("<span style='color:red;'>" + _text + "</span>"), _value);
                    else
                        _node = new ASTreeViewNode(_text, _value);

                    if (_itemJabatan.ChildNode.Count > 0)
                    {
                        BuildJabatanChildNode(_itemJabatan, _node.ChildNodes);
                    }

                    _rootNode.AppendChild(_node);
                }
            }
        }

        protected void BuildJabatanChildNode(JabatanData _node, List<ASTreeViewNode> _nodeList)
        {
            foreach (JabatanData _item in _node.ChildNode)
            {
                string _value = _item.KdJabatan;
                string _text = _item.KdJabatan + ". " + _item.NmJabatan;

                ASTreeViewNode _newNode;
                if (_item.IsActive == 0)
                    _newNode = new ASTreeViewNode(HttpUtility.HtmlDecode("<span style='color:red;'>" + _text + "</span>"), _value);
                else
                    _newNode = new ASTreeViewNode(_text, _value);

                if (_item.ChildNode.Count > 0)
                {
                    BuildJabatanChildNode(_item, _newNode.ChildNodes);
                }

                _nodeList.Add(_newNode);
            }
        }

        [WebMethod]
        public static JabatanData GetJabatanData(int kdDivisi, string kdJabatan)
        {
            JabatanData _data = new JabatanSystem().GetJabatanData(kdDivisi, kdJabatan);
            return _data;
        }

        protected void btnAddJabatan_Click(object sender, EventArgs e)
        {
            JabatanData _data = new JabatanData();
            //_data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            _data.KdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            _data.NmJabatan = txtNamaJabatan.Text;
            _data.ParentKdJabatan = hidParentKdJabatan.Value;
            _data.MinSalary = Convert.ToDouble(txtMinSalary.Text);
            _data.MaxSalary = Convert.ToDouble(txtMaxSalary.Text);
            _data.Fasilitas = txtFasilitas.Text;
            _data.IsActive = 1;

            int success = new JabatanSystem().InsertJabatan(_data);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Position data saved successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Danger);                
            }

            BuildJabatanTree();
            popUpAddEditJabatan.Hide();
        }

        protected void btnEditJabatan_Click(object sender, EventArgs e)
        {
            JabatanData _data = new JabatanData();
            //_data.KdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            _data.KdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            _data.KdJabatan = hidKdJabatan.Value;
            _data.NmJabatan = txtNamaJabatan.Text;
            _data.ParentKdJabatan = hidParentKdJabatan.Value;
            _data.MinSalary = Convert.ToDouble(txtMinSalary.Text);
            _data.MaxSalary = Convert.ToDouble(txtMaxSalary.Text);
            _data.Fasilitas = txtFasilitas.Text;
            _data.IsActive = 1;

            int success = new JabatanSystem().UpdateJabatan(_data);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Position data updated successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to update the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }

            BuildJabatanTree();
            popUpAddEditJabatan.Hide();
        }

        protected void btnYesConfirm_Click(object sender, EventArgs e)
        {
            //int kdDivisi = Convert.ToInt32(ddlDivisi.SelectedValue);
            int kdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            string kdJabatan = hidKdJabatan.Value;

            int success = new JabatanSystem().DeleteJabatan(kdDivisi, kdJabatan);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Position data deleted successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to delete the data", AlertType.Danger);
            }

            BuildJabatanTree();
            popUpConfirm.Hide();
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e)
        {
            BuildJabatanTree();
            popUpConfirm.Hide();
        }
    }
}
