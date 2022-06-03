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
                bool rent = false;
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
                bool IsApartamentFull = chosenBuilding.AddStudentToApartament(firstName, lastName, username, email, age, password, apartment, rent);
                bool check = false;
                foreach (string usernames in chosenBuilding.ReturnListUsers())
                {
                    if (usernames == username)
                    {
                        check = true;
                    }
                    
                }

                if (IsApartamentFull && check == false)
                {
                    MessageBox.Show("Registered sucessfully");
                    chosenBuilding.AddToListWithUserNames(username);
                }
                else if (check == true)
                {
                    MessageBox.Show("Username taken");
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
        

        //Show all your roommates when you log in
        private void ShowRoommates(string username, string building, Student loggedUser)
        {
            lbStudentRoommates.Items.Clear();
            foreach (Student student in selectedBuilding(building).ShowAllStudents(loggedUser))
            {
                if (student.GetUsername() != username)
                {
                    lbStudentRoommates.Items.Add(student.GetName() + " " + student.GetLastName());
                }
            }
            foreach (Student student in selectedBuilding(building).ShowAllStudents(loggedUser))
            {
                if (student.GetUsername() != username)
                {
                    cbRoommatesComplains.Items.Add(student.GetName() + " " + student.GetLastName());
                }
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

                ShowRoommates(username, building, loggedUser);
            }


           else
           {
                MessageBox.Show("Account not found");
           }

            tbStudentLoginUsername.Text = "";
            tbStudentLoginPassword.Text = "";
            cbBuilding.Text = "";

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
            
            label19.Text = $"Hello {student.GetName()} {student.GetLastName()} ";
            label22.Text = $"Hello {student.GetName()} {student.GetLastName()} ";
            label25.Text = $"Hello {student.GetName()} {student.GetLastName()} ";
            label28.Text = $"Hello {student.GetName()} {student.GetLastName()} ";
            label31.Text = $"Hello {student.GetName()} {student.GetLastName()} ";
            label34.Text = $"Hello {student.GetName()} {student.GetLastName()} ";
            label37.Text = $"Hello {student.GetName()} {student.GetLastName()} ";
            label30.Text = $"Hello {student.GetName()} {student.GetLastName()} ";




            lbLivingIn.Text = $"You are currently living in: {building}, {student.GetApartment()}";
            label23.Text = $"You are currently living in: {building}, {student.GetApartment()}";
            label26.Text = $"You are currently living in: {building}, {student.GetApartment()}";
            label29.Text = $"You are currently living in: {building}, {student.GetApartment()}";
            label32.Text = $"You are currently living in: {building}, {student.GetApartment()}";
            label35.Text = $"You are currently living in: {building}, {student.GetApartment()}";
            label38.Text = $"You are currently living in: {building}, {student.GetApartment()}";
            label39.Text = $"You are currently living in: {building}, {student.GetApartment()}";


        }




        

        

        private void btnShowA_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingA.ShowAllStudents(loggedUser))
            {
                lbInfo.Items.Add(student.GetInfo());
            }
                
        }

        

      
        

       

        private void btnShowB_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingB.ShowAllStudents(loggedUser))
            {
                lbInfo.Items.Add(student.GetInfo());
            }

        }

        private void btnShowC_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingC.ShowAllStudents(loggedUser))
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


        //START RULES

        //Request a rule as a student
        private void btnRuleRequest_Click(object sender, EventArgs e)
        {
            string requestedRule = tbRuleRequest.Text;
            lbRequestedRules.Items.Add(requestedRule);
            tbRuleRequest.Text = "";
        }


        //Add the requested rule as a general rule for everyone
        private void btnAddRequestedRule_Click(object sender, EventArgs e)
        {
            if (lbRequestedRules.SelectedItem == null)
            {
                MessageBox.Show("You need to select a rule!");
            }
            else
            {
                string requestedRule = lbRequestedRules.SelectedItem.ToString();

                int index = lbRequestedRules.Items.IndexOf(requestedRule);
                lbRequestedRules.Items.RemoveAt(index);


                lbCurrentRulesAdmin.Items.Add(requestedRule);
                lbRules.Items.Add(requestedRule);
            }


        }

        //Remove requested rule from the admin page

        private void btnRemoveRequestedRuleAdmin_Click(object sender, EventArgs e)
        {
            if (lbRequestedRules.SelectedItem == null)
            {
                MessageBox.Show("You need to select a rule!");
            }
            else
            {

                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this requested rule?",
                "Delete rule?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string requestedRule = lbRequestedRules.SelectedItem.ToString();
                    int index = lbRequestedRules.Items.IndexOf(requestedRule);
                    lbRequestedRules.Items.RemoveAt(index);
                }

            }
        }


        //Remove rule from general rules
        private void btnRemoveRule_Click(object sender, EventArgs e)
        {
            if (lbCurrentRulesAdmin.SelectedItem == null)
            {
                MessageBox.Show("You need to select a rule!");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this rule?",
                "Delete rule?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string rule = lbCurrentRulesAdmin.SelectedItem.ToString();
                    int index = lbCurrentRulesAdmin.Items.IndexOf(rule);

                    lbCurrentRulesAdmin.Items.RemoveAt(index);
                    lbRules.Items.RemoveAt(index);

                }


            }
        }
        //Adding the written rule as an admin
        private void btnAddRule_Click(object sender, EventArgs e)
        {

            if (tbAddRule.Text == "")
            {
                MessageBox.Show("Please write down the rule you want to add!");
            }

            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to add this rule?",
                "Add rule?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string newRule = tbAddRule.Text;

                    lbCurrentRulesAdmin.Items.Add(newRule);
                    lbRules.Items.Add(newRule);
                }
            }

            tbAddRule.Text = "";
        }

        //END OF RULES



        //START COMPLAINTS

        //STUDENT COMPLAINTS

        //Diffrent kind of a complaint
        private void button102_Click(object sender, EventArgs e)
        {
            //Get the info of the student making the complaint
            string[] splitedUsername = label30.Text.Split().ToArray();

            string name = splitedUsername[1] + " " + splitedUsername[2];

            string[] livingIn = label39.Text.Split().ToArray();

            string address = livingIn[5] + " " + livingIn[6];


            string complaint = tbDiffrentComplaint.Text;

            //Send the complaint to Admin
            lbAdminComplaints.Items.Add($"{name}, from {address} has a complaint about - {complaint}");


            tbDiffrentComplaint.Text = "";

        }


        //Student making complaint about his roommates
        private void btnComplaintAboutRoommate_Click(object sender, EventArgs e)
        {
            string roommate = cbRoommatesComplains.Text;

            if (roommate == "")
            {
                MessageBox.Show("Please enter which roommate you want to make complaint about!");
            }
            else
            {
                if (cbComplaintsDoesntClean.Checked || cbComplaintsInvitingPeople.Checked || cbComplaintsMakingNoise.Checked || cbLowHygiene.Checked)
                {
                    string complaint = "";
                    if (cbComplaintsDoesntClean.Checked)
                    {
                        complaint = "Does not clean!";
                    }
                    else if (cbComplaintsInvitingPeople.Checked)
                    {
                        complaint = "Inviting people without asking!";
                    }
                    else if (cbComplaintsMakingNoise.Checked)
                    {
                        complaint = "Making noise after 23:00 o'clock!";
                    }
                    else if (cbLowHygiene.Checked)
                    {
                        complaint = "Roomate has a very low hygiene!";
                    }



                    //Get the info of the student making the complaint
                    string[] splitedUsername = label30.Text.Split().ToArray();

                    string name = splitedUsername[1] + " " + splitedUsername[2];

                    string[] livingIn = label39.Text.Split().ToArray();

                    string address = livingIn[5] + " " + livingIn[6];


                    //Send the complaint to Admin
                    lbAdminComplaints.Items.Add($"{name}, from {address} - Complaint about {roommate} - {complaint}");


                    cbRoommatesComplains.Text = "";
                }
                else
                {
                    MessageBox.Show("Please select your complaint about the roommate!");
                }
            }
        }

        //Student make complaint about a broken facility
        private void btnComplaintBrokenFacility_Click(object sender, EventArgs e)
        {
            if (cbBrokenFacility.Text == "")
            {
                MessageBox.Show("Please select the broken facility!");
            }
            else
            {

                //Get the info of the student making the complaint
                string[] splitedUsername = label30.Text.Split().ToArray();

                string name = splitedUsername[1] + " " + splitedUsername[2];

                string[] livingIn = label39.Text.Split().ToArray();

                string address = livingIn[5] + " " + livingIn[6];


                string brokenFacility = cbBrokenFacility.Text;


                //Send the complaint to Admin
                lbAdminComplaints.Items.Add($"{name}, from {address} - Complaint about broken facility - {brokenFacility}");

            }


            cbBrokenFacility.Text = "";
        }


        //ADMIN COMPLAINTS

        //Admin highlighting the complaint (Making it a diffrent color because it is important
        private void btnHighlightComplaint_Click(object sender, EventArgs e)
        {


            if (lbAdminComplaints.SelectedItem == null)
            {
                MessageBox.Show("Please select a complaint!");
            }
            else
            {
                int index = lbAdminComplaints.Items.IndexOf(lbAdminComplaints.SelectedItem);

            }
        }


        //Admin taking care of the complaint
        private void btnComplaintFixed_Click(object sender, EventArgs e)
        {
            if (lbAdminComplaints.SelectedItem == null)
            {
                MessageBox.Show("Please select a complaint!");
            }
            else
            {
                int index = lbAdminComplaints.Items.IndexOf(lbAdminComplaints.SelectedItem);
                lbAdminComplaints.Items.RemoveAt(index);
            }

        }

        //END OF COMPLAINTS

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


        private void button58_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentHomePage");
        }

        private void button50_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGroceries");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
        }

        private void btnPayRent_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("PayRent");
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

        private void btnShowStudents_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedBuilding = cbSelectedBuilding.Text;
                string selectedApartament = cbSelectedApartament.Text;
                switch (selectedBuilding)
                {
                    case "BuildingA":
                        foreach (Student student in buildingA.ReturnStudentsByChosenApartament(selectedApartament))
                        {
                            lbStudentsRent.Items.Add(student);
                        }  
                        break;
                    case "BuildingB":
                        foreach (Student student in buildingB.ReturnStudentsByChosenApartament(selectedApartament))
                        {
                            lbStudentsRent.Items.Add(student);
                        }
                        break;
                    case "BuildingC":
                        foreach (Student student in buildingC.ReturnStudentsByChosenApartament(selectedApartament))
                        {
                            lbStudentsRent.Items.Add(student);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        




        //END OF BUTTONS
        ////////////////////////////////////////////////////////////////////////////////////



    }
}
