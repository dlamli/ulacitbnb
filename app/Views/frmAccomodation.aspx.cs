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
    public partial class frmAccomodation : System.Web.UI.Page
    {
        IEnumerable<Accomodation> accomodations = new ObservableCollection<Accomodation>();

        AccomodationManager accomodationManager = new AccomodationManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitializeControls();
            }
        }

        private async void InitializeControls() 
        {
            try
            {
                accomodations = await accomodationManager.GetAccomodations(Session["Token"].ToString());
                gvAccomodation.DataSource = accomodations.ToList();
                gvAccomodation.DataBind();
            }
            catch (Exception ex)
            {
                lblStatus.Text = "There was an error at the time of charging the accomodations list.";
                lblStatus.Visible = true;
                throw;
            }
        }

        //Delete PopUp
        protected async void btnAcceptModal_Click(object sender, EventArgs e)
        {
            try
            {
                var result = string.Empty;
                result = await accomodationManager.DeleteAccomodation(lblDeleteCode.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    //Close Delete Modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                    "$(function() {closeModal();} )", true);

                    // Alert User
                    ltrAlertModalHeader.Text = "Success!";
                    ltrAlertModalMsg.Text = "Accomodation removed successfully.";

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
                ltrAlertModalMsg.Text = "There was an error. This accomodation couldn't be removed.";

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

        protected void btnCancelMaintModal_Click(object sender, EventArgs e)
        {
            //Close the maintenance modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {closeModalMaintenance();} )", true);
        }

        protected async void btnAcceptMaintModal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    //Create - Post - New
                    if (string.IsNullOrEmpty(txtAcc_ID.Text)) 
                    {
                        Accomodation accomodation = new Accomodation()
                        {
                            Acc_Name = txtAcc_Name.Text,
                            Hos_ID = Int32.Parse(txtHos_ID.Text),
                            Acc_Country = txtAcc_Country.Text,
                            Acc_State = txtAcc_State.Text,
                            Acc_Zipcode = txtAcc_Zipcode.Text,
                            Acc_Address = txtAcc_Address.Text,
                            Acc_Description = txtAcc_Description.Text,
                            Acc_Evaluation = ddlEvaluation.SelectedValue
                        };
                        Accomodation response = await accomodationManager.EnterAccomodation(accomodation, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(response.Acc_Name))
                        {
                            // Alert User
                            ltrAlertModalHeader.Text = "Success!";
                            ltrAlertModalMsg.Text = "Accomodation created successfully.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);

                            InitializeControls();
                        } 
                        else 
                        {
                            // Alert User
                            ltrAlertModalHeader.Text = "Action Failed";
                            ltrAlertModalMsg.Text = "There was an error creating the accomodation.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);
                        }
                    }
                    //Edit - Patch - Update
                    else
                    {
                        Accomodation accomodation = new Accomodation()
                        {
                            Acc_ID = Int32.Parse(txtAcc_ID.Text),
                            Acc_Name = txtAcc_Name.Text,
                            Hos_ID = Int32.Parse(txtHos_ID.Text),
                            Acc_Country = txtAcc_Country.Text,
                            Acc_State = txtAcc_State.Text,
                            Acc_Zipcode = txtAcc_Zipcode.Text,
                            Acc_Address = txtAcc_Address.Text,
                            Acc_Description = txtAcc_Description.Text,
                            Acc_Evaluation = ddlEvaluation.SelectedValue
                        };
                        Accomodation response = await accomodationManager.UpdateAccomodation(accomodation, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(response.Acc_Name))
                        {
                            ltrAlertModalHeader.Text = "Success!";
                            ltrAlertModalMsg.Text = "Accomodation modified successfully.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);

                            InitializeControls();

                        }
                        else
                        {
                            // Alert User
                            ltrAlertModalHeader.Text = "Action Failed";
                            ltrAlertModalMsg.Text = "There was an error modifying the accomodation.";

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ltrMaintenanceTitle.Text = "New Accomodation";
            resetControls();

            //Open the maintenance modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {openModalMaintenance(); } )", true);
        }

        protected void gvAccomodation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow item = gvAccomodation.Rows[index];

            var acc_id = item.Cells[0].Text;
            var acc_name = item.Cells[1].Text;
            var acc_hosID = item.Cells[2].Text;
            var acc_country = item.Cells[3].Text;
            var acc_state = item.Cells[4].Text;
            var acc_zipCode = item.Cells[5].Text;
            var acc_address = item.Cells[6].Text;
            var acc_descrpition = item.Cells[7].Text;
            var acc_eval = item.Cells[8].Text;

            switch (e.CommandName)
            {
                case "editt_action":
                    ltrMaintenanceTitle.Text = acc_id + " - " + acc_name;

                    //Gather data from the item selected to the modal form
                    txtAcc_ID.Text = acc_id;
                    txtAcc_Name.Text = acc_name;
                    txtHos_ID.Text = acc_hosID;
                    txtAcc_Country.Text = acc_country;
                    txtAcc_State.Text = acc_state;
                    txtAcc_Zipcode.Text = acc_zipCode;
                    txtAcc_Address.Text = acc_address;
                    txtAcc_Description.Text = acc_descrpition;
                    ddlEvaluation.SelectedValue = acc_eval;

                    //Open maintenance modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                        "$(function() {openModalMaintenance(); } )", true);

                    break;

                case "deletee_action":
                    lblDeleteCode.Text = acc_id;
                    ltrMyModalMessage.Text = string.Concat("Please confirm that you want to delete the accomodation ", acc_id, " - ", acc_name, ".");
                    //Open my modal
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                            "$(function() {openModal(); } )", true);
                    break;

                default:
                    break;
            }
        }

        protected void btnAlertModalOk_Click(object sender, EventArgs e)
        {
            //Close alert modal
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                "$(function() {closeAlertModal(); } )", true);
        }

        private void resetControls()
        {
            txtAcc_ID.Text = string.Empty;
            txtAcc_Name.Text = string.Empty;
            txtHos_ID.Text = string.Empty;
            txtAcc_Country.Text = string.Empty;
            txtAcc_State.Text = string.Empty;
            txtAcc_Zipcode.Text = string.Empty;
            txtAcc_Address.Text = string.Empty;
            txtAcc_Description.Text = string.Empty;
            ddlEvaluation.SelectedValue = default;
        }
    }
}