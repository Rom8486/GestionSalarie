namespace GestionSalaraies
{
    partial class FormAccueilMDI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.gestionDesSalariésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirGestionSalaries = new System.Windows.Forms.ToolStripMenuItem();
            this.gestionDesUtilisateursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ouvrirGestionUtilisateurs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestionDesSalariésToolStripMenuItem,
            this.gestionDesUtilisateursToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(629, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // gestionDesSalariésToolStripMenuItem
            // 
            this.gestionDesSalariésToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirGestionSalaries});
            this.gestionDesSalariésToolStripMenuItem.Name = "gestionDesSalariésToolStripMenuItem";
            this.gestionDesSalariésToolStripMenuItem.Size = new System.Drawing.Size(121, 20);
            this.gestionDesSalariésToolStripMenuItem.Text = "Gestion des salariés";
            // 
            // ouvrirGestionSalaries
            // 
            this.ouvrirGestionSalaries.Name = "ouvrirGestionSalaries";
            this.ouvrirGestionSalaries.Size = new System.Drawing.Size(107, 22);
            this.ouvrirGestionSalaries.Text = "Ouvrir";
            this.ouvrirGestionSalaries.Click += new System.EventHandler(this.ouvrirGestionSalaries_Click);
            // 
            // gestionDesUtilisateursToolStripMenuItem
            // 
            this.gestionDesUtilisateursToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ouvrirGestionUtilisateurs});
            this.gestionDesUtilisateursToolStripMenuItem.Name = "gestionDesUtilisateursToolStripMenuItem";
            this.gestionDesUtilisateursToolStripMenuItem.Size = new System.Drawing.Size(141, 20);
            this.gestionDesUtilisateursToolStripMenuItem.Text = "Gestion des Utilisateurs";
            // 
            // ouvrirGestionUtilisateurs
            // 
            this.ouvrirGestionUtilisateurs.Name = "ouvrirGestionUtilisateurs";
            this.ouvrirGestionUtilisateurs.Size = new System.Drawing.Size(107, 22);
            this.ouvrirGestionUtilisateurs.Text = "Ouvrir";
            this.ouvrirGestionUtilisateurs.Click += new System.EventHandler(this.ouvrirGestionUtilisateurs_Click);
            // 
            // FormAccueilMDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 628);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.Location = new System.Drawing.Point(100, 100);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormAccueilMDI";
            this.Text = "FormAccueilMDI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem gestionDesSalariésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestionDesUtilisateursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ouvrirGestionSalaries;
        private System.Windows.Forms.ToolStripMenuItem ouvrirGestionUtilisateurs;
    }
}