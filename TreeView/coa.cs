using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TreeView
{
    public partial class frmAccounts : Form
    {
        TreeNode _selectedNode = null;

        DataTable _acountsTb1 = null;
        DataTable _acountsTb2 = null;
        DataTable _acountsTb3 = null;
        DataTable _acountsTb4= null;
        DataTable _acountsTb5 = null;
        DataTable _acountstbl1 = null;

        SqlConnection _connection;
        SqlCommand _command;

        SqlDataAdapter _adapter1;
        SqlDataAdapter _adapter2;
        SqlDataAdapter _adapter3;
        SqlDataAdapter _adapter4;
        SqlDataAdapter _adapter5;
        SqlDataAdapter _adapterl1;
        SqlDataAdapter _adapterl2;
        SqlDataAdapter _adapterl3;
        SqlDataAdapter _adapterl4;
        string level;

        string cs = ConfigurationManager.ConnectionStrings["accounts"].ConnectionString;
        public frmAccounts()
        {
            InitializeComponent();
        }



        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode==null)
            {
                MessageBox.Show("Please Select Account!");
            }
            else
            {
                _selectedNode = treeView1.SelectedNode;
                ShowNodeData(_selectedNode);
            }
        }
        private void atThisLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            string levelcode;
            levelcode = treeView1.SelectedNode.Text;
            string part = levelcode.Substring(0, levelcode.IndexOf(" -"));
            if (part.Length == 2)
            {
                String sqll1 = "select  top 1 level1code from coa_level1 order by coa_level1.level1code desc";
                try
                {
                    int l1c = 0;
                    DataTable _acountstbl1 = new DataTable();
                    _connection.Open();
                    _adapterl1 = new SqlDataAdapter(sqll1, _connection);
                    _adapterl1.Fill(_acountstbl1);
                    if (_acountstbl1.Rows.Count>0)
                    {
                        //MessageBox.Show(_acountstbl1.Rows[0]["level1code"].ToString());
                        //MessageBox.Show("level1");
                        l1c = int.Parse(_acountstbl1.Rows[0]["level1code"].ToString());
                    }
                    else
                    {
                        l1c = 0;
                    }
                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9)
                    {
                        txtCode.Text = l1c1.ToString();
                    }
                    else if (l1c1 > 99)
                    {
                        MessageBox.Show("List if already full...");
                        return;
                    }
                    else
                    {
                        txtCode.Text =  "0" + (string)l1c1.ToString();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }               
                level = "1";
                btnupdate.Enabled = false;
                cmdSave.Enabled=true;
            }
            else if (part.Length == 6)
            {                
                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(3,3);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1first;
                String sqll1 = "select  top 1 level2code from coa_level2 Where level1code='"+ level1first + "' order by level2code desc";
                try
                {
                    int l1c = 0;
                    DataTable _acountstbl1 = new DataTable();
                    _connection.Open();
                    _adapterl1 = new SqlDataAdapter(sqll1, _connection);
                    _adapterl1.Fill(_acountstbl1);
                    if (_acountstbl1.Rows.Count>0)
                    {
                        //MessageBox.Show(_acountstbl1.Rows[0]["level2code"].ToString());
                        String pp = _acountstbl1.Rows[0]["level2code"].ToString();
                        //MessageBox.Show("level2");
                        String part4 = pp.Substring(2,3);
                        l1c = int.Parse(part4);
                    }
                    else
                    {
                        l1c = 0;
                    }

                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9 && l1c1<=99)
                    {
                        txtCode.Text = level1first + "-0"+ l1c1.ToString();
                    }
                    else if (l1c1 >99 && l1c1 <= 999)
                    {
                        txtCode.Text = level1first + "-" +l1c1.ToString();
                    }
                    else if (l1c1 > 999)
                    {
                        MessageBox.Show("List if already full...");
                        return;
                    }
                    else
                    {
                        txtCode.Text = level1first + "-00" + (string)l1c1.ToString();
                    }
                    string txtcode = txtCode.Text;
                    string newlevel1code = txtcode.Replace("-", ""); // .Substring(txtcode.IndexOf('-') + 0);
                    txtCode1.Text = newlevel1code;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                level = "2";
                btnupdate.Enabled = false;
                cmdSave.Enabled = true;
            }
            else if (part.Length == 10)
            {
                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(3,3);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1first.Replace("-","");
                String sqll2 = "select  top 1 level2code,level3code from coa_level3 Where level2code='" + txtCode2.Text + "' order by level3code desc";
                try
                {
                    int l1c = 0;
                    DataTable _acountstbl2 = new DataTable();
                    _connection.Open();
                    _adapterl2 = new SqlDataAdapter(sqll2, _connection);
                    _adapterl2.Fill(_acountstbl2);
                    if (_acountstbl2.Rows.Count>0)
                    {
                        //MessageBox.Show(_acountstbl2.Rows[0]["level2code"].ToString());
                        String pp = _acountstbl2.Rows[0]["level3code"].ToString();
                        //MessageBox.Show("level3");
                        //String part4 = pp.Substring(0, level1full.Length - 0);
                        String part4 = pp.Substring(5);
                        l1c = int.Parse(part4);
                    }
                    else
                    {
                        l1c = 0;
                    }

                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9 && l1c1 <= 99)
                    {
                        String modified = level1first.Insert(2, "-");
                        txtCode.Text = level1first + "-0" + l1c1.ToString();
                    }
                    else if (l1c1 > 99 && l1c1 <= 999)
                    {
                        String modified = level1first.Insert(2, "-");
                        txtCode.Text = level1first + "-" + l1c1.ToString();
                    }
                    else if (l1c1 > 999)
                    {
                        MessageBox.Show("List if already full...");
                        return;
                    }
                    else
                    {
                        String modified = level1first.Insert(2, "-");
                        //String modified1 = modified.Insert(6, "-");
                        txtCode.Text = level1first + "-00" + (string)l1c1.ToString();
                    }
                    string txtcode = txtCode.Text;
                    string newlevel1code = txtcode.Replace("-", ""); // .Substring(txtcode.IndexOf('-') + 0);
                    txtCode1.Text = newlevel1code;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                level = "3";
                btnupdate.Enabled = false;
                cmdSave.Enabled = true;
            }
            else if (part.Length == 14)
            {
                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(3,3);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1first.Replace("-","");
                String sqll2 = "select  top 1 level3code,level4code from coa_level4 Where level3code='" + txtCode2.Text + "' order by level4code desc";
                try
                {
                    int l1c = 0;
                    DataTable _acountstbl3 = new DataTable();
                    _connection.Open();
                    _adapterl3 = new SqlDataAdapter(sqll2, _connection);
                    _adapterl3.Fill(_acountstbl3);
                    if (_acountstbl3.Rows.Count>0)
                    {
                        //MessageBox.Show(_acountstbl2.Rows[0]["level2code"].ToString());
                        String pp = _acountstbl3.Rows[0]["level4code"].ToString();
                        //MessageBox.Show("level4");
                        //String part4 = pp.Substring(0, level1full.Length - 0);
                        String part4 = pp.Substring(8);
                        l1c = int.Parse(part4);
                    }
                    else
                    {
                        l1c = 0;
                    }
                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9 && l1c1 <= 99)
                    {
                        String modified = level1first.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        txtCode.Text = level1first + "-0" + l1c1.ToString();
                    }
                    else if (l1c1 > 99 && l1c1 <= 999)
                    {
                        String modified = level1first.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        txtCode.Text = level1first + "-" + l1c1.ToString();
                    }
                    else if (l1c1 > 999)
                    {
                        MessageBox.Show("List if already full...");
                        return;
                    }
                    else
                    {
                        String modified = level1first.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        //String modified1 = modified.Insert(6, "-");
                        txtCode.Text = level1first + "-00" + (string)l1c1.ToString();
                    }
                    string txtcode = txtCode.Text;
                    string newlevel1code = txtcode.Replace("-", ""); // .Substring(txtcode.IndexOf('-') + 0);
                    txtCode1.Text = newlevel1code;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                level = "4";
                btnupdate.Enabled = false;
                cmdSave.Enabled = true;
            }
            else if (part.Length == 19)
            {
                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(15,4);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 5);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1first.Replace("-","");
                String sqll2 = "select  top 1 level4code,level5code from coa_level5 Where level4code='" + txtCode2.Text + "' order by level5code desc";
                try
                {
                    int l1c = 0;
                    DataTable _acountstbl3 = new DataTable();
                    _connection.Open();
                    _adapterl3 = new SqlDataAdapter(sqll2, _connection);
                    _adapterl3.Fill(_acountstbl3);
                    if (_acountstbl3.Rows.Count>0)
                    {
                        //MessageBox.Show(_acountstbl2.Rows[0]["level2code"].ToString());
                        String pp = _acountstbl3.Rows[0]["level5code"].ToString();
                        //MessageBox.Show("level4");
                        //String part4 = pp.Substring(0, level1full.Length - 0);
                        String part4 = pp.Substring(11);
                        l1c = int.Parse(part4);
                    }
                    else
                    {
                        l1c = 0;
                    }
                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9 && l1c1 <= 99)
                    {
                        String modified = level1first.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1first + "-00" + l1c1.ToString();
                    }
                    else if (l1c1 > 99 && l1c1 <= 999)
                    {
                        String modified = level1first.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1first + "-0" + l1c1.ToString();
                    }
                    else if (l1c1 > 999 && l1c1 <= 9999)
                    {
                        String modified = level1first.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1first + "-" + l1c1.ToString();
                    }
                    else if (l1c1>9999)
                    {
                        MessageBox.Show("List if already full...");
                        return;
                    }
                    else
                    {
                        String modified = level1first.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1first + "-000" + (string)l1c1.ToString();
                    }
                    string txtcode = txtCode.Text;
                    string newlevel1code = txtcode.Replace("-", ""); // .Substring(txtcode.IndexOf('-') + 0);
                    txtCode1.Text = newlevel1code;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                level = "5";
                btnupdate.Enabled = false;
                cmdSave.Enabled = true;
            }
        }
        private void ShowNodeData(TreeNode nod)
        {
            //txtCode.Text = treeView1.SelectedNode.Text;
            string levelcode;
            levelcode = treeView1.SelectedNode.Text;
            string part = levelcode.Substring(0, levelcode.IndexOf(" -"));
            //MessageBox.Show(part);
            if (part.Length==2)
            {
                //MessageBox.Show("level1");
                txtCode.Text = part;
                txtCode1.Text = part;
                txtName.Text = levelcode.Substring(levelcode.LastIndexOf("- ") + 2);
                level = "1";
            }
            else if (part.Length == 6)
            {
                //MessageBox.Show("level2");
                //String modified = part.Insert(2, "-");
                //txtCode.Text = modified;
                txtCode.Text = part;

                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(3, 3);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1first;

                txtCode1.Text = level1full.Replace("-","");
                txtName.Text = levelcode.Substring(levelcode.LastIndexOf("- ")+2);
                level = "2";
            }
            else if (part.Length == 10)
            {
                // MessageBox.Show("level3");
                //String modified = part.Insert(2, "-");
                //String modified1 = modified.Insert(6, "-");
                //txtCode.Text = modified1;
                txtCode.Text = part;

                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(3, 3);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1first.Replace("-","");

                txtCode1.Text = level1full.Replace("-", "");

                //txtCode1.Text = part;
                txtName.Text = levelcode.Substring(levelcode.LastIndexOf("- ") + 2);
                level = "3";
            }
            else if (part.Length == 14)
            {
                //MessageBox.Show("level4");
                //String modified = part.Insert(2, "-");
                //String modified1 = modified.Insert(6, "-");
                //String modified2 = modified1.Insert(10, "-");
                txtCode.Text = part;

                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(2, level1full.Length - 2);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1first.Replace("-", "");

                txtCode1.Text = part.Replace("-", "");
                txtName.Text = levelcode.Substring(levelcode.LastIndexOf("- ") + 2);
                level = "4";
            }
            else if (part.Length == 19)
            {
                //MessageBox.Show("level5");
                //String modified = part.Insert(2, "-");
                //String modified1 = modified.Insert(6, "-");
                //String modified2 = modified1.Insert(10, "-");
                //String modified3 = modified2.Insert(14, "-");
                txtCode.Text = part;

                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(0, level1full.Length - 5);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 5);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1first.Replace("-", "");

                txtCode1.Text = part.Replace("-", "");
                txtName.Text = levelcode.Substring(levelcode.LastIndexOf("- ") + 2);
                level = "5";
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _connection = new SqlConnection(cs);
            _command = new SqlCommand();
            _command.Connection = _connection;
            try
            {
                _connection.Open();
                PopulateTreeView1();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _connection.Close();
            }
            btnupdate.Enabled = false;
            cmdSave.Enabled = false;
        }
        private void PopulateTreeView1()
        {
            _acountsTb1 = new DataTable();
            string sql1 = "SELECT * FROM coa_level1";
            _adapter1 = new SqlDataAdapter(sql1, _connection);
            _adapter1.Fill(_acountsTb1);
            int i;
            for (i = 0; i <= _acountsTb1.Rows.Count - 1; i++)
            {
                TreeNode t = new TreeNode();
                t.Text = _acountsTb1.Rows[i]["level1code"].ToString() + " - " + _acountsTb1.Rows[i]["level1name"].ToString();
                t.Name = _acountsTb1.Rows[i]["level1code"].ToString();
                t.Tag = _acountsTb1.Rows.IndexOf(_acountsTb1.Rows[i]);
                treeView1.Nodes.Add(t);
                treeView1.Nodes[i].ForeColor = Color.Blue;
                string sql2 = "SELECT * from coa_level2 where level1code='" + _acountsTb1.Rows[i]["level1code"].ToString() + "'";
                _adapter2 = new SqlDataAdapter(sql2, _connection);
                _acountsTb2 = new DataTable();
                _adapter2.Fill(_acountsTb2);
                if (_acountsTb2.Rows.Count!=0)
                {
                    int j;
                    for (j=0; j<=_acountsTb2.Rows.Count-1; j++)
                    {
                        TreeNode t1 = new TreeNode();

                        String modified9 = _acountsTb2.Rows[j]["level2code"].ToString().Insert(2, "-");
                        //String modified10 = modified.Insert(6, "-");
                        //String modified2 = modified1.Insert(10, "-");
                        //String modified3 = modified2.Insert(14, "-");
                        t1.Text = modified9 + " - " + _acountsTb2.Rows[j]["level2name"].ToString();

                        //t1.Text = _acountsTb2.Rows[j]["level2code"].ToString() + " - " + _acountsTb2.Rows[j]["level2name"].ToString();
                        t1.Name = _acountsTb2.Rows[j]["level2code"].ToString();
                        t1.Tag = _acountsTb2.Rows.IndexOf(_acountsTb2.Rows[j]);
                        treeView1.Nodes[i].Nodes.Add(t1);
                        treeView1.Nodes[i].Nodes[j].ForeColor = Color.Green;
                            string sql3 = "SELECT * from coa_level3 where level2code='" + _acountsTb2.Rows[j]["level2code"].ToString() + "'";
                            _adapter3 = new SqlDataAdapter(sql3, _connection);
                            _acountsTb3 = new DataTable();
                            _adapter3.Fill(_acountsTb3);
                            if (_acountsTb3.Rows.Count!=0)
                            {
                                int k = 0;
                                for (k=0; k<= _acountsTb3.Rows.Count-1; k++)
                                {
                                TreeNode t3 = new TreeNode();

                                String modified7 = _acountsTb3.Rows[k]["level3code"].ToString().Insert(2, "-");
                                String modified8 = modified7.Insert(6, "-");
                                //String modified9 = modified1.Insert(10, "-");
                                //String modified10 = modified2.Insert(14, "-");
                                t3.Text = modified8 + " - " + _acountsTb3.Rows[k]["level3name"].ToString();

                                //t3.Text = _acountsTb3.Rows[k]["level3code"].ToString() + " - " + _acountsTb3.Rows[k]["level3name"].ToString();
                                t3.Name = _acountsTb3.Rows[k]["level3code"].ToString();
                                t3.Tag = _acountsTb3.Rows.IndexOf(_acountsTb3.Rows[k]);
                                treeView1.Nodes[i].Nodes[j].Nodes.Add(t3);
                                treeView1.Nodes[i].Nodes[j].Nodes[k].ForeColor = Color.Black;

                                string sql4 = "SELECT * from coa_level4 where level3code='" + _acountsTb3.Rows[k]["level3code"].ToString() + "'";
                                _adapter4 = new SqlDataAdapter(sql4, _connection);
                                _acountsTb4 = new DataTable();
                                _adapter4.Fill(_acountsTb4);
                                if (_acountsTb4.Rows.Count != 0)
                                {
                                    int l = 0;
                                    for (l = 0; l <= _acountsTb4.Rows.Count - 1; l++)
                                    {
                                        TreeNode t4 = new TreeNode();

                                        String modified4 = _acountsTb4.Rows[l]["level4code"].ToString().Insert(2, "-");
                                        String modified5 = modified4.Insert(6, "-");
                                        String modified6 = modified5.Insert(10, "-");
                                        //String modified3 = modified2.Insert(14, "-");
                                        t4.Text = modified6 + " - " + _acountsTb4.Rows[l]["level4name"].ToString();

                                        //t4.Text = _acountsTb4.Rows[l]["level4code"].ToString() + " - " + _acountsTb4.Rows[l]["level4name"].ToString();
                                        t4.Name = _acountsTb4.Rows[l]["level4code"].ToString();
                                        t4.Tag = _acountsTb4.Rows.IndexOf(_acountsTb4.Rows[l]);
                                        treeView1.Nodes[i].Nodes[j].Nodes[k].Nodes.Add(t4);
                                        treeView1.Nodes[i].Nodes[j].Nodes[k].Nodes[l].ForeColor = Color.Red;

                                        string sql5 = "SELECT * from coa_level5 where level4code='" + _acountsTb4.Rows[l]["level4code"].ToString() + "'";
                                        _adapter5 = new SqlDataAdapter(sql5, _connection);
                                        _acountsTb5 = new DataTable();
                                        _adapter5.Fill(_acountsTb5);
                                        if (_acountsTb5.Rows.Count != 0)
                                        {
                                            int m = 0;
                                            for (m = 0; m <= _acountsTb5.Rows.Count - 1; m++)
                                            {
                                                TreeNode t5 = new TreeNode();

                                                String modified = _acountsTb5.Rows[m]["level5code"].ToString().Insert(2, "-");
                                                String modified1 = modified.Insert(6, "-");
                                                String modified2 = modified1.Insert(10, "-");
                                                String modified3 = modified2.Insert(14, "-");
                                                t5.Text= modified3 + " - " + _acountsTb5.Rows[m]["level5name"].ToString();

                                                //t5.Text = _acountsTb5.Rows[m]["level5code"].ToString() + " - " + _acountsTb5.Rows[m]["level5name"].ToString();
                                                t5.Name = _acountsTb5.Rows[m]["level5code"].ToString();
                                                t5.Tag = _acountsTb5.Rows.IndexOf(_acountsTb5.Rows[m]);
                                                treeView1.Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes.Add(t5);
                                                treeView1.Nodes[i].Nodes[j].Nodes[k].Nodes[l].Nodes[m].ForeColor = Color.Gold;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (level=="1")
            {
                if (txtName.Text=="")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate=System.DateTime.Today.ToShortDateString();
                string sql = "INSERT INTO coa_level1  VALUES ('"+ txtCode.Text + "' ,'"+txtName.Text+"','"+ cdate + "')";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            } 
            else if (level == "2")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "INSERT INTO coa_level2  VALUES ('" + txtCode2.Text + "','" + txtCode1.Text + "' ,'" + txtName.Text + "','"+ cdate+"')";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
            else if (level == "3")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "INSERT INTO coa_level3  VALUES ('" + txtCode2.Text + "','" + txtCode1.Text + "' ,'" + txtName.Text + "','"+ cdate+"')";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
            else if (level == "4")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "INSERT INTO coa_level4  VALUES ('" + txtCode2.Text + "','" + txtCode1.Text + "' ,'" + txtName.Text + "','"+ cdate+"')";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
            else if (level == "5")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "INSERT INTO coa_level5  VALUES ('" + txtCode2.Text + "','" + txtCode1.Text + "' ,'" + txtName.Text + "','"+ cdate+"')";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
        }
        ///////////////////////////////////////////////////////////////////////
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            string levelcode;
            if (_selectedNode==null)
            {
                MessageBox.Show("Please Select Account");
                return;
            }
            levelcode = treeView1.SelectedNode.Text;
            string part = levelcode.Substring(0, levelcode.IndexOf(" -"));
            //MessageBox.Show(part);
            if (part.Length == 2)
            {
                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                txtCode2.Text = level1full;
                String sqll1 = "select  top 1 level1code,level2code from coa_level2 Where level1code='"+ txtCode2.Text + "'order by coa_level2.level2code desc";
                try
                {
                    int l1c=0;
                    DataTable _acountstbl1 = new DataTable();
                    _connection.Open();
                    _adapterl1 = new SqlDataAdapter(sqll1, _connection);
                    _adapterl1.Fill(_acountstbl1);
                    if (_acountstbl1.Rows.Count > 0)
                    {
                        String pp = _acountstbl1.Rows[0]["level2code"].ToString();
                        String part4 = pp.Substring(2, pp.Length - 2);
                        l1c = int.Parse(part4);
                    }
                    else
                    {
                        l1c = 0;
                    }
                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9 && l1c1 <=99)
                    {
                        txtCode.Text = l1c1.ToString();
                    }
                    else if (l1c1 > 99 && l1c1 <= 999)
                    {
                        txtCode.Text = level1full+"-0" + (string)l1c1.ToString();
                    }
                    else if (l1c1 > 999)
                    {
                        MessageBox.Show("List is already fill");
                    }
                    else
                    {
                        txtCode.Text = level1full+"-00" + (string)l1c1.ToString();
                    }
                    string txtcode = txtCode.Text;
                    string newlevel1code = txtcode.Replace("-", ""); // .Substring(txtcode.IndexOf('-') + 0);
                    txtCode1.Text = newlevel1code;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                level = "2";
                btnupdate.Enabled = false;
                cmdSave.Enabled = true;
            }
            else if (part.Length == 6)
            {
                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(3, 3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                txtCode2.Text = level1full.Replace("-","");
                String sqll1 = "select  top 1 level2code,level3code from coa_level3 Where level2code='" + txtCode2.Text + "' order by level3code desc";
                try
                {

                    //
                    int l1c = 0;
                    DataTable _acountstbl1 = new DataTable();
                    _connection.Open();
                    _adapterl1 = new SqlDataAdapter(sqll1, _connection);
                    _adapterl1.Fill(_acountstbl1);
                    if (_acountstbl1.Rows.Count > 0)
                    {
                        String pp = _acountstbl1.Rows[0]["level3code"].ToString();
                        String part4 = pp.Substring(5);
                        l1c = int.Parse(part4);
                    }
                    else
                    {
                        l1c = 0;
                    }                
                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9 && l1c1 <= 99)
                    {
                        String modified = level1full.Insert(2, "-");
                        txtCode.Text = level1full + "-0" + l1c1.ToString();
                    }
                    else if (l1c1 > 99 && l1c1 <= 999)
                    {
                        String modified = level1full.Insert(2, "-");
                        txtCode.Text = level1full + "-" + l1c1.ToString();
                    }
                    else if (l1c1 > 999)
                    {
                        MessageBox.Show("Already List if full");
                        return;
                    }
                    else
                    {
                        String modified = level1full.Insert(2, "-");
                        txtCode.Text = level1full + "-00" + (string)l1c1.ToString();
                    }
                    string txtcode = txtCode.Text;
                    string newlevel1code = txtcode.Replace("-", ""); // .Substring(txtcode.IndexOf('-') + 0);
                    txtCode1.Text = newlevel1code;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                level = "3";
                btnupdate.Enabled = false;
                cmdSave.Enabled = true;
            }
            else if (part.Length == 10)
            {
                //
                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(3, 3);
               // MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1full.Replace("-","");
                String sqll2 = "select  top 1 level3code,level4code from coa_level4 Where level3code='" + txtCode2.Text + "' order by level4code desc";
                try
                {
                    int l1c = 0;
                    DataTable _acountstbl3 = new DataTable();
                    _connection.Open();
                    _adapterl3 = new SqlDataAdapter(sqll2, _connection);
                    _adapterl3.Fill(_acountstbl3);
                    if (_acountstbl3.Rows.Count > 0)
                    {
                        String pp = _acountstbl3.Rows[0]["level4code"].ToString();
                        //MessageBox.Show("level4");
                        //String part4 = pp.Substring(0, level1full.Length - 0);
                        String part4 = pp.Substring(8);
                        l1c = int.Parse(part4);
                    }
                    else
                    {
                        l1c = 0;
                    }
                    //MessageBox.Show(_acountstbl2.Rows[0]["level2code"].ToString());
                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9 && l1c1 <= 99)
                    {
                        String modified = level1full.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        //String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1full + "-0" + l1c1.ToString();
                    }
                    else if (l1c1 > 99 && l1c1 <= 999)
                    {
                        String modified = level1full.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        //String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1full + "-" + l1c1.ToString();
                    }
                    else if (l1c1 > 999)
                    {
                        MessageBox.Show("Already List if full");
                        return;
                    }
                    else
                    {
                        String modified = level1full.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        //String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1full + "-00" + (string)l1c1.ToString();
                    }
                    string txtcode = txtCode.Text;
                    string newlevel1code = txtcode.Replace("-", ""); // .Substring(txtcode.IndexOf('-') + 0);
                    txtCode1.Text = newlevel1code;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                level = "4";
                btnupdate.Enabled = false;
                cmdSave.Enabled = true;
            }
            else if (part.Length == 14)
            {
                string level1full = levelcode.Substring(0, levelcode.IndexOf(" -"));
                //MessageBox.Show(level1full);
                String level1last3 = level1full.Substring(11,3);
                //MessageBox.Show(level1last3);
                String level1first = level1full.Substring(0, level1full.Length - 4);
                //MessageBox.Show(level1first);
                txtCode2.Text = level1full.Replace("-","");
                String sqll2 = "select  top 1 level4code,level5code from coa_level5 Where level4code='" + txtCode2.Text + "' order by level5code desc";
                try
                {
                    int l1c = 0;
                    DataTable _acountstbl3 = new DataTable();
                    _connection.Open();
                    _adapterl3 = new SqlDataAdapter(sqll2, _connection);
                    _adapterl3.Fill(_acountstbl3);
                    //MessageBox.Show(_acountstbl2.Rows[0]["level2code"].ToString());
                    if (_acountstbl3.Rows.Count>0)
                    {
                        String pp = _acountstbl3.Rows[0]["level5code"].ToString();
                        //MessageBox.Show("level4");
                        //String part4 = pp.Substring(0, level1full.Length - 0);
                        String part4 = pp.Substring(11);
                        l1c = int.Parse(part4);
                    }
                    else
                    {
                        l1c = 0;
                    }
 
                    txtName.Text = "";
                    int l1c1 = l1c + 1;
                    if (l1c1 > 9 && l1c1 <= 99)
                    {
                        String modified = level1full.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1full + "-00" + l1c1.ToString();
                    }
                    else if (l1c1 > 99 && l1c1 <= 999)
                    {
                        String modified = level1full.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1full + "-0" + l1c1.ToString();
                    }
                    else if (l1c1 > 999 && l1c1 <= 9999)
                    {
                        String modified = level1full.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1full + "-" + l1c1.ToString();
                    }
                    else
                    {
                        String modified = level1full.Insert(2, "-");
                        String modified1 = modified.Insert(6, "-");
                        String modified2 = modified1.Insert(10, "-");
                        txtCode.Text = level1full + "-000" + (string)l1c1.ToString();
                    }
                    string txtcode = txtCode.Text;
                    string newlevel1code = txtcode.Replace("-", ""); // .Substring(txtcode.IndexOf('-') + 0);
                    txtCode1.Text = newlevel1code;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                level = "5";
                btnupdate.Enabled = false;
                cmdSave.Enabled = true;

            }
            else if (part.Length == 19)
            {
                MessageBox.Show("Can Not Add this Level");
                String modified = part.Insert(2, "-");
                String modified1 = modified.Insert(6, "-");
                String modified2 = modified1.Insert(10, "-");
                String modified3 = modified2.Insert(14, "-");
                txtCode.Text = modified3;
                txtCode1.Text = part;
                txtName.Text = levelcode.Substring(levelcode.LastIndexOf("-") + 2);
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
            }
        }
//////////////////////////////////////////////////////////////////////
        private void button1_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtCode1.Text = "";
            txtCode2.Text = "";
            treeView1.Nodes.Clear();
            PopulateTreeView1();
            treeView1.Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _selectedNode = treeView1.SelectedNode;
            ShowNodeData(_selectedNode);
            btnupdate.Enabled = true;
            cmdSave.Enabled = false;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            txtCode.Text = "";
            txtName.Text = "";
            txtCode1.Text = "";
            txtCode2.Text = "";
            btnupdate.Enabled = false;
            cmdSave.Enabled = false;
            treeView1.Nodes.Clear();
            PopulateTreeView1();
            treeView1.Refresh();
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (level == "1")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "UPDATE coa_level1 SET level1name='" + txtName.Text + "' WHERE level1code='"+ txtCode1.Text + "'";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
            else if (level == "2")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "UPDATE coa_level2 SET level2name='" + txtName.Text + "' WHERE level2code='" + txtCode1.Text + "'";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
            else if (level == "3")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "UPDATE coa_level3 SET level3name='" + txtName.Text + "' WHERE level3code='" + txtCode1.Text + "'";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
            else if (level == "4")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "UPDATE coa_level4 SET level4name='" + txtName.Text + "' WHERE level4code='" + txtCode1.Text + "'";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
            else if (level == "5")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Account Name is Must");
                    txtName.Focus();
                    return;
                }
                string cdate = System.DateTime.Today.ToShortDateString();
                string sql = "UPDATE coa_level5 SET level5name='" + txtName.Text + "' WHERE level5code='" + txtCode1.Text + "'";
                try
                {
                    _connection.Open();
                    _command.CommandText = sql;
                    _command.ExecuteNonQuery();
                    MessageBox.Show("Data Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _connection.Close();
                }
                txtCode.Text = "";
                txtName.Text = "";
                txtCode1.Text = "";
                txtCode2.Text = "";
                btnupdate.Enabled = false;
                cmdSave.Enabled = false;
                treeView1.Nodes.Clear();
                PopulateTreeView1();
                treeView1.Refresh();
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
       
        private void btncodesearch_Click(object sender, EventArgs e)
        {
            txtCode.Clear();
            txtName.Clear();
            string name = string.Empty;
            name = txtcodesearc.Text;
            bool b = true;
            if (name != string.Empty)
            {
                try
                {
                    TreeNode[] arr = treeView1.Nodes.Find(name, b);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        treeView1.SelectedNode = arr[i];
                        treeView1.SelectedNode.BackColor = Color.Red;
                        ShowNodeData(_selectedNode);
                    }
                }

                catch { }
               // MessageBox.Show("Not Found");
            }
            else
                MessageBox.Show("Enter Name");
        }

        private void btnnamesearch_Click(object sender, EventArgs e)
        {
            txtCode.Clear();
            txtName.Clear();
            string text = string.Empty;
            text = txtnamesearch.Text;
            bool b = true;
            if (text != string.Empty)
            {
                try
                {
                    TreeNode[] arr = treeView1.Nodes.Find(text, b);
                    for (int i = 0; i < arr.Length; i++)
                    {
                        treeView1.SelectedNode = arr[i];
                        treeView1.SelectedNode.BackColor = Color.Blue;
                        ShowNodeData(_selectedNode);
                    }
                }
                catch { }
                //MessageBox.Show("Not Found");
            }
            else
                MessageBox.Show("Enter Text");
        }

        private void txtcodesearc_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
