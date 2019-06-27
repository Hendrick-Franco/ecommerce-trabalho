using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ecommerce_Definitivo.Models;

namespace Ecommerce_Definitivo.Controllers
{
    public class vendasController : Controller
    {
        private Context db = new Context();

        // GET: vendas
        public ActionResult Index()
        {
            //switch (Tipo)
            //{
            //    case "Cartão":
            //        FormaPagamento cartao = new cartao();
            //        Create(cartao);
            //        //Create(cartao,Carrinho); -- Redicionaria para criação da VENDA.
            //        break;
            //    case "Boleto":
            //        FormaPagamento boleto = new boleto();
            //        Create(boleto);
            //        //Create(boleto,Carrinho); -- Redicionaria para criação da VENDA.
            //        break;
            //}

            return View(db.venda.ToList());
            //return RedirectToAction("Create");

            //return View(db.venda.ToList());
        }

        // GET: vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = db.venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: vendas/Create
        public ActionResult Create()
        {
            // IFormaP formaPagamento
            //venda venda = new venda();
            //venda.dataVenda = DateTime.Now;
            //Capturar o Valor do Carrinho,

            //Foreach in Carrinho:
            //FormaPagamento.valor = Produto.valor.
            //formaPagamento.valor =  
            return View();
        }

        // POST: vendas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "vendaId,dataVenda,vlrTotal")] venda venda)
        {
            if (ModelState.IsValid)
            {
                db.venda.Add(venda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(venda);
        }

        // GET: vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = db.venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: vendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "vendaId,dataVenda,vlrTotal")] venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(venda);
        }

        // GET: vendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = db.venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            venda venda = db.venda.Find(id);
            db.venda.Remove(venda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult RealizarCheckout(FormaPagamento formapg)
        {
            //Chamando os negocios...
            //Apos realizar pagamento e salvar na tabela Venda, Limpar o Carrinho.
            //var radioButton = Request.Form["radioTipoPagamento"];
            //if (radioButton.) {

            //}
            string sessionId = Session["id"].ToString();
            venda venda = new venda();
            List<ItemCarrinho> carrinho = new List<ItemCarrinho>();
            carrinho = (List<ItemCarrinho>)Session["Carrinho"];
            if ((sessionId != null))
            {
                venda.Conta = db.Conta.Find(Convert.ToInt32(sessionId));
            }
            else
            {
                return RedirectToAction("Logar", "contas");
            }
            venda.dataVenda = DateTime.Now;
            venda.formapagamento = formapg;

            foreach (ItemCarrinho itemCarrinho in carrinho)
            {
                venda.vlrTotal += (itemCarrinho.produto.preco * itemCarrinho.quantidade);
                db.ItemVenda.Add(itemCarrinho);
            }
            db.venda.Add(venda);
            db.SaveChanges();

            return View();
        }
    }
}
