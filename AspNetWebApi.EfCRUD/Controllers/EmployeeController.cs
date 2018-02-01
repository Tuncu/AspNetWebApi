using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetWebApi.EfCRUD.Controllers
{
    public class EmployeeController : ApiController
    {
        EmployeDBEntities db = new EmployeDBEntities();
        //public IEnumerable<Employee> Get()
        //{
        //    return db.Employees.ToList();
        //}

            /*query string kullanma*/
        public HttpResponseMessage Get(string gender="all",int? top=0)
        {
            IQueryable<Employee> query = db.Employees;
            gender = gender.ToLower();

            switch (gender)
            {
                case "all":
                    break;
                case "erkek":
                case "kadın":
                    query = query.Where(e => e.Gender.ToLower() == gender);
                    break;
                default:
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"{gender} geçerli değil");
            }

            if(top>0)
            {
                query = query.Take(top.Value);
            }

            return Request.CreateResponse(HttpStatusCode.OK, query.ToList());
        }

        public Employee Get(int id)
        {
            return db.Employees.SingleOrDefault(x => x.Id == id);
        }

        //Tabi hoş değiş bişey döndürmesi lazım
        //public void Post(Employee employee)
        //{
        //    db.Employees.Add(employee);
        //    db.SaveChanges();
        //}

        public HttpResponseMessage Post(Employee employee)
        {            
            try
            {
                db.Employees.Add(employee);
                if (db.SaveChanges()>0)
                {
                    //return Request.CreateResponse(HttpStatusCode.Created, employee);

                    HttpResponseMessage message= Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.Id);
                    return message;
                } 
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Islem yapılamadadı");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

           
        }


        public HttpResponseMessage Put(Employee employee)
        {
            try
            {
                var emp = db.Employees.SingleOrDefault(e => e.Id == employee.Id);

                if(emp==null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee Id: " + emp.Id);
                }
                else
                {
                    emp.Name = employee.Name;
                    emp.Salary = employee.Salary;
                    emp.Surname = employee.Surname;
                    emp.Gender = employee.Gender;

                    if (db.SaveChanges() > 0)
                    {
                        //return Request.CreateResponse(HttpStatusCode.Created, employee);

                        HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, employee);
                        message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.Id);
                        return message;
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Islem yapılamadadı");
                    }
                }   
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var emp = db.Employees.SingleOrDefault(x => x.Id == id);
                
                if(emp==null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee Id: " + emp.Id);
                }
                else
                {
                    db.Employees.Remove(emp);
                    if (db.SaveChanges() > 0)
                    {
                        //return Request.CreateResponse(HttpStatusCode.Created, employee);

                        return Request.CreateResponse(HttpStatusCode.OK);
                        
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Islem yapılamadadı");
                    }

                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
