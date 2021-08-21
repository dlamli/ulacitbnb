using AppUlacitBnB.Controllers;
using AppUlacitBnB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppUlacitBnB.Views
{
    public partial class frmService : System.Web.UI.Page
    {
        IEnumerable<Service> services = new ObservableCollection<Service>();
        ServiceManager serviceManager = new ServiceManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("~/frmLogin.aspx");
                }
                else
                {
                    initControllers();
                }
            }
        }

        private async void initControllers()
        {
            try
            {
                services = await serviceManager.getServiceList(Session["Token"].ToString());
                gvService.DataSource = services.ToList();
                gvService.DataBind();
            }
            catch (Exception crash)
            {
                lblStatus.Text = "An error ocurred to load service. Details: " + crash.Message;
                lblStatus.Visible = true;
            }
        }

        protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvService.Rows[index];

            switch (e.CommandName)
            {
                case "updateService":

                    ltrTitleManagement.Text = "Update Customer";
                    txtIdManagement.Text = row.Cells[0].Text;
                    txtNameManagement.Text = row.Cells[1].Text;
                    txtDescriptionManagement.Text = row.Cells[2].Text;
                    ddlType.SelectedValue = row.Cells[3].Text;
                    ddlStatus.SelectedValue = row.Cells[4].Text;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openManagement(); } );", true);

                    break;
                case "deleteService":

                    lblIdDelete.Text = row.Cells[0].Text;
                    lblIdDelete.Visible = false;
                    ltrModalMessage.Text = "Confirm to delete service: " + row.Cells[0].Text + " - " + row.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ltrTitleManagement.Text = "New Service";
            txtIdManagement.Text = string.Empty;
            lblResult.Text = string.Empty;
            txtNameManagement.Text = string.Empty;
            txtDescriptionManagement.Text = string.Empty;


            ScriptManager.RegisterStartupScript(this,
                 this.GetType(), "LaunchServerSide", "$(function() {openManagement(); } );", true);
        }

        protected async void btnAceptManagement_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtIdManagement.Text))//Insert
                    {
                        Service customer = new Service()
                        {
                            Name = txtNameManagement.Text,
                            Description = txtDescriptionManagement.Text,
                            Type = ddlType.SelectedValue,
                            Status = ddlStatus.SelectedValue,
                        };

                        Service serviceResponse = await serviceManager.EnterService(customer, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(serviceResponse.Name))
                        {
                            lblStatus.Text = "Service registered";
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Green;
                            initControllers();
                        }
                    }
                    else//MODIFICAR
                    {
                        Service customer = new Service()
                        {
                            ID = Convert.ToInt32(txtIdManagement.Text),
                            Name = txtNameManagement.Text,
                            Description = txtDescriptionManagement.Text,
                            Type = ddlType.SelectedValue,
                            Status = ddlStatus.SelectedValue
                        };

                        Service customerResponse = await serviceManager.UpdateService(customer, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(customerResponse.Name))
                        {
                            lblStatus.Text = "Service Updated";
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Green;
                            initControllers();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "An error ocurred to load service. Details: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelManagement_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
            "$(function() {closeManagement(); } );", true);
        }

        protected async void btnAceptModal_Click(object sender, EventArgs e)
        {
            try
            {
                string result = string.Empty;
                result = await serviceManager.DeleteService(lblIdDelete.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    lblStatus.Text = "Service deleted";
                    lblStatus.Visible = true;
                    lblStatus.ForeColor = Color.Green;
                    initControllers();
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }

    }
}