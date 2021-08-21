<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmReservation.aspx.cs" Inherits="AppUlacitBnB.Views.frmReservation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <script type="text/javascript">

        function openModal() {
            $('#myModal').modal('show'); //message window
        }

        function closeModal() {
            $('#myModal').modal('hide'); //message window close
        }

        function openManagement() {
            $('#myModalManagement').modal('show'); //management window
        }

        function closeManagement() {
            $('#myModalManagement').modal('hide'); //management window close
        }

        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvPayment tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });

        </script>

    <H1>Reservation Management</H1>
    <div class="container">
        <input id="myInput"¨Placeholder="Search" class="form-control" type="text" />

        <asp:GridView ID="gvReservation" OnRowCommand="gvReservation_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" 
            AlternatingRowStyle-BackColor="LightGray" HeaderStyle-BackColor="LightBlue" 
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Reservation Code" DataField="Res_ID" />
                <asp:BoundField HeaderText="Start Date" DataField="Res_StartDate" />
                <asp:BoundField HeaderText="Reservation Date" DataField="Res_ReservationDate" />
                <asp:BoundField HeaderText="End Date" DataField="Res_EndDate" />
                <asp:BoundField HeaderText="Status" DataField="Res_Status" />
                <asp:BoundField HeaderText="Quantity" DataField="Res_Quantity" ItemStyle-HorizontalAlign="Right"/>
                <asp:BoundField HeaderText="Resolution Date" DataField="Res_ResolutionDate" ItemStyle-HorizontalAlign="Right"/>
                <asp:BoundField HeaderText="Payment ID" DataField="Res_PaymentID" ItemStyle-HorizontalAlign="Right"/>
                <asp:BoundField HeaderText="Customer ID" DataField="Cus_ID" ItemStyle-HorizontalAlign="Right"/>
                <asp:BoundField HeaderText="Room ID" DataField="Roo_ID" ItemStyle-HorizontalAlign="Right"/>
                <asp:ButtonField HeaderText="Modify" Text="Modify" CommandName="modifyReservation" ItemStyle-HorizontalAlign="Center" 
                    ControlStyle-CssClass="btn btn-primary"/>
                <asp:ButtonField HeaderText="Delete" Text="Delete" CommandName="deleteReservation" ItemStyle-HorizontalAlign="Center" 
                    ControlStyle-CssClass="btn btn-danger"/>
            </Columns>
        </asp:GridView>

        <asp:LinkButton type="button" OnClick="btnNew_Click" CssClass="btn btn-success" ID="btnNew" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> New Reservation"/>
        <br />

        <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />

    </div>
    <!-- MODAL WINDOW -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button> 
                    <h4 class="modal-title">Reservation Management</h4>
                </div>
                <div class="modal-body">
                    <p><asp:Literal id="ltrModalMessage" runat="server"/><asp:Label ID="lblDeleteCode" runat="server" /></p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnAcceptModal" runat="server" OnClick="btnAcceptModal_Click"   
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Accept" CssClass="btn btn-success"/>

                    <asp:LinkButton ID="btnCancelModal" runat="server" OnClick="btnCancelModal_Click"   
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Cancel" CssClass="btn btn-danger"/>
                </div>
            </div>
        </div>
    </div>

    <!-- MANAGEMENT WINDOW -->
    <div id="myModalManagement" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content" >
                <div class="modal-header">
                    <h4 class="modal-title"><asp:Literal ID="ltrManagementTitle" runat="server"/></h4>
                </div>
                <div class="modal-body">
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantCode" Text="ReservationID" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantStartDate" Text="StartDate" runat="server"></asp:Literal>
                            
                            <td>
                                <asp:TextBox ID="txtMantStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" 
                                    ErrorMessage="Start Date is required" ControlToValidate="txtMantStartDate" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantResDate" Text="ReservationDate" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantResDate" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvResDate" runat="server" 
                                    ErrorMessage="Reservation Date is required" ControlToValidate="txtMantResDate" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantEndDate" Text="EndDate" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" 
                                    ErrorMessage="End Date is required" ControlToValidate="txtMantEndDate" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantStatus" Text="Status" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True">Disponible</asp:ListItem>
                                    <asp:ListItem>Ocupado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantQuantity" Text="Quantity" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlQuantity" runat="server" CssClass="form-control">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem Selected="True">2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantResolutionDate" Text="ResolutionDate" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantResolutionDate" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvResolutionDate" runat="server" 
                                    ErrorMessage="Taxes are required" ControlToValidate="txtMantResolutionDate" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantPaymentID" Text="PaymentID" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantPaymentID" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvPaymentID" runat="server" 
                                    ErrorMessage="A total is required" ControlToValidate="txtMantPaymentID" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantCustID" Text="CustomerID" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantCustID" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvCustomerID" runat="server" 
                                    ErrorMessage="A total is required" ControlToValidate="txtMantCustID" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantRoomID" Text="RoomID" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantRoomID" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvRoomID" runat="server" 
                                    ErrorMessage="A total is required" ControlToValidate="txtMantRoomID" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResult" ForeColor="Maroon" Visible="false" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnMantAccept" runat="server" OnClick="btnMantAccept_Click"   
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Accept" CssClass="btn btn-success"/>

                    <asp:LinkButton ID="btnMantCancel" runat="server" OnClick="btnMantCancel_Click" 
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Cancel" CssClass="btn btn-danger"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
