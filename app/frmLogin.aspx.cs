using AppUlacitBnB.Controllers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ulacitbnb.Models;

namespace AppUlacitBnB
{
    public partial class frmLogin : System.Web.UI.Page
    {
        protected async void loginCustomer(LoginRequest loginRequest)
        {
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

        protected async void loginHost(LoginRequest loginRequest)
        {
            HostManager hostManager = new HostManager();
            Host host = await hostManager.Validate(loginRequest);

            if (host != null)
            {
                JwtSecurityToken jwtSecurityToken;
                var jwtHandler = new JwtSecurityTokenHandler();
                jwtSecurityToken = jwtHandler.ReadJwtToken(host.Token);
                Session["UserID"] = host.ID;
                Session["Token"] = host.Token;
                FormsAuthentication.RedirectFromLoginPage(host.Name, false);
            }
            else
            {
                lblError.Text = "Invalid Credentials";
                lblError.Visible = true;
            }
        }

        protected async void btnIngresar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    LoginRequest loginRequest = new LoginRequest()
                    { Username = txtUsername.Text, Password = txtPassword.Text };
                    
                    if (cbIsHostUser.Checked)
                    {
                        loginHost(loginRequest);
                    }
                    else
                    {
                        loginCustomer(loginRequest);
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