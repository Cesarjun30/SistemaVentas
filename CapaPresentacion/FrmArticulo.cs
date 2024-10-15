using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using CapaNegocio;


namespace CapaPresentacion
{
    public partial class FrmArticulo : Form
    {

        private bool IsNuevo = false, IsEditar = false;

        private static FrmArticulo _Instancia;
        public static FrmArticulo GetInstancia()
        {
            if(_Instancia == null) 
            {
               _Instancia = new FrmArticulo();
             
            }
            return _Instancia;
        }

        public void setCategoria(string idcategoria, string nombre )
        {
            this.txtIdcategoria.Text = idcategoria;
            this.txtCategoria.Text = nombre;

        }

        public FrmArticulo()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre, "Ingrese el Nombre del Articulo");
            this.ttMensaje.SetToolTip(this.pxImagen, "Seleccione la imagen del Articulo");
            this.ttMensaje.SetToolTip(this.txtCategoria, "Seleccione la categoria del Articulo");
            this.ttMensaje.SetToolTip(this.cbIdpresentacion, "Seleccione la presentacion del Articulo");


            this.txtIdcategoria.Visible = false;
            this.txtCategoria.ReadOnly = true;
            this.LlenarComboPresentacion();
        }


        // Mostrar mensaje de Confirmacion 

        private void MensajeOk(string mensaje)
        {

            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        //Mostrar Mensaje de Error 


        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Limpiar controles del formulario

        private void Limpiar()
        {
            this.txtCodigo.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdcategoria.Text = string.Empty;
            this.txtCategoria.Text = string.Empty;
            this.txtIdArticulo.Text = string.Empty;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.file;


        }


        // Metodo para Mostrar Registros 

        private void Mostrar()
        {

            this.dataListado.DataSource = NArticulo.Mostrar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        // Habilitar los controles del Formulario 

        private void Habilitar(bool valor)
        {
            this.txtCodigo.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.btnBuscarCategoria.Enabled = valor;
            this.cbIdpresentacion.Enabled = valor;
            this.btnCargar.Enabled = valor;
            this.btnEditar.Enabled = valor;
            this.txtIdArticulo.ReadOnly = !valor;

        }



        // Procedimiento para habilitar los botones 


        private void Botones()
        {

            if (this.IsNuevo || this.IsEditar)
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;

            }
            else
            {

                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;


            }

        }
        // Metodo para ocultar columnas

        private void OcultarColumnas()
        {

            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
            this.dataListado.Columns[6].Visible = false;
            this.dataListado.Columns[8].Visible = false;

        }

        

        

        // Metodo para llenar Combobox con informacion de la Presentacion 

        private void LlenarComboPresentacion() 
        {
            cbIdpresentacion.DataSource = NPresentacion.Mostrar();
            cbIdpresentacion.ValueMember = "idpresentacion";
            cbIdpresentacion.DisplayMember = "nombre";
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK) 
            { 
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = Image.FromFile(dialog.FileName);
               
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.file;

        }

        

        private void FrmArticulo_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.CenterToScreen();
            this.Mostrar();
            this.Habilitar(true);
            this.Botones();
        }

        // Buscar 

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

       
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.BuscarNombre();
        }

        // Metodo para BuscarNombre 

        private void BuscarNombre()
        {

            this.dataListado.DataSource = NArticulo.BuscarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }

        // Nuevo 

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtNombre.Focus();
        }

       
        // Guardar 

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty || this.txtIdcategoria.Text == string.Empty || this.txtNombre.Text == string.Empty)
                {

                    MensajeError("Falta igresar algunos datos, seran remarcados");
                    errorIcono.SetError(txtNombre, "Ingrese un Valor");
                    errorIcono.SetError(txtCodigo, "Ingrese un Valor");
                    errorIcono.SetError(txtCategoria, "Ingrese un Valor");
                }
                else
                {
                        // aqui se envia una imagen asi que vamos a usar un objeto tipo memory stream 

                    System.IO.MemoryStream ms= new System.IO.MemoryStream();

                    // guardamos la imagen en el buffer

                    this.pxImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                    // obtenemos lo que esta almacendo en el buffer en una variable tipo byte.

                    byte[] imagen = ms.GetBuffer();

                    if (this.IsNuevo)
                    {
                        rpta = NArticulo.Insertar(this.txtCodigo.Text, this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim(), imagen,
                               Convert.ToInt32(this.txtIdcategoria.Text), Convert.ToInt32(this.cbIdpresentacion.SelectedValue));


                    }
                    else
                    {

                        rpta = NArticulo.Editar(Convert.ToInt32(this.txtIdArticulo.Text), this.txtCodigo.Text, this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim(), imagen,
                               Convert.ToInt32(this.txtIdcategoria.Text), Convert.ToInt32(this.cbIdpresentacion.SelectedValue));
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
                    else
                    {
                        this.MensajeError(rpta);

                    }
                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

      

        // Editar 

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtIdArticulo.Text.Equals(""))
            {

                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);

            }
            else
            {
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


        // Chk Eliminar 

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {

                this.dataListado.Columns[0].Visible = true;
            }
            else
            {

                this.dataListado.Columns[0].Visible = false;
            }
        }



        // Eliminar 

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
                            Rpta = NArticulo.Eliminar(Convert.ToInt32(Codigo));
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


        // CellContentClick 

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {

                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);

            }

        }

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            FrmVistaCategoria_Articulo form = new FrmVistaCategoria_Articulo();
            form.ShowDialog();
        }




        // Evento Double Click 

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdArticulo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idarticulo"].Value);
            this.txtCodigo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["codigo"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["descripcion"].Value);

            byte[] imagenBuffer = (byte[])this.dataListado.CurrentRow.Cells["imagen"].Value;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

            this.pxImagen.Image = Image.FromStream(ms);
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            
            this.txtIdcategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idcategoria"].Value);
            this.txtCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Categoria"].Value);
            this.cbIdpresentacion.SelectedValue = Convert.ToString(this.dataListado.CurrentRow.Cells["idpresentacion"].Value);


            this.tabControl1.SelectedIndex = 1;
        }

        //fin 
    }
}
