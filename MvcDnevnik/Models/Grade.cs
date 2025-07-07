namespace MvcDnevnik.Models
{
    public class Grade
    {
        public int ID { get; set; }
        public int Value { get; set; }
        
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
