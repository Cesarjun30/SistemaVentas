﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.PerformanceData;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FrmCategoria : Form

    {

        private bool IsNuevo = false, IsEditar = false;


        public FrmCategoria()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el Nombre de la Categoria");
        }


        // Mostrar mensaje de Confirmacion 

        private void MensajeOk(string mensaje){

            MessageBox.Show(mensaje,"Sistema de Ventas",MessageBoxButtons.OK,MessageBoxIcon.Information );
        }



        //Mostrar Mensaje de Error 


        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Limpiar controles del formulario

        private void Limpiar() { 
        
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdcategoria.Text = string.Empty;
            
        
        }

        // Habilitar los controles del Formulario 

        private void Habilitar(bool valor)
        {
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.txtIdcategoria.ReadOnly = !valor;

        }

        // Procedimiento para habilitar los botones 


        private void Botones() {

            if (this.IsNuevo || this.IsEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;

            }
            else {

                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;


            }
        
        }
        // Metodo para ocultar columnas

        private void OcultarColumnas() {

            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
        
        }

        // Metodo para Mostrar Registros 

        private void Mostrar() {

            this.dataListado.DataSource = NCategoria.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        // Metodo para BuscarNombre 

        private void BuscarNombre()
        {

            this.dataListado.DataSource = NCategoria.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void FrmCategoria_Load(object sender, EventArgs e)
        {


            this.Top = 0;
            this.CenterToScreen();
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
        }

        // guardar 

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = " ";
                if (this.txtNombre.Text == string.Empty) {

                    MensajeError("Falta igresar algunos datos, seran remarcados");
                    errorIcono.SetError(txtNombre, " Ingrese un Nombre");
                }
                else {

                    if (this.IsNuevo)
                    {
                        rpta = NCategoria.Insertar(this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim());


                    }
                    else {

                        rpta = NCategoria.Editar(Convert.ToInt32(this.txtIdcategoria.Text),this.txtNombre.Text.Trim().ToUpper(), 
                               this.txtDescripcion.Text.Trim());
                    }

                    if (rpta.Equals("OK"))
                    {

                        if (this.IsNuevo)
                        {

                            this.MensajeOk("Se ha insertado de forma correcta el registro ");
                        }
                        else
                        {

                            this.MensajeOk("se Actualizo de forma correcta el registro");

                        }


                    }
                    else { 
                    this.MensajeError(rpta);
                    
                    }
                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar(); 
                
                }
            }
            catch(Exception ex) {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdcategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        //Editar 

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdcategoria.Text.Equals(""))
            {

                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);

            }
            else {
                this.MensajeError("Debe de seleccionar primero el registro a modificar");
            
            }
        }

        // Cancelar 

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
        }

       
        //  Check Eliminar 

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {

                this.dataListado.Columns[0].Visible = true;
            }
            else {

                this.dataListado.Columns[0].Visible = false;
            }
        }

        // Data Listado

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index) {

                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            
            }
        }


        // Eliminar}

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                DialogResult Opcion;
                Opcion = MessageBox.Show("Realmente desea Eliminar los registros ", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (Opcion == DialogResult.OK)
                {
                    string Codigo;
                    string Rpta = "";


                    foreach (DataGridViewRow row in dataListado.Rows)
                    {

                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {

                            Codigo = Convert.ToString(row.Cells[1].Value);
                            Rpta = NCategoria.Eliminar(Convert.ToInt32(Codigo));
                            if (Rpta.Equals("OK"))
                            {

                                this.MensajeOk("Se Elimino Correctamente el registro");
                                chkEliminar.Checked = false;
                            }
                            else
                            {

                                this.MensajeError(Rpta);
                                chkEliminar.Checked = false;
                            }

                        }


                    }

                    this.Mostrar();
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }


        // fin 

    }
}
