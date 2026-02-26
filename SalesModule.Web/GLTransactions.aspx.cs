using System;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;
using SalesModule.BusinessLogic;
using SalesModule.DataAccess;

namespace SalesModule.Web
{
    public partial class GLTransactions : System.Web.UI.Page
    {
        private IGLTransactionService _glService;

        protected void Page_Load(object sender, EventArgs e)
        {
            var container = (IUnityContainer)Application["UnityContainer"];
            _glService = container.Resolve<IGLTransactionService>();

            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var transactions = _glService.GetAllTransactions();
            gvGLTransactions.DataSource = transactions;
            gvGLTransactions.DataBind();
        }

        protected void gvGLTransactions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var transaction = (GLTransaction)e.Row.DataItem;
                var gvLines = (GridView)e.Row.FindControl("gvLines");
                if (gvLines != null)
                {
                    gvLines.DataSource = transaction.GLTransactionLines;
                    gvLines.DataBind();

                    decimal totalDebit = transaction.GLTransactionLines.Sum(l => l.Debit);
                    decimal totalCredit = transaction.GLTransactionLines.Sum(l => l.Credit);
                    var lblTotal = (Label)e.Row.FindControl("lblTotal");
                    if (lblTotal != null)
                    {
                        lblTotal.Text = $"Dr: {totalDebit:F2} / Cr: {totalCredit:F2}";
                        if (totalDebit != totalCredit)
                            lblTotal.CssClass = "text-danger fw-bold";
                        else
                            lblTotal.CssClass = "text-success";
                    }
                }
            }
        }
    }
}