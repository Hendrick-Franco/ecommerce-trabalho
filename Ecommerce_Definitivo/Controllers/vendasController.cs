using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ecommerce_Definitivo.Models;

namespace Ecommerce_Definitivo.Controllers
{
    public class vendasController : Controller
    {
        private Context db = new Context();

        // GET: vendas
        public ActionResult Index()
        {

            int contaid = Convert.ToInt32(Session["id"].ToString());


            //pega todas as vendas
            var Vendas = from v in db.venda
                         select v;
            // Pega os todos os dados de itemCarrinhos.
            try
            {
                var itemcarrinhos = db.ItemVenda.ToList();
            }
            catch
            {

            }
            if (contaid > 0)
            {
                Vendas = Vendas.Where(v => v.ContaId == contaid);


            }
            // Junta tabela venda com item venda,

            var query2 = from v in db.venda
                         join ic in db.ItemVenda on v.vendaId equals ic.vendaId into gj
                         from ic in gj
                         select new { vendaId = v.vendaId, Quantidade = ic.quantidade }
                         ;
            return View(Vendas);

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


        [HttpPost]
        [ActionName("RealizarPagamento")]
        public ActionResult RealizarPagamento()
        {
            //Metodos pra preencher Todos os campos para FINALIZAR a venda.

            //Pedir campos para preenchimento do cartão caso necessario.
            //OBS: Caso necessario já ter Cartão registrado ou cadastrar.

            //Capturando a Sessão.
            string sessionId = Session["id"].ToString();

            //Instanciando a Venda.
            venda venda = new venda();
            var escolha = Request.Form["radioFormaPagamento"];

            //Instanciando o ItemCarrinho
            List<ItemCarrinho> carrinho = new List<ItemCarrinho>();

            //Capturando a Sessão do Carrinho
            carrinho = (List<ItemCarrinho>)Session["Carrinho"];

            //Realizando Validação do Carrinho / Conta.
            if ((sessionId != null))
            {
                venda.Conta = db.Conta.Find(Convert.ToInt32(sessionId));
            }
            else
            {
                return RedirectToAction("Logar", "contas");
            }

            //Preenchendo a Data da Venda  = "Agora".
            venda.dataVenda = DateTime.Now;



            //Percorrendo todos os itens do carrinho.
            foreach (ItemCarrinho itemCarrinho in carrinho)
            {
                venda.vlrTotal += (itemCarrinho.produto.preco * itemCarrinho.quantidade);
                //Gravando os dados no ItemCarrinho na base.
                db.ItemVenda.Add(itemCarrinho);
            }

            boleto boleto = new boleto();
            cartao cartao = new cartao();
            //Verificando quais os metodos para criar Boleto ou Capturar o Cartão.

            if (escolha == "Boleto")
            {
                //boleto = GerarBoleto();
            }
            if (escolha == "Cartao")//VERIFICAR PORQUE NAO RECUPERA O FORM.
            {
                cartao.numero = Request.Form["NumeroCartao"];
                cartao.datevalidade = Request.Form["Mes"] + Request.Form["Ano"];
                cartao.codigoseguranca = Request.Form["CodigoSeg"];
            }

            //Preenchendo a forma de pagamento.

            //Gravando os dados da Venda.
            db.venda.Add(venda);

            //Salvando Alterações.
            db.SaveChanges();


            //ESPAÇO PARA REALIZAR OS METODOS DE CRIAÇÂO DE BOLETO
            //EXEMPLO:
            //GerarBoleto(db.venda);
            //Abre o PDF.

            //Retorna compra de Sucesso ou mensagem de Erro.
            return View();

        }
    }
}
