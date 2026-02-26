<%@ Page Title="GL Transactions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GLTransactions.aspx.cs" Inherits="SalesModule.Web.GLTransactions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <h2 class="mb-3">General Ledger Transactions</h2>

        <asp:GridView ID="gvGLTransactions" runat="server" AutoGenerateColumns="False" DataKeyNames="Id"
            CssClass="table table-striped table-bordered table-hover"
            OnRowDataBound="gvGLTransactions_RowDataBound"
            EmptyDataText="No GL transactions found.">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="GL #" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd HH:mm}" HeaderStyle-CssClass="bg-primary text-white" />
                <asp:TemplateField HeaderText="Lines" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:GridView ID="gvLines" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-bordered"
                            ShowHeader="True" GridLines="None" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="Account" HeaderText="Account" />
                                <asp:BoundField DataField="Debit" HeaderText="Debit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="Credit" HeaderText="Credit" DataFormatString="{0:F2}" ItemStyle-HorizontalAlign="Right" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total" HeaderStyle-CssClass="bg-primary text-white">
                    <ItemTemplate>
                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>