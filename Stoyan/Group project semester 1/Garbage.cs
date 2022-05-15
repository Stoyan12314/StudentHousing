using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    public class Garbage
    {
        private string garbage;

        public Garbage(string newGarbage)
        {
            this.garbage = newGarbage;
        }

        public string GetInfo()
        {
            return this.garbage;
        }
    }
}
