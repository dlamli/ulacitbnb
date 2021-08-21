<%@ Page Title="" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmRoom.aspx.cs" Inherits="AppUlacitBnB.Views.frmRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

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

        $(document).ready(function () {//filter data grid view
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvRoom tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });

    </script>

     <h1>Room Maintenance</h1>

    <div class="container">

        <div class="row">

            <div class="column" style="float: left; width: 40%; padding: 10px">
                <input id="myInput" placeholder="Buscar" class="form-control" type="text" />
            </div>

            <div class="column text-right" style="float: left; width: 60%; padding: 10px">
                <asp:LinkButton ID="btnNew" type="button" OnClick="btnNew_Click" CssClass="btn btn-success" runat="server">New Room</asp:LinkButton>
            </div>

        </div>

         <br />

        <asp:GridView ID="gvRoom" runat="server" OnRowCommand="gvRoom_RowCommand" AutoGenerateColumns="false" CssClass="table table-sm" HeaderStyle-BackColor="DarkGray" HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Accomodation" DataField="Acc_ID" />
                <asp:BoundField HeaderText="Room ID" DataField="Roo_ID" />
                <asp:BoundField HeaderText="Price" DataField="Roo_Price" />
                <asp:BoundField HeaderText="Room Quantity" DataField="Roo_Quantity" />
                <asp:BoundField HeaderText="Room Type" DataField="Roo_Type" />
                <asp:BoundField HeaderText="Evaluation" DataField="Roo_Evaluation" />
                <asp:BoundField HeaderText="Bed Quantity" DataField="Roo_BedQuantity" />
                <asp:BoundField HeaderText="Service ID" DataField="Ser_ID" />
                <asp:BoundField HeaderText="Accomodation" DataField="Acc_ID" />
                <asp:ButtonField HeaderText="Modify" Text="Edit" CommandName="editt_action" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-default rounded" />
                <asp:ButtonField HeaderText="Delete" Text="Delete" CommandName="deletee_action" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger rounded" />
            </Columns>
        </asp:GridView>

        <br />

        <asp:Label ID="lblStatus" ForeColor="WhiteSmoke" runat="server" Visible="false" />

        <!-- Modal Window -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismss="modal" text="text" runat="server">&times;</button>
                        <h4 class="modal-title">Delete this room?</h4>
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
                                    <asp:Literal ID="ltrRoo_ID" Text="ID:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRoo_ID" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrAcc_ID" Text="Accomodation:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcc_ID" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvtxtAcc_ID" runat="server" ErrorMessage="An accomodation is required." ControlToValidate="txtAcc_ID" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrRoo_Price" Text="Price:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRoo_Price" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvtxtRoo_Price" runat="server" ErrorMessage="A price is required for this room." ControlToValidate="txtRoo_Price" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrRoo_Quantity" text="Room quantity:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRoo_Quantity" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvtxtRoo_Quantity" runat="server" ErrorMessage="A quantity is required." Visible="false" ControlToValidate="txtRoo_Quantity" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrRoo_Type" text="Room type:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRoo_Type" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvtxtRoo_Type" runat="server" ErrorMessage="Room type required." ControlToValidate="txtRoo_Type" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrRoo_BedQuantity" text="Bed quantity:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRoo_BedQuantity" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrSer_ID" text="Service:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSer_ID" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrRoo_Evaluation" Text="Evaluation:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:DropdownList ID="ddlEvaluation" runat="server" CssClass="form-control">
                                        <asp:ListItem Selected="True" Value="1">1 Star</asp:ListItem>
                                        <asp:ListItem Value="2">2 Stars</asp:ListItem>
                                        <asp:ListItem Value="3">3 Stars</asp:ListItem>
                                        <asp:ListItem Value="4">4 Stars</asp:ListItem>
                                        <asp:ListItem Value="5">5 Stars</asp:ListItem>
                                    </asp:DropdownList>
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

    </div>

</asp:Content>
