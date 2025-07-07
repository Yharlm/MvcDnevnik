using Microsoft.Extensions.Hosting;

namespace MvcDnevnik.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Grade> Grades { get; } = new();

    }


}
