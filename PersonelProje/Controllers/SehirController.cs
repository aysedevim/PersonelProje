using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.Data;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class SehirController : TemelController
    {
        public SehirController(IConfiguration config) : base(config)
        {
        }

        public Sehir SehirBul(int Id)
        {
           
            string qry = $"select * from Sehir where Id='{Id}'";
            return Connect().Query<Sehir>(qry).FirstOrDefault(); 
        }
        public IActionResult Liste()
        {
            string qry = "select * from Sehir order by SehirAdi ";
            var Sehir = Connect().Query<Sehir>(qry).ToList(); 
            return View(Sehir);
        }
        public IActionResult Guncel(int Id)
        {

            return View(SehirBul(Id));

        }
        [HttpPost]
        public IActionResult Guncel(Sehir sehir)                              
        {

            string qry = $"update Sehir set SehirAdi=@SehirAdi where Id = @Id"; 
            Connect().ExecuteScalar<int>(qry, sehir);
            return RedirectToAction("Liste");
        }
        public IActionResult Sil(int Id) 
        {
            
            return View(SehirBul(Id));
        }

        [HttpPost]
        public IActionResult Sil(Sehir sehir) 
        {
            
            string qry = $"delete from Sehir where Id = @Id";
            Connect().ExecuteScalar<int>(qry, sehir);
            return RedirectToAction("Liste");
        }


        public IActionResult Giris(Sehir yeniSehir, bool d) 
        {
            return View(yeniSehir);
        }

        [HttpPost]
        public IActionResult Giris(Sehir sehir)
        {

            string qry = $"insert into Sehir values (@Id,@SehirAdi)";
            Connect().ExecuteScalar<int>(qry, sehir);
            return RedirectToAction("Liste");


        }
    }
}

