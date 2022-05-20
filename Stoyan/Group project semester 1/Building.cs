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
        
        
        //Constructor: Creating the building with the apartments in it
        public Building(string name)
        {

            //The list is for the particular building
            apartamentsInBuilding = new List<Apartaments>();
            this.buildingName = name;

            //To create the apartments in the building
            Apartaments apartament;
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment1",6));
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment2",6));
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment3",3));
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment4",3));
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment5",2));
        }


        //Chosing the building and the apartment in which the student should be added
        public bool AddStudentToApartament( string Firstname, string lastName, string username, string email, int age, string password, string selectedApartament)
        {

            //Check if the student is added or not
            bool returnBool=default;
            foreach (Apartaments apartament in apartamentsInBuilding)
            {
                if (apartament.GetApartamentName() == selectedApartament)
                {
                    returnBool =  apartament.AddStudent(Firstname,lastName, username, email, age, password, buildingName, selectedApartament);
                    break;
                }
            }
            return returnBool;  
        }


        //Returning the list of the students in each apartment
        public List<Student> ShowAllStudents()
        {

            List<Student> listWithStudents = new List<Student>();
            foreach (Apartaments apartament in apartamentsInBuilding)
            {
                foreach (Student student in apartament.GetAllStudents())
                {
                    listWithStudents.Add(student);
                }
                
            }
            return listWithStudents;
        }


        //Checking if the login info is correct, checking username and password
        public bool CheckUserNameAndPassword(string username,string password)
        {
            foreach (Apartaments apartament in apartamentsInBuilding)
            {
                foreach (Student student in apartament.GetAllStudents())
                {
                    if (student.GetUsername() == username && student.GetPassword() == password)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
        

    }
}
