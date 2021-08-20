using AppUlacitBnB.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ulacitbnb.Models;

namespace AppUlacitBnB.Views
{
    public partial class frmPayment : System.Web.UI.Page
    {
        IEnumerable<Payment> payments = new ObservableCollection<Payment>();
        PaymentManager paymentManager = new PaymentManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeControllers();
            }
        }

        private async void InitializeControllers()
        {
            try
            {
                payments = await paymentManager.GetPaymentList(Session["Token"].ToString());
                gvPayments.DataSource = payments.ToList();
                gvPayments.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "There was an error loading the list of services. Detail: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected async void btnAcceptModal_Click(object sender, EventArgs e)
        {
            try
            {
                string result = string.Empty;
                result = await paymentManager.DeletePayment(lblDeleteCode.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    ltrModalMessage.Text = "Payment deleted";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { closeModal(); });", true);
                }
            }
            catch (Exception exec)
            {
                lblStatus.Text = "There was an error in the operation. Details: " + exec.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { closeModal(); });", true);
        }

        protected async void btnMantAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtMantCode.Text)) //ENTER
                    {
                        Payment payment = new Payment()
                        {
                            Pay_Brand = ddlBrand.SelectedValue,
                            Pay_Type = txtMantType.Text,
                            Pay_Modality = txtMantModality.Text,
                            Pay_Date = DateTime.Parse(txtMantDate.Text),
                            Pay_Amount = Int32.Parse(txtMantAmount.Text),
                            Pay_Taxes = decimal.Parse(txtMantTaxes.Text),
                            Pay_Total = decimal.Parse(txtMantTotal.Text)
                        };

                        Payment paymentResponse = await paymentManager.EnterPayment(payment, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(paymentResponse.Pay_Type))
                        {
                            lblResult.Text = "Payment successfuly added";
                            lblResult.Visible = true;
                            lblResult.ForeColor = Color.Green;
                            InitializeControllers();
                        }
                    }
                    else //MODIFY
                    {
                        Payment payment = new Payment()
                        {
                            Pay_ID = Convert.ToInt32(txtMantCode.Text),
                            Pay_Brand = ddlBrand.SelectedValue,
                            Pay_Type = txtMantType.Text,
                            Pay_Modality = txtMantModality.Text,
                            Pay_Date = DateTime.Parse(txtMantDate.Text),
                            Pay_Amount = Int32.Parse(txtMantAmount.Text),
                            Pay_Taxes = decimal.Parse(txtMantTaxes.Text),
                            Pay_Total = decimal.Parse(txtMantTotal.Text)
                        };

                        Payment paymentResponse = await paymentManager.UpdatePayment(payment, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(paymentResponse.Pay_Type))
                        {
                            lblResult.Text = "Payment successfuly modified";
                            lblResult.Visible = true;
                            lblResult.ForeColor = Color.Green;
                            InitializeControllers();
                        }
                    }
                }
            }
            catch (Exception exec)
            {
                lblStatus.Text = "There was an error in the operation. Details: " + exec.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnMantCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {closeManagement(); } );", true);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ltrManagementTitle.Text = "New Payment";
            txtMantCode.Text = string.Empty;
            txtMantType.Text = string.Empty;
            txtMantModality.Text = string.Empty;
            txtMantDate.Text = string.Empty;
            txtMantAmount.Text = string.Empty;
            txtMantTaxes.Text = string.Empty;
            txtMantTotal.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {openManagement(); } );", true);
        }

        private void CleanControls()
        {
            foreach(var item in Page.Controls)
            {
                if (item is TextBox)
                    ((TextBox)item).Text = string.Empty;
            }
        }

        protected void gvPayments_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvPayments.Rows[index];

            switch (e.CommandName)
            {
                case "Modify":
                    ltrManagementTitle.Text = "Modify payment";
                    txtMantCode.Text = row.Cells[0].Text;
                    ddlBrand.SelectedValue = row.Cells[1].Text;
                    txtMantType.Text = row.Cells[2].Text;
                    txtMantModality.Text = row.Cells[3].Text;
                    txtMantDate.Text = row.Cells[4].Text;
                    txtMantAmount.Text = row.Cells[5].Text;
                    txtMantTaxes.Text = row.Cells[6].Text;
                    txtMantTotal.Text = row.Cells[7].Text;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openManagement(); } );", true);

                    break;
                case "Delete":
                    lblDeleteCode.Text = row.Cells[0].Text;
                    lblDeleteCode.Visible = false;
                    ltrModalMessage.Text = "Do you wish to delete this payment?" + row.Cells[0].Text + " - " + row.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                        "$(function() {openModal(); } );", true);
                    break;
                default:
                    break;
            }
        }
    }
}