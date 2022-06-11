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
        BuildingsManager buildingsManager= new BuildingsManager();
        public Form1()
        {
            InitializeComponent();    
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

                ShowRoommates(username, chosenBuilding, loggedUser);
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

        public void LoadText(Building building, Student student)
        {
            //Loading the top text of each STUDENT PAGE
            var list = new List<Label>()
            {
                 label19,label22,label25,label28,label31,label34,label37,label30
            };
            foreach (var item in list)
            {
                item.Text = $"Hello, {student.GetName()} {student.GetLastName()} ";
            }

            //Loading the bottom text of each STUDENT PAGE
            var list2 = new List<Label>()
            {
                lbLivingIn,label23,label26,label29,label32,label35,label38,label39
            };
            foreach(var item in list2)
            {
                item.Text = $"You are currently living in: {chosenBuilding.BuildingName}, {student.GetApartment()}";
            }




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
            tabControl.SelectTab("StudentGrocerie");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGrocerie");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGrocerie");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGrocerie");
        }

        private void button47_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGrocerie");
        }

        private void button55_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGrocerie");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentGrocerie");
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
            tabControl.SelectTab("StudentGrocerie");
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

        private void btnBackFromRentPayment_Click(object sender, EventArgs e)
        {
            tabControl.SelectTab("StudentRent");
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
            {
                tabControl.SelectTab("HomePage");
                currentApartment.cartGroceries.Clear();
                loggedUser = null;
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

        private void AdminAccount_Click(object sender, EventArgs e)
        {

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



        //END OF BUTTONS
        ////////////////////////////////////////////////////////////////////////////////////



    }
}
