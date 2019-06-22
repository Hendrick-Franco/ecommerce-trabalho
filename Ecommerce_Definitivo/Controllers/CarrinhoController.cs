using Ecommerce_Definitivo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce_Definitivo.Controllers
{
    public class CarrinhoController : Controller
    {

        Context db = new Context();
        private string strCarrinho = "Carrinho";
        // GET: Carrinho
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AdicionarItemCarrinho(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if (Session[strCarrinho] == null)
            {
                List<ItemCarrinho> carrinho = new List<ItemCarrinho>
                {
                    new ItemCarrinho(db.produto.Find(id),1)
                };
                Session[strCarrinho] = carrinho;
            }
            else
            {
                List<ItemCarrinho> carrinho = (List<ItemCarrinho>)Session[strCarrinho];
                int JaExiste = JaExisteNoCarrinho(id);
                if (JaExiste == -1) {
                    carrinho.Add(new ItemCarrinho(db.produto.Find(id), 1));
                }
                else {
                    carrinho[JaExiste].quantidade++;
                }                
                Session[strCarrinho] = carrinho;
            }
            return View("Index");
        }

        private int JaExisteNoCarrinho(int? id) {
            List<ItemCarrinho> carrinho = (List<ItemCarrinho>)Session[strCarrinho];
            for (int i = 0; i < carrinho.Count; i++) {
                if (carrinho[i].produto.produtoId == id) return i;
            }
            return -1;
        }

        public ActionResult RemoverItemCarrinho(int? id)
        {
            int contIguais = 0; var index = -2;
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            int JaExiste = JaExisteNoCarrinho(id);
            List<ItemCarrinho> carrinho = (List<ItemCarrinho>)Session[strCarrinho];
            for (int i = 0; i < carrinho.Count; i++)
            {
                if (carrinho[i].quantidade > 1) { 
                    index = i;
                    contIguais += 1;
                }
            }
            if (contIguais == 0)
            {
                carrinho.RemoveAt(JaExiste);
            }
            else {
                carrinho[index].quantidade -= 1;
            }                
            return View("Index");
        }
    }
}