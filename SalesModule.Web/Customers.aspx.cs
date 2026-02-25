using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Unity;
using SalesModule.BusinessLogic;
using SalesModule.DataAccess;

namespace SalesModule.Web
{
    public partial class Customers : System.Web.UI.Page
    {
        private ICustomerService _customerService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Resolve service from Unity container stored in Application
            var container = (IUnityContainer)Application["UnityContainer"];
            _customerService = container.Resolve<ICustomerService>();

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var customers = _customerService.GetAllCustomers();
            gvCustomers.DataSource = customers;
            gvCustomers.DataBind();
        }

        protected void gvCustomers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCustomers.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvCustomers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCustomers.EditIndex = -1;
            BindGrid();
        }

        protected void gvCustomers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);
            var row = gvCustomers.Rows[e.RowIndex];
            var txtName = (TextBox)row.FindControl("txtName");
            string name = txtName.Text;

            var customer = new Customer { Id = id, Name = name };
            try
            {
                _customerService.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                // Show error message (you could add a label)
                Response.Write($"<script>alert('{ex.Message}');</script>");
            }

            gvCustomers.EditIndex = -1;
            BindGrid();
        }

        protected void gvCustomers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(gvCustomers.DataKeys[e.RowIndex].Value);
            try
            {
                _customerService.DeleteCustomer(id);
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
            string name = txtName.Text;
            var customer = new Customer { Name = name };
            try
            {
                _customerService.CreateCustomer(customer);
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex.Message}');</script>");
                e.Cancel = true; // prevent default insert
                return;
            }
            BindGrid();
            e.Cancel = true; // we manually inserted, so cancel default to avoid double insert
        }

        protected void dvInsert_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            // Refresh grid after insert
            BindGrid();
        }
        protected void dvInsert_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            // Cast sender to DetailsView
            DetailsView dv = (DetailsView)sender;

            // Prevent leaving Insert mode
            e.Cancel = true;

            // Clear the textbox
            TextBox txtName = (TextBox)dv.FindControl("txtNewName");
            if (txtName != null)
            {
                txtName.Text = "";
            }
        }
    }
}