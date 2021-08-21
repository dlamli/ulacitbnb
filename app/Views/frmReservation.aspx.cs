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
    public partial class frmReservation : System.Web.UI.Page
    {
        IEnumerable<Reservation> reservations = new ObservableCollection<Reservation>();
        ReservationManager reservationManager = new ReservationManager();

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
                    InitializeControllers();
                }
            }
        }

        private async void InitializeControllers()
        {
            try
            {
                reservations = await reservationManager.GetReservationList(Session["Token"].ToString());
                gvReservation.DataSource = reservations.ToList();
                gvReservation.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "There was an error loading the list of services. Detail: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void gvReservation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvReservation.Rows[index];

            switch (e.CommandName)
            {
                case "modifyReservation":

                    ltrManagementTitle.Text = "Modify reservation";
                    txtMantCode.Text = row.Cells[0].Text;
                    txtMantStartDate.Text = row.Cells[1].Text;
                    txtMantResDate.Text = row.Cells[2].Text;
                    txtMantEndDate.Text = row.Cells[3].Text;
                    ddlStatus.SelectedValue = row.Cells[4].Text;
                    ddlQuantity.SelectedValue = row.Cells[5].Text;
                    txtMantResolutionDate.Text = row.Cells[6].Text;
                    txtMantPaymentID.Text = row.Cells[7].Text;
                    txtMantCustID.Text = row.Cells[8].Text;
                    txtMantRoomID.Text = row.Cells[9].Text;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openManagement(); } );", true);

                    break;

                case "deletePayment":

                    lblDeleteCode.Text = row.Cells[0].Text;
                    lblDeleteCode.Visible = false;
                    ltrModalMessage.Text = "Do you wish to delete this reservation?" + row.Cells[0].Text + " - " + row.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;

                default:
                    break;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ltrManagementTitle.Text = "New Reservation";
            txtMantCode.Text = string.Empty;
            txtMantStartDate.Text = string.Empty;
            txtMantResDate.Text = string.Empty;
            txtMantEndDate.Text = string.Empty;
            txtMantResolutionDate.Text = string.Empty;
            txtMantPaymentID.Text = string.Empty;
            txtMantCustID.Text = string.Empty;
            txtMantRoomID.Text = string.Empty;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {openManagement(); } );", true);
        }

        protected async void btnMantAccept_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtMantCode.Text)) //ENTER
                    {
                        Reservation reservation = new Reservation()
                        {
                            Res_StartDate = DateTime.Parse(txtMantStartDate.Text),
                            Res_ReservationDate = DateTime.Parse(txtMantResDate.Text),
                            Res_EndDate = DateTime.Parse(txtMantEndDate.Text),
                            Res_Status = ddlStatus.SelectedValue,
                            Res_Quantity = decimal.Parse(ddlQuantity.SelectedValue),
                            Res_ResolutionDate = txtMantResolutionDate.Text,
                            Res_PaymentID = int.Parse(txtMantPaymentID.Text),
                            Cus_ID = int.Parse(txtMantCustID.Text),
                            Roo_ID = int.Parse(txtMantRoomID.Text)
                    };

                        Reservation reservationResponse = await reservationManager.EnterReservation(reservation, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(reservationResponse.Res_Status))
                        {
                            lblResult.Text = "Reservation successfuly added";
                            lblResult.Visible = true;
                            lblResult.ForeColor = Color.Green;

                            InitializeControllers();
                        }
                    }
                    else //MODIFY
                    {
                        Reservation reservation = new Reservation()
                        {
                            Res_ID = int.Parse(txtMantCode.Text),
                            Res_StartDate = DateTime.Parse(txtMantStartDate.Text),
                            Res_ReservationDate = DateTime.Parse(txtMantResDate.Text),
                            Res_EndDate = DateTime.Parse(txtMantEndDate.Text),
                            Res_Status = ddlStatus.SelectedValue,
                            Res_Quantity = decimal.Parse(ddlQuantity.SelectedValue),
                            Res_ResolutionDate = txtMantResolutionDate.Text,
                            Res_PaymentID = int.Parse(txtMantPaymentID.Text),
                            Cus_ID = int.Parse(txtMantCustID.Text),
                            Roo_ID = int.Parse(txtMantRoomID.Text)
                        };

                        Reservation reservationResponse = await reservationManager.UpdateReservation(reservation, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(reservationResponse.Res_Status))
                        {
                            lblResult.Text = "Reservation successfuly modified";
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

        protected async void btnAcceptModal_Click(object sender, EventArgs e)
        {
            try
            {
                string result = string.Empty;
                result = await reservationManager.DeleteReservation(lblDeleteCode.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    ltrModalMessage.Text = "Reservation deleted";
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

        private void CleanControls()
        {
            foreach (var item in Page.Controls)
            {
                if (item is TextBox)
                    ((TextBox)item).Text = string.Empty;
            }
        }
    }
}
