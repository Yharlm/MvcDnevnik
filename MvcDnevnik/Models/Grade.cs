



using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public bool IsFirstSemester
        {
            get
            {
                return Date.Month >= 9 || Date.Month <= 6;
            }
            
        }
    }


}
