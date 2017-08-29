using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using OurFirstApi.DataAccess;
using OurFirstApi.Models;

namespace OurFirstApi.Controllers
{
    //api/employees
    [RoutePrefix("api/employee")]
    public class EmployeesController : ApiController
    {
        //api/employees
        public HttpResponseMessage Get()
        {
            try
            {
                var employeeData = new EmployeeDataAccess();
                var employees = employeeData.GetAll();

                return Request.CreateResponse(HttpStatusCode.OK, employees);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Query blew up");
            }
        }

        //api/employees/3000
        [HttpGet, Route("{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var repo = new EmployeeDataAccess();
                var result = repo.Get(id);

                if (result == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Couldn't find that employee");
                }

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpGet, Route("name/{firstName}")]
        public HttpResponseMessage Get(string firstName)
        {
            using (var connection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                try
                {

                    var employeeByName = new EmployeeDataAccess();
                    var result = employeeByName.Get(firstName);

                    return Request.CreateResponse(HttpStatusCode.OK, result);

                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                }
            }
        }

    }
}
