using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WinCreateEntity
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = ConfigurationManager.ConnectionStrings["SqlConnString_L"].ConnectionString;
            textBox2.Text = ConfigurationManager.AppSettings["AppFilePath"];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection(textBox1.Text.Trim());
            conn.Open();

            string selectQuery = "select * from information_schema.tables";// where table_schema='newblueFocus' and table_type='base table'";
            MySqlCommand command = new MySqlCommand(selectQuery, conn);
            MySqlDataAdapter ad = new MySqlDataAdapter(command);
            System.Data.DataSet ds = new DataSet();
            ad.Fill(ds);
            conn.Close();

            DataTable dt = ds.Tables[0];
            DataColumn dc = new DataColumn("cbx", typeof(string));
            dt.Columns.Add(dc);

            DataView dv = dt.DefaultView;
            dv.RowFilter = "table_schema='dx' and table_type='base table'";

            dataGridView1.DataSource = dv;

            //StringBuilder sbt = new StringBuilder();
            //foreach (DataRowView item in dv)
            //{
            //    sbt.Append("public DbSet<");
            //    sbt.Append(item["table_name"]);
            //    sbt.Append("> ");
            //    sbt.Append(item["table_name"]);
            //    sbt.AppendLine(" { get; set; }");
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataView dt = (DataView)dataGridView1.DataSource;
            if (dt == null || dt.Count < 1)
            {
                MessageBox.Show("   没有可以生成的表   ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            button2.Enabled = false;
            //string sql = "select a.name colname,b.name typename from syscolumns a inner join SysTypes b on a.xtype=b.xtype where b.status=0 and a.id =";
            string sql = "select * from information_schema.columns where table_schema='dx' and table_name='{0}'";

            StringBuilder sbt = new StringBuilder();

            foreach (DataRowView item in dt)
            {
                if (item["cbx"].ToString() != "1")
                    continue;

                if (item["table_name"].ToString() == "testdetail")
                {
                
                }

                string className = GetEntName(item["table_name"].ToString());
                sbt.Length = 0;
                sbt.AppendLine("using System;");
                sbt.AppendLine("using System.ComponentModel.DataAnnotations;");
                sbt.AppendLine("using System.ComponentModel.DataAnnotations.Schema;");
                sbt.AppendLine();
                sbt.AppendLine("/********************************************");
                sbt.AppendLine(" * " + item["table_name"] + "实体类");
                sbt.AppendLine(" * ");
                sbt.AppendLine(" * *****************************************/");
                sbt.AppendLine("namespace Model");
                sbt.AppendLine("{");
                sbt.AppendLine("    [Table(\"" + item["table_name"] + "\")]");
                sbt.AppendLine("    public class " + className);
                sbt.AppendLine("    {");

                MySqlConnection conn = new MySqlConnection(textBox1.Text.Trim());
                conn.Open();

                string selectQuery = string.Format(sql, item["table_name"]);
                MySqlCommand command = new MySqlCommand(selectQuery, conn);
                MySqlDataAdapter ad = new MySqlDataAdapter(command);
                System.Data.DataTable dtCols = new DataTable();
                ad.Fill(dtCols);
                conn.Close();

                int od = 0;
                //DataTable dtCols = SqlHelper.SqlQueryData(sql + item["id"] + " order by colid");
                foreach (DataRow row in dtCols.Rows)
                {
                    if (row["column_key"].ToString().ToLower() == "pri")
                    {
                        ++od;
                        sbt.AppendLine("        [Key]");//


                        sbt.Append("        [Column(\"" + row["column_name"]);
                        sbt.AppendLine("\",Order=" + od + ")]");
                    }
                    else
                        sbt.AppendLine("        [Column(\"`" + row["column_name"] + "`\")]");

                    sbt.AppendLine("        public " + GetType(row["data_type"].ToString()) + " " + GetColName(row["column_name"].ToString()));
                    sbt.AppendLine("        { get;set; }");
                    sbt.AppendLine("");
                }
                sbt.AppendLine("    }");
                sbt.AppendLine("}");

                string filename = className + ".cs";
                if (!Directory.Exists(textBox2.Text.Trim()))
                    Directory.CreateDirectory(textBox2.Text.Trim());
                if (!File.Exists(Path.Combine(textBox2.Text.Trim(), filename)))
                {
                    FileStream fs = File.Create(Path.Combine(textBox2.Text.Trim(), filename));
                    fs.Close(); fs.Dispose();
                }
                //记入日志
                using (TextWriter writer = File.CreateText(Path.Combine(textBox2.Text.Trim(), filename)))
                {
                    writer.Write(sbt.ToString());
                    writer.Close();
                }
            }

            button2.Enabled = true;
            MessageBox.Show("   成功生成   ", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public string GetEntName(string name)
        {
            //string[] p = name.Split(new char[] { '_' });
            //return p[1] + "Entity";

            //return name.Replace("_", "") + "Entity";

            return name.Replace("_", "");
        }

        public string GetColName(string name)
        {
            string p = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name);
            if (p.StartsWith("_"))
                p = p.TrimStart('_');

            return p.Replace("-", "_");
        }

        public string GetType(string sqltype)
        {
            string type_ = "string";
            switch (sqltype.ToLower())
            {
                case "int":
                case "smallint":
                    type_ = "int?";
                    break;
                case "decimal":
                case "numeric":
                case "float":
                case "money":
                    type_ = "decimal?";
                    break;
                case "bit":
                    type_ = "bool";
                    break;
                case "nvarchar":
                case "varchar":
                case "nchar":
                case "char":
                case "ntext":
                case "text":
                    type_ = "string";
                    break;
                case "date":
                case "datetime":
                case "smalldatetime":
                    type_ = "DateTime?";
                    break;
                case "uniqueidentifier":
                    type_ = "Guid";
                    break;
                default:
                    type_ = "string";
                    break;
            }            

            return type_;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataView dt = (DataView)dataGridView1.DataSource;
            if (dt == null || dt.Count < 1)
                return;

            foreach (DataRowView item in dt)
            {
                item["cbx"] = "1";
            }
        }
    }
}
