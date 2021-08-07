using AppUlacitBnB.Models;
using AppUlacitBnB.Controllers;
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

                    CustomerManager customerManager = new CustomerManager();

                    Customer customer = new Customer();

                    customer = await customerManager.Validate(loginRequest);

                    if (customer != null)
                    {
                        JwtSecurityToken jwtSecurityToken;
                        var jwtHandler = new JwtSecurityTokenHandler();
                        jwtSecurityToken = jwtHandler.ReadJwtToken(customer.Token);

                        Session["UserID"] = customer.ID;
                        Session["Identification"] = customer.Identification;
                        Session["Name"] = customer.Name;
                        Session["Token"] = customer.Token;

                        FormsAuthentication.RedirectFromLoginPage(customer.Identification, false);

                    }
                    else
                    {
                        lblError.Text = "Invalid Credentials";
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