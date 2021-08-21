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
                hosts = await hostManager.GetHostsList();
                gvHosts.DataSource = hosts.ToList();
                gvHosts.DataBind();
            }
            catch (Exception crash)
            {
                lblStatus.Text = "An error ocurred to load service. Details: " + crash.Message;
                lblStatus.Visible = true;
            }
         

        }

        protected void gvHosts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvHosts.Rows[index];

            switch (e.CommandName)
            {
                case "delete_action":
                    lblDeleteCode.Text = row.Cells[0].Text;
                    ltrMyModalMessage.Text = "Please confirm that you want to delete the host";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                        "$(function() {openModal(); } )", true);
                    break;
            }
        }


        protected async void btnAcceptModal_Click(object sender, EventArgs e)
        {
            try
            {
                string result = string.Empty;
                result = await hostManager.Delete(lblDeleteCode.Text);
                if (!string.IsNullOrEmpty(result))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                     "$(function() {closeModal();} )", true);

                    ltrAlertModalHeader.Text = "Success";
                    ltrAlertModalMsg.Text = "Host removed";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                        "$(function() {openAlertModal(); } )", true);
                    InitControllers();
                }
            }
            catch (Exception crash)
            {
                lblStatus.Text = "An error ocurred to load service. Details: " + crash.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
            "$(function() {closeModal();} )", true);
        }

        protected void btnAlertModalOk_Click(object sender, EventArgs e)
        {
            //Close alert modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {closeAlertModal(); } )", true);
        }
    }
}