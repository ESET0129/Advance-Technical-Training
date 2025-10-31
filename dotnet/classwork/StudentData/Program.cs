//namespace StudentData
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello, World!");
//        }
//    }
//}

using StudentData;
var students = new List<Student>
            {
                new Student{ Id=1, Name="Asha", Course="C#", Marks=92, City="Bangalore"},
                new Student{ Id=2, Name="Ravi", Course="Java", Marks=85, City="Pune"},
                new Student{ Id=3, Name="Sneha", Course="Python", Marks=78, City="Hyderabad"},
                new Student{ Id=4, Name="Kiran", Course="C#", Marks=88, City="Delhi"},
                new Student{ Id=5, Name="Meena", Course="Python", Marks=95, City="Bangalore"},
                new Student{ Id=6, Name="Vijay", Course="C#", Marks=82, City="Chennai"},
                new Student{ Id=7, Name="Deepa", Course="Java", Marks=91, City="Mumbai"},
                new Student{ Id=8, Name="Arjun", Course="Python", Marks=89, City="Hyderabad"},
                new Student{ Id=9, Name="Priya", Course="C#", Marks=97, City="Pune"},
                new Student{ Id=10, Name="Rohit", Course="Java", Marks=74, City="Delhi"}
            };

//Q1:


var topScorers = students
            .GroupBy(s => s.Course)
            .Select(group => group.OrderByDescending(s => s.Marks).First());

foreach (var student in topScorers)
{
    Console.WriteLine($"{student.Course}: {student.Name} ({student.Marks})");
}

//Q2:Display average marks of all students city-wise.

var cityAverages = students
            .GroupBy(s => s.City)
            .Select(group => new
            {
                City = group.Key,
                AverageMarks = group.Average(s => s.Marks)
            });

foreach (var result in cityAverages)
{
    Console.WriteLine($"{result.City}: {result.AverageMarks}");
}

//Q3:Display names and marks of students ranked by marks.
var rankedStudents = students
            .OrderByDescending(s => s.Marks);

Console.WriteLine(" Student Rank Report ");

// Display the ranked list
foreach (var student in rankedStudents)
{
    Console.WriteLine($"{student.Name}: {student.Marks} : {student.City}");
}





/*Student Tasks
 
1.Find the highest scorer in each course.
 
2.Display average marks of all students city-wise.
 
3.Display names and marks of students ranked by marks.
*/