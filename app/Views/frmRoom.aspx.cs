using AppUlacitBnB.Controllers;
using AppUlacitBnB.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AppUlacitBnB.Views
{
    public partial class frmRoom : System.Web.UI.Page
    {

        IEnumerable<Room> rooms = new ObservableCollection<Room>();
        RoomManager roomManager = new RoomManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeControls();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private async void InitializeControls()
        {
            try
            {
                rooms = await roomManager.GetRooms(Session["Token"].ToString());
                gvRoom.DataSource = rooms.ToList();
                gvRoom.DataBind();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "There was an error at the time of charging the rooms list.";
                lblStatus.Visible = true;
                throw;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ltrMaintenanceTitle.Text = "New Room";
            resetControls();

            //Open the maintenance modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {openModalMaintenance(); } )", true);
        }

        protected async void btnAcceptModal_Click(object sender, EventArgs e)
        {
            try
            {
                var result = string.Empty;
                result = await roomManager.DeleteRoom(lblDeleteCode.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    //Close Delete Modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                    "$(function() {closeModal();} )", true);

                    // Alert User
                    ltrAlertModalHeader.Text = "Success!";
                    ltrAlertModalMsg.Text = "Room removed successfully.";

                    //Open alert modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                        "$(function() {openAlertModal(); } )", true);

                    InitializeControls();
                }
            }
            catch (Exception)
            {
                //Close Delete Modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {closeModal();} )", true);

                //Alert User
                ltrAlertModalHeader.Text = "Action Failed";
                ltrAlertModalMsg.Text = "There was an error. This room couldn't be removed.";

                //Open alert modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                    "$(function() {openAlertModal(); } )", true);

                throw;
            }
        }

        protected void btnCancelModal_Click(object sender, EventArgs e)
        {
            //Close My Modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {closeModal();} )", true);
        }

        protected async void btnAcceptMaintModal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    //Create - Post - New
                    if (string.IsNullOrEmpty(txtRoo_ID.Text))
                    {
                        Room room = new Room()
                        {
                            Roo_Price = Decimal.Parse(txtRoo_Price.Text),
                            Roo_Quantity = Decimal.Parse(txtRoo_Quantity.Text),
                            Roo_Type = txtRoo_Type.Text,
                            Roo_Evaluation = ddlEvaluation.SelectedValue,
                            Roo_BedQuantity = Decimal.Parse(txtRoo_BedQuantity.Text),
                            Ser_ID = Int32.Parse(txtSer_ID.Text),
                            Acc_ID = Int32.Parse(txtAcc_ID.Text)
                        };
                        Room response = await roomManager.EnterRoom(room, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(response.Roo_ID.ToString()))
                        {
                            // Alert User
                            ltrAlertModalHeader.Text = "Success!";
                            ltrAlertModalMsg.Text = "Room created successfully.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);

                            InitializeControls();
                        }
                        else
                        {
                            // Alert User
                            ltrAlertModalHeader.Text = "Action Failed";
                            ltrAlertModalMsg.Text = "There was an error creating the room.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);
                        }
                    }
                    //Edit - Patch - Update
                    else
                    {
                        Room room = new Room()
                        {
                            Roo_ID = Int32.Parse(txtRoo_ID.Text),
                            Roo_Price = Decimal.Parse(txtRoo_Price.Text),
                            Roo_Quantity = Decimal.Parse(txtRoo_Quantity.Text),
                            Roo_Type = txtRoo_Type.Text,
                            Roo_Evaluation = ddlEvaluation.SelectedValue,
                            Roo_BedQuantity = Decimal.Parse(txtRoo_BedQuantity.Text),
                            Ser_ID = Int32.Parse(txtSer_ID.Text),
                            Acc_ID = Int32.Parse(txtAcc_ID.Text)
                        };
                        Room response = await roomManager.UpdateRoom(room, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(response.Roo_ID.ToString()))
                        {
                            ltrAlertModalHeader.Text = "Success!";
                            ltrAlertModalMsg.Text = "Room modified successfully.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);

                            InitializeControls();

                        }
                        else
                        {
                            // Alert User
                            ltrAlertModalHeader.Text = "Action Failed";
                            ltrAlertModalMsg.Text = "There was an error modifying the room.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Alert User
                ltrAlertModalHeader.Text = "Action Failed";
                ltrAlertModalMsg.Text = "An unexpected error ocurred, please retry the action intended.";

                //Open alert modal
                ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                    "$(function() {openAlertModal(); } )", true);

                throw;
            }
        }

        protected void btnCancelMaintModal_Click(object sender, EventArgs e)
        {
            //Close the maintenance modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {closeModalMaintenance();} )", true);
        }

        protected void btnAlertModalOk_Click(object sender, EventArgs e)
        {
            //Close alert modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {closeAlertModal(); } )", true);
        }

        private void resetControls()
        {
            txtRoo_ID.Text = string.Empty;
            txtAcc_ID.Text = string.Empty;
            txtRoo_Price.Text = string.Empty;
            txtRoo_Quantity.Text = string.Empty;
            txtRoo_Type.Text = string.Empty;
            txtRoo_BedQuantity.Text = string.Empty;
            txtSer_ID.Text = string.Empty;
            ddlEvaluation.SelectedValue = default;
        }

        protected void gvRoom_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow item = gvRoom.Rows[index];

            var acc_id = item.Cells[0].Text;
            var roo_id = item.Cells[1].Text;
            var roo_price = item.Cells[2].Text;
            var roo_qty = item.Cells[3].Text;
            var roo_type = item.Cells[4].Text;
            var roo_eval = item.Cells[5].Text;
            var bed_qty = item.Cells[6].Text;
            var ser_id = item.Cells[7].Text;

            switch (e.CommandName)
            {
                case "editt_action":
                    ltrMaintenanceTitle.Text = roo_id + " - " + roo_type;

                    //Gather data from the item selected to the modal form
                    txtRoo_ID.Text = roo_id;
                    txtAcc_ID.Text = acc_id;
                    txtRoo_Price.Text = roo_price;
                    txtRoo_Quantity.Text = roo_qty;
                    txtRoo_Type.Text = roo_type;
                    txtRoo_BedQuantity.Text = bed_qty;
                    txtSer_ID.Text = ser_id;
                    ddlEvaluation.SelectedValue = roo_eval;

                    //Open maintenance modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                        "$(function() {openModalMaintenance(); } )", true);

                    break;

                case "deletee_action":
                    lblDeleteCode.Text = roo_id;
                    ltrMyModalMessage.Text = string.Concat("Please confirm that you want to delete the room ", roo_id, " - ", roo_type, ".");
                    //Open my modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                            "$(function() {openModal(); } )", true);
                    break;

                default:
                    break;
            }
        }
    }
}