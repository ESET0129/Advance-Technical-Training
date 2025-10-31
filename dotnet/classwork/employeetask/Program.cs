//namespace employeetask
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello, World!");
//        }
//    }
//}

using employeetask;
//Employee Data
var employees = new List<Employee>
            {
                new Employee{ Id=1, Name="Ravi", Department="IT", Salary=85000, Experience=5, Location="Bangalore"},
                new Employee{ Id=2, Name="Priya", Department="HR", Salary=52000, Experience=4, Location="Pune"},
                new Employee{ Id=3, Name="Kiran", Department="Finance", Salary=73000, Experience=6, Location="Hyderabad"},
                new Employee{ Id=4, Name="Asha", Department="IT", Salary=95000, Experience=8, Location="Bangalore"},
                new Employee{ Id=5, Name="Vijay", Department="Marketing", Salary=68000, Experience=5, Location="Mumbai"},
                new Employee{ Id=6, Name="Deepa", Department="HR", Salary=61000, Experience=7, Location="Delhi"},
                new Employee{ Id=7, Name="Arjun", Department="Finance", Salary=82000, Experience=9, Location="Bangalore"},
                new Employee{ Id=8, Name="Sneha", Department="IT", Salary=78000, Experience=4, Location="Pune"},
                new Employee{ Id=9, Name="Rohit", Department="Marketing", Salary=90000, Experience=10, Location="Delhi"},
                new Employee{ Id=10, Name="Meena", Department="Finance", Salary=66000, Experience=3, Location="Mumbai"}
            };

//List<string> allEmployees = new List<string>();

//foreach (var employee in employees )
//{
//    allEmployees.Add(employee.Name);
//}

//Q1
//to find the all data 
//var allEmployees = employees.Where(employees => employees.Department == "IT");

//foreach (var name in allEmployees)
//{
//    Console.WriteLine(name.Name);
//}

//Q2: List names and salaries of employees who earn more than 70,000.

//var allEmployees = employees.Where(employees => employees.Salary >= 70000);

//foreach (var name in allEmployees)
//{
//    Console.WriteLine($"{name.Name}--{name.Salary}");
//}

//Q3: Find all employees located in Bangalore.
//var allEmployees = employees.Where(employees => employees.Location =="Bangalore");

//foreach (var name in allEmployees)
//{
//    Console.WriteLine($"{name.Name}");
//}

//Q4:Display employees having more than 5 years of experience.
//var allEmployees = employees.Where(employees => employees.Experience >= 5);

//foreach (var name in allEmployees)
//{
//    Console.WriteLine($"{name.Name}");
//}

//Q5:Show names of employees and their salaries in ascending order of salary.
//var allEmployees = employees.OrderBy(employees => employees.Salary);

//foreach (var name in allEmployees)

//{
//    Console.WriteLine($"{name.Name} -- {name.Salary}");
//}


/*
 Data :   
//Employee Data
var employees = new List<Employee>
            {
                new Employee{ Id=1, Name="Ravi", Department="IT", Salary=85000, Experience=5, Location="Bangalore"},
                new Employee{ Id=2, Name="Priya", Department="HR", Salary=52000, Experience=4, Location="Pune"},
                new Employee{ Id=3, Name="Kiran", Department="Finance", Salary=73000, Experience=6, Location="Hyderabad"},
                new Employee{ Id=4, Name="Asha", Department="IT", Salary=95000, Experience=8, Location="Bangalore"},
                new Employee{ Id=5, Name="Vijay", Department="Marketing", Salary=68000, Experience=5, Location="Mumbai"},
                new Employee{ Id=6, Name="Deepa", Department="HR", Salary=61000, Experience=7, Location="Delhi"},
                new Employee{ Id=7, Name="Arjun", Department="Finance", Salary=82000, Experience=9, Location="Bangalore"},
                new Employee{ Id=8, Name="Sneha", Department="IT", Salary=78000, Experience=4, Location="Pune"},
                new Employee{ Id=9, Name="Rohit", Department="Marketing", Salary=90000, Experience=10, Location="Delhi"},
                new Employee{ Id=10, Name="Meena", Department="Finance", Salary=66000, Experience=3, Location="Mumbai"}
            };

/*
 *Tasks :
Employee Tasks
 
1.Display all employees working in the IT department.
 
2.List names and salaries of employees who earn more than 70,000.
 
3.Find all employees located in Bangalore.
 
4.Display employees having more than 5 years of experience.
 
5.Show names of employees and their salaries in ascending order of salary.
 
//Product Data
 
var products = new List<Product>
            {
                new Product{ Id=1, Name="Laptop", Category="Electronics", Price=75000, Stock=15 },
                new Product{ Id=2, Name="Smartphone", Category="Electronics", Price=55000, Stock=25 },
                new Product{ Id=3, Name="Tablet", Category="Electronics", Price=30000, Stock=10 },
                new Product{ Id=4, Name="Headphones", Category="Accessories", Price=2000, Stock=100 },
                new Product{ Id=5, Name="Shirt", Category="Fashion", Price=1500, Stock=50 },
                new Product{ Id=6, Name="Jeans", Category="Fashion", Price=2200, Stock=30 },
                new Product{ Id=7, Name="Shoes", Category="Fashion", Price=3500, Stock=20 },
                new Product{ Id=8, Name="Refrigerator", Category="Appliances", Price=45000, Stock=8 },
                new Product{ Id=9, Name="Washing Machine", Category="Appliances", Price=38000, Stock=6 },
                new Product{ Id=10, Name="Microwave", Category="Appliances", Price=12000, Stock=12 }
            };
 
//Student Data
 
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
 
//Order Data
 
var orders = new List<Order>
            {
                new Order{ OrderId=1001, CustomerId=1, Amount=2500, OrderDate=new DateTime(2025,5,12)},
                new Order{ OrderId=1002, CustomerId=2, Amount=1800, OrderDate=new DateTime(2025,5,13)},
                new Order{ OrderId=1003, CustomerId=1, Amount=4500, OrderDate=new DateTime(2025,5,20)},
                new Order{ OrderId=1004, CustomerId=3, Amount=6700, OrderDate=new DateTime(2025,6,01)},
                new Order{ OrderId=1005, CustomerId=4, Amount=2500, OrderDate=new DateTime(2025,6,02)},
                new Order{ OrderId=1006, CustomerId=2, Amount=5600, OrderDate=new DateTime(2025,6,10)},
                new Order{ OrderId=1007, CustomerId=5, Amount=3100, OrderDate=new DateTime(2025,6,12)},
                new Order{ OrderId=1008, CustomerId=3, Amount=7100, OrderDate=new DateTime(2025,7,01)},
                new Order{ OrderId=1009, CustomerId=4, Amount=4200, OrderDate=new DateTime(2025,7,05)},
                new Order{ OrderId=1010, CustomerId=5, Amount=2900, OrderDate=new DateTime(2025,7,10)}
            };
 
 


