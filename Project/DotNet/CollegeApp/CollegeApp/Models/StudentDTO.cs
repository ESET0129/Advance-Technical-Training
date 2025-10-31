namespace CollegeApp.Models
{
    public class StudentDTO
    {
        public string RollNumber { get; set; } 

        public string Name { get; set; } 

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public string Gender { get; set; }

        public int CourseId { get; set; }
    }
}
