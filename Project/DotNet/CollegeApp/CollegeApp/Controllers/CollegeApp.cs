using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CollegeApp.Data.Repository;
using CollegeApp.Models;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CollegeApp : ControllerBase
    {
        private readonly ICollegeRepository<Course> _courseRepository;
        private readonly ICollegeRepository<Student> _studentRepository;

        public CollegeApp(
            ICollegeRepository<Course> courseRepository,
            ICollegeRepository<Student> studentRepository)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }

        // Course Endpoints - All users can access GET endpoints
        [HttpGet("Courses/All")]
        public async Task<ActionResult<List<Course>>> GetAllCourses()
        {
            var courses = await _courseRepository.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("Courses/{id}")]
        public async Task<ActionResult<Course>> GetCourseById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var course = await _courseRepository.GetByIdAsync(id);

            if (course == null)
            {
                return NotFound($"Course with ID {id} not found");
            }

            return Ok(course);
        }

        [HttpGet("Courses/Name/{name}")]
        public async Task<ActionResult<Course>> GetCourseByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be empty");
            }

            var course = await _courseRepository.GetByName(name);

            if (course == null)
            {
                return NotFound($"Course with name '{name}' not found");
            }

            return Ok(course);
        }

        // Admin only endpoints for Courses
        [HttpPost("Courses/Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateCourse([FromBody] CourseDTO courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data cannot be null");
            }

            var course = new Course
            {
                CourseCode = courseDto.CourseCode,
                CourseName = courseDto.CourseName,
                Department = courseDto.Department,
                Semester = courseDto.Semester
            };

            await _courseRepository.AddAsync(course);
            return Ok(new { message = "Course created successfully", id = course.CourseId });
        }

        [HttpPut("Courses/Update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] CourseDTO courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Course data cannot be null");
            }

            var existingCourse = await _courseRepository.GetByIdAsync(id);
            if (existingCourse == null)
            {
                return NotFound($"Course with ID {id} not found");
            }

            existingCourse.CourseCode = courseDto.CourseCode;
            existingCourse.CourseName = courseDto.CourseName;
            existingCourse.Department = courseDto.Department;
            existingCourse.Semester = courseDto.Semester;

            await _courseRepository.UpdateAsync(id, existingCourse);
            return Ok(new { message = "Course updated successfully", id = id });
        }

        [HttpDelete("Courses/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound($"Course with ID {id} not found");
            }

            await _courseRepository.DeleteAsync(id);
            return Ok(new { message = "Course deleted successfully", success = true });
        }

        // Student Endpoints - All users can access GET endpoints
        [HttpGet("Students/All")]
        public async Task<ActionResult<List<Student>>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllAsync();
            return Ok(students);
        }

        [HttpGet("Students/{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound($"Student with ID {id} not found");
            }

            return Ok(student);
        }

        [HttpGet("Students/Name/{name}")]
        public async Task<ActionResult<Student>> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name cannot be empty");
            }

            var student = await _studentRepository.GetByName(name);

            if (student == null)
            {
                return NotFound($"Student with name '{name}' not found");
            }

            return Ok(student);
        }

        // Admin only endpoints for Students
        [HttpPost("Students/Create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateStudent([FromBody] StudentDTO studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data cannot be null");
            }

            var student = new Student
            {
                RollNumber = studentDto.RollNumber,
                Name = studentDto.Name,
                Email = studentDto.Email,
                Phone = studentDto.Phone,
                Address = studentDto.Address,
                DateOfBirth = studentDto.DateOfBirth,
                Gender = studentDto.Gender,
                CourseId = studentDto.CourseId
            };

            await _studentRepository.AddAsync(student);
            return Ok(new { message = "Student created successfully", id = student.StudentId });
        }

        [HttpPut("Students/Update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateStudent(int id, [FromBody] StudentDTO studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data cannot be null");
            }

            var existingStudent = await _studentRepository.GetByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound($"Student with ID {id} not found");
            }

            existingStudent.RollNumber = studentDto.RollNumber;
            existingStudent.Name = studentDto.Name;
            existingStudent.Email = studentDto.Email;
            existingStudent.Phone = studentDto.Phone;
            existingStudent.Address = studentDto.Address;
            existingStudent.DateOfBirth = studentDto.DateOfBirth;
            existingStudent.Gender = studentDto.Gender;
            existingStudent.CourseId = studentDto.CourseId;

            await _studentRepository.UpdateAsync(id, existingStudent);
            return Ok(new { message = "Student updated successfully", id = id });
        }

        [HttpDelete("Students/Delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID must be greater than 0");
            }

            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found");
            }

            await _studentRepository.DeleteAsync(id);
            return Ok(new { message = "Student deleted successfully", success = true });
        }
    }
}
