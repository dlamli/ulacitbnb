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
            txtDate.Text = string.Empty;
            dpRate.SelectedValue = default;
            txtRecommendation.Text = string.Empty;
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

        protected void btnAcceptMaintModal_Click(object sender, EventArgs e)
        {

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
                    txtRecommendation.Text = row.Cells[3].Text;
                    txtUsefull.Text = row.Cells[3].Text;
                    txtTitle.Text = row.Cells[6].Text;
                    txtCustomerId.Text = row.Cells[7].Text;
                    txtAccomodationID.Text = row.Cells[8].Text;
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

        protected void btnAcceptModal_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancelModal_Click(object sender, EventArgs e)
        {

        }
    }
}