using LivrariaJulio.Mvc.Models;
using LivrariaJulio.Mvc.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LivrariaJulio.Mvc.Controllers
{
    public class AutorController : Controller
    {

        #region Actions

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "AutorID,Nome,DataNascimento")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(autor).Replace("  ", ""));
                    var response = new Requisicao().Post("/api/Autor", data);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return View();
        }

        #endregion Fim Actions

        #region Http

        public JsonResult GetAutores(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var response = new Requisicao().Get("/api/Autor");

            List<Autor> autores;
            using (var jsonTextReader = new JsonTextReader(new StreamReader(response.GetResponseStream())))
            {
                autores = new JsonSerializer().Deserialize<List<Autor>>(jsonTextReader);
            }

            int totalRecords = autores.Count;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (!string.IsNullOrWhiteSpace(sord))
            {
                if (sord.ToUpper() == "DESC")
                {
                    autores = autores.OrderByDescending(x => x.Nome).ToList();
                    autores = autores.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    autores = autores.OrderBy(x => x.Nome).ToList();
                    autores = autores.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
            }

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = autores
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Autores()
        {
            var response = new Requisicao().Get("/api/Autor");

            List<Autor> autores;
            using (var jsonTextReader = new JsonTextReader(new StreamReader(response.GetResponseStream())))
            {
                autores = new JsonSerializer().Deserialize<List<Autor>>(jsonTextReader);
            }

            autores = autores.OrderBy(x => x.Nome).ToList();

            return Json(autores, JsonRequestBehavior.AllowGet);
            
        }
        
        public string AddAutores(Autor autor)
        {
            var msg = "Preencha Nome e Data de Nascimento válida.";

            try
            {
                if (!string.IsNullOrWhiteSpace(autor.Nome) || autor.DataNascimento != DateTime.MinValue)
                {
                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(autor).Replace("  ", ""));
                    var response = new Requisicao().Post("/api/Autor", data);

                    msg = "Autor adicionado com sucesso.";

                    if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                    {
                        msg = "Falha ao cadastrar autor.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro: " + ex.Message;
            }

            return msg;
        }

        public string EditAutores(Autor autor)
        {
            var msg = "Preencha Nome e Data de Nascimento válida.";

            try
            {
                if (!string.IsNullOrWhiteSpace(autor.Nome) || autor.DataNascimento != DateTime.MinValue)
                {
                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(autor).Replace("  ", ""));
                    var response = new Requisicao().Put("/api/Autor", data);

                    msg = "Autor alterado com sucesso.";

                    if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                    {
                        msg = "Falha ao alterar autor.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro: " + ex.Message;
            }

            return msg;
        }

        public string DeleteAutores(string id)
        {
            string msg;

            try
            {
                var response = new Requisicao().Delete("/api/Autor/" + id);

                msg = "Autor apagado com sucesso.";

                if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                {
                    msg = "Falha ao apagar autor.";
                }
            }
            catch (Exception ex)
            {
                msg = "Erro: " + ex.Message;
            }

            return msg;
        }

        #endregion Fim Http

    }
}