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
    public partial class frmHotel : System.Web.UI.Page
    {
        IEnumerable<Customer> customers = new ObservableCollection<Customer>();
        CustomerManager customerManager = new CustomerManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CodigoUsuario"] == null)
                {
                    Response.Redirect("~/frmLogin.aspx");
                }
                else
                    InicializarControles();
            }
        }

        private async void InicializarControles()
        {
            try
            {
                customers = await customerManager.getCustomerList(Session["Token"].ToString());
                gvHoteles.DataSource = customers.ToList();
                gvHoteles.DataBind();
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected async void btnAceptarModal_Click(object sender, EventArgs e)
        {
            try
            {
                string resultado = string.Empty;
                resultado = await customerManager.DeleteCustomer(lblCodigoEliminar.Text, Session["Token"].ToString());
                if (!string.IsNullOrEmpty(resultado))
                {
                    ltrModalMensaje.Text = "Hotel eliminado";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { openModal(); });", true);
                    InicializarControles();
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error al cargar la lista de servicios. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelarModal_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide", "$(function() { CloseModal(); });", true);
        }

        protected async void btnAceptarMant_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    if (string.IsNullOrEmpty(txtCodigoMant.Text))//INSERTAR
                    {
                        Customer customer = new Customer()
                        {
                            Name = txtNombreMant.Text,
                            Email = txtEmailMant.Text,
                            Password = txtDireccionMant.Text,
                            Phone = txtTelefono.Text,
                            Status = ddlCategoria.SelectedValue
                        };

                        Customer response = await customerManager.EnterCustomer(customer, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(response.Name))
                        {
                            lblResultado.Text = "Hotel ingresado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                    else//MODIFICAR
                    {
                        Customer customer = new Customer()
                        {
                            ID = Convert.ToInt32(txtCodigoMant.Text),
                            Name = txtNombreMant.Text,
                            Email = txtEmailMant.Text,
                            Password = txtDireccionMant.Text,
                            Phone = txtTelefono.Text,
                            Status = ddlCategoria.SelectedValue
                        };

                        Customer response = await customerManager.UpdateCustomer(customer, Session["Token"].ToString());

                        if (!string.IsNullOrEmpty(response.Name))
                        {
                            lblResultado.Text = "Hotel modificado con exito";
                            lblResultado.Visible = true;
                            lblResultado.ForeColor = Color.Green;
                            InicializarControles();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                lblStatus.Text = "Hubo un error en la operacion. Detalle: " + exc.Message;
                lblStatus.Visible = true;
            }
        }

        protected void btnCancelarMant_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "LaunchServerSide",
               "$(function() {CloseMantenimiento(); } );", true);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            ltrTituloMantenimiento.Text = "Nuevo hotel";
            lblResultado.Text = string.Empty;
            txtCodigoMant.Text = string.Empty;
            txtNombreMant.Text = string.Empty;
            txtEmailMant.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            LimpiarControles();

            ScriptManager.RegisterStartupScript(this,
                 this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
        }

        private void LimpiarControles()
        {
            foreach (Control item in Page.FindControl("Content1").Controls)
            {
                foreach (Control hijo in item.Controls)
                {
                    if (hijo is TextBox)
                        ((TextBox)hijo).Text = string.Empty;

                }
            }
        }

        protected void gvHoteles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow fila = gvHoteles.Rows[index];

            switch (e.CommandName)
            {
                case "Modificar":
                    ltrTituloMantenimiento.Text = "Modificar hotel";
                    txtCodigoMant.Text = fila.Cells[0].Text;
                    txtNombreMant.Text = fila.Cells[1].Text;
                    txtEmailMant.Text = fila.Cells[2].Text;
                    txtDireccionMant.Text = fila.Cells[3].Text;
                    txtTelefono.Text = fila.Cells[4].Text;
                    ddlCategoria.SelectedValue = fila.Cells[5].Text;
                    ScriptManager.RegisterStartupScript(this,
                this.GetType(), "LaunchServerSide", "$(function() {openModalMantenimiento(); } );", true);
                    break;
                case "Eliminar":
                    lblCodigoEliminar.Text = fila.Cells[0].Text;
                    lblCodigoEliminar.Visible = false;
                    ltrModalMensaje.Text = "Confirme que desea eliminar el hotel " + fila.Cells[0].Text + "-" + fila.Cells[1].Text;
                    ScriptManager.RegisterStartupScript(this,
               this.GetType(), "LaunchServerSide", "$(function() {openModal(); } );", true);

                    break;
                default:
                    break;
            }
        }
    }
}