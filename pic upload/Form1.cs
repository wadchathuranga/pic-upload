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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        //database connection
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ACER\Documents\Visual Studio 2013\Projects\pic upload\pic upload\Database1.mdf;Integrated Security=True");

        string imgLocation;
        

        //image select button
        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog imgOpen = new OpenFileDialog();
            imgOpen.Filter = "Image Files (*.jpg)|*.jpg | All Files (*.*)|*.*";


            if (imgOpen.ShowDialog() == DialogResult.OK)
            {
                imgLocation = imgOpen.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;

                
            }

        }

        
        //image save button
        private void button3_Click(object sender, EventArgs e)
        {

            byte[] images = null;
            FileStream Streem = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(Streem);
            images = brs.ReadBytes((int)Streem.Length);

            con.Open();
            string query = "INSERT INTO pro_img (Id,name,gender,town,mobile,Image) VALUES ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "',@images)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.Add(new SqlParameter("@images", images));
            int N = cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show(N.ToString()+"Data saved successfull");

        }

        //image & data view button
        private void button2_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            byte[] images;
            FileStream Streem = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader brs = new BinaryReader(Streem);
            images = brs.ReadBytes((int)Streem.Length);

            con.Open();
            string updateQuery = "UPDATE pro_img SET name='" + textBox2.Text + "',gender='" + textBox3.Text + "',town='" + textBox4.Text + "',mobile='" + textBox5.Text + "',Image=@images WHERE Id='"+textBox1.Text+"'";
            SqlCommand cmd = new SqlCommand(updateQuery, con);
            cmd.Parameters.Add(new SqlParameter("@images", images));
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Profile Update Successfully..!");
        }

        
    }
}
