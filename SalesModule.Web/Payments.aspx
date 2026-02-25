<%@ Page Title="Payments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="SalesModule.Web.Payments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">Payments</h2>

        <asp:GridView ID="gvPayments" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="table table-striped table-bordered table-hover"
            EmptyDataText="No payments recorded.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Payment #" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <%# Eval("Customer.Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" DataFormatString="{0:F2}" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:TemplateField HeaderText="Invoice" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <%# Eval("Invoice.Id") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="bg-primary text-white" />
            </Columns>
        </asp:GridView>

        <hr class="my-4" />

        <div class="card">
            <div class="card-header bg-success text-white">
                <h5 class="mb-0">Record New Payment</h5>
            </div>
            <div class="card-body">
                <div class="row mb-3">
                    <label class="col-sm-2 col-form-label">Customer</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-select" DataTextField="Name" DataValueField="Id" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            <asp:ListItem Text="-- Select Customer --" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row mb-3">
                    <label class="col-sm-2 col-form-label">Invoice</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-select" DataTextField="DisplayText" DataValueField="Id">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row mb-3">
                    <label class="col-sm-2 col-form-label">Amount</label>
                    <div class="col-sm-4">
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" TextMode="Number" step="0.01"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-4 offset-sm-2">
                        <asp:Button ID="btnRecordPayment" runat="server" Text="Record Payment" CssClass="btn btn-primary" OnClick="btnRecordPayment_Click" />
                    </div>
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="mt-2 d-block"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>