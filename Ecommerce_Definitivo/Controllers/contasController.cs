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
    public class contasController : Controller
    {

        private Context db = new Context();




        // GET: contas
        public ActionResult Index(string pesquisa)
        {

            var homeprodutos = db.produto.Where(c => c.vitrine == true).ToList();
            return View(homeprodutos);
        }
        public ActionResult SearchBar(string pesquisa)
        {
            if (pesquisa == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            produto produto = db.produto.Find(pesquisa);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: contas/Details/5/perfil
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conta conta = db.Conta.Find(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }


        // GET: contas/Cadastro
        public ActionResult Cadastro()
        {
            var modelo = new conta();
            var model = new ContaViewModel();

            return View(model);
        }

        // GET: contas/Login
        public ActionResult Login()
        {


            return View();
        }

        // POST: contas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastro([Bind(Exclude = "Imagem")] ContaViewModel model)
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
                ModelState.AddModelError("ImageUpload", "Escolha uma iamgem GIF, JPG ou PNG.");
            }

            if (ModelState.IsValid)
            {
                var conta = new conta();

                conta.email = model.email;
                conta.cpf = model.cpf;
                conta.senha = model.senha;
                conta.nome = model.nome;
                conta.contaId = model.contaId;

                // Salvar a imagem para a pasta e pega o caminho
                using (var binaryReader = new BinaryReader(model.ImageUpload.InputStream))
                    conta.Imagem = binaryReader.ReadBytes(model.ImageUpload.ContentLength);

                db.Conta.Add(conta);
                db.SaveChanges();
                Logar(conta.email, conta.senha);
                return RedirectToAction("Index");


            }

            // Se ocorrer um erro retorna para pagina

            return View(model);
        }






        //Validar login

        public ActionResult Logar(string email, string senha)
        {
            try
            {
                using (var context = new Context())
                {
                    var SQL = context.Conta
                        .Where(c => c.email == email && c.senha == senha).First();


                    Session.Timeout = 20;
                    Session.Add("id", SQL.contaId);
                    Session.Add("nome", SQL.nome);
                    Session.Add("email", SQL.email);

                    return RedirectToAction($"Details/{SQL.contaId}");

                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
        }

        public ActionResult Deslogar()
        {
            if (Session["id"] != null && Session["email"] != null && Session["id"] != null)
            {
                Session.RemoveAll();
                return RedirectToAction("Index");
            }

            return View();
        }

        // Esqueceu a senha
        public ActionResult EsqueceuSenha(string senha, string email, string cpf)
        {

            try
            {
                using (var context = new Context())
                {
                    var conta = context.Conta
                            .Where(c => c.cpf == cpf).First();
                    if (conta != null)
                    {
                        conta.senha = senha;

                        resetSenha(conta);

                    }

                    return RedirectToAction("Index");
                }


            }
            catch
            {
                return View();
            }

        }
        public ActionResult resetSenha([Bind(Include = "contaId,email,senha,nome,cpf")] conta conta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conta).State = EntityState.Modified;
                db.SaveChanges();


            }
            return View(conta);
        }



        // GET: contas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conta conta = db.Conta.Find(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }

        // POST: contas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "contaId,email,senha,nome,cpf")] conta conta)
        {

            if (ModelState.IsValid)
            {
                db.Entry(conta).State = EntityState.Modified;
                db.SaveChanges();

                //altera session apos update
                Session.Add("id", conta.contaId);
                Session.Add("nome", conta.nome);
                Session.Add("email", conta.email);

            }
            return RedirectToAction("Index");
        }

        // GET: contas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            conta conta = db.Conta.Find(id);
            if (conta == null)
            {
                return HttpNotFound();
            }
            return View(conta);
        }

        // POST: contas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            conta conta = db.Conta.Find(id);
            db.Conta.Remove(conta);
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



        public FileContentResult UserPhotos()
        {


            int userId = Convert.ToInt32(Session["id"].ToString());

            if (userId == 0)
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);

                return File(imageData, "image/png");

            }
            // to get the user details to load user Image

            var userImage = db.Conta.Where(x => x.contaId == userId).FirstOrDefault();
            if (userImage.Imagem != null)
            {
                return new FileContentResult(userImage.Imagem, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/Images/noImg.jpg");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");
            }
        }

    }



}

