<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmAccomodation.aspx.cs" Inherits="AppUlacitBnB.Views.frmAccomodation" %>

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
                $("#MainContent_gvAccomodation tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });

    </script>

    <h1>Accomodation Maintenance</h1>

    <div class="container">

        <div class="row">

            <div class="column" style="float: left; width: 40%; padding: 10px">
                <input id="myInput" placeholder="Buscar" class="form-control" type="text" />
            </div>
            <div class="column text-right" style="float: left; width: 60%; padding: 10px">
                <asp:LinkButton ID="btnNew" type="button" OnClick="btnNew_Click" CssClass="btn btn-success" runat="server">New Accomodation</asp:LinkButton>
            </div>

        </div>

        <br />

        <asp:GridView ID="gvAccomodation" runat="server" OnRowCommand="gvAccomodation_RowCommand" AutoGenerateColumns="false" CssClass="table table-striped" AlternatingRowStyle-BackColor="WhiteSmoke" HeaderStyle-BackColor="Black"
            HeaderStyle-ForeColor="Gray" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Accomodation ID" DataField="Acc_ID" />
                <asp:BoundField HeaderText="Name" DataField="Acc_Name" />
                <asp:BoundField HeaderText="Host ID" DataField="Hos_ID" />
                <asp:BoundField HeaderText="Country" DataField="Acc_Country" />
                <asp:BoundField HeaderText="State" DataField="Acc_State" />
                <asp:BoundField HeaderText="Zip Code" DataField="Acc_Zipcode" />
                <asp:BoundField HeaderText="Address" DataField="Acc_Address" />
                <asp:BoundField HeaderText="Description" DataField="Acc_Description" />
                <asp:BoundField HeaderText="Evaluation" DataField="Acc_Evaluation" />
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
                        <h4 class="modal-title">Delete this accomodation?</h4>
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
                                    <asp:Literal ID="ltrAcc_ID" Text="ID:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcc_ID" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrHos_ID" Text="Host:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHos_ID" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvHos_ID" runat="server" ErrorMessage="A host is required" ControlToValidate="txtHos_ID" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrAcc_Name" Text="Name:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcc_Name" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvtxtAcc_Name" runat="server" ErrorMessage="A name is required for this accomodation" ControlToValidate="txtAcc_Name" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrAcc_Country" text="Country:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcc_Country" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvtxtAcc_Country" runat="server" ErrorMessage="A country is required" Visible="false" ControlToValidate="txtAcc_Country" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrAcc_State" text="State:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcc_State" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="State is required" ControlToValidate="txtAcc_State" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrAcc_Zipcode" text="Zip Code:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcc_Zipcode" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrAcc_Address" text="Address:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcc_Address" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrAcc_Description" text="Description:" runat="server"></asp:Literal>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAcc_Description" runat="server" Enabled="true" CssClass="form-control"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="rfvtxtAcc_Description" runat="server" ErrorMessage="Description is required" ControlToValidate="txtAcc_Description" EnableClientScript="false"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="ltrAcc_Evaluation" Text="Evaluation:" runat="server"></asp:Literal>
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
