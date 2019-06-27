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

        public ActionResult Index()
        {

            var homeprodutos = db.produto.Where(c => c.vitrine == true).ToList();
            return View(homeprodutos);
        }
    
   
    }
}

