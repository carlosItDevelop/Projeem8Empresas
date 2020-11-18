using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using Projeem.Controller;
using System.Configuration;

namespace Projeem.View
{

    public partial class frmMDIForm : Form
    {

        private string vServer = "";

        public string VServer
        {
            get { return vServer; }
            set { vServer = value; }
        }

        private const MessageBoxButtons cOk = MessageBoxButtons.OK;
        private const MessageBoxIcon cInfo = MessageBoxIcon.Information;
        private const MessageBoxIcon cErro = MessageBoxIcon.Error;


        SqlConnection cn;
        ArrayList listaImg = new ArrayList();
        public frmMDIForm()
        {
            vServer = ConfigurationManager.AppSettings["servidor"];

            cn = new SqlConnection();
            InitializeComponent();
            sbLblTitle.Text  = "   PROJEEM III";
            sbLblVersao.Text = " - Versão: " + Application.ProductVersion + " - By: COOPERCHIP - Soluções em Infomática Ltda.";


            int ano = DateTime.Now.Year;
            string mes = DateTime.Now.Month.ToString();
            if ( int.Parse(mes) < 10){
                mes = "0" + mes;
            }
            CSharpUtil.Util.Referencia = ano.ToString() + mes;
            CSharpUtil.Util.User = "CAS";
            CSharpUtil.Util.Date = DateTime.Now.ToShortDateString();

        }


        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void frmMDIForm_Load(object sender, EventArgs e)
        {
            // Preencher um ArrayList com as imagnes do diretório
            // AppSetupPath + @"\img\";
            string diretorio = Application.StartupPath + @"\img";
            DirectoryInfo dir = new DirectoryInfo(diretorio);
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                listaImg.Add(file);
            }
            tmTrocaImg.Enabled = false;
            this.MaximizeBox = false;
        }




        private void fundoFixoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fundoFixoToolStripMenuItem.Enabled = false;
            imagemDeFundoToolStripMenuItem.Enabled = true;
            congelarImagensToolStripMenuItem.Enabled = false;
            tmTrocaImg.Enabled = false;
            this.BackgroundImage = Image.FromFile(Application.StartupPath + @"\img\logo.jpg");
            lblNameImg.Text = ".: [ " + "LOGO.JPG" + " ] :.";
        }

        private void imagemDeFundoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fundoFixoToolStripMenuItem.Enabled = true;
            imagemDeFundoToolStripMenuItem.Enabled = false;
            congelarImagensToolStripMenuItem.Enabled = true;
            tmTrocaImg.Enabled = true;
        }

        private void congelarImagensToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fundoFixoToolStripMenuItem.Enabled = true;
            imagemDeFundoToolStripMenuItem.Enabled = true;
            congelarImagensToolStripMenuItem.Enabled = false;
            tmTrocaImg.Enabled = false;
        }

        int num_img = 0;
        string fileImg = null;
        string pathImg = Application.StartupPath + @"\img";
        private void tmTrocaImg_Tick(object sender, EventArgs e)
        {
            num_img = (++CSharpUtil.Util.NumImg);
            if (num_img >= listaImg.Count)
            {
                num_img = CSharpUtil.Util.NumImg = 0;
            }
            fileImg = listaImg[num_img].ToString();
            // Garante que só vai Mostrar arquivos .jpg
            if (fileImg.ToLower().Substring(fileImg.Length - 3) == "jpg")
            {
                this.BackgroundImage = Image.FromFile(pathImg + "\\" + fileImg);
                lblNameImg.Text = ".: [ " + fileImg.ToUpper() + " ] :.";
            }
        }

        private void semBackGroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmTrocaImg.Enabled = false;
            this.BackgroundImage = null;
        }


        private void toolBtnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void toolCboDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection cboCn = null;
            cboCn = new SqlConnection("Data Source="+ VServer +";Initial Catalog=" + toolCboDatabase.Text + ";Integrated Security=True;");
            cboCn.Open();

            Indices.dbInUse = toolCboDatabase.Text;
            mnuMontaTabela.Enabled = true;


            frmAplicaDebitoECredito f = new frmAplicaDebitoECredito(cboCn);
            f.MdiParent = this;
            f.Show();
        }

        private void mnuMontaTabela_Click(object sender, EventArgs e)
        {
            //SqlConnection cn = new SqlConnection("Data Source="+ VServer +";Initial Catalog=" + Indices.dbInUse + ";Integrated Security=True;");
            //try
            //{
            //    cn.Open();

            //    int vCount = 0;
            //    for (int j = 1; j <= 23; j++)
            //    {
            //        for (int m = 2; m <= 24; m++)
            //        {
            //            for (int n = 3; n <= 25; n++)
            //            {
            //                if (n > m && m > j)
            //                {
            //                    vCount++;
            //                    Funcoes.InsereRegistro(j, m, n, vCount, cn);
            //                }
            //            }
            //        }
            //    }
            //    cn.Dispose();
            //    cn.Close();
            //    CSharpUtil.Util.Msg("Registros Inseridos Com Sucesso!!!");
            //} catch (Exception ex){
            //    CSharpUtil.Util.Msg(ex.Message);
            //}
        }

        private void mnuGerarTodosOsDebitos_Click(object sender, EventArgs e)
        {
            string[] db = new string[8] { "newprojeem_iii_8", "newprojeem_iii_7", "newprojeem_iii_6", "newprojeem_iii_5", 
                                          "newprojeem_iii_4", "newprojeem_iii_3", "newprojeem_iii_2", "newprojeem_iii_1" };
            SqlConnection cn;
            for (int i = 0; i < 8; i++) {
                cn = new SqlConnection("Data Source=" + VServer + ";Initial Catalog=" + db[i] + ";Integrated Security=True;");
                cn.Open();
                Indices.dbInUse = db[i];
                frmAplicaDebitoECredito frm = new frmAplicaDebitoECredito(cn);
                frm.MdiParent = this;
                frm.Show();                
            }
        }



    }
} // Fim da classe e do Namespace