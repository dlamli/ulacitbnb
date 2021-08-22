using AppUlacitBnB.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ulacit_bnb.Models;

namespace AppUlacitBnB.Views
{
    public partial class frmReview : System.Web.UI.Page
    {
        IEnumerable<Review> reviews = new ObservableCollection<Review>();
        ReviewManager reviewManager = new ReviewManager();


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
                reviews = await reviewManager.GetReviewsList(Session["Token"].ToString());
                gvReviews.DataSource = reviews.ToList();
                gvReviews.DataBind();
            }
            catch (Exception crash)
            {
                lblStatus.Text = "An error ocurred to load service. Details: " + crash.Message;
                lblStatus.Visible = true;
            }
        }

        private void resetControls()
        {
            txtID.Text = string.Empty;
            txtDate.Text = DateTime.Today.ToString();
            dpRate.SelectedValue = default;
            cbRecommendation.Checked = true;
            txtComment.Text = string.Empty;
            txtUsefull.Text = string.Empty;
            txtCustomerId.Text = string.Empty;
            txtAccomodationID.Text = string.Empty;
            txtTitle.Text = string.Empty;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ltrMaintenanceTitle.Text = "New Review";
            resetControls();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
            "$(function() {openModalMaintenance(); } )", true);
        }

        protected async void btnAcceptMaintModal_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtID.Text))
                    {
                        Review review = new Review()
                        {
                            Title = txtTitle.Text,
                            Date = Convert.ToDateTime(txtDate.Text),
                            Rate = Int32.Parse(dpRate.SelectedValue),
                            Recommendation = cbRecommendation.Checked,
                            Comment = txtComment.Text,
                            Usefull = Int32.Parse(txtUsefull.Text),
                            CustomerID = Int32.Parse(txtCustomerId.Text),
                            AccomodationID = Int32.Parse(txtAccomodationID.Text)
                        };
                        Review response = await reviewManager.EnterReview(review, Session["Token"].ToString());
                        if (response.Rate <= 1)
                        {
                            ltrAlertModalHeader.Text = "Success!";
                            ltrAlertModalMsg.Text = "New Review created successfully.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);
                            initControllers();
                        }
                    }
                    else
                    {
                        Review review = new Review()
                        {
                            ID = Int32.Parse(txtID.Text),
                            Title = txtTitle.Text,
                            Date = Convert.ToDateTime(txtDate.Text),
                            Rate = Int32.Parse(dpRate.SelectedValue),
                            Recommendation = cbRecommendation.Checked,
                            Comment = txtComment.Text,
                            Usefull = Int32.Parse(txtUsefull.Text),
                            CustomerID = Int32.Parse(txtCustomerId.Text),
                            AccomodationID = Int32.Parse(txtAccomodationID.Text)
                        };
                        Review response = await reviewManager.UpdateReview(review, Session["Token"].ToString());

                        if (response.Rate >= 1)
                        {
                            ltrAlertModalHeader.Text = "Success!";
                            ltrAlertModalMsg.Text = "Review modified successfully.";

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);

                            initControllers();
                        }
                        else
                        {
                            // Alert User
                            ltrAlertModalHeader.Text = "Action Failed";
                            ltrAlertModalMsg.Text = "There was an error modifying the review.";

                            //Open alert modal
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                                "$(function() {openAlertModal(); } )", true);
                        }

                    }
                }
            }
            catch (Exception crash)
            {
                lblStatus.Text = "An error ocurred to load service. Details: " + crash.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelMaintModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
               "$(function() {closeModal();} )", true);
        }

        protected void gvReviews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvReviews.Rows[index];

            switch (e.CommandName)
            {
                case "editt_action":
                    ltrMaintenanceTitle.Text = "Edit Review";
                    txtID.Text = row.Cells[0].Text;
                    txtDate.Text = row.Cells[1].Text;
                    dpRate.SelectedValue = row.Cells[2].Text;
                    txtComment.Text = row.Cells[4].Text;
                    txtUsefull.Text = row.Cells[5].Text;
                    txtTitle.Text = row.Cells[6].Text;
                    txtCustomerId.Text = row.Cells[7].Text;
                    txtAccomodationID.Text = row.Cells[8].Text;
                    cbRecommendation.Checked = bool.Parse(row.Cells[3].Text);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                     "$(function() {openModalMaintenance(); } )", true);
                    break;
                case "delete_action":
                    lblDeleteCode.Text = row.Cells[0].Text;
                    ltrMyModalMessage.Text = "Please confirm that you want to delete the review";
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
                result = await reviewManager.DeleteRoom(lblDeleteCode.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(result))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                     "$(function() {closeModal();} )", true);

                    ltrAlertModalHeader.Text = "Success";
                    ltrAlertModalMsg.Text = "Review removed";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
                        "$(function() {openAlertModal(); } )", true);

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