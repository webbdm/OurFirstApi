using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OurFirstApi.Models;

namespace OurFirstApi.Controllers
{
    [RoutePrefix("otherapi/zalues")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet, Route("all")]
        public IEnumerable<string> GetAllValues()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet, Route("{randomNumber}")]
        public int Get(int randomNumber)
        {
            return randomNumber;
        }

        // POST api/values
        [HttpPost,Route("")]
        public HttpResponseMessage AddEmployee(EmployeeListResult employee)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");

            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/values/5
        [HttpPut, Route("{id}")]
        public HttpResponseMessage UpdateEmployee(int id, EmployeeListResult employee)
        {
            Console.WriteLine($"{employee.FirstName} {employee.LastName}");

            return Request.CreateResponse(HttpStatusCode.Accepted);
        }

        // DELETE api/values/5
        [HttpDelete, Route("{id}")]
        public void DeleteEmployee(int id)
        {
        }
    }
}
