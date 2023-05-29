using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoggingDemo.Controllers;
using Serilog;
using System.Xml.Linq;
using System.Data.SqlClient;
using AnnouncementAPI.Models;
using System.Data;

namespace LPL.AnnouncementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : LoggingDemo.Controllers.BaseController
    {
        private readonly string dbName = "OneLPL";
        private readonly string getStoreProc= "spGET";
        private readonly string saveStoreProc= "spSAVE";
        public UserController(): base()
        {

        }
        [HttpGet("/get")]
        public IActionResult GetRecords()
        {
            try
            {
                using (SqlConnection connection = GetSqlConnection(dbName))
                {
                    var users = new List<User>();

                    using (SqlCommand command = new SqlCommand(getStoreProc, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new User()
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


                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                //LogError("Error occurrued", ex.Message, lineNo, fileName, token);
                Log.Error(ex.Message);
                return StatusCode(500);
            }
        }


        [HttpPost("/save")]
        public IActionResult PostRecords(User user, string actionPerformed)
        {
            try
            {
                using (SqlConnection connection = GetSqlConnection(dbName))
                {
                    

                    var users = new List<User>();

                    using (SqlCommand command = new SqlCommand(saveStoreProc, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", user.UserId);
                        command.Parameters.AddWithValue("@Name", user.Name);
                        command.Parameters.AddWithValue("@Email", user.Email);
                        command.Parameters.AddWithValue("@PhnNo", user.PhNo);
                        command.Parameters.AddWithValue("@Address", user.Address);
                        command.Parameters.AddWithValue("@Action", actionPerformed);

                        int i = command.ExecuteNonQuery();

                        if (i >= 1)
                            return Ok("User " + actionPerformed + " action Performed Successfully.");
                        else
                        {
                            return BadRequest("Action Failed");
                        }
                    }


                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                //LogError("Error occurrued", ex.Message, lineNo, fileName, token);
                Log.Error(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
