<%@ Page Title="Invoices" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Invoices.aspx.cs" Inherits="SalesModule.Web.Invoices" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">Invoices</h2>

        <asp:GridView ID="gvInvoices" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="table table-striped table-bordered table-hover"
            OnRowCommand="gvInvoices_RowCommand"
            EmptyDataText="No invoices found.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Invoice #" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <%# Eval("Customer.Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:BoundField DataField="GrossTotal" HeaderText="Total" DataFormatString="{0:F2}" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnPost" runat="server" CommandName="Post" CommandArgument='<%# Eval("Id") %>'
                            CssClass='<%# Eval("Status").ToString() == "Open" ? "btn btn-sm btn-primary" : "btn btn-sm btn-secondary disabled" %>'
                            Enabled='<%# Eval("Status").ToString() == "Open" %>'>
                            <i class="bi bi-send"></i> Post
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <hr class="my-4" />

        <div class="card">
            <div class="card-header bg-success text-white">
                <h5 class="mb-0">Create Invoice from Sales Order</h5>
            </div>
            <div class="card-body">
                <div class="row">
                    <div class="col-md-4">
                        <label>Select Order</label>
                        <asp:DropDownList ID="ddlOrders" runat="server" CssClass="form-select" DataTextField="DisplayText" DataValueField="Id">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 align-self-end">
                        <asp:Button ID="btnCreateInvoice" runat="server" Text="Create Invoice" CssClass="btn btn-success" OnClick="btnCreateInvoice_Click" />
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="mt-2 d-block"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>