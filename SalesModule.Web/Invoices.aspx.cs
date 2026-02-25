using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;
using SalesModule.BusinessLogic;
using SalesModule.DataAccess;

namespace SalesModule.Web
{
    public partial class Invoices : System.Web.UI.Page
    {
        private IInvoiceService _invoiceService;
        private ISalesOrderService _salesOrderService;

        protected void Page_Load(object sender, EventArgs e)
        {
            var container = (IUnityContainer)Application["UnityContainer"];
            _invoiceService = container.Resolve<IInvoiceService>();
            _salesOrderService = container.Resolve<ISalesOrderService>();

            if (!IsPostBack)
            {
                BindInvoicesGrid();
                BindOrdersDropdown();
            }
        }

        private void BindInvoicesGrid()
        {
            var invoices = _invoiceService.GetAllInvoices();
            gvInvoices.DataSource = invoices;
            gvInvoices.DataBind();
        }

        private void BindOrdersDropdown()
        {
            var list = _salesOrderService.GetOrdersAvailableForInvoiceDisplay();
            ddlOrders.DataSource = list;
            ddlOrders.DataTextField = "DisplayText";
            ddlOrders.DataValueField = "Id";
            ddlOrders.DataBind();
            ddlOrders.Items.Insert(0, new ListItem("-- Select Order --", "0"));
        }

        protected void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            int orderId = Convert.ToInt32(ddlOrders.SelectedValue);
            if (orderId <= 0)
            {
                lblMessage.Text = "Please select an order.";
                return;
            }

            try
            {
                var invoice = _invoiceService.CreateInvoiceFromOrder(orderId);
                lblMessage.Text = $"Invoice #{invoice.Id} created successfully.";
                BindInvoicesGrid();
                BindOrdersDropdown();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void gvInvoices_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Post")
            {
                int invoiceId = Convert.ToInt32(e.CommandArgument);
                try
                {
                    _invoiceService.PostInvoice(invoiceId);
                    lblMessage.Text = "Invoice posted successfully.";
                    BindInvoicesGrid();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error: " + ex.Message;
                }
            }
        }
    }
}