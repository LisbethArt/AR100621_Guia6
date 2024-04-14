using AR100621_Guia6.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AR100621_Guia6
{
    public partial class FrmTabla : Form
    {
        public int? id;
        Persona personas = null;

        public FrmTabla(int? id = null)
        {
            InitializeComponent();
            this.id = id;
            if(id!=null)
            {
                CargarDatos();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            using(BDD_UFGEntities db = new BDD_UFGEntities())
            {
                // Creamos un objeto del tipo de la tabla de la base
                if (id == null)
                {
                    personas = new Persona();
                }
                // Lo llenamos con la información desde el formulario
                personas.id = int.Parse(txtId.Text);
                personas.nombre = txtNombre.Text;
                personas.correo = txtCorreo.Text;
                personas.fecha_nacimiento = dtpFecha.Value;

                if (id == null)
                {
                    // Agregamos ese nuevo objeto a la entidad
                    db.Persona.Add(personas);
                }
                else
                {
                    db.Entry(personas).State = System.Data.Entity.EntityState.Modified;
                }
                // Guardamos cambios en la base
                db.SaveChanges();
                this.Close();
            }
        }

        private void CargarDatos()
        {
            using(BDD_UFGEntities db= new BDD_UFGEntities()) 
            {
                personas = db.Persona.Find(id);
                txtId.Text = personas.id.ToString();
                txtNombre.Text = personas.nombre;
                txtCorreo.Text = personas.correo;
                dtpFecha.Value = (DateTime)personas.fecha_nacimiento;
            }
        }
    }
}
