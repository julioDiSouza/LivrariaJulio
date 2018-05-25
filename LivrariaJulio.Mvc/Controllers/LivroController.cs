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
    public class LivroController : Controller
    {

        #region Actions

        public ActionResult Index()
        {
            return View();
        }

        #endregion Fim Actions

        #region Http

        public JsonResult GetLivros(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var response = new Requisicao().Get("/api/Livro");

            List<Livro> livros;
            using (var jsonTextReader = new JsonTextReader(new StreamReader(response.GetResponseStream())))
            {
                livros = new JsonSerializer().Deserialize<List<Livro>>(jsonTextReader);
            }

            int totalRecords = livros.Count;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (!string.IsNullOrWhiteSpace(sord))
            {
                if (sord.ToUpper() == "DESC")
                {
                    livros = livros.OrderByDescending(x => x.Titulo).ToList();
                    livros = livros.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    livros = livros.OrderBy(x => x.Titulo).ToList();
                    livros = livros.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
            }

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = livros
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
              
        public string AddLivros(Livro livro)
        {
            var msg = "Preencha todos os campos corretamente.";

            try
            {
                if (!string.IsNullOrWhiteSpace(livro.Titulo) && livro.Autor.Nome != "0" && livro.Editora.Nome != "0")
                {
                    Livro obj = new Livro();
                    obj.Titulo = livro.Titulo;
                    obj.AutorID = Convert.ToInt32(livro.Autor.Nome);
                    obj.EditoraID = Convert.ToInt32(livro.Editora.Nome);

                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj).Replace("  ", ""));
                    var response = new Requisicao().Post("/api/Livro", data);

                    msg = "Livro adicionado com sucesso.";

                    if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                    {
                        msg = "Falha ao cadastrar Livro.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro: " + ex.Message;
            }

            return msg;
        }

        public string EditLivros(Livro livro)
        {
            var msg = "Preencha todos os campos corretamente.";

            try
            {
                if (!string.IsNullOrWhiteSpace(livro.Titulo) && livro.Autor.Nome != "0" && livro.Editora.Nome != "0")
                {
                    Livro obj = new Livro();
                    obj.LivroID = livro.LivroID;
                    obj.Titulo = livro.Titulo;
                    obj.AutorID = Convert.ToInt32(livro.Autor.Nome);
                    obj.EditoraID = Convert.ToInt32(livro.Editora.Nome);

                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj).Replace("  ", ""));
                    var response = new Requisicao().Put("/api/Livro", data);

                    msg = "Livro alterado com sucesso.";

                    if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                    {
                        msg = "Falha ao alterar Livro.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro: " + ex.Message;
            }

            return msg;
        }

        public string DeleteLivros(string id)
        {
            string msg;

            try
            {
                var response = new Requisicao().Delete("/api/Livro/" + id);

                msg = "Livro apagado com sucesso.";

                if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                {
                    msg = "Falha ao apagar Livro.";
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