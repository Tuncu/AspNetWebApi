using AspNetWebApiRouting.AttributeBasedRouting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApiRouting.AttributeBasedRouting.Controllers
{
    public class EmployeeController : ApiController
    {
        static List<Employee> employees = new List<Employee>()
        {
            new Employee {Id=1,Name="Person 1" },
            new Employee {Id=2,Name="Person 2" },
            new Employee {Id=3,Name="Person 3" }
        };

        
        public IEnumerable<Employee> Get()
        {
            return employees;
        }

        
        public Employee Get(int id)
        {
            return employees.FirstOrDefault(x => x.Id == id);
        }
        [Route("api/employee/{id}/tasks")]
        public IEnumerable<string> GetEmployeeTasks(int id)
        {
            switch (id)
            {
                case 1:
                    return new List<string> { "Task 1-1", "Task 1-2", "Task 1-3" };
                case 2:
                    return new List<string> { "Task 2-1", "Task 2-2", "Task 2-3" };
                case 3:
                    return new List<string> { "Task 3-1", "Task 3-2", "Task 3-3" };
                default:
                    return null;
            }
        }
    }
}
