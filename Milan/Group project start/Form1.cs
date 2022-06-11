using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group_project_semester_1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        List<Student> registeredStudents = new List<Student>();

        const string adminUsername = "Admin123";
        const string adminPassword = "Admin123";

        public void RegistrationFailedOrDone()
        {
            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbUsername.Text = "";
            tbEmail.Text = "";
            tbAge.Text = "";
            tbPassword.Text = "";
            tbConfirmPassword.Text = "";

            
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (tbFirstName.Text == "" || tbLastName.Text == "" || tbUsername.Text == "" || tbEmail.Text == "" || tbPassword.Text == "" || tbConfirmPassword.Text == "" || tbAge.Text == "")
            {
                MessageBox.Show("You have a missing component! Please try again!");
                RegistrationFailedOrDone();
            }
            else if (tbPassword.Text != tbConfirmPassword.Text)
            {
                tbPassword.Text = "";
                tbConfirmPassword.Text = "";

                MessageBox.Show("Password does not match! Please check your password and try again!");
            }

            else
            {
                string firstName = tbFirstName.Text;
                string lastName = tbLastName.Text;
                string username = tbUsername.Text;
                string email = tbEmail.Text;
                int age = int.Parse(tbAge.Text);
                string password = tbPassword.Text;

                string building = "";
                string apartment = "";
                if (rbBuildingA.Checked)
                {
                    building = "A";
                }
                else if (rbBuildingB.Checked)
                {
                    building = "B";
                }
                else if (rbBuildingC.Checked)
                {
                    building = "C";
                }


                if (rbApartment1.Checked)
                {
                    apartment = "Apartment 1";
                }
                else if (rbApartment2.Checked)
                {
                    apartment = "Apartment 2";
                }
                else if (rbApartment3.Checked)
                {
                    apartment = "Apartment 3";
                }
                else if (rbApartment4.Checked)
                {
                    apartment = "Apartment 4";
                }
                else if (rbApartment5.Checked)
                {
                    apartment = "Apartment 5";
                }


                Student student = new Student(firstName, lastName, username, email, age, password, building, apartment);


                lbInfo.Items.Add(student.GetInfo());
                
                RegistrationFailedOrDone();
            }
            
            
        }


        //Login Administrator

        private void btnLoginAdmin_Click(object sender, EventArgs e)
        {
            string username = tbAdminLoginUsername.Text;
            string password = tbAdminLoginPassword.Text;

            if (username != adminUsername || password != adminPassword)
            {
                MessageBox.Show("Username or password is incorrect! Please try again!");
                tbAdminLoginPassword.Text = "";
                tbAdminLoginUsername.Text = "";
            }
            else
            {
                tabControl.SelectTab("AdminPage");
            }
        }


        //Login Student
        private void btnStudentLogin_Click(object sender, EventArgs e)
        {

        }


        //Tab Control Buttons

        private void btnHomePageRegister_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("Register");
        }

        private void btnHomePageLogin_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("LoginAs");
        }

        private void btnRegisterBackToHomePage_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("HomePage");
        }

        private void btnLoginAsStudent_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("LoginStudent");
        }

        private void btnLoginAsAdmin_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("LoginAdmin");
        }

        private void btnLoginAsBackToHomePage_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("HomePage");
        }

        private void btnStudentLoginBackToLoginAs_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("LoginAs");
        }

        private void btnAdminLoginBackToLoginAs_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("LoginAs");
        }

        

        
    }
}
