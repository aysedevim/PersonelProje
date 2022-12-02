using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.DTO;
using PersonelProje.Data;
using PersonelProje.Models;

namespace PersonelProje.Controllers
{
    public class PersonelController : TemelController
    {
        PersonelModel _model; //newlemek yerine global değişken kullanıldı.
       //PersonelModel _model= new PersonelModel(); 
        public PersonelController(IConfiguration c ,PersonelModel model) : base(c)
        {
            _model = model;
        }

        public List<Ulke> Ulkeler()
        {
            return Connect().Query<Ulke>($"select * from Ulke").ToList();
        }
        public List<Sehir> Sehirler()
        {
            return Connect().Query<Sehir>($"select * from Sehir").ToList();
        }

        public Personel PersonelBul(int Id)
        {
            return Connect().Query<Personel>($"select * from Personel where Id='{Id}'").FirstOrDefault();
        }
      
        public IActionResult Liste()
        {
            string qry = $"select p.Id,Ad+' '+Soyad AdSoy, UlkeAdi,SehirAdi from Personel p\r\ninner join Sehir s on s.Id=p.SehirId\r\ninner join Ulke u on u.Id=p.UlkeId";
            var Liste= Connect().Query<PersonelDTO>(qry).ToList();
            return View(Liste);
          
        }
        public IActionResult Guncel(int Id) //Get
        {
            _model.Personel = PersonelBul(Id);
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Güncelleme İşlemi";
            _model.BtnText = "Güncelle";
            _model.BtnClass = "btn btn-success";
            return View("Genel",_model); //parantez içerisinde _model yazmamızın sebebi sehirId ve ulkeId yi list options şeklinde çekecek olmamızdır.
        }
        [HttpPost]
        public IActionResult Guncel(PersonelModel model) //Post
        {
            Personel personel = model.Personel;
            string qry = "update Personel set Ad=@Ad, Soyad=@Soyad, Maas=@Maas, UlkeId=@UlkeId, SehirId=@SehirId where Id=@Id";
            Connect().ExecuteScalar<int>(qry, personel);
            return RedirectToAction("Liste");
        }

        public IActionResult Giris()
        {
            _model.Personel = new Personel();
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Yeni Giriş";
            _model.BtnText = "Kaydet";
            _model.BtnClass = "btn btn-primary";
            return View("Genel", _model);
        }
        [HttpPost]
        public IActionResult Giris(PersonelModel model) 
        {
            Personel personel = model.Personel;
            string qry = "insert into Personel(Ad,Soyad,Maas,UlkeId,SehirId) values(@Ad,@Soyad,@Maas,@UlkeId,@SehirId)";
            Connect().ExecuteScalar<int>(qry, personel);
            return RedirectToAction("Liste");
        }
        public IActionResult Sil(int Id) 
        {
            _model.Personel = PersonelBul(Id);
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Silme İşlemi";
            _model.BtnText = "Sil";
            _model.BtnClass = "btn btn-danger";
            return View("Genel", _model);
        }
        [HttpPost]
        public IActionResult Sil(PersonelModel model) 
        {
            Personel personel = model.Personel;
            string qry = "delete from Personel where Id=@Id";
            Connect().ExecuteScalar<int>(qry, personel);
            return RedirectToAction("Liste");
        }


    }

}
