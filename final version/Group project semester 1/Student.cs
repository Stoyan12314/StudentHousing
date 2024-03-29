﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    [Serializable()]
    public class Student
    {
        private string firstName;
        private string lastName;
        private string username;
        private string email;
        private int age;
        private string password;
        private string apartment;
        private bool rent;


        public Student(string name, string lastName, string username, string email, int age, string password, string apartment, bool rent)
        {
            this.firstName = name;
            this.lastName = lastName;
            this.username = username;
            this.email = email;
            this.age = age;
            this.password = password;
            this.apartment = apartment;
            this.rent = rent;
        }

        public Building Building { get; set; }

        public override string ToString()
        {

            if (rent)
            {
                return $"{username} - {Building} - {apartment} - {firstName} {lastName} - Rent payed!";
            }
            else
            {
                return $"{username} - {Building} - {apartment} - {firstName} {lastName} - Rent not payed!";
            }

        }


        //String which is returned fo the listbox on the Admin Home Page
        public string AdminInfo()
        {
            return $"{firstName} {lastName} - Living in {Building}, {apartment}";
        }

        public string AdminTableAnnouncement => $"{username}, {apartment}, {Building.BuildingName}";

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

        public string GetApartment()
        {
            return this.apartment;
        }
        public bool GetRent()
        {
            return this.rent;
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



        public void SetApartment(string apartment)
        {
            this.apartment = apartment;
        }
        public void SetRent(bool rent)
        {
            this.rent = rent;
        }





        public string GetInfo()
        {
            return $"{this.firstName} {this.lastName} is {this.age} years old. Email - {this.email}; Username - {this.username}; Password - {this.password}. Living in: Building {Building} {apartment} \n";
        }


    }

    
}
