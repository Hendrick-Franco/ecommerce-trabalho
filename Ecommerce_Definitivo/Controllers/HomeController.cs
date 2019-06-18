using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce_Definitivo.Models;

namespace Ecommerce_Definitivo.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        public ActionResult Index(string pesquisa)
        {

            var homeprodutos = db.produto.Where(c => c.vitrine == true).ToList();
            return View(homeprodutos);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

