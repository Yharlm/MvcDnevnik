namespace MvcDnevnik.Models
{
    public class Complaint_Subject
    {
        public int ID { get; set; }

        public List<Complaints> Complaints { get; set; } = new List<Complaints>();
        public Subject Subject { get; set; }


    }
}
