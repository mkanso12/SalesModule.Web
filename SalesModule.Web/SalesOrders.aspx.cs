using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Unity;
using SalesModule.BusinessLogic;
using SalesModule.DataAccess;
using SalesModule.Web.Model;

namespace SalesModule.Web
{
    public partial class SalesOrders : System.Web.UI.Page
    {
        private ISalesOrderService _orderService;
        private ICustomerService _customerService;
        private IItemService _itemService;

        protected void Page_Load(object sender, EventArgs e)
        {
            var container = (IUnityContainer)Application["UnityContainer"];
            _orderService = container.Resolve<ISalesOrderService>();
            _customerService = container.Resolve<ICustomerService>();
            _itemService = container.Resolve<IItemService>();

            if (!IsPostBack)
            {
                LoadCustomers();
                LoadItems();
                BindOrdersGrid();
                Session["OrderLines"] = new List<OrderLineDto>(); // DTO list
                BindLinesGrid();
            }
        }

        private void LoadCustomers()
        {
            try
            {
                var customers = _customerService.GetAllCustomers();
                ddlCustomer.DataSource = customers;
                ddlCustomer.DataTextField = "Name";
                ddlCustomer.DataValueField = "Id";
                ddlCustomer.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading customers: " + ex.Message;
            }
        }

        private void LoadItems()
        {
            try
            {
                var items = _itemService.GetAllItems();
                ddlNewItem.DataSource = items;
                ddlNewItem.DataTextField = "Name";
                ddlNewItem.DataValueField = "Id";
                ddlNewItem.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error loading items: " + ex.Message;
            }
        }

        private void BindOrdersGrid()
        {
            var orders = _orderService.GetAllOrders();
            gvOrders.DataSource = orders;
            gvOrders.DataBind();
        }

        private void BindLinesGrid()
        {
            var lines = Session["OrderLines"] as List<OrderLineDto> ?? new List<OrderLineDto>();
            gvLines.DataSource = lines;
            gvLines.DataBind();
            UpdateNetTotal();
        }

        protected void ddlNewItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId = Convert.ToInt32(ddlNewItem.SelectedValue);
            if (itemId > 0)
            {
                var item = _itemService.GetItem(itemId);
                if (item != null)
                {
                    txtNewPrice.Text = item.UnitPrice.ToString("F2");
                }
            }
        }

        protected void btnAddLine_Click(object sender, EventArgs e)
        {
            int itemId = Convert.ToInt32(ddlNewItem.SelectedValue);
            if (itemId <= 0)
            {
                lblMessage.Text = "Please select an item.";
                return;
            }

            if (!int.TryParse(txtNewQty.Text, out int qty) || qty <= 0)
            {
                lblMessage.Text = "Quantity must be a positive integer.";
                return;
            }

            if (!decimal.TryParse(txtNewPrice.Text, out decimal price) || price < 0)
            {
                lblMessage.Text = "Unit price must be a valid non‑negative number.";
                return;
            }

            var item = _itemService.GetItem(itemId);
            if (item == null)
            {
                lblMessage.Text = "Invalid item.";
                return;
            }

            var dto = new OrderLineDto
            {
                ItemId = itemId,
                ItemName = item.Name,
                Qty = qty,
                UnitPrice = price
            };

            var lines = Session["OrderLines"] as List<OrderLineDto> ?? new List<OrderLineDto>();
            lines.Add(dto);
            Session["OrderLines"] = lines;

            // Clear inputs
            txtNewQty.Text = "";
            txtNewPrice.Text = "";
            ddlNewItem.SelectedIndex = 0;

            BindLinesGrid();
            lblMessage.Text = "Line added successfully.";
        }

        protected void gvLines_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var lineId = (Guid)gvLines.DataKeys[e.RowIndex].Value;
            var lines = Session["OrderLines"] as List<OrderLineDto> ?? new List<OrderLineDto>();
            var line = lines.FirstOrDefault(l => l.LineId == lineId);
            if (line != null)
            {
                lines.Remove(line);
                Session["OrderLines"] = lines;
            }
            BindLinesGrid();
        }

        protected void btnSaveOrder_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            int customerId = Convert.ToInt32(ddlCustomer.SelectedValue);
            if (customerId <= 0)
            {
                lblMessage.Text = "Please select a customer.";
                return;
            }

            var dtoLines = Session["OrderLines"] as List<OrderLineDto> ?? new List<OrderLineDto>();
            if (dtoLines.Count == 0)
            {
                lblMessage.Text = "Please add at least one line item.";
                return;
            }

            // Convert DTOs to domain entities (no navigation properties)
            var domainLines = dtoLines.Select(d => new SalesOrderLine
            {
                ItemId = d.ItemId,
                Qty = d.Qty,
                UnitPrice = d.UnitPrice
            }).ToList();

            var order = new SalesOrder
            {
                CustomerId = customerId
            };

            try
            {
                _orderService.CreateOrder(order, domainLines);
                lblMessage.Text = "Order created successfully!";
                ddlCustomer.SelectedIndex = 0;
                Session["OrderLines"] = new List<OrderLineDto>();
                BindLinesGrid();
                BindOrdersGrid();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlCustomer.SelectedIndex = 0;
            Session["OrderLines"] = new List<OrderLineDto>();
            BindLinesGrid();
            lblMessage.Text = "";
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteOrder")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                try
                {
                    _orderService.DeleteOrder(id);
                    BindOrdersGrid();
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Delete error: " + ex.Message;
                }
            }
        }
        private void UpdateNetTotal() {
        var lines = Session["OrderLines"] as List<OrderLineDto> ?? new List<OrderLineDto>();
            var netTotal = lines.Sum(x => x.Qty * x.UnitPrice);
            lblNetTotal.Text = netTotal.ToString();
        }
    }
}