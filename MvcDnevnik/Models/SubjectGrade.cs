namespace MvcDnevnik.Models
{
    public class SubjectGrade
    {
        public string Subject { get; set; }

        public double Grade { get; set; }

        public int SubjectId { get; set; }

        public GradeType GradeType { get; set; }
    }
}
