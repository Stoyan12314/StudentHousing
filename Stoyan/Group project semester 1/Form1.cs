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


        //Creating each building
        Building buildingA = new Building("BuildingA");
        Building buildingB = new Building("BuildingB");
        Building buildingC = new Building("BuildingC");
        Building chosenBuilding = default;
        Student loggedUser = default; 
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
                //Building chosenBuilding=default;


                string firstName = tbFirstName.Text;
                string lastName = tbLastName.Text;
                string username = tbUsername.Text;
                string email = tbEmail.Text;
                int age = int.Parse(tbAge.Text);
                string password = tbPassword.Text;
                string apartment = "";
                if (rbBuildingA.Checked)
                {
                    chosenBuilding = buildingA;
                }
                else if (rbBuildingB.Checked)
                {
                    chosenBuilding = buildingB;
                }
                else if (rbBuildingC.Checked)
                {
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
                tbAdminLoginPassword.Text = "";
                tbAdminLoginUsername.Text = "";
            }
        }




        //Login Student
        private void btnStudentLogin_Click(object sender, EventArgs e)
        {
           string building = cbBuilding.Text;
           string username= tbStudentLoginUsername.Text;
           string password = tbStudentLoginPassword.Text;
           loggedUser = CheckLogin(building, username, password);
            //label19
            if (loggedUser != null)
            {
                tabControl.SelectTab("StudentHomePage");
                LoadText(building, loggedUser);
                lbStudentRoommates.Items.Clear();
                foreach (Student student in selectedBuilding(building).ShowAllStudents())
                {
                    if (student.GetUsername() != username)
                    {
                        lbStudentRoommates.Items.Add(student.GetName());
                    }
                }
            }


           else
           {
                MessageBox.Show("Account not found");
           }
           
                

        }
        public Student CheckLogin(string building, string username, string password)
        {
            Student check = null;
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
        public Building selectedBuilding(string buildingName)
        {
            Building selectedBuilding = default;
            if (buildingName == "BuildingA")
            {
                selectedBuilding= buildingA;
            }
            else if (buildingName == "BuildingB")
            {
                selectedBuilding= buildingB;

            }
            else if (buildingName == "BuildingC")
            {
                selectedBuilding=  buildingC;
            }
            return selectedBuilding;
        }

        public void LoadText(string building, Student student)
        {
            lbLivingIn.Text = $"You are currently living in: {building}";
            label23.Text = $"You are currently living in: {building}";
            label26.Text = $"You are currently living in: {building}";
            label32.Text = $"You are currently living in: {building}";
            label29.Text = $"You are currently living in: {building}";
            label34.Text = $"You are currently living in: {building}";
            label37.Text = $"You are currently living in: {building}";
            label35.Text = $"You are currently living in: {building}";
            label38.Text = $"You are currently living in: {building}";
            label28.Text = $"Hello {student.GetName()} !";
            label19.Text = $"Hello {student.GetName()} !";
            label22.Text = $"Hello {student.GetName()} !";
            label25.Text = $"Hello {student.GetName()} !";
            label31.Text = $"Hello {student.GetName()} !";
            label32.Text = $"Hello {student.GetName()} !";
            label34.Text = $"Hello {student.GetName()} !";
            label35.Text = $"Hello {student.GetName()} !";
            label37.Text = $"Hello {student.GetName()} !";
            label38.Text = $"Hello {student.GetName()} !";
        }




        

        

        private void btnShowA_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingA.ShowAllStudents())
            {
                lbInfo.Items.Add(student.GetInfo());
            }
                
        }

        

      
        

       

        private void btnShowB_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingB.ShowAllStudents())
            {
                lbInfo.Items.Add(student.GetInfo());
            }

        }

        private void btnShowC_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingC.ShowAllStudents())
            {
                lbInfo.Items.Add(student.GetInfo());
            }
        }

        private void lbStudentRoommates_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StudentHomePage_Click(object sender, EventArgs e)
        {

        }
        public void RefreshCleaningTasks()
        {
            Apartaments apartamentToAddTaskTo = chosenBuilding.ReturnsChosenApartament(loggedUser);
            lbCleaningSchedule.Items.Clear();
            foreach (CleaningSchedule schedule in apartamentToAddTaskTo.GetListCleaningSchedule())
            {
                lbCleaningSchedule.Items.Add(schedule.GetInfo());
            }
        }
        private void btnAddTaskCleaning_Click(object sender, EventArgs e)
        {

            Apartaments apartamentToAddTaskTo = chosenBuilding.ReturnsChosenApartament(loggedUser);
            string date = dateTimePicker1.Value.ToShortDateString();
            string room = comboBox_tasks.Text;
            apartamentToAddTaskTo.AddCleaningTask(loggedUser.GetUsername(), date, room);
            RefreshCleaningTasks();
        }

        private void btnRemoveTaskCleaning_Click(object sender, EventArgs e)
        {
            Apartaments apartamentToRemoveTaskFrom = chosenBuilding.ReturnsChosenApartament(loggedUser);
            string selectedTask = "";
            if (lbCleaningSchedule.SelectedIndex != -1)
            {
              selectedTask = lbCleaningSchedule.SelectedItem.ToString();   
            }
            if (selectedTask == "")
            {
                MessageBox.Show("Please select task to remove from");
            }
            else
            {
                apartamentToRemoveTaskFrom.RemoveCleaningShedule(selectedTask, loggedUser.GetUsername());
                RefreshCleaningTasks();           
            }
        }
        private void UpdateGroceries()
        {
            Apartaments chosenApartament = chosenBuilding.ReturnsChosenApartament(loggedUser);
            lbGroceriesStudent.Items.Clear();
            foreach (Groceries groceries in chosenApartament.GetGroceries())
            {
                lbGroceriesStudent.Items.Add(groceries.Info());
            }
        }
        private void btnAddTaskGroceries_Click(object sender, EventArgs e)
        {
            Apartaments apartamentToAddGroceriesToo = chosenBuilding.ReturnsChosenApartament(loggedUser);
            string date = dateTimePicker1.Value.ToShortDateString();
            apartamentToAddGroceriesToo.AddGroceries(loggedUser.GetUsername(), date, "will buy groceries on this date");
            
            UpdateGroceries();


        }

        private void btnRemoveTaskGroceries_Click(object sender, EventArgs e)
        {
            Apartaments apartamentToRemoveGroceriesFrom = chosenBuilding.ReturnsChosenApartament(loggedUser);
           
            string selectedGrocerie = "";
            if (lbGroceriesStudent.SelectedIndex != -1)
            {
                selectedGrocerie = lbGroceriesStudent.SelectedItem.ToString();
            }
            if (selectedGrocerie == "")
            {
                MessageBox.Show("Please select grocerie to remove from");
            }
            else
            {
                apartamentToRemoveGroceriesFrom.RemoveGrocerie(selectedGrocerie, loggedUser.GetUsername());
                UpdateGroceries();
            }
        }

        private void btnAddTaskGarbage_Click(object sender, EventArgs e)
        {
            Apartaments apartamentToAddGarbageToo = chosenBuilding.ReturnsChosenApartament(loggedUser);
            string date = dateTimePicker1.Value.ToShortDateString();

            apartamentToAddGarbageToo.AddGarbage("will throw garbage on this date", date, loggedUser.GetUsername());
            UpdateGarbage();
        }
        private void UpdateGarbage()
        {
            Apartaments chosenApartament = chosenBuilding.ReturnsChosenApartament(loggedUser);
            lbGarbage.Items.Clear();
            foreach (Garbage garbage in chosenApartament.GetGarbages())
            {
                lbGarbage.Items.Add(garbage.Info());
            }
        }

        private void btnRemoveTaskGarbage_Click(object sender, EventArgs e)
        {
            Apartaments apartamentToRemoveGarbageFrom = chosenBuilding.ReturnsChosenApartament(loggedUser);
            string selectedGarbage = "";
            if (lbGarbage.SelectedIndex != -1)
            {
                selectedGarbage = lbGarbage.SelectedItem.ToString();
            }
            if (selectedGarbage == "")
            {
                MessageBox.Show("Please select garbage to remove from");
            }
            else
            {
                apartamentToRemoveGarbageFrom.RemoveGarbage(selectedGarbage, loggedUser.GetUsername());
                UpdateGarbage();
            }
        }


        //ALL THE TAB CONTROL BUTTONS !!!

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

        private void button60_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
        }

        private void button59_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
        }

        private void button61_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
        }

        private void button62_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
        }

        private void button63_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
        }

        private void button64_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
        }

        private void button65_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
        }

        private void button66_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminPage");
        }

        private void button77_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminPage");
        }

        private void button83_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminPage");
        }

        private void button89_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminPage");
        }

        private void button95_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminPage");
        }

        private void button101_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminPage");
        }

        private void button67_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAccount");
        }

        private void button76_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAccount");
        }

        private void button82_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAccount");
        }

        private void button88_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAccount");
        }

        private void button94_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAccount");
        }

        private void button100_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAccount");
        }

        private void button68_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAnouncments");
        }

        private void button75_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAnouncments");
        }

        private void button81_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAnouncments");
        }

        private void button87_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAnouncments");
        }

        private void button93_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAnouncments");
        }

        private void button99_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAnouncments");
        }

        private void button69_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRules");
        }

        private void button74_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRules");
        }

        private void button80_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRules");
        }

        private void button86_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRules");
        }

        private void button92_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRules");
        }

        private void button98_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRules");
        }

        private void button70_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminComplaints");
        }

        private void button73_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminComplaints");
        }

        private void button79_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminComplaints");
        }

        private void button85_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminComplaints");
        }

        private void button91_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminComplaints");
        }

        private void button97_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminComplaints");
        }

        private void button71_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRent");
        }

        private void button72_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRent");
        }

        private void button78_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRent");
        }

        private void button84_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRent");
        }

        private void button90_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRent");
        }

        private void button96_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRent");
        }


        private void btnLogOutAdmin_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
            loggedUser = null;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
                tabControl.SelectTab("HomePage");
            loggedUser = null;
        }
    }
}
