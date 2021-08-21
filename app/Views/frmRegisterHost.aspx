<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="frmRegisterHost.aspx.cs" Inherits="AppUlacitBnB.Views.frmRegisterHost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Host</title>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/css/bootstrap.min.css" integrity="sha384-B0vP5xmATw1+K9KRQjQERJvTumQW0nPEzvF6L/Z6nronJ3oUOFUFpCjEUQouq2+l" crossorigin="anonymous"/>
    <style>
        body {
            height: 100vh;
            overflow: hidden;
            background: rgb(255,136,0);
            background: linear-gradient(10deg, rgba(255,136,0,1) 31%, rgba(255,29,0,0.8492530801383054) 90%);
        }
        .register-card {
            width: 530px;
            margin: 70px auto;
        }
    </style>
</head>
<body>
    <form id="registerForm" class="card register-card" runat="server">
        <div class="card-header text-center">Register Host</div>
        <div class="card-body">
            <div class="form-row">
                <div class="form-group col-6">
                    <asp:Label Text="Name" runat="server" />
                    <asp:TextBox ID="inputName" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Name is required" ControlToValidate="inputName" CssClass="invalid-feedback d-block"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group col-6">
                    <asp:Label Text="Name" runat="server" />
                    <asp:TextBox ID="inputLastName" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Last Name is required" ControlToValidate="inputLastName" CssClass="invalid-feedback d-block"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group col-6">
                    <asp:Label Text="Password" runat="server" />
                    <asp:TextBox ID="inputPassword" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password is required" ControlToValidate="inputPassword" CssClass="invalid-feedback d-block"></asp:RequiredFieldValidator>
                </div>
                <div class="form-group col-6">
                    <asp:Label Text="Confirm Password" runat="server" />
                    <asp:TextBox ID="confirmPasswordInput" CssClass="form-control" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password doesn't match" ControlToValidate="confirmPasswordInput" ControlToCompare="inputPassword" CssClass="invalid-feedback d-block"></asp:CompareValidator>
                </div>
                <div class="form-group col-12">
                    <asp:Label Text="Description" runat="server" />
                    <asp:TextBox id="inputDescription" TextMode="MultiLine" Height="60" MaxLength="100" CssClass="form-control" runat="server"/>
                </div>
                <div class="col-6">
                    <asp:Button ID="btnConfirm" Text="Confirm" CssClass="btn btn-success w-100" OnClick="Register" runat="server"/>
                </div>
                 <div class="col-6">
                    <input type="reset" value="Reset" class="btn btn-secondary w-100"/>
                </div>
            </div>
            <asp:Label ID="statusLabel" CssClass="invalid-feedback d-block text-center" Visible="false" runat="server" Text="Label"></asp:Label>
        </div>
    </form>
</body>
</html>
