using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionSalaraies
{
    public partial class FormAccueilMDI : Form
    {
        public FormAccueilMDI()
        {
            InitializeComponent();
        }

        private void ouvrirGestionSalaries_Click(object sender, EventArgs e)
        {
            GestionDesSalaries child = new GestionDesSalaries();
            child.MdiParent = this;
            child.Show();
        }

        private void ouvrirGestionUtilisateurs_Click(object sender, EventArgs e)
        {
            FrmUtilisateurs child = new FrmUtilisateurs();
            child.MdiParent = this;
            child.Show();
        }
    }
}
