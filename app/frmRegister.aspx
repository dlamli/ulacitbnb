<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="frmRegister.aspx.cs" Inherits="AppUlacitBnB.frmRegistro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Register</title>
    <link rel="stylesheet" href="Content/register-style.css" />
</head>
<body>
    <div id="myModal" class="modal" runat="server">
        <form class="modal-content animate" runat="server">
            <div class="imgcontainer">
                &nbsp;<img src="img/img_avatar2.png" class="auto-style1" />
            </div>
            <div class="container">
                <h1>Registro</h1>
                <asp:TextBox ID="txtName" Placeholder="Enter the name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server"
                    ErrorMessage="Name is required" ControlToValidate="txtName" ForeColor="Maroon"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtLastName" Placeholder="Enter the last name" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                    ErrorMessage="Lastname is required" ControlToValidate="txtLastName" ForeColor="Maroon"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtIdentification" Placeholder="Enter identification" runat="server" MaxLength="9"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvIdentification" runat="server"
                    ErrorMessage="Identification is required" ControlToValidate="txtIdentification" ForeColor="Maroon"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtEmail" Placeholder="Enter an email" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ErrorMessage="Email is required" ControlToValidate="txtEmail" ForeColor="Maroon"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtBirthDate" Placeholder="Enter a birthdate" runat="server"></asp:TextBox>
                <asp:Button ID="btnBirthDate" OnClick="btnBirthDate_Click" runat="server" Text="Choose a date" CausesValidation="false" />
                <asp:Calendar ID="cldBirthDate" OnSelectionChanged="cldBirthDate_SelectionChanged" runat="server" Visible="false"></asp:Calendar>
                <asp:RequiredFieldValidator ID="rfvFechaNac" runat="server" ForeColor="Maroon"
                    ErrorMessage="Birthdate is required" ControlToValidate="txtBirthDate"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtPhone" Placeholder="Enter a phone number" MaxLength="8" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ForeColor="Maroon"
                    ErrorMessage="Phone number is required" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtPassword" Placeholder="Enter a password" TextMode="Password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ForeColor="Maroon"
                    ErrorMessage="Password is required" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                <asp:TextBox ID="txtConfirmPassword" Placeholder="Confirm password" TextMode="Password" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmarPassword" runat="server" ForeColor="Maroon"
                    ErrorMessage="Confirm password is required" ControlToValidate="txtConfirmPassword"></asp:RequiredFieldValidator>
                <br />
                <asp:CompareValidator ID="cvPassword" runat="server" ErrorMessage="Password must match"
                    ControlToValidate="txtPassword" ControlToCompare="txtConfirmPassword" ForeColor="Maroon"></asp:CompareValidator>
                <asp:Label ID="lblStatus" runat="server" Text="" Visible="false" ForeColor="Maroon"></asp:Label>
            </div>
            <div class="container">
                <asp:Button ID="btnConfirm" runat="server" Text="Register" CssClass="successbtn" OnClick="btnConfirm_Click" />
                <input type="reset" value="Clean" class="cancelbtn" />
            
            </div>
        </form>
    </div>
</body>
</html>
