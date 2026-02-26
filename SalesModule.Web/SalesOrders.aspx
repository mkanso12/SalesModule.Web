<%@ Page Title="Sales Orders" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SalesOrders.aspx.cs" Inherits="SalesModule.Web.SalesOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">Sales Orders</h2>

        <asp:GridView ID="gvOrders" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="table table-striped table-bordered table-hover"
            OnRowCommand="gvOrders_RowCommand"
            EmptyDataText="No orders found.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Order #" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:TemplateField HeaderText="Customer" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <%# Eval("Customer.Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnView" runat="server" CommandName="View" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-info" ToolTip="View/Edit">
                            <i class="bi bi-eye"></i> View
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteOrder" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-sm btn-danger"
                            OnClientClick="return confirm('Are you sure you want to delete this order?');" ToolTip="Delete">
                            <i class="bi bi-trash"></i> Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <hr class="my-4" />

        <div class="card">
            <div class="card-header bg-success text-white">
                <h5 class="mb-0">Create New Order</h5>
            </div>
            <div class="card-body">
                <asp:HiddenField ID="hfOrderId" runat="server" Value="0" />
                <div class="row mb-3">
                    <label class="col-sm-2 col-form-label">Customer</label>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-select" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Select Customer --" Value="0" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="ddlCustomer" InitialValue="0"
                            ErrorMessage="Customer is required." CssClass="text-danger" Display="Dynamic" />
                    </div>
                </div>

                <h5>Order Lines</h5>
                <asp:GridView ID="gvLines" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="LineId"
                    CssClass="table table-sm table-bordered"
                    OnRowDeleting="gvLines_RowDeleting"
                    ShowHeaderWhenEmpty="True"
                    EmptyDataText="No lines added yet.">
                    <Columns>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <%# Eval("ItemName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("UnitPrice", "{0:F2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <%# Eval("Total", "{0:F2}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnRemoveLine" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-danger"
                                    OnClientClick="return confirm('Remove this line?');">
                    <i class="bi bi-trash"></i>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <!-- External Add Line controls -->
                <div class="row mt-3 align-items-end">
                    <div class="col-md-3">
                        <label>Item</label>
                        <asp:DropDownList ID="ddlNewItem" runat="server" CssClass="form-select"
                            DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlNewItem_SelectedIndexChanged">
                            <asp:ListItem Text="-- Select Item --" Value="0" />
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label>Quantity</label>
                        <asp:TextBox ID="txtNewQty" runat="server" CssClass="form-control"
                            TextMode="Number" step="1" min="1"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <label>Unit Price</label>
                        <asp:TextBox ID="txtNewPrice" runat="server" CssClass="form-control"
                            TextMode="Number" step="0.01"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:LinkButton ID="btnAddLine" runat="server" CssClass="btn btn-success" OnClick="btnAddLine_Click">
                            <i class="bi bi-plus"></i> Add Line
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-2">
                          <label>net total</label>
                         <asp:Label ID="lblNetTotal" runat="server" CssClass="mt-2 d-block" Text ="0.0"></asp:Label>
                    </div>
                </div>

                <div class="mt-3">
                    <asp:Button ID="btnSaveOrder" runat="server" Text="Save Order" CssClass="btn btn-primary" OnClick="btnSaveOrder_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-secondary" OnClick="btnCancel_Click" CausesValidation="false" />
                </div>
                <asp:Label ID="lblMessage" runat="server" CssClass="mt-2 d-block"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
