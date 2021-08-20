<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmCustomer.aspx.cs" Inherits="AppUlacitBnB.Views.frmCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Bootstrap -->
    <!-- Bootstrap DatePicker -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/css/bootstrap-datepicker.css" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.6.4/js/bootstrap-datepicker.js" type="text/javascript"></script>
    <!-- Bootstrap DatePicker -->
    <script type="text/javascript">
        $(function () {
            $('[id*=txtBirthdateManagement]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "mm/dd/yyyy",
                language: "tr"
            });
        });
    </script>
    <script type="text/javascript">
        //Message window
        function openModal() {
            $('#myModal').modal('show');
        }
        //Management window
        function openManagement() {
            $('#myModalManagement').modal('show');
        }
        //Close message window
        function closeModal() {
            $('#myModal').modal('hide');
        }
        //Close management window
        function closeManagement() {
            $('#myModalManagement').modal('hide');
        }
        //Filter datagrid
        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvCustomer tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>

    <h1>Customer Management</h1>
    <div class="container">
        <input id="myInput" placeholder="Search" class="form-control" type="text" />
        <asp:GridView runat="server" ID="gvCustomer" OnRowCommand="gvCustomer_RowCommand" AutoGenerateColumns="false"
            CssClass="table table-striped" AlternatingRowStyle-BackColor="WhiteSmoke" HeaderStyle-BackColor="Black"
            HeaderStyle-ForeColor="Gray" Width="100%">

            <Columns>

                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="Name" DataField="Name" />
                <asp:BoundField HeaderText="LastName" DataField="LastName" />
                <asp:BoundField HeaderText="Identification" DataField="Identification" />
                <asp:BoundField HeaderText="Password" DataField="Password" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:BoundField HeaderText="BirthDate" DataField="BirthDate" />
                <asp:BoundField HeaderText="Phone" DataField="Phone" />
                <asp:ButtonField HeaderText="Update" Text="Update" CommandName="updateCustomer" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-primary" />
                <asp:ButtonField HeaderText="Delete" Text="Delete" CommandName="deleteCustomer" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" />

            </Columns>

        </asp:GridView>

        <asp:LinkButton runat="server" type="button" ID="btnNew" OnClick="btnNew_Click"
            Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo"></asp:LinkButton>
        <br />

        <asp:Label runat="server" ID="lblStatus" ForeColor="Maroon" Visible="false"></asp:Label>

    </div>

    <!--Modal Window-->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times</button>
                    <h4 class="modal-title">Customer Management</h4>
                </div>

                <div class="modal-body">
                    <p>
                        <asp:Literal runat="server" ID="ltrModalMessage"></asp:Literal>
                        <asp:Label runat="server" ID="lblIdDelete"></asp:Label>
                    </p>
                </div>

                <div class="modal-footer">
                    <asp:LinkButton runat="server" ID="btnAceptModal" OnClick="btnAceptModal_Click" type="button"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Confirm" CssClass="btn btn-success"></asp:LinkButton>
                    <asp:LinkButton ID="btnCancelModal" runat="server" OnClick="btnCancelModal_Click" type="button"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cancel" CssClass="btn btn-danger" />
                </div>

            </div>
        </div>
    </div>

    <!--Management Window-->
    <div id="myModalManagement" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Literal ID="ltrTitleManagement" runat="server" /></h4>
                </div>

                <div class="modal-body">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Literal ID="ltrIdManagement" Text="CustomerID" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdManagement" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrNameManagement" Text="Name" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNameManagement" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvName" runat="server"
                                    ErrorMessage="Name is required" ControlToValidate="txtNameManagement" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrLastNameManagement" Text="LastName" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtLastNameManagement" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                                    ErrorMessage="LastName is required" ControlToValidate="txtLastNameManagement" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrIdentification" Text="Identification" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtIdentificationManagement" MaxLength="8" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvIdentification" runat="server"
                                    ErrorMessage="Identification is required" ControlToValidate="txtIdentificationManagement" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrPassword" Text="Password" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtPasswordManagement" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                                    ErrorMessage="Password is required" ControlToValidate="txtPasswordManagement" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrEmail" Text="Email" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtEmailManagement" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                    ErrorMessage="Email is required" ControlToValidate="txtEmailManagement" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrStatus" Text="Status" runat="server" /></td>
                            <td>
                                <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="Active">Active</asp:ListItem>
                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrBirthdate" Text="Birthdate" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtBirthdateManagement" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvBirthdate" runat="server"
                                    ErrorMessage="Birthdate is required" ControlToValidate="txtBirthdateManagement" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrPhone" Text="Phone number" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtPhoneManagement" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvPhone" runat="server"
                                    ErrorMessage="Phone is required" ControlToValidate="txtPhoneManagement" EnableClientScript="False"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResult" ForeColor="Maroon" Visible="false" runat="server" />
                </div>

                <div class="modal-footer">
                    <asp:LinkButton type="button" ID="btnAceptManagement" runat="server" OnClick="btnAceptManagement_Click"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Confirm" CssClass="btn btn-success" />
                    <asp:LinkButton type="button" ID="btnCancelManagement" runat="server" OnClick="btnCancelManagement_Click"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cancel" CssClass="btn btn-danger" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>
