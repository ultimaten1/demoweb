using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace demo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index(string empSearch)
        {
            string mainConn = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(mainConn);
            string sqlQuery = "SELECT * FROM [dbo].[Employee] WHERE Name LIKE '%" + empSearch + "%'";
            SqlCommand sqlComm = new SqlCommand(sqlQuery, sqlConn);
            sqlConn.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlComm);
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            List<EmpClass> empClasses = new List<EmpClass>();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                empClasses.Add(new EmpClass
                {
                    Name = Convert.ToString(dataRow["Name"]),
                    Email = Convert.ToString(dataRow["Email"]),
                    Salary = Convert.ToString(dataRow["Salary"])
                });
            }

            sqlConn.Close();
            ModelState.Clear();
            return View(empClasses);
        }
    }
}