<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmHosts.aspx.cs" Inherits="AppUlacitBnB.Views.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .search-input {
            margin: 40px 0 20px
        }
    </style>
    <script>
        $(document).ready(function () {
            $("#MainContent_txtSearch").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvHosts tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
    <h1>Hosts List</h1>
    <asp:TextBox ID="txtSearch" Placeholder="Search" runat="server" CssClass="search-input form-control" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
    <asp:GridView ID="gvHosts" runat="server" AutoGenerateColumns="false" cssClass="table table-sm" HeaderStyle-BackColor="black" HeaderStyle-ForeColor="gray">
        <Columns>
            <asp:BoundField  HeaderText="Name" DataField="Name"/>
            <asp:BoundField  HeaderText="LastName" DataField="Name"/>
            <asp:BoundField  HeaderText="Description" DataField="Description"/>
            <asp:BoundField  HeaderText="Status" DataField="Status"/>
        </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="lblStatus" ForeColor="Maroon" Visible="false"></asp:Label>
</asp:Content>
