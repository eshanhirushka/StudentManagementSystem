using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace StudentEnrolmentSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void studentBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.studentBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.studentDataSet);

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'studentDataSet.Student' table. You can move, or remove it, as needed.
            this.studentTableAdapter.Fill(this.studentDataSet.Student);


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }
        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void studentBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            int ageval = Int16.Parse(label2.Text);
            if (registrationNumberTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Registration Number!");
                return;
            }
            else if (studentNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Student Name!");
                return;
            }
            else if (dateOfBirthDateTimePicker.Text == "" + DateTime.Today)
            {
                MessageBox.Show("Please Enter Bday!");
                return;
            }
            else if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("Please check one radio button!");
                return;
            }
            else if (contactNumberTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Student Contact Number!");
                return;
            }
            else if (comboBox1.Text == String.Empty)
            {
                MessageBox.Show("Please select a Course!");
                return;
            }
            if (ageval <= 18)
            {
                MessageBox.Show("Cannot Enroll – Below 18 years");
            }
            else
            {

                String radiaButtonval = "";

                if (radioButton1.Checked == true)
                {
                    radiaButtonval = "M";
                }
                else if (radioButton2.Checked == true)
                {
                    radiaButtonval = "F";
                }
                else
                {
                    radiaButtonval = "O";
                }




                string connetionString = null;
                string sql = null;

                // All the info required to reach your db. See connectionstrings.com
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Database; Trusted_Connection=True;";

                // Prepare a proper parameterized query 
                sql = "insert into Student ([RegistrationNumber], [StudentName], [DateOfBirth], [Gender], [Age], [ContactNumber], [CourseEnrolledIn]) values(@RegistrationNumber, @StudentName, @DateOfBirth, @Gender, @Age, @ContactNumber, @CourseEnrolledIn)";
                // Create the connection (and be sure to dispose it at the end)
                using (SqlConnection cnn = new SqlConnection(connetionString))
                {

                    try
                    {
                        // Open the connection to the database. 
                        // This is the first critical step in the process.
                        // If we cannot reach the db then we have connectivity problems
                        cnn.Open();

                        // Prepare the command to be executed on the db
                        using (SqlCommand cmd = new SqlCommand(sql, cnn))
                        {


                            // Create and set the parameters values 
                            cmd.Parameters.Add("@RegistrationNumber", SqlDbType.Int).Value = registrationNumberTextBox.Text;
                            cmd.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = studentNameTextBox.Text;
                            cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = dateOfBirthDateTimePicker.Text;
                            cmd.Parameters.Add("@Gender", SqlDbType.Char).Value = radiaButtonval;
                            cmd.Parameters.Add("@Age", SqlDbType.Int).Value = label2.Text;
                            cmd.Parameters.Add("@ContactNumber", SqlDbType.VarChar).Value = contactNumberTextBox.Text;
                            cmd.Parameters.Add("@CourseEnrolledIn", SqlDbType.VarChar).Value = comboBox1.Text;



                            // Let's ask the db to execute the query
                            int rowsAdded = cmd.ExecuteNonQuery();
                            if (rowsAdded > 0)
                                MessageBox.Show("Student Inserted Successfully");

                            else
                                // Well this should never really happen
                                MessageBox.Show("Data not inserted");


                        }
                    }
                    catch (Exception ex)
                    {
                        // We should log the error somewhere, 
                        // for this example let's just show a message
                        MessageBox.Show("ERROR:" + ex.Message);
                    }

                }
                registrationNumberTextBox.Text = "";
                studentNameTextBox.Text = "";
                dateOfBirthDateTimePicker.Text = "";
                label2.Text = "";
                contactNumberTextBox.Text = "";
                comboBox1.Items[comboBox1.SelectedIndex] = string.Empty;
            }





        }
        public static void main(string[] args)
        {
            Application.Run(new Form1());
        }

        private void dateOfBirthDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime theDate = dateOfBirthDateTimePicker.Value;
            DateTime today = DateTime.Today;
            int age = today.Year - theDate.Year;
            label2.Text = "" + age;

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            //button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirm Delete Yes/No", "Confirm delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string connetionString = null;
                string sql = null;

                // All the info required to reach your db. See connectionstrings.com
                connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Database; Trusted_Connection=True;";

                // Prepare a proper parameterized query 
                sql = "Delete From Student Where [RegistrationNumber]=@RegistrationNumber";
                // Create the connection (and be sure to dispose it at the end)
                using (SqlConnection cnn = new SqlConnection(connetionString))
                {

                    try
                    {
                        // Open the connection to the database. 
                        // This is the first critical step in the process.
                        // If we cannot reach the db then we have connectivity problems
                        cnn.Open();

                        // Prepare the command to be executed on the db
                        using (SqlCommand cmd = new SqlCommand(sql, cnn))
                        {


                            // Create and set the parameters values 
                            cmd.Parameters.AddWithValue("@RegistrationNumber", SqlDbType.Int).Value = registrationNumberTextBox.Text;

                            // Let's ask the db to execute the query
                            int rowsDeleted = cmd.ExecuteNonQuery();
                            if (rowsDeleted > 0)
                                MessageBox.Show("Row Deleted!");
                            else
                                MessageBox.Show("There is No RegistraionNumber" + rowsDeleted);

                            // button2.Enabled = false;


                        }
                    }
                    catch (Exception ex)
                    {
                        // We should log the error somewhere, 
                        // for this example let's just show a message
                        MessageBox.Show("ERROR:" + ex.Message);
                    }

                }
                registrationNumberTextBox.Text = "";
                studentNameTextBox.Text = "";
                dateOfBirthDateTimePicker.Text = "";
                label2.Text = "";
                contactNumberTextBox.Text = "";
                comboBox1.Text = String.Empty;
            }
            else
                MessageBox.Show("Data not Deleted");


        }

        private void registrationNumberTextBox_TextChanged(object sender, EventArgs e)
        {

            string connetionString = null;
            string sql, sql1 = null;

            // All the info required to reach your db. See connectionstrings.com
            connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Database; Trusted_Connection=True;";

            // Prepare a proper parameterized query 
            sql = "Select COUNT(*) From Student Where [RegistrationNumber]=@RegistrationNumber";
            // Create the connection (and be sure to dispose it at the end)
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {

                try
                {
                    // Open the connection to the database. 
                    // This is the first critical step in the process.
                    // If we cannot reach the db then we have connectivity problems
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sql, cnn))
                    {


                        // Create and set the parameters values 
                        cmd.Parameters.AddWithValue("@RegistrationNumber", registrationNumberTextBox.Text);
                        int rowsAlrdyInserted = (int)cmd.ExecuteScalar();
                        if (rowsAlrdyInserted > 0)
                        {
                            button2.Enabled = true;
                            button1.Enabled = false;

                            sql1 = "Select * From Student Where [RegistrationNumber]=@RegistrationNumber";
                            SqlCommand oCmd = new SqlCommand(sql1, cnn);
                            oCmd.Parameters.AddWithValue("@RegistrationNumber", registrationNumberTextBox.Text);
                            
                            using (SqlDataReader oReader = oCmd.ExecuteReader())
                            {
                                while (oReader.Read())
                                {
                                    String radiaButtonval = "M";
                                    studentNameTextBox.Text = oReader["StudentName"].ToString();
                                    dateOfBirthDateTimePicker.Text = oReader["DateOfBirth"].ToString();
                                    label2.Text = oReader["Age"].ToString();
                                    if(oReader["Gender"].ToString() == radiaButtonval)
                                    {
                                        radioButton1.Checked = true;
                                    }
                                    else
                                    {
                                        radioButton2.Checked = true;
                                    }
                                    contactNumberTextBox.Text = oReader["ContactNumber"].ToString();
                                    comboBox1.SelectedItem = oReader["CourseEnrolledIn"].ToString();

                                    
                                }

                                //`cnn.Close();
                            }


                        }
                        else
                        {
                            button1.Enabled = true;
                            button2.Enabled = false;
                        }



                    }
                }
                catch (Exception ex)
                {
                    // We should log the error somewhere, 
                    // for this example let's just show a message
                    MessageBox.Show("ERROR:" + ex.Message);
                }

            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button1.Enabled = true;
            button4.Enabled = true;
            registrationNumberTextBox.Text = String.Empty;
            studentNameTextBox.Text = String.Empty;
            dateOfBirthDateTimePicker.Text = String.Empty;
            label2.Text = String.Empty;
            contactNumberTextBox.Text = String.Empty;
           // comboBox1.Text = String.Empty;
            comboBox1.SelectedIndex = -1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void studentNameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("file:///C:/Users/Eshan%20Hirushka/Desktop/ESoft.html");
        }
    }





}
