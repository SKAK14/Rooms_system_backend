using demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly Hotel _context;
        public RoomsController(Hotel context)
        {
            _context = context;
        }

        // GET: api/<RoomsController>
        [HttpGet]

        //public async Task<ActionResult<IEnumerable<Room>>> Get()
        public string Get()
        {
            SqlConnection conn = new SqlConnection("Server=DESKTOP-F1S5O7D\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;");
            SqlDataAdapter adapter = new SqlDataAdapter("select * from Rooms", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return Newtonsoft.Json.JsonConvert.SerializeObject(dt);
           // return dt.ToString();

        }

        // GET api/<RoomsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            SqlConnection conn = new SqlConnection("Server=DESKTOP-F1S5O7D\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;");
            SqlDataAdapter adapter = new SqlDataAdapter($"select * from Rooms where Id ={id}", conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            if (id == null)
            {
                return "Invalid Id"; // Return a 404 response if room with the given id is not found.
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            }

            /* var room = rooms.FirstOrDefault(r => r.Id == id);
            if (room == null)
            {
                return NotFound(); // Return a 404 response if room with the given id is not found.
            }
            return room;*/
        }
        // POST api/<RoomsController>
        [HttpPost]
        public string Post([FromBody] Room newRoom)
        {
            Console.WriteLine($"Hello workd {newRoom}");
            SqlConnection conn = new SqlConnection("Server=DESKTOP-F1S5O7D\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;");
            SqlCommand cmd = new SqlCommand($"INSERT INTO Rooms (Id, Beds, IsFull) VALUES ({newRoom.Id}, {newRoom.Beds}, {(newRoom.IsFull ? 1 : 0)});", conn);
            conn.Open();
            
            try
            {
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return "Record inserted successfully.";
                }
                else
                {
                    return "Record not found or couldn't be insert.";
                }
            } catch (Exception ex)
            {
                return "Record not found or couldn't be insert.";
            }
            
        }

        // PUT api/<RoomsController>/5
        [HttpPut("{id}")]
        public string Put(int id, [FromBody] Room updatedRoom)
        {
            string connectionString = "Server=DESKTOP-F1S5O7D\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string updateQuery = "UPDATE Rooms SET Beds = @Beds, IsFull = @IsFull WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Beds", updatedRoom.Beds);
                    cmd.Parameters.AddWithValue("@IsFull", updatedRoom.IsFull ? 1 : 0);
                    cmd.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Record updated successfully.";
                    }
                    else
                    {
                        return "Record not found or couldn't be updated.";
                    }
                }
                
            }
        }


        // DELETE api/<RoomsController>/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string connectionString = "Server=DESKTOP-F1S5O7D\\SQLEXPRESS;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string deleteQuery = "DELETE FROM Rooms WHERE Id = @Id";

                using (SqlCommand cmd = new SqlCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return "Record deleted successfully.";
                    }
                    else
                    {
                        return "Record not found or couldn't be deleted.";
                    }
                }
                
            }
        }

    }
}
