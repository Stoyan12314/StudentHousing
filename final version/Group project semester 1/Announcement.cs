using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_project_semester_1
{
    [Serializable()]
    internal class Announcement
    {

        public static List<Announcement> Announcements { get; } = new List<Announcement>();
        public Announcement(string message)
        {
            Sender = "Admin";
            Message = message;
        }
        public string Sender { get; }
        public string Message { get; }
        public List<Student> Recevers { get; set; } = new List<Student>();
        public override string ToString()
        {
            return Message;
        }
    }
}
