using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using cesarini.filippo._5H.SecondaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace cesarini.filippo._5H.SecondaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            return RedirectToAction("Login", "Account");
            return View();
        }
         public IActionResult generic()
        {
            if(User.Identity.IsAuthenticated)
            return RedirectToAction("Login", "Account");
            return View();
        }

         public IActionResult elements()
        {
            if(User.Identity.IsAuthenticated)
            return RedirectToAction("Login", "Account");
            return View();
        }

        public IActionResult landing()
        {
            if(User.Identity.IsAuthenticated)
            return RedirectToAction("Login", "Account");
            return View();
        }

        [Authorize]

        public IActionResult Privacy()
        {
            if(User.Identity.IsAuthenticated)
            return RedirectToAction("Login", "Account");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

          public IActionResult Cancella()
        {
            return View();
        }
          
        [HttpGet]
         public IActionResult Prenota()
        {
            return View();
        }

         [HttpGet]
         public IActionResult Elenco()
        {
            var db= new PrenotazioniContext();
            return View(db);
        }

          [HttpPost]
      public IActionResult Prenota(Prenotazione p)
        {
            //tipo - etichetta - operatore - valore - terminatore di istruzione 
            //var a=10;

            //tipo - etichetta - operatore - valore - terminatore di istruzione 
            PrenotazioniContext db = new PrenotazioniContext();
            db.Prenotazioni.Add(p);
            db.SaveChanges();
            
            return View("Elenco", db);
        }

        [HttpGet]
         public IActionResult Modifica(int Id)
        {
            var db = new PrenotazioniContext();
            Prenotazione prenotazione=db.Prenotazioni.Find(Id);
            return View("Modifica",prenotazione);
        
        }

         [HttpPost]
        
        public IActionResult Modifica(int id,Prenotazione nuovo)
        {
            var db = new PrenotazioniContext();
            var vecchio=db.Prenotazioni.Find(id);
            if(vecchio!=null)
            {
                vecchio.Nome=nuovo.Nome;
                vecchio.Email=nuovo.Email;
                db.Prenotazioni.Update(vecchio);
                db.SaveChanges();
                return View("Elenco",db);
            }
            return NotFound();
        }

        // Delete
        [HttpGet]
        public IActionResult Cancella( int id)
        {
            var db = new PrenotazioniContext();
            Prenotazione prenotazione=db.Prenotazioni.Find(id);
            db.Remove(prenotazione);
            db.SaveChanges();
            return View("Elenco",db);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
       
           public IActionResult Upload()
        {
            return View();
        } 

         
         [HttpPost]
         public IActionResult Upload(CreatePost post)
        {
            MemoryStream memStream=new MemoryStream();
            post.MyCSV.CopyTo(memStream);
            //mette a zero il puntatore dello StreamReader
            memStream.Seek(0,0);

            StreamReader fim=new StreamReader(memStream);
            if(!fim.EndOfStream)
            {
                //accedi al database
                var db=new PrenotazioniContext(); //oppure PrenotazioneContext db=new PrenotazioneContext(); 
                string riga = fim.ReadLine();
                while(!fim.EndOfStream)
                {
                    riga = fim.ReadLine();
                    string[] colonne = riga.Split(";");
                    Prenotazione p= new Prenotazione{Nome=colonne[0], Email=colonne[1], DataPrenotazione=Convert.ToDateTime(colonne[2])};
                    
                    db.Prenotazioni.Add(p);
                }                
                db.SaveChanges();
            
                return View("Elenco", db);                
            }         
            return View();
        }
    }
}
