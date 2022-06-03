using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    public class Groceries
    {
        private string buyer;
        private string date;
        private string grocerie;

        public Groceries(string buyer ,string grocerie, string date)
        {
            this.grocerie = grocerie;
            this.date = date; 
            this.buyer = buyer;
        }
        public string GetBuyer()
        {
            return this.buyer;  
        }
        public string GetDate()
        {
            return this.date;
        }
        public string GetGrocerie()
        {
            return this.grocerie;
        }
        public string Info()
        {
            return $"{buyer}-{date}-{grocerie}";
        }
    }
}
