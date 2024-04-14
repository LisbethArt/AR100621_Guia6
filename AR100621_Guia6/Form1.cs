using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AR100621_Guia6.Modelo;

namespace AR100621_Guia6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Refrescar();
        }

        private void Refrescar()
        {
            using (BDD_UFGEntities db = new BDD_UFGEntities())
            {
                // Selecciona los datos disponibles en la tabla Persona
                var lista = from datos in db.Persona
                            select datos;
                // Se carga el DGV con la información recopilada desde la BD
                dgvDatos.DataSource = lista.ToList();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {   // Creamos un objeto del formulario FrmTabla
            FrmTabla frmtabla = new FrmTabla();
            // Abrimos con ShowDialog
            frmtabla.ShowDialog();
            // Refrescamos los datos
            Refrescar();
        }

        // Permite retornar entero o nulo
        private int? ObtenerId()
        {
            try
            {
                // Devolver el valor del índice de la fila seleccionada
                return int.Parse(dgvDatos.Rows[dgvDatos.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch 
            { 
                return null; 
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int? id = ObtenerId();
            if(id!= null)
            {
                FrmTabla frmtabla = new FrmTabla(id);
                frmtabla.ShowDialog();
            }
            Refrescar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? id = ObtenerId();
            if (id != null)
            {
                using (BDD_UFGEntities db = new BDD_UFGEntities())
                {
                    Persona personas = db.Persona.Find(id);
                    db.Persona.Remove(personas);

                    db.SaveChanges();
                }
            }
            Refrescar();
        }
    }
}
