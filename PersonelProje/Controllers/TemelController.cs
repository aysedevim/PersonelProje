using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class TemelController : Controller
    {
        private readonly IConfiguration _config;
        public TemelController(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection Connect()
        {
            return new SqlConnection(_config.GetConnectionString("Baglanti"));


        }
    }
}
