using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using Dapper;
using OurFirstApi.Models;

namespace OurFirstApi.DataAccess
{
    public class IntRepository : IRepository<int>
    {
        public List<int> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Get(int id)
        {
            throw new NotImplementedException();
        }
    }

    public class FakeEmployeeDataAccess : IRepository<EmployeeListResult>
    {
        public List<EmployeeListResult> GetAll()
        {
            throw new NotImplementedException();
        }

        public EmployeeListResult Get(int id)
        {
            throw new NotImplementedException();
        }

        public string Stuff()
        {
            return "stuff";
        }
    }

    public class EmployeeDataAccess : IRepository<EmployeeListResult>
    {
        public List<EmployeeListResult> GetAll()
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                connection.Open();

                var result = connection.Query<EmployeeListResult>("select * " +
                                                                  "from Employee");
                return result.ToList();
            }
        }
        public EmployeeListResult Get(int id)
        {
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Chinook"].ConnectionString))
            {
                connection.Open();

                var result = connection.QueryFirstOrDefault<EmployeeListResult>(
                    "Select * From Employee where EmployeeId = @id",
                    new {id = id});

                return result;
            }
        }
    }

    public interface IRepository<T>
    {
        List<T> GetAll();
        T Get(int id);
    }
}