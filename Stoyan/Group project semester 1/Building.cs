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
        List<Apartaments> apartamentsInBuilding;
        
        
        public Building(string name)
        {
            apartamentsInBuilding = new List<Apartaments>();
            this.buildingName = name;
            Apartaments apartament;
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment1",6));
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment2",6));
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment3",3));
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment4",3));
            apartamentsInBuilding.Add(apartament = new Apartaments("Apartment5",2));
        }

        public bool AddStudentToApartament( string Firstname, string lastName, string username, string email, int age, string password, string selectedApartament)
        {
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
