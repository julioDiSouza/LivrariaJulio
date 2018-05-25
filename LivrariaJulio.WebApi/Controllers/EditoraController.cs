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
    public class EditoraController : ApiController
    {

        #region Globais

        private Context db = new Context();
        private JsonSerializerSettings camelCase = new JsonSerializerSettings();

        #endregion Fim Globais

        #region Construtor

        public EditoraController()
        {
            // Ajuste para devolver Json no Padrão camelCase
            this.camelCase.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        #endregion Fim Construtor

        #region Http

        [HttpGet]
        public JsonResult<List<Editora>> GetEditoras()
        {
            try
            {
                var editoras = db.Editoras.ToList();

                return Json(editoras, camelCase);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        [HttpGet]
        public JsonResult<Editora> GetEditora(int id)
        {
            try
            {
                var editora = db.Editoras.Find(id);

                return Json(editora, camelCase);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.InternalServerError,
                    ex.Message));
            }
        }

        [HttpPost]
        public HttpResponseMessage PostEditora([FromBody] Editora editora)
        {
            try
            {
                db.Editoras.Add(editora);
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
        public HttpResponseMessage PutEditora([FromBody] Editora editora)
        {
            try
            {
                db.Entry(editora).State = EntityState.Modified;
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
        public HttpResponseMessage DeleteEditora(int id)
        {
            try
            {
                var editora = db.Editoras.Find(id);

                db.Editoras.Remove(editora);
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