<%@ Page Async="true" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="frmPayment.aspx.cs" Inherits="AppUlacitBnB.Views.frmPayment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript">

        function openModal() {
            $('#myModal').modal('show'); //message window
        }

        function openManagement() {
            $('#myModalManagement').modal('show'); //management window
        }

        function closeModal() {
            $('#myModal').modal('hide'); //message window close
        }

        function closeManagement() {
            $('#myModalManagement').modal('hide'); //management window close
        }

        $(document).ready(function () {
            $("#myInput").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $("#MainContent_gvPayments tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });
        });
    </script>

    <H1>Payment Management</H1>
    <div class="container">
        <input id="myInput"¨Placeholder="Search" class="form-control" type="text" />
        <asp:GridView ID="gvPayments" OnRowCommand="gvPayments_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered" 
            AlternatingRowStyle-BackColor="LightGray" HeaderStyle-BackColor="LightBlue" 
            HeaderStyle-ForeColor="White" Width="100%">
            <Columns>
                <asp:BoundField HeaderText="Payment Code" DataField="Pay_ID" />
                <asp:BoundField HeaderText="Card Brand" DataField="Pay_Brand" />
                <asp:BoundField HeaderText="Type" DataField="Pay_Type" />
                <asp:BoundField HeaderText="Modality" DataField="Pay_Modality" />
                <asp:BoundField HeaderText="Date" DataField="Pay_Date" />
                <asp:BoundField HeaderText="Amount" DataField="Pay_Amount" ItemStyle-HorizontalAlign="Right"/>
                <asp:BoundField HeaderText="Taxes" DataField="Pay_Taxes" ItemStyle-HorizontalAlign="Right"/>
                <asp:BoundField HeaderText="Total" DataField="Pay_Total" ItemStyle-HorizontalAlign="Right"/>
                <asp:ButtonField HeaderText="Modify" Text="Modify" CommandName="ModifyPayment" ItemStyle-HorizontalAlign="Center" 
                    ControlStyle-CssClass="btn btn-primary"/>
                <asp:ButtonField HeaderText="Delete" Text="Delete" CommandName="DeletePayment" ItemStyle-HorizontalAlign="Center" 
                    ControlStyle-CssClass="btn btn-danger"/>
            </Columns>
        </asp:GridView>
        <asp:LinkButton type="button" OnClick="btnNew_Click" CssClass="btn btn-success" ID="btnNew" runat="server" Text="<span aria-hidden='true' class='glyphicon glyphicon-floppy-disk'></span> New Payment"/>
        <br />
        <asp:Label ID="lblStatus" ForeColor="Maroon" runat="server" Visible="false" />
    </div>
    <!-- MODAL WINDOW -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button> 
                    <h4 class="modal-title">Payment Management</h4>
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
                                <asp:Literal ID="ltrMantCode" Text="PaymentID" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantCode" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantBrand" Text="Brand" runat="server"></asp:Literal>
                            
                            <td>
                                <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control">
                                    <asp:ListItem Selected="True">Visa</asp:ListItem>
                                    <asp:ListItem>Mastercard</asp:ListItem>
                                    <asp:ListItem>American Express</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantType" Text="Type" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantType" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvType" runat="server" 
                                    ErrorMessage="Type is required" ControlToValidate="txtMantType" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantModality" Text="Modality" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantModality" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvModality" runat="server" 
                                    ErrorMessage="Modality is required" ControlToValidate="txtMantModality" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantDate" Text="Date" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantDate" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvDate" runat="server" 
                                    ErrorMessage="A date is required" ControlToValidate="txtMantDate" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantAmount" Text="Amount" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantAmount" runat="server" CssClass="form-control" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" 
                                    ErrorMessage="Amount is required" ControlToValidate="txtMantAmount" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantTaxes" Text="Taxes" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantTaxes" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvTaxes" runat="server" 
                                    ErrorMessage="Taxes are required" ControlToValidate="txtMantTaxes" EnableClientScript="false"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="ltrMantTotal" Text="Total" runat="server"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txtMantTotal" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RequiredFieldValidator ID="rfvTotal" runat="server" 
                                    ErrorMessage="A total is required" ControlToValidate="txtMantTotal" EnableClientScript="false"></asp:RequiredFieldValidator>
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
