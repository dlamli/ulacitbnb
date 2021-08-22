<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmHosts.aspx.cs" Inherits="AppUlacitBnB.Views.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .search-input {
            margin: 40px 0 20px
        }
    </style>
    <script>
        function openModal() {
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }

        function openAlertModal() {
            $('#alertModal').modal('show');
        }

        function closeAlertModal() {
            $('#alertModal').modal('hide');
        }

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
    <asp:TextBox ID="txtSearch" Placeholder="Search" runat="server" CssClass="search-input form-control"></asp:TextBox>
    <asp:GridView ID="gvHosts" runat="server" OnRowCommand="gvHosts_RowCommand" AutoGenerateColumns="false" cssClass="table table-sm" HeaderStyle-BackColor="black" HeaderStyle-ForeColor="gray">
        <Columns>
             <asp:BoundField  HeaderText="ID" DataField="ID"/>
            <asp:BoundField  HeaderText="Name" DataField="Name"/>
            <asp:BoundField  HeaderText="Last Name" DataField="LastName"/>
            <asp:BoundField  HeaderText="Password" DataField="Password"/>
            <asp:BoundField  HeaderText="Description" DataField="Description"/>
            <asp:BoundField  HeaderText="Status" DataField="Status"/>
            <asp:ButtonField HeaderText="Delete" Text="Delete" CommandName="delete_action" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
    </asp:GridView>
    <asp:Label runat="server" ID="lblStatus" ForeColor="Maroon" Visible="false"></asp:Label>

         <!-- Modal Window -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismss="modal" text="text" runat="server">&times;</button>
                        <h4 class="modal-title">Delete this review?</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <asp:Literal ID="ltrMyModalMessage" runat="server" />
                            <asp:Label ID="lblDeleteCode" runat="server" Visible="false" />
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="btnAcceptModal" runat="server" CssClass="btn btn-default rounded" OnClick="btnAcceptModal_Click"
                            Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Ok" />
                        <asp:LinkButton ID="btnCancelModal" runat="server" CssClass="btn btn-danger rounded" OnClick="btnCancelModal_Click"
                            Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cancel" />
                    </div>
                </div>
            </div>
        </div>

       <div id="alertModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismss="modal" text="text" runat="server">&times;</button>
                        <h4 class="modal-title"> <asp:Literal ID="ltrAlertModalHeader" runat="server"/></h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <asp:Literal ID="ltrAlertModalMsg" runat="server"/>
                        </p>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="btnAlertModalOk" runat="server" CssClass="btn btn-default rounded" OnClick="btnAlertModalOk_Click"
                             Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Ok" />
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
