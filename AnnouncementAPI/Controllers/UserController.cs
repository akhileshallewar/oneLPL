using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoggingDemo.Controllers;
using Serilog;
using System.Xml.Linq;
using System.Data.SqlClient;
using AnnouncementAPI.Models;
using System.Data;
using LPL.LoggingDemo.Controllers;

namespace LPL.AnnouncementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly string dbName = "OneLPL"; //database name
        private readonly string getStoreProc = "spGET"; //name of the stored procedure to fetch records
        private readonly string saveStoreProc = "spSAVE"; //name of the stored procedure to save records

        public UserController() : base()
        {

        }

        //HTTP GET method to fetch records from the database
        [HttpGet("/get")]
        public IActionResult GetRecords()
        {
            try
            {
                using (SqlConnection connection = GetSqlConnection(dbName)) //establishing connection to the database
                {
                    var users = new List<User>(); //list to store user records

                    using (SqlCommand command = new SqlCommand(getStoreProc, connection)) //creating a new SQL command object using the stored procedure name and the connection object
                    {
                        command.CommandType = CommandType.StoredProcedure; //setting the command type to stored procedure

                        using (SqlDataReader reader = command.ExecuteReader()) //executing the command and creating a data reader object
                        {
                            while (reader.Read()) //reading each record from the data reader
                            {
                                users.Add(new User() //creating a new User object with the values from the record and adding it to the list
                                {
                                    UserId = Convert.ToInt32(reader["UserID"]),
                                    Name = reader["Name"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    PhNo = Convert.ToInt64(reader["PhnNo"]),
                                    Address = reader["Address"].ToString()

                                });
                            }
                        }
                    }


                    return Ok(users); //returning the list of users as an HTTP response with status code 200 (OK)
                }
            }
            catch (Exception ex)
            {
                //LogError("Error occurrued", ex.Message, lineNo, fileName, token);
                Log.Error(ex.Message); //logging the error message using Serilog
                return StatusCode(500); //returning an HTTP response with status code 500 (Internal Server Error)
            }
        }


        //HTTP POST method to save user records to the database
        [HttpPost("/save")]
        public IActionResult PostRecords(User user, string actionPerformed)
        {
            try
            {
                using (SqlConnection connection = GetSqlConnection(dbName)) //establishing connection to the database
                {

                    using (SqlCommand command = new SqlCommand(saveStoreProc, connection)) //creating a new SQL command object using the stored procedure name and the connectionobject
                    {
                        command.CommandType = CommandType.StoredProcedure; //setting the command type to stored procedure
                        command.Parameters.AddWithValue("@UserID", user.UserId); //adding parameters to the command object
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@PhnNo", user.PhNo);
                        command.Parameters.AddWithValue("@Address", user.Address);
                        command.Parameters.AddWithValue("@Action", actionPerformed);

                        int i = command.ExecuteNonQuery(); //executing the command and getting the number of affected rows

                        if (i >= 1) //if at least one row is affected
                            return Ok("User " + actionPerformed + " action Performed Successfully."); //returning an HTTP response with status code 200 (OK) and a success message
                        else
                        {
                            return BadRequest("Action Failed"); //returning an HTTP response with status code 400 (Bad Request) and an error message
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message); //logging the error message using Serilog
                return StatusCode(500); //returning an HTTP response with status code 500 (Internal Server Error)
            }
        }
    }
}