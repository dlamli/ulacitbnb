<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmHotel.aspx.cs" Inherits="AppReservasULACIT.Views.frmHotel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function openModal() {
            $('#myModal').modal('show'); //ventana de mensajes
        }

        function openModalMantenimiento() {
            $('#myModalMantenimiento').modal('show'); //ventana de mantenimiento
        }

        function CloseModal() {
            $('#myModal').modal('hide');//cierra ventana de mensajes
        }

        function CloseMantenimiento() {
            $('#myModalMantenimiento').modal('hide'); //cierra ventana de mantenimiento
        }

        $(document).ready(function () { //filtrar el datagridview
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvHoteles tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>

    <h1>Customer Management</h1>
    <div class="container">
        <input id="myInput" placeholder="Buscar" class="form-control" type="text" />
        <asp:GridView ID="gvHoteles" OnRowCommand="gvHoteles_RowCommand" runat="server" AutoGenerateColumns="false"
            CssClass="table table-striped" AlternatingRowStyle-BackColor="LightBlue" HeaderStyle-BackColor="Navy"
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="CustomerID" DataField="ID" />
                <asp:BoundField HeaderText="Nombre" DataField="Name" />
                <asp:BoundField HeaderText="Email" DataField="Email" />
                <asp:BoundField HeaderText="Password" DataField="Password" />
                <asp:BoundField HeaderText="Phone" DataField="Phone" />
                <asp:BoundField HeaderText="Status" DataField="Status" />
                <asp:ButtonField HeaderText="Modificar" Text="Modificar" CommandName="Modificar"
                    ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-primary" />
                <asp:ButtonField HeaderText="Eliminar" Text="Eliminar" CommandName="Eliminar"
                    ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="btn btn-danger" />
            </Columns>
        </asp:GridView>
        <asp:LinkButton type="button" OnClick="btnNuevo_Click" CssClass="btn btn-success" ID="btnNuevo" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> Nuevo" />
        <br />
        <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />
    </div>
    <!--VENTANA MODAL -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Customer Management</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Literal ID="ltrModalMensaje" runat="server" /><asp:Label ID="lblCodigoEliminar" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnAceptarModal" runat="server" OnClick="btnAceptarModal_Click" type="button"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" CssClass="btn btn-success" />

                    <asp:LinkButton ID="btnCancelarModal" runat="server" OnClick="btnCancelarModal_Click" type="button"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cancelar" CssClass="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
    <!--VENTANA DE MANTENIMIENTO-->
    <div id="myModalMantenimiento" class="modal fade" role="dialog">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">
                        <asp:Literal ID="ltrTituloMantenimiento" runat="server" /></h4>
                </div>
                <div class="modal-body">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <asp:Literal ID="ltrCodigoMant" Text="Codigo" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtCodigoMant" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrNombreMant" Text="Nombre" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtNombreMant" runat="server" CssClass="form-control"></asp:TextBox></td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                                    ErrorMessage="El nombre del hotel es requerido" ControlToValidate="txtNombreMant" EnableClientScript="False"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrEmail" Text="Email" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtEmailMant" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrDireccion" Text="Direccion" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtDireccionMant" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrTelefono" Text="Telefono" runat="server"></asp:Literal></td>
                            <td>
                                <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrCategoria" Text="Categoria" runat="server" /></td>
                            <td>
                                <asp:DropDownList ID="ddlCategoria" CssClass="form-control" runat="server">
                                    <asp:ListItem Selected="True" Value="Active">Actice</asp:ListItem>
                                    <asp:ListItem Value="Inactive">Inactive</asp:ListItem>
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                    <asp:Label ID="lblResultado" ForeColor="Maroon" Visible="false" runat="server" />
                </div>
                <div class="modal-footer">
                    <asp:LinkButton type="button" ID="btnAceptarMant" runat="server" OnClick="btnAceptarMant_Click"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-ok'></span> Aceptar" CssClass="btn btn-success" />

                    <asp:LinkButton type="button" ID="btnCancelarMant" runat="server" OnClick="btnCancelarMant_Click"
                        Text="<span aria-hidden='true' class='glyphicon glyphicon-remove'></span> Cancelar" CssClass="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
