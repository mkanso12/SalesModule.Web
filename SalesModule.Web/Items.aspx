<%@ Page Title="Items" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Items.aspx.cs" Inherits="SalesModule.Web.Items" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">Items</h2>

        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="table table-striped table-bordered table-hover"
            OnRowEditing="gvItems_RowEditing" OnRowCancelingEdit="gvItems_RowCancelingEdit"
            OnRowUpdating="gvItems_RowUpdating" OnRowDeleting="gvItems_RowDeleting"
            EmptyDataText="No items found.">
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
                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitPrice", "{0:F2}") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUnitPrice" runat="server" Text='<%# Bind("UnitPrice") %>' CssClass="form-control form-control-sm" TextMode="Number" step="0.01"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="On Hand" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:Label ID="lblOnHand" runat="server" Text='<%# Eval("OnHandQuantity") %>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOnHand" runat="server" Text='<%# Bind("OnHandQuantity") %>' CssClass="form-control form-control-sm" TextMode="Number" step="1"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Actions" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-sm btn-warning me-1" ToolTip="Edit">
                            <i class="bi bi-pencil"></i> Edit
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-sm btn-danger"
                            OnClientClick="return confirm('Are you sure you want to delete this item?');" ToolTip="Delete">
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

        <h3 class="mb-3">Add New Item</h3>
        <asp:DetailsView ID="dvInsert" runat="server" AutoGenerateRows="False" DefaultMode="Insert"
            OnItemInserting="dvInsert_ItemInserting" OnItemInserted="dvInsert_ItemInserted"
            OnModeChanging="dvInsert_ModeChanging"
            DataKeyNames="Id" CssClass="table table-bordered" GridLines="None">
            <Fields>
                <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="bg-success text-white">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtNewName" runat="server" CssClass="form-control" placeholder="Enter item name"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtNewName"
                            ErrorMessage="Name is required." CssClass="text-danger" Display="Dynamic" />
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="bg-success text-white">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtNewUnitPrice" runat="server" CssClass="form-control" placeholder="0.00" TextMode="Number" step="0.01"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvUnitPrice" runat="server" ControlToValidate="txtNewUnitPrice"
                            ErrorMessage="Unit price is required." CssClass="text-danger" Display="Dynamic" />
                        <asp:RangeValidator ID="rvUnitPrice" runat="server" ControlToValidate="txtNewUnitPrice"
                            MinimumValue="0" MaximumValue="9999999" Type="Double" ErrorMessage="Price must be >= 0." CssClass="text-danger" Display="Dynamic" />
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="On Hand Quantity" HeaderStyle-CssClass="bg-success text-white">
                    <InsertItemTemplate>
                        <asp:TextBox ID="txtNewOnHand" runat="server" CssClass="form-control" placeholder="0" TextMode="Number" step="1"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvOnHand" runat="server" ControlToValidate="txtNewOnHand"
                            ErrorMessage="Quantity is required." CssClass="text-danger" Display="Dynamic" />
                        <asp:RangeValidator ID="rvOnHand" runat="server" ControlToValidate="txtNewOnHand"
                            MinimumValue="0" MaximumValue="9999999" Type="Integer" ErrorMessage="Quantity must be >= 0." CssClass="text-danger" Display="Dynamic" />
                    </InsertItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <InsertItemTemplate>
                        <asp:LinkButton ID="btnInsert" runat="server" CommandName="Insert" CssClass="btn btn-success">
                            <i class="bi bi-plus"></i> Add Item
                        </asp:LinkButton>
                    </InsertItemTemplate>
                </asp:TemplateField>
            </Fields>
        </asp:DetailsView>
    </div>
</asp:Content>