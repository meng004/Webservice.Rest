using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebService.Rest.Models;

namespace WebService.Rest.Controllers
{
    public class ProvincesController : ApiController
    {
        private CityDataContext db = new CityDataContext();

        // GET api/Provinces
        public IEnumerable<Province> GetProvinces()
        {
            return db.Provinces.AsEnumerable();
        }

        // GET api/Provinces/5
        public Province GetProvince(int id)
        {
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return province;
        }

        // PUT api/Provinces/5
        public HttpResponseMessage PutProvince(int id, Province province)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != province.ProvinceId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(province).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Provinces
        public HttpResponseMessage PostProvince(Province province)
        {
            if (ModelState.IsValid)
            {
                db.Provinces.Add(province);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, province);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = province.ProvinceId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Provinces/5
        public HttpResponseMessage DeleteProvince(int id)
        {
            Province province = db.Provinces.Find(id);
            if (province == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Provinces.Remove(province);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, province);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}