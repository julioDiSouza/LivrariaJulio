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
    public class AutorController : ApiController
    {
        
        #region Globais

        private JsonSerializerSettings camelCase = new JsonSerializerSettings();
        private Context db = new Context();

        #endregion Fim Globais

        #region Construtor

        public AutorController()
        {
            // Ajuste para devolver Json no Padrão camelCase
            this.camelCase.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion Fim Construtor

        #region Http

        [HttpGet]
        public JsonResult<List<Autor>> GetAutores()
        {
            try
            {
                // Obtem Autores
                var autores = db.Autores.ToList();

                // Retorna Json
                return Json(autores, camelCase);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        [HttpGet]
        public JsonResult<Autor> GetAutor(int id)
        {
            try
            {
                // Obtem Autor
                var autor = db.Autores.Find(id);

                // Retorna Json
                return Json(autor, camelCase);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        [HttpPost]
        public HttpResponseMessage PostAutor([FromBody] Autor autor)
        {
            try
            {
                db.Autores.Add(autor);
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
        public HttpResponseMessage PutAutor([FromBody] Autor autor)
        {
            try
            {
                db.Entry(autor).State = EntityState.Modified;
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
        public HttpResponseMessage DeleteAutor(int id)
        {
            try
            {
                var autor = db.Autores.Find(id);

                db.Autores.Remove(autor);
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