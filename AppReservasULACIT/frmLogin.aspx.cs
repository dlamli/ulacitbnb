using AppUlacitBnB.Controllers;
using AppUlacitBnB.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppUlacitBnB
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    LoginRequest loginRequest = new LoginRequest()
                    { Username = txtUsername.Text, Password = txtPassword.Text };

                    Authentication auth = new Authentication();

                    Customer customer = new Customer();

                    customer = await auth.Validar(loginRequest);

                    if (customer != null)
                    {
                        JwtSecurityToken jwtSecurityToken;
                        var jwtHandler = new JwtSecurityTokenHandler();
                        jwtSecurityToken = jwtHandler.ReadJwtToken(customer.Token);

                        Session["CodigoUsuario"] = customer.ID;
                        Session["Identificacion"] = customer.Identification;
                        Session["Nombre"] = customer.Name;
                        Session["Token"] = customer.Token;

                        FormsAuthentication.RedirectFromLoginPage(customer.Identification, false);

                    }
                    else
                    {
                        lblError.Text = "Credenciales invalidas";
                        lblError.Visible = true;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}