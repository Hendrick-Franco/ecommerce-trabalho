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
    public class produtoesController : Controller
    {
       Context db;
        public produtoesController()
        {
            db = new Context();
        }
        // GET: Produtos
        public ActionResult Index()
        {
            var produto = db.produto.ToList();
            return View(produto);
        }

        public ActionResult Create()
        {
            ViewBag.Categorias = db.Categorias;
            var model = new ProdutoViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProdutoViewModel model)
        {
            var imageTypes = new string[]{
                    "image/gif",
                    "image/jpeg",
                    "image/pjpeg",
                    "image/png"
                };
            if (model.ImageUpload == null || model.ImageUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ImageUpload", "Este campo é obrigatório");
            }
            else if (!imageTypes.Contains(model.ImageUpload.ContentType))
            {
                ModelState.AddModelError("ImageUpload", "Escolha uma imagem GIF, JPG ou PNG.");
            }

            if (ModelState.IsValid)
            {
                var produto = new produto();
                produto.nome = model.Nome;
                produto.preco = model.Preco;
                produto.vitrine = model.Vitrine;
                produto.descricao = model.Descricao;
                produto.CategoriaId = model.CategoriaId;

                // Salvar a imagem para a pasta e pega o caminho
                using (var binaryReader = new BinaryReader(model.ImageUpload.InputStream))
                    produto.Imagem = binaryReader.ReadBytes(model.ImageUpload.ContentLength);

                db.produto.Add(produto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Se ocorrer um erro retorna para pagina
            ViewBag.Categories = db.Categorias;
            return View(model);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produto produto = db.produto.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categorias = db.Categorias;
            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,Nome,Preco,CategoriaId")] ProdutoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var produto = db.produto.Find(model.ProdutoId);
                if (produto == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                produto.nome = model.Nome;
                produto.preco = model.Preco;
                produto.CategoriaId = model.CategoriaId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = db.Categorias;
            return View(model);
        }

        // GET: Produtos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produto produto = db.produto.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            ViewBag.Categoria = db.Categorias.Find(produto.CategoriaId).CategoriaNome;
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            produto produto = db.produto.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categoria = db.Categorias.Find(produto.CategoriaId).CategoriaNome;
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            produto produto = db.produto.Find(id);
            db.produto.Remove(produto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

  
}
