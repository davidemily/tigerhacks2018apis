using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace storyEndpoint.Controllers
{
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        [HttpPut]
        public IActionResult Put()
        {
            try
            {
                string myConnectionString = "server=localhost;database=tigerhacks;uid=root;pwd=sqlpass;sslmode=none;";
                MySqlConnection conn = new MySqlConnection(myConnectionString);
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = ("select * from story where user_id = 1;");
                cmd.Connection.Open();
                var result = cmd.ExecuteReader();
                MailMessage mail = new MailMessage();
                
                while (result.Read())
                {
                    mail.Subject = result.GetString(1);
                    mail.Body = result.GetString(2);
                    var toAddress = result.GetString(4);
                    mail.To.Add(new MailAddress(toAddress));
                }


                SmtpClient client = new SmtpClient();
                client.Host = "localhost";
                client.Send(mail); 

                cmd.Connection.Close();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BadRequestResult();
            }
        }
    }
}