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
using WirajayaRMS.Web.UserControl;
using System.Web.Services;

namespace WirajayaRMS.Web.Settings
{
    public partial class LevelApproval : System.Web.UI.Page
    {
        List<LevelApprovalData> _listLvApproval
        {
            get
            {
                if (ViewState["_listLvApproval"] == null)
                    return new List<LevelApprovalData>();
                else
                    return (List<LevelApprovalData>)ViewState["_listLvApproval"];
            }
            set
            {
                ViewState["_listLvApproval"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Master.PageTitle = Page.Title;
            Master.PageSubTitle = "Level approval settings page";
            BuildLevelApprovalTree();
        }

        protected void BuildLevelApprovalTree()
        {
            List<DivisiData> _listDivisi = new DivisiSystem().GetDivisiList();

            tvLvApproval.ContextMenu.MenuItems.Clear();
            tvLvApproval.ContextMenu.MenuItems.Add(new ASContextMenuItem("Add", "return onAddContextMenu(" + tvLvApproval.ContextMenuClientID + ")"));
            tvLvApproval.ContextMenu.MenuItems.Add(new ASContextMenuItem("Edit", "return onEditContextMenu(" + tvLvApproval.ContextMenuClientID + ")"));
            tvLvApproval.ContextMenu.MenuItems.Add(new ASContextMenuItem("Delete", "return onDeleteContextMenu(" + tvLvApproval.ContextMenuClientID + ")"));

            tvLvApproval.RootNode.Clear();

            foreach (DivisiData _itemDivisi in _listDivisi)
            {
                string _rootText = _itemDivisi.NmDivisi;
                string _rootValue = _itemDivisi.KdDivisi.ToString();

                ASTreeViewNode _rootNode = new ASTreeViewNode(_rootText, _rootValue);

                tvLvApproval.RootNode.AppendChild(_rootNode);

                _listLvApproval = new LevelApprovalSystem().GetLevelApprovalList(_itemDivisi.KdDivisi);

                foreach (LevelApprovalData _itemLvApproval in _listLvApproval)
                {
                    string _value = _itemLvApproval.KdLevelApproval;
                    string _text = _itemLvApproval.KdLevelApproval + ". " + _itemLvApproval.NmLevelApproval;

                    ASTreeViewNode _node;

                    if (_itemLvApproval.IsActive == 0)
                        _node = new ASTreeViewNode(HttpUtility.HtmlDecode("<span style='color:red;'>" + _text + "</span>"), _value);
                    else
                        _node = new ASTreeViewNode(_text, _value);

                    _node = new ASTreeViewNode(_text, _value);

                    if (_itemLvApproval.ChildNode.Count > 0)
                    {
                        BuildLevelApprovalChildNode(_itemLvApproval, _node.ChildNodes);
                    }

                    _rootNode.AppendChild(_node);
                }
            }
        }

        private void BuildLevelApprovalChildNode(LevelApprovalData _node, List<ASTreeViewNode> _nodeList)
        {
            foreach (LevelApprovalData _item in _node.ChildNode)
            {
                string _value = _item.KdLevelApproval;
                string _text = _item.NmLevelApproval;

                ASTreeViewNode _newNode = new ASTreeViewNode(_value + ". " + _text, _value);

                if (_item.ChildNode.Count > 0)
                {
                    BuildLevelApprovalChildNode(_item, _newNode.ChildNodes);
                }

                _nodeList.Add(_newNode);
            }
        }

        [WebMethod]
        public static LevelApprovalData GetLevelApprovalData(int kdDivisi, string kdLevelApproval)
        {
            LevelApprovalData _data = new LevelApprovalSystem().GetLevelApprovalData(kdDivisi, kdLevelApproval);
            return _data;
        }

        protected void btnAddLevelApproval_Click(object sender, EventArgs e)
        {
            LevelApprovalData _data = new LevelApprovalData();
            _data.KdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            _data.NmLevelApproval = txtNmLevelApproval.Text;
            _data.ParentKdLevelApproval = hidParentKdLvApproval.Value;
            _data.StatusDokumen = txtStatusDokumen.Text;
            _data.IsActive = 1;

            int success = new LevelApprovalSystem().InsertLevelApproval(_data);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Level approval data saved successfully", AlertType.Success);
            }
            else 
            {
                alertNotification.Show("Failed to save the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }

            BuildLevelApprovalTree();
            popUpAddEditLvApproval.Hide();
        }

        protected void btnEditLevelApproval_Click(object sender, EventArgs e)
        {
            LevelApprovalData _data = new LevelApprovalData();
            _data.KdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            _data.KdLevelApproval = hidKdLvApproval.Value;
            _data.NmLevelApproval = txtNmLevelApproval.Text;
            _data.ParentKdLevelApproval = hidParentKdLvApproval.Value;
            _data.StatusDokumen = txtStatusDokumen.Text;
            _data.IsActive = 1;

            int success = new LevelApprovalSystem().UpdateLevelApproval(_data);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Level approval data updated successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to update the data. Please re-check the data you input or try again in a moment", AlertType.Danger);
            }

            BuildLevelApprovalTree();
            popUpAddEditLvApproval.Hide();
        }

        protected void btnYesConfirm_Click(object sender, EventArgs e) 
        {
            int kdDivisi = Convert.ToInt32(hidKdDivisi.Value);
            string kdLevelApproval = hidKdLvApproval.Value;

            int success = new LevelApprovalSystem().DeleteLevelApproval(kdDivisi, kdLevelApproval);

            alertNotification.Visible = false;

            if (success > 0)
            {
                alertNotification.Show("Level approval data deleted successfully", AlertType.Success);
            }
            else
            {
                alertNotification.Show("Failed to delete the data", AlertType.Danger);
            }

            BuildLevelApprovalTree();
            popUpConfirm.Hide();
        }

        protected void btnNoConfirm_Click(object sender, EventArgs e) 
        {
            popUpConfirm.Hide();
        }
    }
}
