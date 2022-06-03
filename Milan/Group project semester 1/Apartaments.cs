using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    public class Apartaments
    {
        private string name;
        private int ocupants;
        List<Student> listOfStudents;
        List<Garbage> listOfGarbage;
        List<CleaningSchedule> listCleaningSchedules;
        List<Groceries> listOfGroceries;

        //creating apartment; giving it name. Creating a list of students and tasks for each apartment
        public Apartaments(string name, int numberOfOcupants)
        {
            this.name = name; 
            this.ocupants= numberOfOcupants;
            listOfStudents= new List<Student>();
            listOfGarbage = new List<Garbage>();
            listCleaningSchedules = new List<CleaningSchedule>();
            listOfGroceries = new List<Groceries>();
        }
        public string GetApartamentName()
        { 
            return name;
        }
        //get list with cleaning schedule
        public List<CleaningSchedule> GetListCleaningSchedule()
        {
            return this.listCleaningSchedules;
        }
        //removes the shedule from the list if the input id is present
        public void RemoveCleaningShedule(string selectedTask, string user)
        {
            //gets the selected task from the listbox and seperates the values to different variables
            
            string[] delimiter = { "-" };
            string[] pieces = selectedTask.Split(delimiter, StringSplitOptions.None);
            foreach (CleaningSchedule schedule in this.listCleaningSchedules.ToList())
            {  //checks for the correct task, which is being deleted, from the creator of the task
                if (pieces[0] == schedule.GetName() && pieces[0] == user && pieces[1]==schedule.GetDate() && pieces[2]== schedule.SelectedRoom())
                {
                    this.listCleaningSchedules.Remove(schedule);
                    break;
                }  
            }
        }
        //creates a grocerie
        public void AddGroceries(string buyer, string grocerie, string date)
        {
            Groceries groceries = new Groceries(buyer, grocerie, date);
            listOfGroceries.Add(groceries);
        }
        public void RemoveGrocerie(string selectedGrocery, string user)
        {
            string[] delimiter = { "-" };
            string[] pieces = selectedGrocery.Split(delimiter, StringSplitOptions.None);
            foreach (Groceries grocery in this.listOfGroceries.ToList())
            {  //checks for the correct task, which is being deleted, from the creator of the task
                if (pieces[0] == grocery.GetBuyer() && pieces[0] == user && pieces[1] == grocery.GetDate() && pieces[2] == grocery.GetGrocerie())
                {
                    this.listOfGroceries.Remove(grocery);
                    break;
                }
            }
        }
        public void AddCleaningTask(string cleaner, string date, string selectedRoom)
        {
            CleaningSchedule cleaning = new CleaningSchedule(cleaner, date, selectedRoom);
            listCleaningSchedules.Add(cleaning);
        }
        //Checking if it has place for the student if yes - adding student.
        public bool AddStudent(string name, string lastName, string username, string email, int age, string password, string building, string apartment, bool rent)
        {
            if (listOfStudents.Count < ocupants)
            {
                Student student = new Student(name, lastName, username, email, age, password, building, apartment, rent);
                
                this.listOfStudents.Add(student);
                return true;
            }
            else
            {
                return false;
            }
            
            
        }
        

        //Returning student list of the apartment you picked
        public List<Student> GetAllStudents()
        {
            return this.listOfStudents;
        }
        //returns the groceries list
        public List<Groceries> GetGroceries()
        {
            return this.listOfGroceries;
        }
        public List<Garbage> GetGarbages()
        {
            return this.listOfGarbage;
        }
        //Adding garbage
        public void AddGarbage(string newGarbage, string date, string thrower)
        {
            Garbage garbage = new Garbage(newGarbage, date, thrower);
            listOfGarbage.Add(garbage);
        }


        //Removing garbage
        public void RemoveGarbage(string selectedTask, string user)
        {
            string[] delimiter = { "-" };
            string[] pieces = selectedTask.Split(delimiter, StringSplitOptions.None);
            foreach (Garbage garbage in this.listOfGarbage.ToList())
            {
                if (pieces[0] == garbage.GetThrower() && pieces[0] == user && pieces[1] == garbage.GetGarbage() && pieces[2] == garbage.GetDate())
                {
                    this.listOfGarbage.Remove(garbage);
                    break;
                }
            }
            
           
        }
        //public void RemoveCleaningShedule(string selectedTask, string user)
        //{
        //    //gets the selected task from the listbox and seperates the values to different variables

        //    string[] delimiter = { "-" };
        //    string[] pieces = selectedTask.Split(delimiter, StringSplitOptions.None);
        //    foreach (CleaningSchedule schedule in this.listCleaningSchedules.ToList())
        //    {  //checks for the correct task, which is being deleted, from the creator of the task
        //        if (pieces[0] == schedule.GetName() && pieces[0] == user && pieces[1] == schedule.GetDate() && pieces[2] == schedule.SelectedRoom())
        //        {
        //            this.listCleaningSchedules.Remove(schedule);
        //            break;
        //        }
        //    }
        //}




        private void RemoveTask(Garbage task)
        {
            listOfGarbage.Remove(task);
        }
      
    }
}
