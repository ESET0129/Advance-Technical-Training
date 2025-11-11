using dataaccess;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.Pages
{
    public class Firstbase : ComponentBase
    {
        public IEnumerable<Employee> Employee {get; set;}

        protected override void OnInitialized()
        {
            LoadEmployee();
        }
        private void LoadEmployee()
        {
            Employee = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Position = "Developer" },
                new Employee { Id = 2, Name = "Jane Smith", Position = "Designer" },
                new Employee { Id = 3, Name = "Sam Brown", Position = "Manager" }
            };
        }

    }
}
