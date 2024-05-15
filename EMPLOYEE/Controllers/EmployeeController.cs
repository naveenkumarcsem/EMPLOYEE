using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class EmployeeController : Controller
{
    private readonly string connectionString = "Server=LAPTOP-96DDPN98;Database=EmployeeDB;Trusted_Connection=True;MultipleActiveResultSets=True";

    public ActionResult Index()
    {
        List<Employee> employees = new List<Employee>();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetAllEmployees", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        Employee employee = new Employee
                        {
                            EmployeeID = Convert.ToInt32(rdr["EmployeeID"]),
                            Name = rdr["Name"].ToString(),
                            Age = Convert.ToInt32(rdr["Age"]),
                            DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"]),
                            Email = rdr["Email"].ToString(),
                            Salary = Convert.ToDecimal(rdr["Salary"]),
                            Phone = rdr["Phone"].ToString()
                        };
                        employees.Add(employee);
                    }
                }
            }
        }
        return View(employees);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Employee employee)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("InsertEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                // Add parameters for Primary Address
                cmd.Parameters.AddWithValue("@PrimaryDoorNo", employee.PrimaryAddress.DoorNo);
                cmd.Parameters.AddWithValue("@PrimaryStreetName", employee.PrimaryAddress.StreetName);
                cmd.Parameters.AddWithValue("@PrimaryStreetName2", employee.PrimaryAddress.StreetName2);
                cmd.Parameters.AddWithValue("@PrimaryLandmark", employee.PrimaryAddress.Landmark);
                cmd.Parameters.AddWithValue("@PrimaryCity", employee.PrimaryAddress.City);
                cmd.Parameters.AddWithValue("@PrimaryState", employee.PrimaryAddress.State);
                // Add parameters for Temporary Address
                cmd.Parameters.AddWithValue("@TemporaryDoorNo", employee.TemporaryAddress.DoorNo);
                cmd.Parameters.AddWithValue("@TemporaryStreetName", employee.TemporaryAddress.StreetName);
                cmd.Parameters.AddWithValue("@TemporaryStreetName2", employee.TemporaryAddress.StreetName2);
                cmd.Parameters.AddWithValue("@TemporaryLandmark", employee.TemporaryAddress.Landmark);
                cmd.Parameters.AddWithValue("@TemporaryCity", employee.TemporaryAddress.City);
                cmd.Parameters.AddWithValue("@TemporaryState", employee.TemporaryAddress.State);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
        Employee employee = new Employee();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        employee.EmployeeID = Convert.ToInt32(rdr["EmployeeID"]);
                        employee.Name = rdr["Name"].ToString();
                        employee.Age = Convert.ToInt32(rdr["Age"]);
                        employee.DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"]);
                        employee.Email = rdr["Email"].ToString();
                        employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                        employee.Phone = rdr["Phone"].ToString();

                        Address primaryAddress = new Address
                        {
                            DoorNo = rdr["DoorNo"].ToString(),
                            StreetName = rdr["StreetName"].ToString(),
                            StreetName2 = rdr["StreetName2"].ToString(),
                            Landmark = rdr["Landmark"].ToString(),
                            City = rdr["City"].ToString(),
                            State = rdr["State"].ToString(),
                            AddressType = "Primary"
                        };
                        employee.PrimaryAddress = primaryAddress;

                        rdr.NextResult();

                        if (rdr.Read())
                        {
                            Address temporaryAddress = new Address
                            {
                                DoorNo = rdr["DoorNo"].ToString(),
                                StreetName = rdr["StreetName"].ToString(),
                                StreetName2 = rdr["StreetName2"].ToString(),
                                Landmark = rdr["Landmark"].ToString(),
                                City = rdr["City"].ToString(),
                                State = rdr["State"].ToString(),
                                AddressType = "Temporary"
                            };
                            employee.TemporaryAddress = temporaryAddress;
                        }
                    }
                }
            }
        }
        return View(employee);
    }

    [HttpPost]
    public ActionResult Edit(Employee employee)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdateEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Age", employee.Age);
                cmd.Parameters.AddWithValue("@DateOfBirth", employee.DateOfBirth);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                // Add parameters for Primary Address
                cmd.Parameters.AddWithValue("@PrimaryDoorNo", employee.PrimaryAddress.DoorNo);
                cmd.Parameters.AddWithValue("@PrimaryStreetName", employee.PrimaryAddress.StreetName);
                cmd.Parameters.AddWithValue("@PrimaryStreetName2", employee.PrimaryAddress.StreetName2);
                cmd.Parameters.AddWithValue("@PrimaryLandmark", employee.PrimaryAddress.Landmark);
                cmd.Parameters.AddWithValue("@PrimaryCity", employee.PrimaryAddress.City);
                cmd.Parameters.AddWithValue("@PrimaryState", employee.PrimaryAddress.State);
                // Add parameters for Temporary Address
                cmd.Parameters.AddWithValue("@TemporaryDoorNo", employee.TemporaryAddress.DoorNo);
                cmd.Parameters.AddWithValue("@TemporaryStreetName", employee.TemporaryAddress.StreetName);
                cmd.Parameters.AddWithValue("@TemporaryStreetName2", employee.TemporaryAddress.StreetName2);
                cmd.Parameters.AddWithValue("@TemporaryLandmark", employee.TemporaryAddress.Landmark);
                cmd.Parameters.AddWithValue("@TemporaryCity", employee.TemporaryAddress.City);
                cmd.Parameters.AddWithValue("@TemporaryState", employee.TemporaryAddress.State);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("DeleteEmployee", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        return RedirectToAction("Index");
    }
}
