



namespace MvcDnevnik.Models
{
    public enum GradeType : int
    {
        CURRENT = 0,
        SEMESTER_ONE = 1,
        SEMESTER_TWO = 2,
        YEAR = 3
    }
    public class Grade
    {
        public int ID { get; set; }
        public int Value { get; set; }
        
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public GradeType Type { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}
