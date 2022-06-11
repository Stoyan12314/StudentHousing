using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    public class CleaningSchedule
    {
        private string cleaner;
        private string date;
        private string selectedRoom;
     

        public CleaningSchedule(string cleanner, string date, string selectedRoom)
        {
            this.date = date;
            this.selectedRoom = selectedRoom;
            this.cleaner = cleanner;
         
        }
       
        public string GetName()
        {
            return this.cleaner;
        }
        public string GetDate()
        {
            return this.date;
        }
        public string SelectedRoom()
        {
            return this.selectedRoom;
        }
        public string GetInfo()
        {
            return $"{cleaner}-{date}-{selectedRoom}";
        }
    }
}
