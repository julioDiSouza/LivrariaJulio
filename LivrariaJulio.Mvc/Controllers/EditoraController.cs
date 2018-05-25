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
    public class EditoraController : Controller
    {

        #region Actions

        public ActionResult Index()
        {
            return View();
        }

        #endregion Fim Actions

        #region Http

        public JsonResult GetEditoras(string sidx, string sord, int page, int rows)
        {
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;

            var response = new Requisicao().Get("/api/Editora");

            List<Editora> editoras;
            using (var jsonTextReader = new JsonTextReader(new StreamReader(response.GetResponseStream())))
            {
                editoras = new JsonSerializer().Deserialize<List<Editora>>(jsonTextReader);
            }

            int totalRecords = editoras.Count;
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);

            if (!string.IsNullOrWhiteSpace(sord))
            {
                if (sord.ToUpper() == "DESC")
                {
                    editoras = editoras.OrderByDescending(x => x.Nome).ToList();
                    editoras = editoras.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
                else
                {
                    editoras = editoras.OrderBy(x => x.Nome).ToList();
                    editoras = editoras.Skip(pageIndex * pageSize).Take(pageSize).ToList();
                }
            }

            var jsonData = new
            {
                total = totalPages,
                page,
                records = totalRecords,
                rows = editoras
            };
            
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Editoras()
        {
            var response = new Requisicao().Get("/api/Editora");

            List<Editora> editoras;
            using (var jsonTextReader = new JsonTextReader(new StreamReader(response.GetResponseStream())))
            {
                editoras = new JsonSerializer().Deserialize<List<Editora>>(jsonTextReader);
            }

            editoras = editoras.OrderBy(x => x.Nome).ToList();

            return Json(editoras, JsonRequestBehavior.AllowGet);
        }

        public string AddEditoras(Editora editora)
        {
            var msg = "Preencha o nome da Editora.";

            try
            {
                if (!string.IsNullOrWhiteSpace(editora.Nome))
                {
                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(editora).Replace("  ", ""));
                    var response = new Requisicao().Post("/api/Editora", data);

                    msg = "Editora adicionada com sucesso.";

                    if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                    {
                        msg = "Falha ao cadastrar editora.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro: " + ex.Message;
            }

            return msg;
        }

        public string EditEditoras(Editora editora)
        {
            var msg = "Preencha o nome da Editora.";

            try
            {
                if (!string.IsNullOrWhiteSpace(editora.Nome))
                {
                    var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(editora).Replace("  ", ""));
                    var response = new Requisicao().Put("/api/Editora", data);

                    msg = "Editora alterada com sucesso.";

                    if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                    {
                        msg = "Falha ao alterar editora.";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = "Erro: " + ex.Message;
            }

            return msg;
        }

        public string DeleteEditoras(string id)
        {
            string msg;

            try
            {
                var response = new Requisicao().Delete("/api/Editora/" + id);

                msg = "Editora apagada com sucesso.";

                if (response.StatusCode != (HttpStatusCode)200 && response.StatusCode != (HttpStatusCode)201)
                {
                    msg = "Falha ao apagar editora.";
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