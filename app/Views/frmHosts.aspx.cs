using AppUlacitBnB.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ulacitbnb.Models;

namespace AppUlacitBnB.Views
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        IEnumerable<Host> hosts = new ObservableCollection<Host>();
        HostManager hostManager = new HostManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/frmLogin.aspx");
                }
                InitControllers();
            } 
        }

        private async void InitControllers()
        {
            try
            {
                hosts = await hostManager.GetHostsList(Session["Token"].ToString());
                gvHosts.DataSource = hosts.ToList();
                gvHosts.DataBind();
            }
            catch (Exception crash)
            {
                lblStatus.Text = "An error ocurred to load service. Details: " + crash.Message;
                lblStatus.Visible = true;
            }
         

        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}