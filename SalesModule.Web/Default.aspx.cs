using SalesModule.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Unity;

namespace SalesModule.Web
{
    public partial class _Default : Page
    {
        private IInvoiceService _invoiceService;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Resolve the service from the container stored in Application
            var container = (IUnityContainer)Application["UnityContainer"];
            _invoiceService = container.Resolve<IInvoiceService>();
        }

        protected void btnPostTest_Click(object sender, EventArgs e)
        {
            try
            {
                _invoiceService.PostInvoice(5); // Assumes invoice ID 1 exists
                lblResult.Text = "Invoice posted successfully.";
            }
            catch (Exception ex)
            {
                lblResult.Text = "Error: " + ex.Message;
            }
        }
    }
}