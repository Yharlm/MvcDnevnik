namespace MvcDnevnik.Models
{
    public class User
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; } // "Teacher", "Student", "Parent"

        public string Email { get; set; }

        public string PhoneNumber { get; set; }



        // Additional properties can be added as needed
    }
}
