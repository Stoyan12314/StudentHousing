using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Group_project_semester_1
{
    public partial class Form1 : Form
    {
        BuildingsManager buildingsManager= new BuildingsManager();
        public Form1()
        {
            InitializeComponent();
            Size = new Size(934, 766);
            tabControl.Location = new Point(-5, -18);
        }
        //Creating each building
        //public List<Building> buildings = new List<Building>()
        //{
        //    new Building("BuildingA"),
        //    new Building("BuildingB"),
        //    new Building("BuildingC")
        //};
        Apartaments currentApartment;
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
                    chosenBuilding = buildingsManager.ReturnBuildingByName("BuildingA");
                }
                else if (rbBuildingB.Checked)
                {
                    chosenBuilding = buildingsManager.ReturnBuildingByName("BuildingB");
                }
                else if (rbBuildingC.Checked)
                {
                    chosenBuilding = buildingsManager.ReturnBuildingByName("BuildingC");
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
                    tabControl.SelectTab("LoginStudent");
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

                panelNavMenuAdmin.Visible = true;
                panelNavMenuAdmin.Location = new Point(0, 0);
                tabControl.Location = new Point(213, -18);
                Size = new Size(1155, 768);
            }
        }


        //Show all your roommates when you log in
        private void ShowRoommates(string username, Building building, Student loggedUser)
        {
            lbStudentRoommates.Items.Clear();
            foreach (Student student in selectedBuilding(building.BuildingName).ShowAllStudents(loggedUser))
            {
                if (student.GetUsername() != username)
                {
                    lbStudentRoommates.Items.Add(student.GetName() + " " + student.GetLastName());
                }
            }
            foreach (Student student in selectedBuilding(building.BuildingName).ShowAllStudents(loggedUser))
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

            

            string username = tbStudentLoginUsername.Text;
            string password = tbStudentLoginPassword.Text;
            string building = cbBuilding.Text;
            
            foreach(Building buildingObj in buildingsManager.Buildings())
            {
                if (building == buildingObj.BuildingName)
                {
                    chosenBuilding = buildingObj;
                }
            }
            loggedUser = CheckLogin(chosenBuilding, username, password);

            //if (loggedUser.Building == chosenBuilding)
            //{
            //    chosenBuilding = loggedUser.Building;
            //}
            //label19
            if (loggedUser != null)
            {
                tabControl.SelectTab("StudentHomePage");
                LoadText(chosenBuilding, loggedUser);
                lbUsernameTenant.Text = loggedUser.GetUsername();
                lbNamesTenant.Text = $"{loggedUser.GetName()} {loggedUser.GetLastName()}";
                ShowRoommates(username, chosenBuilding, loggedUser);
                panelNavMenu.Visible = true;
                panelNavMenu.Location = new Point(0, 0);
                tabControl.Location = new Point(213, -18);
                Size = new Size(1155, 768);
                gbWanted.Visible = false;
                gbOrder.Visible = false;
                gbAdd.Visible = false;
                gbRent.Visible = false;
            }
            else
            {
                MessageBox.Show("Account not found");
            }


            tbStudentLoginUsername.Text = "";
            tbStudentLoginPassword.Text = "";
            cbBuilding.Text = "";

            currentApartment = chosenBuilding.ReturnsChosenApartament(loggedUser);

            //
            //
            // ADD CHECHER FOR GROCERIE BOXES
            RefreshBoxes();
            //
            //
        }

        public Student CheckLogin(Building building, string username, string password)
        {
            Student check = null;
            if (building.BuildingName == buildingsManager.ReturnBuildingByName("BuildingA").BuildingName)
            {
                check = buildingsManager.ReturnBuildingByName("BuildingA").CheckUserNameAndPassword(username, password);
            }
            else if (building.BuildingName == buildingsManager.ReturnBuildingByName("BuildingB").BuildingName)
            {
                check = buildingsManager.ReturnBuildingByName("BuildingB").CheckUserNameAndPassword(username, password);
            }
            else if (building.BuildingName == buildingsManager.ReturnBuildingByName("BuildingC").BuildingName)
            {
                check = buildingsManager.ReturnBuildingByName("BuildingC").CheckUserNameAndPassword(username, password);
            }
            return check;
        }
        public Building selectedBuilding(string buildingName)
        {
            Building selectedBuilding = default;
            if (buildingName == "BuildingA")
            {
                selectedBuilding = buildingsManager.ReturnBuildingByName("BuildingA");
            }
            else if (buildingName == "BuildingB")
            {
                selectedBuilding = buildingsManager.ReturnBuildingByName("BuildingB");

            }
            else if (buildingName == "BuildingC")
            {
                selectedBuilding = buildingsManager.ReturnBuildingByName("BuildingC");
            }
            return selectedBuilding;
        }
        public void ChangeBtColor(Button button)
        {
            var list = new List<Button>()
            {
                btHomePanel, btCleaningSPanel, btGroceryPanel, btGarbagePanel, btRulesPanel,btAnouncmentsPanel,btRentPanel,btComplaintsPanel,btHomeAdmin,btAccountsAdmin,btAnnouncementsAdmin,btRentAdmin,btRulesAdmin,btComplainsAdmin
            };
            foreach(var item in list)
            {
                if(item != button)
                {
                    item.BackColor = Color.FromArgb(37, 60, 124);
                }
            }
        }

        public void LoadText(Building building, Student student)
        {

           lbLivingIn.Text = $"You are currently living in: \n{chosenBuilding.BuildingName}, {student.GetApartment()}";





            //Depending on which apartment you are living you have diffrent price for rent
            switch (student.GetApartment())
            {
                case "Apartment1":
                    if (student.GetRent() == false)
                    {
                        lblRentPayment.Text = ("Unpayed - You need to pay 300€");
                    }
                    else
                    {
                        lblRentPayment.Text = ("Payed - You have payed 300€");
                    }
                    break;
                case "Apartment2":
                    if (student.GetRent() == false)
                    {
                        lblRentPayment.Text = ("Unpayed - You need to pay 350€");
                    }
                    else
                    {
                        lblRentPayment.Text = ("Payed - You have payed 350€");
                    }
                    break;

                case "Apartment3":
                    if (student.GetRent() == false)
                    {
                        lblRentPayment.Text = ("Unpayed - You need to pay 400€");
                    }
                    else
                    {
                        lblRentPayment.Text = ("Payed - You have payed 400€");
                    }
                    break;

                case "Apartment4":
                    if (student.GetRent() == false)
                    {
                        lblRentPayment.Text = ("Unpayed - You need to pay 450€");
                    }
                    else
                    {
                        lblRentPayment.Text = ("Payed - You have payed 450€");
                    }
                    break;

                case "Apartment5":
                    if (student.GetRent() == false)
                    {
                        lblRentPayment.Text = ("Unpayed - You need to pay 500€");
                    }
                    else
                    {
                        lblRentPayment.Text = ("Payed - You have payed 500€");
                    }
                    break;
                default:
                    break;
            }



        }

        private void btnShowA_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingA").ShowAllStudents(loggedUser))
            {
                lbInfo.Items.Add(student.GetInfo());
            }

        }


        private void btnShowB_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingB").ShowAllStudents(loggedUser))
            {
                lbInfo.Items.Add(student.GetInfo());
            }

        }

        private void btnShowC_Click(object sender, EventArgs e)
        {
            foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingC").ShowAllStudents(loggedUser))
            {
                lbInfo.Items.Add(student.GetInfo());
            }
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


        //Groceries START








 
        
        





        //Garbage
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


        //RENT 
        //ADMIN


        //updating the list of the admin - seeing every single student in the selected apartment of the selected building
        private void UpdateStudentListRent(string selectedBuilding, string selectedApartament)
        {
            lbStudentsRent.Items.Clear();
            switch (selectedBuilding)
            {

                //Each builidng has a diffrent list
                case "BuildingA":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingA").ReturnStudentsByChosenApartament(selectedApartament))
                    {
                        lbStudentsRent.Items.Add(student);
                    }
                    break;
                case "BuildingB":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingB").ReturnStudentsByChosenApartament(selectedApartament))
                    {
                        lbStudentsRent.Items.Add(student);
                    }
                    break;
                case "BuildingC":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingC").ReturnStudentsByChosenApartament(selectedApartament))
                    {
                        lbStudentsRent.Items.Add(student);
                    }
                    break;
                default:
                    break;
            }

        }

        //Changing the rent of the student from unpayed to payed
        private void ChangeStudentRentToPayed(string selectedBuilding, string selectedApartament, string username)
        {
            switch (selectedBuilding)
            {
                //Each building has a diffrent list
                case "BuildingA":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingA").GetAllStudents())
                    {
                        //getting the student we need - we are using username, because each username is diffrent are there can not be two identical usernames
                        if (student.GetUsername() == username)
                        {
                            student.SetRent(true);
                        }

                        //Updating the list so we can see the changes we have made
                        UpdateStudentListRent(selectedBuilding, selectedApartament);
                    }

                    break;
                case "BuildingB":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingB").GetAllStudents())
                    {
                        if (student.GetUsername() == username)
                        {
                            student.SetRent(true);
                        }
                    }
                    //Updating the list so we can see the changes we have made
                    UpdateStudentListRent(selectedBuilding, selectedApartament);
                    break;

                case "BuildingC":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingC").GetAllStudents())
                    {
                        if (student.GetUsername() == username)
                        {
                            student.SetRent(true);
                        }
                    }
                    //Updating the list so we can see the changes we have made
                    UpdateStudentListRent(selectedBuilding, selectedApartament);
                    break;
                default:

                    break;

            }
        }


        //Changing the rent from payed to unpayed for the selected student
        private void ChangeStudentRentToUnpayed(string selectedBuilding, string selectedApartament, string username)
        {
            switch (selectedBuilding)
            {
                case "BuildingA":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingA").GetAllStudents())
                    {
                        if (student.GetUsername() == username)
                        {
                            student.SetRent(false);
                        }
                        //Updating the list so we can see the changes we have made
                        UpdateStudentListRent(selectedBuilding, selectedApartament);
                    }

                    break;
                case "BuildingB":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingB").GetAllStudents())
                    {
                        if (student.GetUsername() == username)
                        {
                            student.SetRent(false);
                        }
                    }
                    //Updating the list so we can see the changes we have made
                    UpdateStudentListRent(selectedBuilding, selectedApartament);
                    break;

                case "BuildingC":
                    foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingC").GetAllStudents())
                    {
                        if (student.GetUsername() == username)
                        {
                            student.SetRent(false);
                        }
                    }
                    //Updating the list so we can see the changes we have made
                    UpdateStudentListRent(selectedBuilding, selectedApartament);
                    break;
                default:

                    break;

            }
        }

        //button to show us all the registered students in this particular building and apartment
        private void btnShowStudents_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedBuilding = cbSelectedBuilding.Text;
                string selectedApartament = cbSelectedApartament.Text;
                UpdateStudentListRent(selectedBuilding, selectedApartament);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            cbSelectedBuilding.Text = "";
            cbSelectedApartament.Text = "";

        }

        //button changing the the rent of the student to payed
        private void btnAlreadyPaidAdmin_Click(object sender, EventArgs e)
        {
            //Check if there is a selected item in the list
            if (lbStudentsRent.SelectedItem == null)
            {
                MessageBox.Show("Please select a student for which you want to change the rent");
            }
            else
            {
                //taking the selected item
                string ChosenStudentToChangeRent = lbStudentsRent.SelectedItem.ToString();

                //split the string into an array
                string[] splitTheString = ChosenStudentToChangeRent.Split().ToArray();


                //taking the info we need - username, building and apartment
                string username = splitTheString[0];
                string selectedBuilding = splitTheString[2];
                string selectedApartament = splitTheString[4];
                //Make the rent payed
                ChangeStudentRentToPayed(selectedBuilding, selectedApartament, username);
            }

        }

        private void btnRequestPaymentAdmin_Click(object sender, EventArgs e)
        {
            //Check if there is a selected item in the list
            if (lbStudentsRent.SelectedItem == null)
            {
                MessageBox.Show("Please select a student for which you want to change the rent");
            }
            else
            {
                //Taking the selected item making it a string
                string ChosenStudentToChangeRent = lbStudentsRent.SelectedItem.ToString();
                //split the string into an array
                string[] splitTheString = ChosenStudentToChangeRent.Split().ToArray();

                //taking the info we need - username, building and apartment
                string username = splitTheString[0];
                string selectedBuilding = splitTheString[2];
                string selectedApartament = splitTheString[4];
                //Make the rent unpayed
                ChangeStudentRentToUnpayed(selectedBuilding, selectedApartament, username);
            }

        }

        //STUDENT
        //Student paying his rent
        private void btnPayYourRent_Click(object sender, EventArgs e)
        {
            if (cbSelectCardType.Text == "" || tbCardNumber.Text == "" || tbNameOnCard.Text == "" || tbExpiryDate.Text == "" || tbCVV.Text == "")
            {
                MessageBox.Show("You have missed a component!");
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to pay your rent?",
             "Pay rent?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    loggedUser.SetRent(true);
                    LoadText(loggedUser.Building, loggedUser);

                    MessageBox.Show("Rent payed!");
                    tabControl.SelectTab("StudentRent");

                    cbSelectCardType.Text = "";
                    tbCardNumber.Text = "";
                    tbNameOnCard.Text = "";
                    tbExpiryDate.Text = "";
                    tbCVV.Text = "";
                }

            }

        }

        //END RENT


        //Checking and giving the list of the students living at this address - ADMIN MAIN PAGE
        private void btnCheckTenants_Click(object sender, EventArgs e)
        {

            string selectedBuilding = cbBuildingAdminMainPage.Text;
            string selectedApartament = cbApartmentAdminMainPage.Text;

            if (cbApartmentAdminMainPage.Text == "" || cbBuildingAdminMainPage.Text == "")
            {
                MessageBox.Show("You have missed a component!");
            }
            else
            {
                lbTenantsAdmin.Items.Clear();
                switch (selectedBuilding)
                {

                    //Each builidng has a diffrent list
                    case "BuildingA":
                        foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingA").ReturnStudentsByChosenApartament(selectedApartament))
                        {
                            lbTenantsAdmin.Items.Add(student.AdminInfo());
                        }
                        break;
                    case "BuildingB":
                        foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingB").ReturnStudentsByChosenApartament(selectedApartament))
                        {
                            lbTenantsAdmin.Items.Add(student.AdminInfo());
                        }
                        break;
                    case "BuildingC":
                        foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingC").ReturnStudentsByChosenApartament(selectedApartament))
                        {
                            lbTenantsAdmin.Items.Add(student.AdminInfo());
                        }
                        break;
                    default:
                        break;

                        //if (lbTenantsAdmin.Items.Count > -1)
                        //{

                        //}
                        //else
                        //{
                        //    MessageBox.Show("There are no registrated students at this address!");
                        //}
                }

                cbApartmentAdminMainPage.Text = "";
                cbBuildingAdminMainPage.Text = "";
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
            panelNavBar.Top = btHomePanel.Top;
            btHomePanel.BackColor = Color.FromArgb(155, 216, 249);
            ChangeBtColor(btHomePanel);

        }


        private void button8_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentCleaning");
            panelNavBar.Top = btCleaningSPanel.Top;
            btCleaningSPanel.BackColor = Color.FromArgb(155, 216, 249);
            ChangeBtColor(btCleaningSPanel);
        }


        private void button7_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGrocerie");
            panelNavBar.Top = btGroceryPanel.Top;
            btGroceryPanel.BackColor = Color.FromArgb(155, 216, 249);
            ChangeBtColor(btGroceryPanel);
        }


        private void button6_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGarbage");
            panelNavBar.Top = btGarbagePanel.Top;
            btGarbagePanel.BackColor = Color.FromArgb(155, 216, 249);
            ChangeBtColor(btGarbagePanel);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRules");
            panelNavBar.Top = btRulesPanel.Top;
            btRulesPanel.BackColor = Color.FromArgb(155, 216, 249);
            ChangeBtColor(btRulesPanel);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentAnouncments");
            panelNavBar.Top = btAnouncmentsPanel.Top;
            btAnouncmentsPanel.BackColor = Color.FromArgb(155, 216, 249);
            ChangeBtColor(btAnouncmentsPanel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
            panelNavBar.Top = btRentPanel.Top;
            btRentPanel.BackColor = Color.FromArgb(155, 216, 249);
            ChangeBtColor(btRentPanel);
        }

        private void button60_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentComplaints");
            panelNavBar.Top = btComplaintsPanel.Top;
            btComplaintsPanel.BackColor = Color.FromArgb(155, 216, 249);
            ChangeBtColor(btComplaintsPanel);
        }

        private void btnPayRent_Click(object sender, EventArgs e)
        {
            gbRent.Visible = true;
        }

        private void btnBackFromRentPayment_Click(object sender, EventArgs e)
        {
            cbSelectCardType.Text = "";
            tbCardNumber.Text = "";
            tbNameOnCard.Text = "";
            tbExpiryDate.Text = "";
            tbCVV.Text = "";
            gbRent.Visible = false;
        }


        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
             "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    tabControl.SelectTab("HomePage");
                    currentApartment.cartGroceries.Clear();
                    loggedUser = null;
                    panelNavMenu.Visible = false;
                    Size = new Size(934, 766);
                    tabControl.Location = new Point(-5, -18);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        public void RefreshBoxes()
        {
            lboxGroceriesALL.Items.Clear();
            cbGrocerie.Items.Clear();
            foreach (Grocery grocery in currentApartment.Groceries)
            {
                lboxGroceriesALL.Items.Add(grocery);
                cbGrocerie.Items.Add(grocery.GrocerieName);
            }
        }

        private void btCancelGrocerie_Click(object sender, EventArgs e)
        {
            currentApartment.cartGroceries.Clear();
            lboxGrocerieCart.Items.Clear();
        }

        private void btAddNewGrocerie_Click(object sender, EventArgs e)
        {
            bool hasItIn = false;
            if (nudNewGroceriePrice.Value > 0)
            {
                foreach(Grocery groceryCheck in currentApartment.Groceries)
                {
                    if (groceryCheck.GrocerieName != tbNewGrocerieName.Text)
                    {
                        hasItIn = false;
                    }
                }
                if (hasItIn == false)
                {
                    Grocery grocery = new Grocery(tbNewGrocerieName.Text, (int)nudNewGroceriePrice.Value);
                    currentApartment.Groceries.Add(grocery);
                    RefreshBoxes();
                }
            }
            else MessageBox.Show($"Price can not be a zero");

        }

        private void btGrocerieAddToCart_Click(object sender, EventArgs e)
        {
            if (cbGrocerie.Text != "" || nudAmountGrocery.Value > 0)
            {
                lboxGrocerieCart.Items.Clear();
                foreach (Grocery grocery in currentApartment.Groceries)
                {
                    if (cbGrocerie.Text == grocery.GrocerieName)
                    {
                        grocery.Amount = (int)nudAmountGrocery.Value;
                        currentApartment.cartGroceries.Add(grocery);
                    }
                }

                foreach (Grocery grocery1 in currentApartment.cartGroceries)
                {
                    lboxGrocerieCart.Items.Add(grocery1);
                }
            }
            else MessageBox.Show($"Select grocery/amount");

        }

        private void btOrderGrocerie_Click(object sender, EventArgs e)
        {
            if (currentApartment.cartGroceries.Count > 0)
            {
                lboxGrocerieCart.Items.Clear();
                foreach (Grocery grocery in currentApartment.cartGroceries)
                {
                    currentApartment.finalListGroceries.Add(grocery);
                    lboxGroceriesToBuy.Items.Add(grocery.ToString());
                }
                lboxGrocerieCart.Items.Clear();
            }
            else MessageBox.Show($"Shoppng cart is empty");
        }

        private void btBuyAllGroceries_Click(object sender, EventArgs e)
        {
            string boughtOn = dtpBuyGrocery.Value.ToString("dd MMMM yyyy"); ;
            //lboxGroceriesToBuy.Items.Clear();
            lboxGroceriesToBuy.Items.Add($"{loggedUser.GetName()} bought the list:");
            foreach(Grocery grocery in currentApartment.finalListGroceries)
            {
                lboxGroceriesToBuy.Items.Add(grocery.ToString());
                currentApartment.totalPrice += grocery.Price * grocery.Amount; 
            }
            lboxGroceriesToBuy.Items.Add($"on { boughtOn }");
            lboxGroceriesToBuy.Items.Add($"{currentApartment.totalPrice}$");
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string selectedAccount = lbInfo.SelectedItem.ToString();
            foreach (Building building in buildingsManager.Buildings())
            {
                foreach (Apartaments apartament in building.apartaments())
                {
                    foreach (Student student in apartament.GetAllStudents().ToList())
                    {
                        if (student.GetInfo() == selectedAccount)
                        {
                            apartament.RemoveStudent(student);
                            lbInfo.Items.Clear();   
                        }
                    }
                }
            }
           // lbInfo.Items
        }

        private void btnShowA_Click_1(object sender, EventArgs e)
        {
            foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingA").GetAllStudents())
            {
                lbInfo.Items.Add(student.GetInfo());
            }
        }


        private void btnShowB_Click_1(object sender, EventArgs e)
        {
            foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingB").GetAllStudents())
            {
                lbInfo.Items.Add(student.GetInfo());
            }
        }

        private void btnShowC_Click_1(object sender, EventArgs e)
        {
            foreach (Student student in buildingsManager.ReturnBuildingByName("BuildingC").GetAllStudents())
            {
                lbInfo.Items.Add(student.GetInfo());
            }
        }


        private void btShowList_Click(object sender, EventArgs e)
        {
            btShowList.BackColor = Color.White;
            btShowOrder.BackColor = Color.Transparent;
            btShowAdd.BackColor = Color.Transparent;
            
            gbWanted.Visible = true;
            gbWanted.Size = new Size(899, 391);
            gbWanted.Location = new Point(22, 304);

            gbAdd.Visible = false;
            gbOrder.Visible = false;
        }

        private void btShowOrder_Click(object sender, EventArgs e)
        {
            btShowOrder.BackColor = Color.White;
            btShowList.BackColor = Color.Transparent;
            btShowAdd.BackColor = Color.Transparent;

            gbOrder.Visible = true;
            gbOrder.Size = new Size(899, 391);
            gbOrder.Location = new Point(22, 304);

            gbAdd.Visible= false;
            gbWanted.Visible= false;
        }

        private void btShowAdd_Click(object sender, EventArgs e)
        {
            btShowOrder.BackColor = Color.Transparent;
            btShowList.BackColor = Color.Transparent;
            btShowAdd.BackColor = Color.White;

            gbAdd.Visible = true;
            gbAdd.Size = new Size(899, 391);
            gbAdd.Location = new Point(22, 304);

            gbWanted.Visible = false;
            gbOrder.Visible= false;
        }


        private void btElse_Click(object sender, EventArgs e)
        {
            btRoomate.BackColor = Color.Transparent;
            btElse.BackColor = Color.White;
            btBroken.BackColor = Color.Transparent;

            gbElse.Visible = true;
            gbElse.Size = new Size(899, 391);
            gbElse.Location = new Point(22, 304);

            gbBrokenShow.Visible = false;
            gbShowRoomate.Visible = false;
        }

        private void btRoomate_Click(object sender, EventArgs e)
        {
            btRoomate.BackColor = Color.White;
            btElse.BackColor = Color.Transparent;
            btBroken.BackColor = Color.Transparent;

            gbShowRoomate.Visible = true;
            gbShowRoomate.Size = new Size(899, 391);
            gbShowRoomate.Location = new Point(22, 304);

            gbElse.Visible = false;
            gbBrokenShow.Visible = false;
        }

        private void btBroken_Click(object sender, EventArgs e)
        {
            btRoomate.BackColor = Color.Transparent;
            btElse.BackColor = Color.Transparent;
            btBroken.BackColor = Color.White;

            gbBrokenShow.Visible = true;
            gbBrokenShow.Size = new Size(899, 391);
            gbBrokenShow.Location = new Point(22, 304);

            gbElse.Visible=false;
            gbShowRoomate.Visible=false;
        }

        private void btLogOutAdmin_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to log out?",
            "Log out?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                tabControl.SelectTab("HomePage");
                loggedUser = null;
                Size = new Size(934, 766);
                tabControl.Location = new Point(-5, -18);
                panelNavMenuAdmin.Visible = false;
            }

        }

        private void btHomeAdmin_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminPage");
            panel42.Top = btHomeAdmin.Top;
            btHomeAdmin.BackColor = Color.Gray;
            ChangeBtColor(btHomeAdmin);
        }

        private void btAccountsAdmin_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAccount");
            panel42.Top = btAccountsAdmin.Top;
            btAccountsAdmin.BackColor = Color.Gray;
            ChangeBtColor(btAccountsAdmin);
        }

        private void btAnnouncementsAdmin_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminAnouncments");
            panel42.Top = btAnnouncementsAdmin.Top;
            btAnnouncementsAdmin.BackColor = Color.Gray;
            ChangeBtColor(btAnnouncementsAdmin);
        }

        private void btRentAdmin_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRent");
            panel42.Top = btRentAdmin.Top;
            btRentAdmin.BackColor = Color.Gray;
            ChangeBtColor(btRentAdmin);
        }

        private void btComplainsAdmin_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminComplaints");
            panel42.Top = btComplainsAdmin.Top;
            btComplainsAdmin.BackColor = Color.Gray;
            ChangeBtColor(btComplainsAdmin);
        }

        private void btRulesAdmin_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("AdminRules");
            panel42.Top = btRulesAdmin.Top;
            btRulesAdmin.BackColor = Color.Gray;
            ChangeBtColor(btRulesAdmin);
        }

        private void btAdminRulesActive_Click(object sender, EventArgs e)
        {
            btAdminRulesActive.BackColor = Color.White;
            btAdminRulesReq.BackColor = Color.Transparent;

            gbAdminActive.Visible = true;
            gbAdminRequested.Visible = false;

            gbAdminActive.Location = new Point(153, 209);
            gbAdminActive.Size = new Size(706, 507);
        }

        private void btAdminRulesReq_Click(object sender, EventArgs e)
        {
            btAdminRulesActive.BackColor = Color.Transparent;
            btAdminRulesReq.BackColor = Color.White;

            gbAdminActive.Visible = false;
            gbAdminRequested.Visible = true;

            gbAdminRequested.Location = new Point(153, 209);

            gbAdminRequested.Size = new Size(706, 507);
        }

        //END OF BUTTONS
        ////////////////////////////////////////////////////////////////////////////////////
    }
}
