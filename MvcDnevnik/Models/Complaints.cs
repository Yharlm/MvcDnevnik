

namespace MvcDnevnik.Models
{
    public enum ComplaintStatus
    {
        Positive,
        Negative
    }
    public class Complaints
    {

        

        public int ID { get; set; }
        public Student Student { get; set; }

        public Subject Subject { get; set; }

        public DateTime Date { get; set; }
        public string ComplaintText { get; set; }

        public ComplaintStatus Status { get; set; }
    }
}
