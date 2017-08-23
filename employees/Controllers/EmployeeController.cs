using employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace employees.Controllers
{
    public class employeeController : ApiController
    {
        public static List<employee> employees = new List<employee>();

        /// <summary>
        /// With Verb "GET", specify with parameters the number of pages and his size to get information about employees.
        /// </summary>
        // GET: employee?page_size=10&page=1
        [AcceptVerbs(method: "GET")]
        public HttpResponseMessage GetAllEmployees(int page_size, int page)
        {
            if (String.IsNullOrEmpty(page.ToString()) || String.IsNullOrEmpty(page_size.ToString()))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad Request");
            }

           return Request.CreateResponse(HttpStatusCode.OK, employees.Skip((page - 1) * page_size).Take(page_size));
            
            
        }

        /// <summary>
        /// With Verb "POST", specify with body parameters the name, email and department to insert an employee in the system. (Id automatically generate).
        /// </summary>
        // POST: employee/
        [HttpPost]
        public HttpResponseMessage Post(employee emp)
        {
            if (employees.Count == 0)
                emp.id = 0;
            else
                emp.id = employees.Last().id+1;
            
            // see if album exists already
            var matchedEmp = employees.SingleOrDefault(x => x.id == emp.id ||
                                             x.name == emp.name);

            if (matchedEmp == null)
                employees.Add(emp);
            else
                matchedEmp = emp;

            // return a string to show that the value got here
            
            return Request.CreateResponse<string>(HttpStatusCode.Created ,"Id: " + emp.id + " - Name: "+ emp.name + " - Added");
        }

        /// <summary>
        /// With Verb "DELETE", specify with parameter an ID to get information about one employee specific.
        /// </summary>
        // DELETE: employee/1
        public HttpResponseMessage Delete(int id)
        {
            employee localEmp = employees.ElementAt(id);
            employees.RemoveAt(id);

           return Request.CreateResponse<string>(HttpStatusCode.OK, "Id: " + localEmp.id + " - Name: " + localEmp.name + " - Deleted");
        }
    }
}
