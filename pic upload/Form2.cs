using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//library use
using System.Data.SqlClient;
using System.IO;

namespace pic_upload
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ACER\Documents\Visual Studio 2013\Projects\pic upload\pic upload\Database1.mdf;Integrated Security=True");

        string imgLocation;
        

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string query = "SELECT * FROM pro_img WHERE Id = '" + textBox1.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader DataRead = cmd.ExecuteReader();
            DataRead.Read();

            if (DataRead.HasRows)
            {
                textBox2.Text = DataRead["name"].ToString();
                textBox3.Text = DataRead["gender"].ToString();
                textBox4.Text = DataRead["town"].ToString();
                textBox5.Text = DataRead["mobile"].ToString();
                byte[] images = ((byte[])DataRead["Image"]);


                if (images == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    MemoryStream mstreem = new MemoryStream(images);
                    pictureBox1.Image = Image.FromStream(mstreem);

                }

            }
            else
            {

                MessageBox.Show("This Image not Available..!");

            }

            con.Close();
        }
    }
}
