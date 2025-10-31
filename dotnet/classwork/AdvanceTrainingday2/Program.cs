namespace AdvanceTrainingday2
{
    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //Console.WriteLine("Hello, World!");

    //    }
    //}
    //class Human
    //{

    //    private string occupation;
    //    private string name;
    //    private int age;

    //    public Human(string occupation, string name, int age)
    //    {
    //        this.occupation = occupation;
    //        this.name = name;
    //        this.age = age;
    //    }

    //    public void DisplayOccupation()
    //    {
    //        Console.WriteLine($"Occupation: {this.occupation}");
    //    }


    //    public void DisplayName()
    //    {

    //        Console.WriteLine($"Name: {this.name} and they are a {this.occupation}.");
    //    }

    //    public void DisplayAge()
    //    {
    //        Console.WriteLine($"{this.name}'s age is {this.age}.");
    //    }
    //}


    /*
     Task 1: Library Management System
    Create a class Book with fields: bookId, title, author, isIssued.
    Implement:

    Parameterized constructor to initialize book details.
    Methods: IssueBook(), ReturnBook(), DisplayBookDetails().

    Create multiple book objects and simulate issuing and returning books.
     -------------------------------------------------------------------------------------------------------------
    Task 2:Movie Ticket Booking System

    Create a class Movie with fields: movieName, totalSeats, bookedSeats.

    Implement:

    Constructor to initialize movie details.

    Methods: BookSeats(int), CancelSeats(int), DisplayAvailableSeats().

    Create objects and simulate booking and cancellation.

    Challenge: Prevent booking more seats than available.
     -----------------------------------------------------------------------------------------------------------------
    Task 3: Static Member and Method Practice

    Create a class Company with fields: employeeName, employeeId, and a static field companyName.

    Implement:

    Constructor to initialize employeeName and employeeId.

    Static method DisplayCompanyName().

    Create multiple objects and demonstrate that all employees share the same company name, while other fields are individual.
     ------------------------------------------------------------------------------------------------------------------------------
    Task 4:Employee Salary Management

    Create a class Employee with fields: id, name, basicSalary, hra, da, grossSalary.

    Implement:

    Constructor to initialize id, name, basicSalary.

    Method to calculate hra = 10% of basicSalary, da = 5% of basicSalary, grossSalary = basic + hra + da.

    Create objects and display detailed salary slips.
    -------------------------------------------------------------------------------------------------------------------------------------------
     */
    //task 1
    public class Book
    {
        private int bookId;
        private string title;
        private string author;
        private bool isIssued;

        public Book(int id, string t, string a)
        {
            bookId = id;
            title = t;
            author = a;
            isIssued = false;
        }

        public void IssueBook()
        {
            if (!isIssued)
            {
                isIssued = true;
                Console.WriteLine($"{title} is issued.");
            }
            else
            {
                Console.WriteLine($"{title} is already issued.");
            }
        }

        public void ReturnBook()
        {
            if (isIssued)
            {
                isIssued = false;
                Console.WriteLine($"{title} got returned.");
            }
            else
            {
                Console.WriteLine($"{title} was not issued.");
            }
        }

        public void DisplayBookDetails()
        {
            
            Console.WriteLine($"\nBook ID: {bookId}");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Author: {author}");
            Console.WriteLine($"Status: {(isIssued ? "Issued" : "Available")}");
        }
    }

    public class LibraryManagement
    {
        public static void Main(string[] args)
        {
            Book book1 = new Book(101, "Langoor khayega angoor", "ANONYMUS");
            Book book2 = new Book(102, "kela on thela", "Mr .X");

            book1.DisplayBookDetails();
            book2.DisplayBookDetails();

            book1.IssueBook();
            book2.IssueBook();
            //book2.IssueBook();

            book1.DisplayBookDetails();
            book2.DisplayBookDetails();

            book1.ReturnBook();
            book2.ReturnBook();
            //book1.ReturnBook();

            book1.DisplayBookDetails();
        }
    }


    //TASK 2
    public class MovieBooking
    {
        private string movieName;
        private int totalSeats;
        private int bookedSeats;

        public MovieBooking(string m, int t, int b)
        {
            movieName = m;
            totalSeats = t;
            bookedSeats = b;
        }

        public void BookSeats()
        {
            //int requiredseats = 3;
            if (totalSeats > 0)
            {
                bool confirm = true;
                Console.WriteLine("number of seats required:");
                int r = Convert.ToInt32(Console.ReadLine());
                if (r > 0)
                {
                    Console.WriteLine("seats booked");
                    totalSeats--;

                }

            }
            else
            {
                Console.WriteLine("seats not booked");
            }
        }

        public void CancelSeats()
        {
            Console.WriteLine("enter number of seats to be cancelled:");
            int c = Convert.ToInt32(Console.ReadLine());
            //int cancelseats = 5;
            totalSeats -= c;
        }

    }

    //}
}
