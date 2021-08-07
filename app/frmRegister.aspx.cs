
﻿using AppReservasULACIT.Models;
using AppUlacitBnB.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppUlacitBnB
{
    public partial class frmRegistro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnBirthDate_Click(object sender, EventArgs e)
        {
            cldBirthDate.Visible = true;
        }

        protected void cldBirthDate_SelectionChanged(object sender, EventArgs e)
        {
            txtBirthDate.Text = cldBirthDate.SelectedDate.ToString("dd/MM/yyyy");
            cldBirthDate.Visible = false;
        }

        protected async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    CustomerManager customerManager = new CustomerManager();
                    Customer customer = new Customer()
                    {
                        Name = txtName.Text,
                        LastName = txtLastName.Text,
                        Identification = txtIdentification.Text,
                        Password = txtPassword.Text,
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        BirthDate = DateTime.ParseExact(txtBirthDate.Text, "dd/MM/yyyy", null),
                        Status = "Active"
                    };

                    Customer customerRegistered = await customerManager.Register(customer);

                    if (!string.IsNullOrEmpty(customer.Identification))
                    {
                        Response.Redirect("frmLogin.aspx");
                    }
                    else
                    {
                        lblStatus.Text = "An error ocurred to register an user";
                        lblStatus.Visible = true;
                    }

                }
                catch (Exception err)
                {
                    lblStatus.Text = $"An error ocurred to register an user. Details: {err.Message}";
                    lblStatus.Visible = true;
                }
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Server.Transfer("frmLogin.aspx");
        }
    }
}