using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using LivrariaJulio.WebApi.Models;
using LivrariaJulio.WebApi.Models.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LivrariaJulio.WebApi.Controllers
{
    public class LivroController : ApiController
    {

        #region Globais

        private Context db = new Context();
        private JsonSerializerSettings camelCase = new JsonSerializerSettings();

        #endregion Fim Globais

        #region Construtor

        public LivroController()
        {
            // Ajuste para devolver Json no Padrão camelCase
            this.camelCase.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion Fim Construtor

        #region Http

        [HttpGet]
        public JsonResult<List<Livro>> GetLivros()
        {
            try
            {
                var livros = db.Livros.ToList();

                return Json(livros, camelCase);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        [HttpGet]
        public JsonResult<Livro> GetLivro(int id)
        {
            try
            {
                var livro = db.Livros.Find(id);

                return Json(livro, camelCase);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        [HttpPost]
        public HttpResponseMessage PostLivro([FromBody] Livro livro)
        {
            try
            {
                db.Livros.Add(livro);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        [HttpPut]
        public HttpResponseMessage PutLivro(Livro livro)
        {
            db.Entry(livro).State = EntityState.Modified;

            try
            {
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteLivro(int id)
        {
            try
            {
                var livro = db.Livros.Find(id);

                db.Livros.Remove(livro);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        #endregion Fim Http

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion Fim Dispose

    }
}