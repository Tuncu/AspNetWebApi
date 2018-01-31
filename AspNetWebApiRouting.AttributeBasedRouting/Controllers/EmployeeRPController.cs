using AspNetWebApiRouting.AttributeBasedRouting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApiRouting.AttributeBasedRouting.Controllers
{
    [RoutePrefix("api/employeepr")]
    public class EmployeeRPController : ApiController
    {
        static List<Employee> employees = new List<Employee>()
        {
            new Employee {Id=1,Name="PersonA" },
            new Employee {Id=2,Name="PersonB" },
            new Employee {Id=3,Name="PersonC" }
        };

        [Route("")]
        public IEnumerable<Employee> Get()
        {
            return employees;
        }

        //[Route("{id}")]//A ile işaretlenmiş yerlerde hata verir nedeni neresi olduğunu bilemez
        //[Route("{id:int}")]
        //[Route("{id:int:min(1):max(3)}")]
        [Route("{id:int}",Name ="GetById")]//Rotaya özel isim verilmesi
        public Employee Get(int id)
        {
            return employees.FirstOrDefault(x => x.Id == id);
        }

        [Route("detail/{id:decimal?}")]//Route içinde default ve optional kullanım
        //[Route("detail/{id:decimal=2}")]//şeklinde de yapılabilir
        public Employee Get(decimal id = 2)
        {
            return employees.FirstOrDefault(x => x.Id == id);
        }

        //[Route("{name}")]//A ile işaretlenmiş yerlerde hata verir nedeni neresi olduğunu bilemez
        //constraintleri https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2 öğrenebilirsiniz
        [Route("{name:alpha:lastletter}")]
        public Employee Get(string name)
        {
            return employees.FirstOrDefault(x => x.Name.ToLower() == name.ToLower());
        }
        [Route("{id}/tasks")]
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

        [Route("~/api/all")]//prefix iptal eder
        //[Route("api/all")]//prefix var böyle çalışmaz api/employeepr/api/all
        public IEnumerable<string> GetTasks()
        {
            return new List<string> { "Task 1-1", "Task 1-2", "Task 1-3", "Task 2-1", "Task 2-2", "Task 2-3", "Task 3-1", "Task 3-2", "Task 3-3" };
        }

        /*
         * Route Name ile link oluşturma
         * Bir Post işlemi sonrası insert verinin mesajın ve datanın linkinide mesaj olarak eklenebilir
         * */

        [Route("add")]
        public HttpResponseMessage Post(Employee emp)
        {
            emp.Id = employees.Max(x => x.Id) + 1;
            employees.Add(emp);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            //response.Headers.Location = new Uri("/api/employeepr/" + emp.Id);
            //response.Headers.Location = new Uri(Request.RequestUri + "/" + emp.Id);
            response.Headers.Location = new Uri(Url.Link("GetById",new { id = emp.Id }));

            return response;
        }
    }
}
