<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReview.aspx.cs" Inherits="AppUlacitBnB.Views.frmReview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
     <style>
        .search-input {
            margin: 40px 0 20px
        }
    </style>
    <script>
        $(function () {
            $('[id*=txtDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "mm/dd/yyyy",
                language: "tr"
            });
        });

        function openModal() {
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }

        function openModalMaintenance() {
            $('#myModalMaintenance').modal('show');
        }

        function closeModalMaintenance() {
            $('#myModalMaintenance').modal('hide');
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
                $("#MainContent_gvReviews tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>
    <h1>Reviews List</h1>
    <asp:TextBox ID="txtSearch" Placeholder="Search" runat="server" CssClass="search-input form-control"></asp:TextBox>
    <asp:GridView ID="gvReviews" runat="server" OnRowCommand="gvReviews_RowCommand" AutoGenerateColumns="false" cssClass="table table-sm" HeaderStyle-BackColor="black" HeaderStyle-ForeColor="gray">
        <Columns>
           <asp:BoundField  HeaderText="ID" DataField="ID"/>
            <asp:BoundField  HeaderText="Date" DataField="Date"/>
            <asp:BoundField  HeaderText="Rate" DataField="Rate"/>
            <asp:BoundField  HeaderText="Recommendation" DataField="Recommendation"/>
            <asp:BoundField  HeaderText="Comment" DataField="Comment"/>
                <asp:BoundField  HeaderText="Usefull" DataField="Usefull"/>
                <asp:BoundField  HeaderText="Title" DataField="Title"/>
                <asp:BoundField  HeaderText="CustomerID" DataField="CustomerID"/>
                <asp:BoundField  HeaderText="AccomodationID" DataField="AccomodationID"/>
             <asp:ButtonField HeaderText="Edit" Text="Edit" CommandName="editt_action" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-primary" />
            <asp:ButtonField HeaderText="Delete" Text="Delete" CommandName="delete_action" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
    </asp:GridView>
     <asp:LinkButton CssClass="btn btn-success" OnClick="btnNew_Click" runat="server" type="button" ID="btnNew" Text="<span aria-hidden='true' class='glyphicon glyphicon-plus'></span> New"></asp:LinkButton>
    <br />
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

     <!-- Modal Maintenance Window -->
        <div id="myModalMaintenance" class="modal fade" role="dialog">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismss="modal" text="text" runat="server">&times;</button>
                        <h4 class="modal-title">
                            <asp:Literal ID="ltrMaintenanceTitle" runat="server" />
                        </h4>
                    </div>
                    <div class="modal-body">
                        <table style="width: 100%; align-items:center; padding-top: 5px;" >
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrID" Text="ID:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtID" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                             <tr>
                               <td>
                                    <asp:Literal ID="ltrTitle" text="Title:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTitle" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrDate" Text="Date:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                     <asp:TextBox ID="txtDate" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrRate" Text="Rate:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:DropdownList ID="dpRate" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="1">1 Star</asp:ListItem>
                                        <asp:ListItem Value="2">2 Stars</asp:ListItem>
                                        <asp:ListItem Value="3">3 Stars</asp:ListItem>
                                        <asp:ListItem Value="4">4 Stars</asp:ListItem>
                                        <asp:ListItem Value="5">5 Stars</asp:ListItem>
                                    </asp:DropdownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrRecommendation" text="Recommendation:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbRecommendation" Enabled="true" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrComment" text="Comment:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtComment" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                               <td>
                                    <asp:Literal ID="ltrUsefull" text="Usefull:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUsefull" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                    
                                </td>
                            </tr>
                            <tr>
                               <td>
                                    <asp:Literal ID="ltrCustomerId" text="Customer ID:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCustomerId" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                       <asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator3" ControlToValidate="txtCustomerId"
                                    ForeColor="Red" runat="server" ErrorMessage="Only numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                             <tr>
                               <td>
                                    <asp:Literal ID="ltrAccomodationId" text="Accomodation ID:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccomodationID" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                      <asp:RegularExpressionValidator
                                    ID="RegularExpressionValidator2" ControlToValidate="txtAccomodationID"
                                    ForeColor="Red" runat="server" ErrorMessage="Only numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <asp:LinkButton ID="btnAcceptMaintModal" runat="server" CssClass="btn btn-default rounded" OnClick="btnAcceptMaintModal_Click"
                            Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Ok" />
                        <asp:LinkButton ID="btnCancelMaintModal" runat="server" CssClass="btn btn-danger rounded" OnClick="btnCancelMaintModal_Click"
                            Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cancel" />
                  
                    </div>
                </div>
            </div>
        </div>


     <!-- Alert Modal Window -->
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
