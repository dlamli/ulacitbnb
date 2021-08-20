using AppUlacitBnB.Models;
using AppUlacitBnB.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace AppUlacitBnB.Views
{
    public partial class frmCustomer : System.Web.UI.Page
    {
        IEnumerable<Customer> customers = new ObservableCollection<Customer>();
        CustomerManager customerManager = new CustomerManager();

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
                customers = await customerManager.getCustomerList(Session["Token"].ToString());
                gvCustomer.DataSource = customers.ToList();
                gvCustomer.DataBind();
            }
            catch (Exception crash)
            {
                lblStatus.Text = "An error ocurred to load service. Details: " + crash.Message;
                lblStatus.Visible = true;
            }
        }


        protected void btnNew_Click(object sender, EventArgs e)
        {
            ltrTitleManagement.Text = "New Customer";
            txtIdManagement.Text = string.Empty;
            lblResult.Text = string.Empty;
            txtNameManagement.Text = string.Empty;
            txtLastNameManagement.Text = string.Empty;
            txtIdentificationManagement.Text = string.Empty;
            txtPasswordManagement.Text = string.Empty;
            txtEmailManagement.Text = string.Empty;
            txtBirthdateManagement.Text = string.Empty;
            txtPhoneManagement.Text = string.Empty;


            ScriptManager.RegisterStartupScript(this,
                 this.GetType(), "LaunchServerSide", "$(function() {openManagement(); } );", true);
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
                result = await customerManager.DeleteCustomer(lblIdDelete.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    lblStatus.Text = "Customer deleted";
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

        protected async void btnAceptManagement_Click(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtIdManagement.Text))//Insert
                    {
                        Customer customer = new Customer()
                        {
                            Name = txtNameManagement.Text,
                            LastName = txtLastNameManagement.Text,
                            Identification = txtIdentificationManagement.Text,
                            Password = txtPasswordManagement.Text,
                            Email = txtEmailManagement.Text,
                            Status = ddlStatus.SelectedValue,
                            BirthDate = Convert.ToDateTime(txtBirthdateManagement.Text, culture),
                            Phone = txtPhoneManagement.Text
                        };

                        Customer customerResponse = await customerManager.EnterCustomer(customer, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(customerResponse.Name))
                        {
                            lblStatus.Text = "Customer registered";
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Green;
                            initControllers();
                        }
                    }
                    else//MODIFICAR
                    {
                        Customer customer = new Customer()
                        {
                            ID = Convert.ToInt32(txtIdManagement.Text),
                            Name = txtNameManagement.Text,
                            LastName = txtLastNameManagement.Text,
                            Identification = txtIdentificationManagement.Text,
                            Password = txtPasswordManagement.Text,
                            Email = txtEmailManagement.Text,
                            Status = ddlStatus.SelectedValue,
                            BirthDate = Convert.ToDateTime(txtBirthdateManagement.Text, culture),
                            Phone = txtPhoneManagement.Text
                        };

                        Customer customerResponse = await customerManager.UpdateCustomer(customer, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(customerResponse.Identification))
                        {
                            lblStatus.Text = "Customer Updated";
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


        protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCustomer.Rows[index];

            switch (e.CommandName)
            {
                case "updateCustomer":

                    ltrTitleManagement.Text = "Update Customer";
                    txtIdManagement.Text = row.Cells[0].Text;
                    txtNameManagement.Text = row.Cells[1].Text;
                    txtLastNameManagement.Text = row.Cells[2].Text;
                    txtIdentificationManagement.Text = row.Cells[3].Text;
                    txtPasswordManagement.Text = row.Cells[4].Text;
                    txtEmailManagement.Text = row.Cells[5].Text;
                    ddlStatus.SelectedValue = row.Cells[6].Text;
                    txtBirthdateManagement.Text = row.Cells[7].Text;
                    txtPhoneManagement.Text = row.Cells[8].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openManagement(); } );", true);

                    break;
                case "deleteCustomer":

                    lblIdDelete.Text = row.Cells[0].Text;
                    lblIdDelete.Visible = false;
                    ltrModalMessage.Text = "Confirm to delete customer: " + row.Cells[0].Text + " - " + row.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }

        protected async void btnAceptModal_Click(object sender, EventArgs e)
        {
            try
            {
                string result = string.Empty;
                result = await customerManager.DeleteCustomer(lblIdDelete.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    ltrModalMessage.Text = "Customer deleted";
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

        protected async void btnAceptManagement_Click(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("en-US");
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtIdManagement.Text))//Insert
                    {
                        Customer customer = new Customer()
                        {
                            Name = txtNameManagement.Text,
                            LastName = txtLastNameManagement.Text,
                            Identification = txtIdentificationManagement.Text,
                            Password = txtPasswordManagement.Text,
                            Email = txtEmailManagement.Text,
                            Status = ddlStatus.SelectedValue,
                            BirthDate = Convert.ToDateTime(txtBirthdateManagement.Text, culture),
                            Phone = txtPhoneManagement.Text
                        };

                        Customer customerResponse = await customerManager.EnterCustomer(customer, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(customerResponse.Name))
                        {
                            lblStatus.Text = "Customer registered";
                            lblStatus.Visible = true;
                            lblStatus.ForeColor = Color.Green;
                            initControllers();
                        }
                    }
                    else//MODIFICAR
                    {
                        Customer customer = new Customer()
                        {
                            ID = Convert.ToInt32(txtIdManagement.Text),
                            Name = txtNameManagement.Text,
                            LastName = txtLastNameManagement.Text,
                            Identification = txtIdentificationManagement.Text,
                            Password = txtPasswordManagement.Text,
                            Email = txtEmailManagement.Text,
                            Status = ddlStatus.SelectedValue,
                            BirthDate = Convert.ToDateTime(txtBirthdateManagement.Text, culture),
                            Phone = txtPhoneManagement.Text
                        };

                        Customer customerResponse = await customerManager.UpdateCustomer(customer, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(customerResponse.Identification))
                        {
                            lblResult.Text = "Customer Updated";
                            lblResult.Visible = true;
                            lblResult.ForeColor = Color.Green;
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


        protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvCustomer.Rows[index];

            switch (e.CommandName)
            {
                case "updateCustomer":

                    ltrTitleManagement.Text = "Update Customer";
                    txtIdManagement.Text = row.Cells[0].Text;
                    txtNameManagement.Text = row.Cells[1].Text;
                    txtLastNameManagement.Text = row.Cells[2].Text;
                    txtIdentificationManagement.Text = row.Cells[3].Text;
                    txtPasswordManagement.Text = row.Cells[4].Text;
                    txtEmailManagement.Text = row.Cells[5].Text;
                    ddlStatus.SelectedValue = row.Cells[6].Text;
                    txtBirthdateManagement.Text = row.Cells[7].Text;
                    txtPhoneManagement.Text = row.Cells[8].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openManagement(); } );", true);

                    break;
                case "deleteCustomer":

                    lblIdDelete.Text = row.Cells[0].Text;
                    lblIdDelete.Visible = false;
                    ltrModalMessage.Text = "Confirm to delete customer: " + row.Cells[0].Text + " - " + row.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }

    }
}