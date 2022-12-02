using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using PersonelProje.Data;

namespace PersonelProje.Controllers
{
    public class UlkeController : TemelController
    {
        public UlkeController(IConfiguration config) : base(config) //Otomatik newlenmesi için miras aldığın yerdeki gibi davran diyoruz. "base" miras aldığın yerdeki değişken gibi davran demektir.
        {
        }

        public Ulke UlkeBul(string Id)
        {
            var con = Connect();
            string qry = $"select * from Ulke where Id='{Id}'";
            return con.Query<Ulke>(qry).FirstOrDefault(); //firstOrDefault tek kayıt döndürür.
           // return secUlke;
        }
        public IActionResult Liste()
        {
            //var con = Connect();
            string qry = "select * from Ulke order by UlkeAdi ";
            var Ulkeler = Connect().Query<Ulke>(qry).ToList(); //toList tüm satırları okur. // con.Query sizden bir sql cümleciği ister.
            return View(Ulkeler);
        }

        public IActionResult Guncel(string Id)
        {
         
            return View(UlkeBul(Id));

        }
        [HttpPost]
        public IActionResult Guncel(Ulke ulke)    //guncel.cshtml içerisinde @model.Id ve @model.ulkeAdi tanımlı olduğundan "string Id, string UlkeAdi" yerine Ulke ulke yazıyoruz.
                                                 //eğer guncel.cshtml içerisinde input name adı databaseteki adından farklı olursa "string Id, string yeniverilenisim" şeklinde yazılmalıdır.
        {
            
            //1.yol
            string qry = $"update Ulke set UlkeAdi=@UlkeAdi where Id = @Id"; //Id='{ulke.Id}' şekilinde yazılması gerekirken Guncel.cshtml içeirisinde
                                                                           //Id readonly değiştirilemez olduğu için Id=@Id şeklinde yazabildik.
            Connect().ExecuteScalar<int>(qry, ulke);
            return RedirectToAction("Liste");

            //2.Yol
            //string qry = $"update Ulke set UlkeAdi=@UlkeAdi where Id=@Id";
            //DynamicParameters par = new DynamicParameters();
            //par.Add("@UlkeAdi", ulke.UlkeAdi);
            //par.Add("@Id", ulke.Id);
            //con.ExecuteScalar<int>(qry, ulke);
            //return RedirectToAction("Liste");

        }

        public IActionResult Sil(string Id) //Get sadece listeyi getirir.
        {
            //SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            //string qry = $"select * from Ulke where Id='{Id}'";
            //var secUlke = con.Query<Ulke>(qry).FirstOrDefault();
            //return View();
            return View(UlkeBul(Id));
        }

        [HttpPost]
        public IActionResult Sil(Ulke ulke) //Post önce get eder daha sonra üzerinde değişiklik yapılır.
        {
            //SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            string qry = $"delete from Ulke where Id = @Id";
            Connect().ExecuteScalar<int>(qry, ulke);
            return RedirectToAction("Liste");
        }

        //1.Yol
        //public IActionResult Giris()
        //{
        //    Ulke yeniUlke = new Ulke();
        //    return View(yeniUlke);

        //}
        //2.Yol
        public IActionResult Giris(Ulke yeniUlke, bool d) //(maskreding-kandırmak) iki metot parametresi aynı olamayacağı için kullanmayacağımız bool bir değer verdik.
                                                           //bu şekilde newlemeden kullanırız.
        {
            return View(yeniUlke);
        }

        [HttpPost]
        public IActionResult Giris(Ulke ulke)
        {
           
            string qry = $"insert into Ulke values (@Id,@UlkeAdi)";
            Connect().ExecuteScalar<int>(qry, ulke); //executescalar insert,delete,uptade komutları çalıştırılır.// int dönmesinin sebebi  kaç satırda değişiklik yapıldığını öğrenmek için count ile bu sayı öğrenilir.
            return RedirectToAction("Liste");



        }
    }
}
