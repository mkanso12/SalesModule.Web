using System;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;
using SalesModule.BusinessLogic;
using SalesModule.DataAccess;

namespace SalesModule.Web
{
    public partial class Payments : System.Web.UI.Page
    {
        private IPaymentService _paymentService;
        private ICustomerService _customerService;
        private IInvoiceService _invoiceService;

        protected void Page_Load(object sender, EventArgs e)
        {
            var container = (IUnityContainer)Application["UnityContainer"];
            _paymentService = container.Resolve<IPaymentService>();
            _customerService = container.Resolve<ICustomerService>(); 
            _invoiceService = container.Resolve<IInvoiceService>();

            if (!IsPostBack)
            {
                LoadCustomers();
                BindPaymentsGrid();
            }
        }

        private void LoadCustomers()
        {
            var customers = _customerService.GetAllCustomers();
            ddlCustomer.DataSource = customers;
            ddlCustomer.DataTextField = "Name";
            ddlCustomer.DataValueField = "Id";
            ddlCustomer.DataBind();
        }

        private void BindPaymentsGrid()
        {
            var payments = _paymentService.GetAllPayments();
            gvPayments.DataSource = payments;
            gvPayments.DataBind();
        }

        protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(ddlCustomer.SelectedValue);
            if (customerId > 0)
            {
                var invoices = _invoiceService.GetPostedInvoicesByCustomer(customerId);
                var list = invoices.Select(i => new
                {
                    Id = i.Id,
                    DisplayText = $"Invoice #{i.Id} - Total: {i.GrossTotal:F2} (Status: {i.Status})"
                }).ToList();
                ddlInvoice.DataSource = list;
                ddlInvoice.DataBind();
                ddlInvoice.Items.Insert(0, new ListItem("-- Select Invoice --", "0"));
            }
            else
            {
                ddlInvoice.Items.Clear();
                ddlInvoice.Items.Insert(0, new ListItem("-- Select Invoice --", "0"));
            }
        }

        protected void btnRecordPayment_Click(object sender, EventArgs e)
        {
            lblMessage.Text = ""; 

            if (!int.TryParse(ddlCustomer.SelectedValue, out int customerId) || customerId <= 0)
            {
                lblMessage.Text = "Please select a customer.";
                return;
            }

            if (!int.TryParse(ddlInvoice.SelectedValue, out int invoiceId) || invoiceId <= 0)
            {
                lblMessage.Text = "Please select an invoice.";
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                lblMessage.Text = "Please enter a valid positive amount.";
                return;
            }
            var invoice = _invoiceService.GetInvoice(invoiceId); 

            if (invoice == null)
            {
                lblMessage.Text = "Selected invoice was not found.";
                return;
            }
            decimal invoiceTotal = invoice.GrossTotal;

            if (amount > invoiceTotal)
            {
                lblMessage.Text = $"Amount cannot be greater than invoice total ({invoiceTotal:F2}).";
                return;
            }

            try
            {
                _paymentService.CreatePayment(customerId, invoiceId, amount);

                lblMessage.Text = "Payment recorded successfully.";
                BindPaymentsGrid();
                ddlCustomer.SelectedIndex = 0;
                ddlInvoice.Items.Clear();
                txtAmount.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}