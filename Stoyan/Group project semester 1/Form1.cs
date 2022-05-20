﻿using System;
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


        //Creating each building
        Building buildingA = new Building("BuildingA");
        Building buildingB = new Building("BuildingB");
        Building buildingC = new Building("BuildingC");


        //Const string of the username and password of administator
        const string adminUsername = "Admin123";
        const string adminPassword = "Admin123";



        //Method to clear the textboxes after registration
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

        //Registration
        private void btnRegister_Click(object sender, EventArgs e)

            //Check if everything is field
        {
            if (tbFirstName.Text == "" || tbLastName.Text == "" || tbUsername.Text == "" || tbEmail.Text == "" || tbPassword.Text == "" || tbConfirmPassword.Text == "" || tbAge.Text == "")
            {
                MessageBox.Show("You have a missing component! Please try again!");
                RegistrationFailedOrDone();
            }

            //checking if password is correctly written
            else if (tbPassword.Text != tbConfirmPassword.Text)
            {
                tbPassword.Text = "";
                tbConfirmPassword.Text = "";

                MessageBox.Show("Password does not match! Please check your password and try again!");
            }


            //Registrate the student
            else
            {

                //Check the current building
                Building chosenBuilding=default;


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
                    chosenBuilding = buildingA;
                }
                else if (rbBuildingB.Checked)
                {
                    building = "B";
                    chosenBuilding = buildingB;
                }
                else if (rbBuildingC.Checked)
                {
                    building = "C";
                    chosenBuilding = buildingC;
                }


                if (rbApartment1.Checked)
                {
                    apartment = "Apartment1";
                }
                else if (rbApartment2.Checked)
                {
                    apartment = "Apartment2";
                }
                else if (rbApartment3.Checked)
                {
                    apartment = "Apartment3";
                }
                else if (rbApartment4.Checked)
                {
                    apartment = "Apartment4";
                }
                else if (rbApartment5.Checked)
                {
                    apartment = "Apartment5";
                }

                //Check if the apartment is full and if we can add another student or not
                bool IsApartamentFull = chosenBuilding.AddStudentToApartament(firstName, lastName, username, email, age, password, apartment);


                if (IsApartamentFull)
                {
                    MessageBox.Show("Registered sucessfully");
                }
                else
                {
                    MessageBox.Show("Apartament full");
                }
                


                //foreach (Student student in buildingA.GetAllStudents())
                //{
                //    lbInfo.Items.Add(student.GetInfo());
                //}
                
                
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
           string building = cbBuilding.Text;
           string username= tbStudentLoginUsername.Text;
           string password = tbStudentLoginPassword.Text;
           bool checkLogin = CheckLogin(building, username, password);
           if (checkLogin)
           {
                tabControl.SelectTab("StudentHomePage");
           }
           else
           {
                MessageBox.Show("Account not found");
           }
           
                

        }
        public bool CheckLogin(string building, string username, string password)
        {
            bool check = default;
            if (building == "BuildingA")
            {
               check =  buildingA.CheckUserNameAndPassword(username, password);
            }
            else if (building == "BuildingB")
            {
                check= buildingB.CheckUserNameAndPassword(username, password);
            }
            else if (building == "BuildingC")
            {
                check = buildingC.CheckUserNameAndPassword(username, password);
            }
            return check;
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

        private void Garbage_Click(object sender, EventArgs e)
        {

        }

        private void rbApartment2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbBuildingA_CheckedChanged(object sender, EventArgs e)
        {
            //rbApartment1.Text = buildingA.;
        }

        private void btnShowA_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingA.ShowAllStudents())
            {
                lbInfo.Items.Add(student.GetInfo());
            }
                
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentHomePage");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentHomePage");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentHomePage");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentHomePage");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentHomePage");
        }

        private void button49_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentHomePage");
        }

        private void button57_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentHomePage");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
        }

        private void button48_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
        }

        private void button56_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGroceries");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGroceries");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGroceries");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGroceries");
        }

        private void button47_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGroceries");
        }

        private void button55_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGroceries");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGroceries");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
        }

        private void button46_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
        }

        private void button54_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
        }

        private void button45_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
        }

        private void button53_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
        }

        private void button44_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
        }

        private void button52_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
        }

        private void button43_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
        }

        private void button51_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
        }

        private void button50_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
        }
    }
}
