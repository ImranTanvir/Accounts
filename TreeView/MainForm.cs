using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounts
{
    public partial class MainForm : Form
    {
        Form frmAccounts;
        Form ApplicationForms;
        public MainForm()
        {
            InitializeComponent();
        }

        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chartOfAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmAccounts == null)
            {
                frmAccounts = new Form();
                frmAccounts.MdiParent = this;
                frmAccounts.FormClosed += frmAccounts_FormClosed;
                frmAccounts.Show();
            }
            else
            {
                frmAccounts.Activate();
            }
        }
        private void frmAccounts_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmAccounts = null;
            // throw new NotImplementedException();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void applicationFormsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ApplicationForms == null)
            {
                ApplicationForms = new ApplicationForms();
                ApplicationForms.MdiParent = this;
                ApplicationForms.FormClosed += ApplicationForms_FormClosed;
                ApplicationForms.Show();
            }
            else
            {
                ApplicationForms.Activate();
            }
        }
        private void ApplicationForms_FormClosed(object sender, FormClosedEventArgs e)
        {
            ApplicationForms = null;
            // throw new NotImplementedException();
        }
    }
}
