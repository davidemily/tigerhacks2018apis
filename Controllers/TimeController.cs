using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace storyEndpoint.Controllers
{
    [Route("api/[controller]")]
    public class TimeController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                string myConnectionString = "server=localhost;database=tigerhacks;uid=root;pwd=sqlpass;sslmode=none;";
                MySqlConnection conn = new MySqlConnection(myConnectionString);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = ("UPDATE story SET last_updated = NOW() WHERE user_id=1;");
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestResult();
            }

            return new OkResult();
        }
    }
}
