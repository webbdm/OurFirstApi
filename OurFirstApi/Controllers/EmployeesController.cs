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
                var repo = new FakeEmployeeDataAccess();
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
                    connection.Open();

                    var result =
                        connection.Query<EmployeeListResult>("Select * From Employee where FirstName = @firstname",
                            new { firstName }).FirstOrDefault();

                    if (result == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Employee with the Name {firstName} was not found");
                    }

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
