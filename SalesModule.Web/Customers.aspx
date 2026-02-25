<%@ Page Title="Customers" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="SalesModule.Web.Customers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">Customers</h2>

        <!-- GridView with Bootstrap styling -->
        <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="table table-striped table-bordered table-hover"
            OnRowEditing="gvCustomers_RowEditing" OnRowCancelingEdit="gvCustomers_RowCancelingEdit"
            OnRowUpdating="gvCustomers_RowUpdating" OnRowDeleting="gvCustomers_RowDeleting"
            EmptyDataText="No customers found.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="True" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>' CssClass="form-control form-control-sm"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-warning me-1" ToolTip="Edit">
                            <i class="bi bi-pencil"></i> Edit
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-danger"
                            OnClientClick="return confirm('Are you sure you want to delete this customer?');" ToolTip="Delete">
                            <i class="bi bi-trash"></i> Delete
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success me-1" ToolTip="Save">
                            <i class="bi bi-check"></i> Save
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary" ToolTip="Cancel">
                            <i class="bi bi-x"></i> Cancel
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

        <hr class="my-4" />

        <h3 class="mb-3">Add New Customer</h3>
        <asp:DetailsView ID="dvInsert" runat="server" AutoGenerateRows="False" DefaultMode="Insert"
            OnItemInserting="dvInsert_ItemInserting" OnItemInserted="dvInsert_ItemInserted"
            OnModeChanging="dvInsert_ModeChanging"
            DataKeyNames="Id" CssClass="table table-bordered" GridLines="None">
            <Fields>
                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="bg-success text-white">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtNewName" runat="server" CssClass="form-control" placeholder="Enter customer name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtNewName"
                            ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowInsertButton="True" ButtonType="Button" ControlStyle-CssClass="btn btn-success mt-2" InsertText="Add Customer" />
            </Fields>
        </asp:DetailsView>
    </div>
</asp:Content>
