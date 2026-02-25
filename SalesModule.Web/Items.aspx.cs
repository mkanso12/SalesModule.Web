using System;
using System.Web.UI.WebControls;
using Unity;
using SalesModule.BusinessLogic;
using SalesModule.DataAccess;

namespace SalesModule.Web
{
    public partial class Items : System.Web.UI.Page
    {
        private IItemService _itemService;

        protected void Page_Load(object sender, EventArgs e)
        {
            var container = (IUnityContainer)Application["UnityContainer"];
            _itemService = container.Resolve<IItemService>();

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var items = _itemService.GetAllItems();
            gvItems.DataSource = items;
            gvItems.DataBind();
        }

        protected void gvItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvItems.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvItems.EditIndex = -1;
            BindGrid();
        }

        protected void gvItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvItems.DataKeys[e.RowIndex].Value);
            var row = gvItems.Rows[e.RowIndex];

            var txtName = (TextBox)row.FindControl("txtName");
            var txtUnitPrice = (TextBox)row.FindControl("txtUnitPrice");
            var txtOnHand = (TextBox)row.FindControl("txtOnHand");

            var item = new Item
            {
                Id = id,
                Name = txtName.Text,
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text),
                OnHandQuantity = Convert.ToInt32(txtOnHand.Text)
            };

            try
            {
                _itemService.UpdateItem(item);
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}');</script>");
            }

            gvItems.EditIndex = -1;
            BindGrid();
        }

        protected void gvItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvItems.DataKeys[e.RowIndex].Value);
            try
            {
                _itemService.DeleteItem(id);
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}');</script>");
            }
            BindGrid();
        }

        protected void dvInsert_ItemInserting(object sender, DetailsViewInsertEventArgs e)
        {
            var txtName = (TextBox)dvInsert.FindControl("txtNewName");
            var txtUnitPrice = (TextBox)dvInsert.FindControl("txtNewUnitPrice");
            var txtOnHand = (TextBox)dvInsert.FindControl("txtNewOnHand");

            var item = new Item
            {
                Name = txtName.Text,
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text),
                OnHandQuantity = Convert.ToInt32(txtOnHand.Text)
            };

            try
            {
                _itemService.CreateItem(item);
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}');</script>");
                e.Cancel = true;
                return;
            }

            BindGrid();
            e.Cancel = true;
        }

        protected void dvInsert_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            BindGrid();
        }

        protected void dvInsert_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            DetailsView dv = (DetailsView)sender;
            e.Cancel = true;

            TextBox txtName = (TextBox)dv.FindControl("txtNewName");
            TextBox txtUnitPrice = (TextBox)dv.FindControl("txtNewUnitPrice");
            TextBox txtOnHand = (TextBox)dv.FindControl("txtNewOnHand");

            if (txtName != null) txtName.Text = "";
            if (txtUnitPrice != null) txtUnitPrice.Text = "";
            if (txtOnHand != null) txtOnHand.Text = "";
        }
    }
}