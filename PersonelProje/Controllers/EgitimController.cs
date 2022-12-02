using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.Data;
using PersonelProje.Models;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class EgitimController : TemelController
    {
        EgitimModel _model;
        public EgitimController(IConfiguration config,EgitimModel model) : base(config)
        {
            _model = model;
        }

        public Egitim EgitimBul(int Id)
        {
            var con = Connect();
            string qry = $"select * from Egitim where Id='{Id}'";
            return con.Query<Egitim>(qry).FirstOrDefault();
        }
        public IActionResult Liste()
        {
            string qry = "select * from Egitim where deleted=0 order by Id";
            var Egitim = Connect().Query<Egitim>(qry).ToList();
            return View(Egitim);
        }
        public IActionResult Guncel(int Id)
        {

            return View(EgitimBul(Id));

        }
        [HttpPost]
        public IActionResult Guncel(Egitim egitim)
        {

            string qry = $"update Egitim set EgitimAdi=@EgitimAdi where Id = @Id";
            Connect().ExecuteScalar<int>(qry, egitim);
            return RedirectToAction("Liste");
        }
        public IActionResult Sil(int Id)
        {

            return View(EgitimBul(Id));
        }

        [HttpPost]
        public IActionResult Sil(Egitim egitim)
        {

            string qry = $"Update Egitim set deleted =1 where Id = @Id";
            Connect().ExecuteScalar<int>(qry, egitim);
            return RedirectToAction("Liste");
        }


        public IActionResult Giris(Egitim yeniEgitim, bool d)
        {
            _model.Egitim = yeniEgitim;
            string qry = "select * from Egitim order by Id ";
            _model.SonId = Connect().Query<Egitim>(qry).ToList().Max(x => x.Id)+1;
            return View(_model);
        }

        [HttpPost]
        //public IActionResult Giris(Egitim egitim)  ////Egitim id identity yes(1) için
        //{

        //    string qry = $"insert into Egitim values (@EgitimAdi)";
        //    Connect().ExecuteScalar<int>(qry, egitim);
        //    return RedirectToAction("Liste");


        //}
        public IActionResult Giris(EgitimModel model) //Egitim için Id atamasını kendimiz otomatik yaptık. identity no(0).
        {
            var egitim=model.Egitim;
            var Id = model.SonId;
            string qry = $"insert into Egitim(Id,EgitimAdi,Deleted) values ('{Id}',@EgitimAdi,0)";
            Connect().ExecuteScalar<int>(qry, egitim);
            return RedirectToAction("Liste");


        }
    }
}
