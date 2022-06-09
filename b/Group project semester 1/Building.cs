using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    public class Building
    {
        private string buildingName;

        //Each building has a list of apartments (5 apartments per Building)
        List<Apartaments> apartamentsInBuilding;
        

        List<string> usernamesList = new List<string>();


        //Constructor: Creating the building with the apartments in it
        public Building(string name)
        {
            //The list is for the particular building

            BuildingName = name;
            apartamentsInBuilding = new List<Apartaments>()
            {
             new Apartaments("Apartment1",6),
             new Apartaments("Apartment2",6),
             new Apartaments("Apartment3",6),
             new Apartaments("Apartment4",3),
             new Apartaments("Apartment5",3),
            };
        }

   
            //To create the apartments in the building
        public string BuildingName { get; }
        
        public void AddToListWithUserNames(string username)
        {
            usernamesList.Add(username);
        }
        //Chosing the building and the apartment in which the student should be added
        public bool AddStudentToApartament( string Firstname, string lastName, string username, string email, int age, string password, string selectedApartament, bool rent)
        {

            //Check if the student is added or not
            bool returnBool=default;
            foreach (Apartaments apartament in apartamentsInBuilding)
            {
                if (apartament.GetApartamentName() == selectedApartament)
                {
                    returnBool =  apartament.AddStudent(Firstname,lastName, username, email, age, password, selectedApartament, rent);
                    break;
                }
            }
            return returnBool;  
        }

        public List<string> ReturnListUsers()
        {
            return usernamesList;
        }

        //Return all Students from that Building
        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            foreach (Apartaments apartament in apartamentsInBuilding)
            {

                foreach (Student student in apartament.GetAllStudents())
                {
                    students.Add(student);
                }
            }
            return students;
        }



        //Returning the list of the students in each apartment
        public List<Student> ShowAllStudents(Student loggedUser)
        {

            List<Student> listWithStudents = new List<Student>();
            foreach (Apartaments apartament in apartamentsInBuilding)
            {
                if (loggedUser.GetApartment() == apartament.GetApartamentName())
                {
                    foreach (Student student in apartament.GetAllStudents())
                    {
                        listWithStudents.Add(student);
                    }
                }
            
                
            }
            return listWithStudents;
        }


        //Checking if the login info is correct, checking username and password and return the logged student
        public Student CheckUserNameAndPassword(string username,string password)
        {
            foreach (Apartaments apartament in apartamentsInBuilding)
            {
                foreach (Student student in apartament.GetAllStudents())
                {
                    if (student.GetUsername() == username && student.GetPassword() == password)
                    {
                        return student;
                    }
                }

            }
            return null;
        }
        //return apartament by selected user
        public Apartaments ReturnsChosenApartament(Student studentApartament)
        {
            foreach (Apartaments apartament in apartamentsInBuilding)
            {
                foreach (Student student in apartament.GetAllStudents())
                {
                    if (student == studentApartament)
                    {
                        return apartament;
                    }  
                } 
            }
            return null;
        }


        //Return all the registrated students in this particular apartment
        public List<Student> ReturnStudentsByChosenApartament(string apartamentName)
        {
            List<Student> students= null;
            foreach (Apartaments apartament in apartamentsInBuilding)
            {
                if (apartament.GetApartamentName() == apartamentName)
                {
                    students= apartament.GetAllStudents();
                }
            }
            return students;
        }

        //Return logged user
        //public bool CheckUserNameAndPassword(string username, string password)
        //{
        //    foreach (Apartaments apartament in apartamentsInBuilding)
        //    {
        //        foreach (Student student in apartament.GetAllStudents())
        //        {
        //            if (student.GetUsername() == username && student.GetPassword() == password)
        //            {
        //                return true;
        //            }
        //        }

        //    }
        //    return false;
        //}


    }
}
