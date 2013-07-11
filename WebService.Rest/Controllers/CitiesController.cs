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
    public class CitiesController : ApiController
    {
        private CityDataContext db = new CityDataContext();

        // GET api/Cities
        public IEnumerable<City> GetCities(int id)
        {
            var cities = db.Cities.Where(t => t.ProvinceId == id).ToList();
            if (cities.Count==0)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return cities;
        }

        // GET api/Cities/5
        //public City GetCity(int id)
        //{
        //    City city = db.Cities.Find(id);
        //    if (city == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return city;
        //}

        // PUT api/Cities/5
        public HttpResponseMessage PutCity(int id, City city)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != city.CityId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(city).State = EntityState.Modified;

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

        // POST api/Cities
        public HttpResponseMessage PostCity(City city)
        {
            if (ModelState.IsValid)
            {
                db.Cities.Add(city);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, city);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = city.CityId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Cities/5
        public HttpResponseMessage DeleteCity(int id)
        {
            City city = db.Cities.Find(id);
            if (city == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Cities.Remove(city);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, city);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}