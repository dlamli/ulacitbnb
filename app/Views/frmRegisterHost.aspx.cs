using AppUlacitBnB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ulacitbnb.Models;

namespace AppUlacitBnB.Views
{
    public partial class frmRegisterHost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected async void Register(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    HostManager hostManager = new HostManager();
                    Host host = new Host()
                    {
                        Name = inputName.Text,
                        LastName = inputLastName.Text,
                        Password = inputPassword.Text,
                        Description = inputDescription.Text,
                        Status = "active"
                    };
                    await hostManager.Register(host);
                    if (!string.IsNullOrEmpty(host.Name))
                    {
                        Response.Redirect("~/frmLogin.aspx");
                    }
                    else
                    {
                        statusLabel.Text = "An error ocurred to register a Host";
                        statusLabel.Visible = true;
                    }
                }
                catch (Exception err)
                {
                    statusLabel.Text = $"An error ocurred to register a Host. Details: {err.Message}";
                    statusLabel.Visible = true;
                }
            }
        }

    }
}