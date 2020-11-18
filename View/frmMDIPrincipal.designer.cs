namespace Projeem.View
{
    partial class frmMDIForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMDIForm));
            this.mNuPrincipal = new System.Windows.Forms.MenuStrip();
            this.arquivosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMontaTabela = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGerarTodosOsDebitos = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.sobreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sStrip = new System.Windows.Forms.StatusStrip();
            this.sbLblTitle = new System.Windows.Forms.ToolStripStatusLabel();
            this.sbLblVersao = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooBtn = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tooBtnImagemDeFundo = new System.Windows.Forms.ToolStripDropDownButton();
            this.fundoFixoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imagemDeFundoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.congelarImagensToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semBackGroundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblNameImg = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolCboDatabase = new System.Windows.Forms.ToolStripComboBox();
            this.toolBtnSair = new System.Windows.Forms.ToolStripButton();
            this.tmTrocaImg = new System.Windows.Forms.Timer(this.components);
            this.mNuPrincipal.SuspendLayout();
            this.sStrip.SuspendLayout();
            this.tooBtn.SuspendLayout();
            this.SuspendLayout();
            // 
            // mNuPrincipal
            // 
            this.mNuPrincipal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.arquivosToolStripMenuItem,
            this.toolStripMenuItem2});
            this.mNuPrincipal.Location = new System.Drawing.Point(0, 0);
            this.mNuPrincipal.Name = "mNuPrincipal";
            this.mNuPrincipal.Size = new System.Drawing.Size(643, 24);
            this.mNuPrincipal.TabIndex = 1;
            this.mNuPrincipal.Text = "menuStrip1";
            // 
            // arquivosToolStripMenuItem
            // 
            this.arquivosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMontaTabela,
            this.mnuGerarTodosOsDebitos,
            this.sairToolStripMenuItem});
            this.arquivosToolStripMenuItem.Name = "arquivosToolStripMenuItem";
            this.arquivosToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.arquivosToolStripMenuItem.Text = "Arquivos";
            // 
            // mnuMontaTabela
            // 
            this.mnuMontaTabela.Enabled = false;
            this.mnuMontaTabela.Name = "mnuMontaTabela";
            this.mnuMontaTabela.Size = new System.Drawing.Size(196, 22);
            this.mnuMontaTabela.Text = "Monta Tabela de Séries";
            this.mnuMontaTabela.Click += new System.EventHandler(this.mnuMontaTabela_Click);
            // 
            // mnuGerarTodosOsDebitos
            // 
            this.mnuGerarTodosOsDebitos.Name = "mnuGerarTodosOsDebitos";
            this.mnuGerarTodosOsDebitos.Size = new System.Drawing.Size(196, 22);
            this.mnuGerarTodosOsDebitos.Text = "Gerar todos os Débitos";
            this.mnuGerarTodosOsDebitos.Click += new System.EventHandler(this.mnuGerarTodosOsDebitos_Click);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.sairToolStripMenuItem.Text = "&Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sobreToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(24, 20);
            this.toolStripMenuItem2.Text = "?";
            // 
            // sobreToolStripMenuItem
            // 
            this.sobreToolStripMenuItem.Name = "sobreToolStripMenuItem";
            this.sobreToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.sobreToolStripMenuItem.Text = "Sobre";
            // 
            // sStrip
            // 
            this.sStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbLblTitle,
            this.sbLblVersao});
            this.sStrip.Location = new System.Drawing.Point(0, 318);
            this.sStrip.Name = "sStrip";
            this.sStrip.Size = new System.Drawing.Size(643, 22);
            this.sStrip.TabIndex = 4;
            this.sStrip.Text = "Versão";
            // 
            // sbLblTitle
            // 
            this.sbLblTitle.Name = "sbLblTitle";
            this.sbLblTitle.Size = new System.Drawing.Size(58, 17);
            this.sbLblTitle.Text = "sbLblTitle";
            // 
            // sbLblVersao
            // 
            this.sbLblVersao.Name = "sbLblVersao";
            this.sbLblVersao.Size = new System.Drawing.Size(70, 17);
            this.sbLblVersao.Text = "sbLblVersao";
            // 
            // tooBtn
            // 
            this.tooBtn.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.tooBtnImagemDeFundo,
            this.lblNameImg,
            this.toolStripLabel1,
            this.toolCboDatabase,
            this.toolBtnSair});
            this.tooBtn.Location = new System.Drawing.Point(0, 24);
            this.tooBtn.Name = "tooBtn";
            this.tooBtn.Size = new System.Drawing.Size(643, 25);
            this.tooBtn.TabIndex = 8;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tooBtnImagemDeFundo
            // 
            this.tooBtnImagemDeFundo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fundoFixoToolStripMenuItem,
            this.imagemDeFundoToolStripMenuItem,
            this.congelarImagensToolStripMenuItem,
            this.semBackGroundToolStripMenuItem});
            this.tooBtnImagemDeFundo.Image = ((System.Drawing.Image)(resources.GetObject("tooBtnImagemDeFundo.Image")));
            this.tooBtnImagemDeFundo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tooBtnImagemDeFundo.Name = "tooBtnImagemDeFundo";
            this.tooBtnImagemDeFundo.Size = new System.Drawing.Size(101, 22);
            this.tooBtnImagemDeFundo.Text = "BackGround";
            this.tooBtnImagemDeFundo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tooBtnImagemDeFundo.ToolTipText = "BackGround - Controle de Imagem";
            // 
            // fundoFixoToolStripMenuItem
            // 
            this.fundoFixoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("fundoFixoToolStripMenuItem.Image")));
            this.fundoFixoToolStripMenuItem.Name = "fundoFixoToolStripMenuItem";
            this.fundoFixoToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.fundoFixoToolStripMenuItem.Text = "Mostrar Logotipo";
            this.fundoFixoToolStripMenuItem.Click += new System.EventHandler(this.fundoFixoToolStripMenuItem_Click);
            // 
            // imagemDeFundoToolStripMenuItem
            // 
            this.imagemDeFundoToolStripMenuItem.Enabled = false;
            this.imagemDeFundoToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("imagemDeFundoToolStripMenuItem.Image")));
            this.imagemDeFundoToolStripMenuItem.Name = "imagemDeFundoToolStripMenuItem";
            this.imagemDeFundoToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.imagemDeFundoToolStripMenuItem.Text = "Imagem em Rotação";
            this.imagemDeFundoToolStripMenuItem.Click += new System.EventHandler(this.imagemDeFundoToolStripMenuItem_Click);
            // 
            // congelarImagensToolStripMenuItem
            // 
            this.congelarImagensToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("congelarImagensToolStripMenuItem.Image")));
            this.congelarImagensToolStripMenuItem.Name = "congelarImagensToolStripMenuItem";
            this.congelarImagensToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.congelarImagensToolStripMenuItem.Text = "Congelar Imagens";
            this.congelarImagensToolStripMenuItem.Click += new System.EventHandler(this.congelarImagensToolStripMenuItem_Click);
            // 
            // semBackGroundToolStripMenuItem
            // 
            this.semBackGroundToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("semBackGroundToolStripMenuItem.Image")));
            this.semBackGroundToolStripMenuItem.Name = "semBackGroundToolStripMenuItem";
            this.semBackGroundToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.semBackGroundToolStripMenuItem.Text = "Sem BackGround";
            this.semBackGroundToolStripMenuItem.Click += new System.EventHandler(this.semBackGroundToolStripMenuItem_Click);
            // 
            // lblNameImg
            // 
            this.lblNameImg.AutoSize = false;
            this.lblNameImg.Name = "lblNameImg";
            this.lblNameImg.Size = new System.Drawing.Size(150, 22);
            this.lblNameImg.ToolTipText = "Nome da Imagem Atual na Tela";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel1.Text = "Empresa: ";
            // 
            // toolCboDatabase
            // 
            this.toolCboDatabase.AutoSize = false;
            this.toolCboDatabase.DropDownHeight = 108;
            this.toolCboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolCboDatabase.DropDownWidth = 150;
            this.toolCboDatabase.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.toolCboDatabase.IntegralHeight = false;
            this.toolCboDatabase.Items.AddRange(new object[] {
            "newprojeem_iii_desenv",
            "newprojeem_iii_1",
            "newprojeem_iii_2",
            "newprojeem_iii_3",
            "newprojeem_iii_4",
            "newprojeem_iii_5",
            "newprojeem_iii_6",
            "newprojeem_iii_7",
            "newprojeem_iii_8"});
            this.toolCboDatabase.Name = "toolCboDatabase";
            this.toolCboDatabase.Size = new System.Drawing.Size(121, 23);
            this.toolCboDatabase.SelectedIndexChanged += new System.EventHandler(this.toolCboDatabase_SelectedIndexChanged);
            // 
            // toolBtnSair
            // 
            this.toolBtnSair.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBtnSair.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnSair.Image")));
            this.toolBtnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnSair.Name = "toolBtnSair";
            this.toolBtnSair.Size = new System.Drawing.Size(23, 22);
            this.toolBtnSair.ToolTipText = "Fechar o Aplicativo";
            this.toolBtnSair.Click += new System.EventHandler(this.toolBtnSair_Click);
            // 
            // tmTrocaImg
            // 
            this.tmTrocaImg.Interval = 3000;
            this.tmTrocaImg.Tick += new System.EventHandler(this.tmTrocaImg_Tick);
            // 
            // frmMDIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(643, 340);
            this.Controls.Add(this.tooBtn);
            this.Controls.Add(this.sStrip);
            this.Controls.Add(this.mNuPrincipal);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mNuPrincipal;
            this.Name = "frmMDIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PROJEEM - Projeto de Engenharia e Estatística Matemática";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMDIForm_Load);
            this.mNuPrincipal.ResumeLayout(false);
            this.mNuPrincipal.PerformLayout();
            this.sStrip.ResumeLayout(false);
            this.sStrip.PerformLayout();
            this.tooBtn.ResumeLayout(false);
            this.tooBtn.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mNuPrincipal;
        private System.Windows.Forms.ToolStripMenuItem arquivosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.StatusStrip sStrip;
        private System.Windows.Forms.ToolStripStatusLabel sbLblTitle;
        private System.Windows.Forms.ToolStripStatusLabel sbLblVersao;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem sobreToolStripMenuItem;
        private System.Windows.Forms.ToolStrip tooBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton tooBtnImagemDeFundo;
        private System.Windows.Forms.ToolStripMenuItem fundoFixoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagemDeFundoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem congelarImagensToolStripMenuItem;
        private System.Windows.Forms.Timer tmTrocaImg;
        private System.Windows.Forms.ToolStripLabel lblNameImg;
        private System.Windows.Forms.ToolStripMenuItem semBackGroundToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolBtnSair;
        private System.Windows.Forms.ToolStripComboBox toolCboDatabase;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem mnuMontaTabela;
        private System.Windows.Forms.ToolStripMenuItem mnuGerarTodosOsDebitos;
    }
}

