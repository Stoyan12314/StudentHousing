using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    [Serializable()]
    public class Grocery
    {
        public Grocery(string grocerieName, double price)
        {
            if (grocerieName == "" || price == 0 )
            {
                throw new Exception("Requred data is not entered correctly");
            }
            GrocerieName = grocerieName;
            Price = price;
        }
        
        public Student Adder { get; set; }
        public string GrocerieName { get; }
        public int Amount { get; set; }
        public double Price { get; }

        public override string ToString()
        {
            return $"{GrocerieName}, Each: {Price}€ ";
        }
        public string Info()
        {
            return $"{GrocerieName} - {Amount}CU - {Price}$";
        }
    }
}
