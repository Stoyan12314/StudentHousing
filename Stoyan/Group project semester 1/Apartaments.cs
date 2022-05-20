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
        

        //creating apartment; giving it name. Creating a list of students and tasks for each apartment
        public Apartaments(string name, int numberOfOcupants)
        {
            this.name = name; 
            this.ocupants= numberOfOcupants;
            listOfStudents= new List<Student>();
            listOfGarbage = new List<Garbage>();
        }
        public string GetApartamentName()
        { 
            return name;
        }



        //Checking if it has place for the student if yes - adding student.
        public bool AddStudent(string name, string lastName, string username, string email, int age, string password, string building, string apartment)
        {
            if (listOfStudents.Count < ocupants)
            {
                Student student = new Student(name, lastName, username, email, age, password, building, apartment);
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


        //Adding tasks
        public void AddGarbage(string newGarbage)
        {
            Garbage garbage = new Garbage(newGarbage);
            listOfGarbage.Add(garbage);
        }


        //Removing tasks
        public void RemoveGarbage(string task)
        {
            foreach (Garbage garbage in listOfGarbage)
            {
                if (garbage.GetInfo() == task)
                {
                    RemoveTask(garbage);
                }
            }
           
        }
        private void RemoveTask(Garbage task)
        {
            listOfGarbage.Remove(task);
        }
      
    }
}
