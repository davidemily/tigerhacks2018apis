using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace storyEndpoint.Controllers
{
    [Route("api/[controller]")]
    public class LastUpdatedController : Controller
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
                cmd.CommandText = ("Select last_updated FROM story WHERE user_id=1;");
                cmd.Connection.Open();
                var result = cmd.ExecuteReader();

                var response = "";
                while (result.Read())
                {
                    response = result.GetString(0);
                }
                
                cmd.Connection.Close();
                return Ok(JsonConvert.SerializeObject(response));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}