using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    public class Student
    {
        private string firstName;
        private string lastName;
        private string username;
        private string email;
        private int age;
        private string password;
        private string building;
        private string apartment;


        public Student(string name, string lastName, string username, string email, int age,  string password,
           string building, string apartment)
        {
            this.firstName = name;
            this.lastName = lastName;
            this.username = username;
            this.email = email;
            this.age = age;
            this.password = password;
            this.building = building;
            this.apartment = apartment;
            


        }

        public Student()
        {

        }

        //Getters
        public string GetName()
        {
            return this.firstName;
        }
        public string GetLastName()
        {
            return this.lastName;
        }
        public string GetUsername()
        {
            return this.username;
        }
        public string GetEmail()
        {
            return this.email;
        }
        public int GetAge()
        {
            return this.age;
        }
 
        public string GetPassword()
        {
            return this.password;
        }

        public string GetBuilding()
        {
            return this.building;
        }

        public string GetApartment()
        {
            return this.apartment;
        }


        //Setters


        public void SetName(string name)
        {
            this.firstName = name;
        }
        public void SetLastName(string lastName)
        {
            this.lastName = lastName;
        }
        public void SetUsername(string username)
        {
            this.username = username;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        public void SetAge(int age)
        {
            this.age = age;
        }
        public void SetPassword(string password)
        {
            this.password = password;
        }

        public void SetBuilding(string building)
        {
            this.building = building;
        }

        public void SetApartment(string apartment)
        {
            this.apartment = apartment;
        }






        public string GetInfo()
        {
            return $"{this.firstName} {this.lastName} is {this.age} years old. Email - {this.email}; Username - {this.username}; Password - {this.password}. Living in: Building {building} {apartment} \n";
        }


    }

    
}
