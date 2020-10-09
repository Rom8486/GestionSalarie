using SalariesDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilitaires;

namespace GestionSalaraies
{
    public partial class GestionDesSalaries : Form
    {
        ErrorProvider erreurCb = new ErrorProvider();
        Salaries salaries;
        Salarie salarie;
        Role role;
        Commercial commercial;

        enum Contextes
        {
            Initial = 0,
            Consultation = 1,
            ModificationInitiale = 2,
            ModificationAnnuler = 3,
            ModificationValider = 4,
            AjoutInitial = 5,
            AjoutValider = 6
        }

        public GestionDesSalaries()
        {

            InitializeComponent();
            ChargerSalaries();
            //salaries.Remove("12ytr98");
            //  salaries.Remove("12ytr98");
            GestionnaireContextes(Contextes.Initial);

            #region GestionnaireContextes
        }
        void GestionnaireContextes(Contextes contexte)

        {

            switch (contexte)
            {
                case Contextes.Initial:
                    cbChoixSalarie.Enabled = true;
                    GBSalarie.Enabled = false;
                    GBSalarie.Visible = false;
                    groupBoxCommercial.Enabled = false;
                    checkBoxCommercial.Checked = false;
                    checkBoxCommercial.Enabled = false;

                    break;
                case Contextes.Consultation:
                    btnAjouter.Enabled = true;
                    GBSalarie.Enabled = true;
                    GBSalarie.Visible = true;
                    textBoxMatricule.ReadOnly = true;
                    textBoxDateDeNaissance.ReadOnly = true;
                    textBoxNom.ReadOnly = true;
                    cbChoixRole.Enabled = true;
                    BtnAnnuler.Enabled = false;
                    btnValider.Enabled = false;
                    textBoxSalaireBrut.ReadOnly = true;
                    textBoxTauxCS.ReadOnly = true;
                    textBoxPrenom.ReadOnly = true;

                    break;
                case Contextes.ModificationInitiale:
                    GBSalarie.Enabled = true;
                    GBSalarie.Visible = true;
                    btnAjouter.Enabled = true;
                    textBoxMatricule.ReadOnly = true;
                    textBoxDateDeNaissance.ReadOnly = true;
                    textBoxNom.ReadOnly = true;
                    cbChoixRole.Enabled = true;

                    BtnAnnuler.Enabled = true;
                    btnValider.Enabled = true;
                    textBoxPrenom.ReadOnly = true;
                    textBoxSalaireBrut.ReadOnly = false;
                    textBoxTauxCS.ReadOnly = false;
                    break;
                case Contextes.ModificationAnnuler:
                    GestionnaireContextes(Contextes.Consultation);

                    break;
                case Contextes.ModificationValider:
                    GBSalarie.Enabled = true;
                    GBSalarie.Visible = true;
                    btnAjouter.Enabled = true;
                    textBoxMatricule.ReadOnly = true;
                    textBoxDateDeNaissance.ReadOnly = true;
                    textBoxNom.ReadOnly = true;
                    cbChoixRole.Enabled = true;
                    BtnAnnuler.Enabled = true;
                    btnValider.Enabled = true;
                    textBoxPrenom.ReadOnly = true;
                    textBoxSalaireBrut.ReadOnly = false;
                    textBoxTauxCS.ReadOnly = false;
                    break;
                case Contextes.AjoutInitial:
                    GBSalarie.Enabled = true;
                    GBSalarie.Visible = true;
                    btnAjouter.Enabled = true;
                    textBoxMatricule.ReadOnly = false;
                    textBoxDateDeNaissance.ReadOnly = false;
                    textBoxNom.ReadOnly = false;
                    cbChoixRole.Enabled = true;


                    BtnAnnuler.Enabled = true;
                    btnValider.Enabled = true;
                    textBoxPrenom.ReadOnly = false;
                    textBoxSalaireBrut.ReadOnly = false;
                    textBoxTauxCS.ReadOnly = false;
                    textBoxMatricule.Clear();
                    textBoxTauxCS.Clear();
                    textBoxPrenom.Clear();
                    textBoxNom.Clear();
                    textBoxSalaireBrut.Clear();
                    textBoxDateDeNaissance.Clear();
                    cbChoixSalarie.ResetText();
                    break;
                case Contextes.AjoutValider:

                    break;
                default:
                    break;

            }


        }
        #endregion 

        private void ChargerSalaries()
        {
            salaries = new Salaries();
            ISauvegarde serialiseur = MonApplication.DispositifSauvegarde;
            salaries.Load(serialiseur, Properties.Settings.Default.AppData);
            foreach (Salarie item in salaries)
            {
                cbChoixSalarie.Items.Add(item.Matricule);
            }
        }
        #region ChargerValeursSalarie
        private void ChargerValeursSalaries()
        {

            textBoxMatricule.Text = salarie.Matricule;
            textBoxNom.Text = salarie.Nom;
            textBoxPrenom.Text = salarie.Prenom;
            textBoxSalaireBrut.Text = salarie.SalaireBrut.ToString();
            textBoxDateDeNaissance.Text = salarie.DateNaissance.ToString("D");
            textBoxTauxCS.Text = salarie.TauxCS.ToString();
            groupBoxCommercial.Enabled = false;
            checkBoxCommercial.Enabled = false;
            textBoxChiffreAffaire.ReadOnly = true;
            textBoxCommission.ReadOnly = true;

            if (!(salarie is Commercial))
            {
                groupBoxCommercial.Enabled = false;
                checkBoxCommercial.Checked = false;
                textBoxChiffreAffaire.Clear();
                textBoxCommission.Clear();
            }

            if (salarie is Commercial)
            {
                Commercial commercial = (Commercial)salarie;

                checkBoxCommercial.Checked = true;

                textBoxChiffreAffaire.Text = commercial.ChiffreAffaire.ToString();
                textBoxCommission.Text = commercial.Commission.ToString();

            }
        }
        #endregion

        private void cbChoixSalarie_SelectedIndexChanged(object sender, EventArgs e)
        {
            erreurCb.SetError(cbChoixSalarie, null);

            salarie = salaries.ExtraireSalarie(cbChoixSalarie.Items[cbChoixSalarie.SelectedIndex].ToString());


            ChargerValeursSalaries();
            GestionnaireContextes(Contextes.Consultation);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            GestionnaireContextes(Contextes.AjoutInitial);
            checkBoxCommercial.Enabled = true;
            textBoxCommission.Clear();
            textBoxChiffreAffaire.Clear();
            checkBoxCommercial.Checked = false;
        }


        #region BtnValider

        private void btnValider_Click(object sender, EventArgs e)
        {
            if (!VerifValiditeSalarie())
            {
                MessageBox.Show("Cerains champs ne sont pas valides", "Attention", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
            }

            if (!(salaries.Contains(salarie)))
            {
                AjouterSalaries();
            }


            bool conversionSalaire = decimal.TryParse(textBoxSalaireBrut.Text, out decimal SalaireConverti);
            if (!conversionSalaire)
            {
                erreurCb.SetError(textBoxSalaireBrut, "format non valide");
            }
            bool ConvertionTauxCS = decimal.TryParse(textBoxTauxCS.Text, out decimal TauxCSConverti);
            if (!ConvertionTauxCS)
            {
                erreurCb.SetError(textBoxTauxCS, "format non valide");
            }



            else if (salaries.Contains(salarie) && ((salarie.SalaireBrut != SalaireConverti) || (salarie.TauxCS != TauxCSConverti)))
            {

                modifierSalarie();
            }

            GestionnaireContextes(Contextes.AjoutInitial);
        }
        #endregion

        #region AjouterSalarie
        public void AjouterSalaries()
        {


            if (!(checkBoxCommercial.Checked))
            {
                Salarie salarie = new Salarie();
                if (Salarie.IsNomPrenomValide(textBoxNom.Text))
                {
                    salarie.Nom = textBoxNom.Text;
                    erreurCb.SetError(textBoxNom, "");
                }
                else
                {

                    erreurCb.SetError(textBoxNom, "Nom non valide");
                }
                if (Salarie.IsNomPrenomValide(textBoxPrenom.Text))
                {
                    salarie.Prenom = textBoxPrenom.Text;
                    erreurCb.SetError(textBoxPrenom, "");
                }
                else
                {
                    erreurCb.SetError(textBoxPrenom, "Prenom non valide");
                }


                bool ConvertionDate = DateTime.TryParse(textBoxDateDeNaissance.Text, out DateTime DateNaissanceConvertie);
                if (ConvertionDate == true && Salarie.IsDateNaissanceValide(DateNaissanceConvertie))
                {
                    salarie.DateNaissance = DateNaissanceConvertie;
                    erreurCb.SetError(textBoxDateDeNaissance, "");
                }
                else
                {
                    erreurCb.SetError(textBoxDateDeNaissance, "Date de naissance non valide");
                }
                if (Salarie.IsMatriculeValide(textBoxMatricule.Text))
                {
                    salarie.Matricule = textBoxMatricule.Text;
                    erreurCb.SetError(textBoxNom, "");
                }

                bool ConvertionSalaire = decimal.TryParse(textBoxSalaireBrut.Text, out decimal SalaireBrutConverti);
                if (ConvertionSalaire == true)
                {
                    salarie.SalaireBrut = SalaireBrutConverti;
                    erreurCb.SetError(textBoxSalaireBrut, "");
                }
                else
                {
                    erreurCb.SetError(textBoxSalaireBrut, "Format Salaire non valide");
                }
                bool ConvertionTauxCS = decimal.TryParse(textBoxTauxCS.Text, out decimal TauxCSConverti);
                if (ConvertionTauxCS == true && Salarie.IsTauxCSValid(TauxCSConverti))
                {
                    salarie.TauxCS = TauxCSConverti;
                    erreurCb.SetError(textBoxTauxCS, "");
                }
                else
                {
                    erreurCb.SetError(textBoxTauxCS, "TauxCS non valide");
                }

                if (!VerifValiditeSalarie())
                {
                    MessageBox.Show("Certains champs ne sont pas valides", "Erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Salarié crée avec succée", "Ajout", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }

                salaries.Add(salarie);
                cbChoixSalarie.Items.Add(salarie.Matricule);
                salaries.Save(MonApplication.DispositifSauvegarde, Properties.Settings.Default.AppData);
            }
            else if (checkBoxCommercial.Checked)
            {
                Commercial commercial = new Commercial();


                textBoxChiffreAffaire.ReadOnly = false;
                textBoxCommission.ReadOnly = false;
                bool converstionCA = decimal.TryParse(textBoxChiffreAffaire.Text, out decimal ChiffreAfConverti);
                if (!converstionCA)
                {
                    MessageBox.Show("Entrer un decimal", "Message d'erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                commercial.ChiffreAffaire = ChiffreAfConverti;
                bool convertionCom = decimal.TryParse(textBoxCommission.Text, out decimal ComConvertie);
                if (!convertionCom)
                {
                    MessageBox.Show("Entrer un decimal entre 0 et 0.6", "Message d'erreur", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                commercial.Commission = ComConvertie;

                if (Salarie.IsNomPrenomValide(textBoxNom.Text))
                {
                    commercial.Nom = textBoxNom.Text;
                    erreurCb.SetError(textBoxNom, "");
                }
                else
                {

                    erreurCb.SetError(textBoxNom, "Nom non valide");
                }
                if (Salarie.IsNomPrenomValide(textBoxPrenom.Text))
                {
                    commercial.Prenom = textBoxPrenom.Text;
                    erreurCb.SetError(textBoxPrenom, "");
                }
                else
                {
                    erreurCb.SetError(textBoxPrenom, "Prenom non valide");
                }


                bool ConvertionDateCom = DateTime.TryParse(textBoxDateDeNaissance.Text, out DateTime DateNaissanceConvertieCom);
                if (ConvertionDateCom == true && Salarie.IsDateNaissanceValide(DateNaissanceConvertieCom))
                {
                    commercial.DateNaissance = DateNaissanceConvertieCom;
                    erreurCb.SetError(textBoxDateDeNaissance, "");
                }
                else
                {
                    erreurCb.SetError(textBoxDateDeNaissance, "Date de naissance non valide");
                }
                if (Salarie.IsMatriculeValide(textBoxMatricule.Text))
                {
                    commercial.Matricule = textBoxMatricule.Text;
                    erreurCb.SetError(textBoxNom, "");
                }

                bool ConvertionSalaireCom = decimal.TryParse(textBoxSalaireBrut.Text, out decimal SalaireBrutConvertiCom);
                if (ConvertionSalaireCom == true)
                {
                    commercial.SalaireBrut = SalaireBrutConvertiCom;
                    erreurCb.SetError(textBoxSalaireBrut, "");
                }
                else
                {
                    erreurCb.SetError(textBoxSalaireBrut, "Format Salaire non valide");
                }
                bool ConvertionTauxCSCom = decimal.TryParse(textBoxTauxCS.Text, out decimal TauxCSConvertiCom);
                if (ConvertionTauxCSCom == true && Salarie.IsTauxCSValid(TauxCSConvertiCom))
                {
                    commercial.TauxCS = TauxCSConvertiCom;
                    erreurCb.SetError(textBoxTauxCS, "");
                }
                else
                {
                    erreurCb.SetError(textBoxTauxCS, "TauxCS non valide");
                }
                salaries.Add(commercial);
                cbChoixSalarie.Items.Add(commercial.Matricule);
                salaries.Save(MonApplication.DispositifSauvegarde, Properties.Settings.Default.AppData);
                checkBoxCommercial.Checked = false;
                textBoxChiffreAffaire.Clear();
                textBoxCommission.Clear();
            }

            textBoxDateDeNaissance.Clear();
            textBoxTauxCS.Clear();
            textBoxMatricule.Clear();
            textBoxNom.Clear();
            textBoxPrenom.Clear();
            textBoxSalaireBrut.Clear();




        }
        #endregion
        private void btnModifier_Click(object sender, EventArgs e)
        {

            GestionnaireContextes(Contextes.ModificationInitiale);
            checkBoxCommercial.Enabled = true;
            groupBoxCommercial.Enabled = true;
            textBoxChiffreAffaire.ReadOnly = false;
            textBoxCommission.ReadOnly = false;

            textBoxSalaireBrut.Focus();
            if (cbChoixSalarie.SelectedItem == null)
            {

                erreurCb.SetError(cbChoixSalarie, "Veuillez selectionner un salarié.");
            }


        }
        #region FonctionVerification Validite
        public bool VerifValiditeSalarie()
        {
           
            Salarie salarie = new Salarie();
            if (!Salarie.IsNomPrenomValide(textBoxNom.Text))
            {
                return false;
            }

            if (!Salarie.IsNomPrenomValide(textBoxPrenom.Text))
            {
                return false;
            }
            bool ConvertionDate = DateTime.TryParse(textBoxDateDeNaissance.Text, out DateTime DateNaissanceConvertie);
            if (!ConvertionDate == true && Salarie.IsDateNaissanceValide(DateNaissanceConvertie))
            {
                return false;
            }
            if (!Salarie.IsMatriculeValide(textBoxMatricule.Text))
            {
                return false;
            }
            bool ConvertionSalaire = decimal.TryParse(textBoxSalaireBrut.Text, out decimal SalaireBrutConverti);
            if (!ConvertionSalaire == true)
            {
                return false;
            }
            bool ConvertionTauxCS = decimal.TryParse(textBoxTauxCS.Text, out decimal TauxCSConverti);
            if (!(ConvertionTauxCS == true && Salarie.IsTauxCSValid(TauxCSConverti)))
            {
                return false;
            }
            return true;
        }
        #endregion

        #region ModifierSalarie
        public void modifierSalarie()
        {

            salarie = salaries.ExtraireSalarie(cbChoixSalarie.Items[cbChoixSalarie.SelectedIndex].ToString());
            salaries.Remove(salarie);
            bool conversionSalaire = decimal.TryParse(textBoxSalaireBrut.Text, out decimal SalaireConverti);
            bool ConvertionTauxCS = decimal.TryParse(textBoxTauxCS.Text, out decimal TauxCSConverti);

            if (!conversionSalaire)
            {
                erreurCb.SetError(textBoxSalaireBrut, "Veuillez saisir un decimal");
            }
            else
            {
                salarie.SalaireBrut = SalaireConverti;
                erreurCb.SetError(textBoxSalaireBrut, "");
            }


            if (!ConvertionTauxCS)
            {
                erreurCb.SetError(textBoxTauxCS, "Veuillez saisir un decimal compris entre 0 et 0.6");
            }
            else
            {
                salarie.TauxCS = TauxCSConverti;
                erreurCb.SetError(textBoxTauxCS, "");
            }

            if (!VerifValiditeSalarie())
            {
                MessageBox.Show("Certains champs ne sont pas valides");
            }
            else
            {
                MessageBox.Show("Salarié modifié");
            }



            salaries.Add(salarie);
            salaries.Save(MonApplication.DispositifSauvegarde, Properties.Settings.Default.AppData);
            textBoxDateDeNaissance.Clear();
            textBoxTauxCS.Clear();
            textBoxMatricule.Clear();
            textBoxNom.Clear();
            textBoxPrenom.Clear();
            textBoxSalaireBrut.Clear();

          

        }
        #endregion

        private void BtnAnnuler_Click(object sender, EventArgs e)
        {
            GestionnaireContextes(Contextes.ModificationAnnuler);

            // après avoir cliqué sur le bouton annulé, il n'y a pas de remise à 0 des valeurs
        }

        private void checkBoxCommercial_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCommercial.Checked)
            {
                groupBoxCommercial.Enabled = true;

            }
            else if (!checkBoxCommercial.Checked)
            {
                groupBoxCommercial.Enabled = true;
                textBoxChiffreAffaire.Clear();
                textBoxCommission.Clear();
            }


        }

        #region ValidatingChamps
        void textBoxMatricule_Validating(object sender, CancelEventArgs e)
        {
            if (!(Salarie.IsMatriculeValide(textBoxMatricule.Text)))
            {
                erreurCb.SetError(textBoxMatricule, "Matricule non valide");
            }
            else
            {
                erreurCb.SetError(textBoxMatricule, "");
            }
        }

      

        private void textBoxNom_Validating(object sender, CancelEventArgs e)
        {
            if (Salarie.IsNomPrenomValide(textBoxNom.Text))
            {
                erreurCb.SetError(textBoxNom, "");
            }
            else
            {
                erreurCb.SetError(textBoxNom, "Erreur format");
            }

        }
        #endregion



        //private void textBoxChiffreAffaire_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void textBoxCommission_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void groupBoxCommercial_Enter(object sender, EventArgs e)
        //{

        //}



        //private void textBoxTauxCS_Click(object sender, EventArgs e)
        //{
        //    erreurCb.SetError(textBoxTauxCS, "");
        //}

        //private void textBoxSalaireBrut_Click(object sender, EventArgs e)
        //{
        //    erreurCb.SetError(textBoxSalaireBrut, "");
        //}


        //private void textBoxDateDeNaissance_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void textBoxTauxCS_TextChanged(object sender, EventArgs e)
        //{

        //}




        //private void textBoxNom_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void cbChoixRole_SelectedIndexChanged(object sender, EventArgs e)
        //{

        //}

        //private void checkBoxCompteBloque_CheckedChanged(object sender, EventArgs e)
        //{

        //}

        //private void textBoxMatricule_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void textBoxPrenom_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void textBoxSalaireBrut_TextChanged(object sender, EventArgs e)
        //{

        //}


    }
}
