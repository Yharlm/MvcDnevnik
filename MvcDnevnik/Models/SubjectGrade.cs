namespace MvcDnevnik.Models
{
    public class SubjectGrade
    {

        public string Subject { get; set; }

        public double Grade { get; set; }

        public int SubjectId { get; set; }

        public GradeType GradeType { get; set; }

        public DateTime date { get; set; }

        public bool isFirstSemester
        {
            get 
            {
                return date.Month >= 9 || date.Month <= 6;
            }
        }
    }
}
