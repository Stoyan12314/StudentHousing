using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    [Serializable()]
    public class Garbage
    {
        private string garbage;
        private string date;
        private string thrower;
        public Garbage(string newGarbage, string date, string thrower)
        {
            this.garbage = newGarbage;
            this.date = date;
            this.thrower = thrower;
        }
        public string GetGarbage()
        {
            return this.garbage;
        }
        public string GetDate()
        {
            return this.date;
        }
        public string GetThrower()
        {
            return this.thrower;
        }
        public string Info()
        {
            return $"{this.thrower}-{this.garbage}-{this.date}";
        }
    }
}
