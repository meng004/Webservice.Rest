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
    public class DistrictsController : ApiController
    {
        private CityDataContext db = new CityDataContext();

        // GET api/Districts
        public IEnumerable<District> GetDistricts(int id)
        {
            var districts = db.Districts.Where(t => t.CityId == id).ToList();
            if (districts.Count==0)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return districts;
        }

        // GET api/Districts/5
        //public District GetDistrict(int id)
        //{
        //    District district = db.Districts.Find(id);
        //    if (district == null)
        //    {
        //        throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
        //    }

        //    return district;
        //}

        // PUT api/Districts/5
        public HttpResponseMessage PutDistrict(int id, District district)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != district.DistrictId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(district).State = EntityState.Modified;

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

        // POST api/Districts
        public HttpResponseMessage PostDistrict(District district)
        {
            if (ModelState.IsValid)
            {
                db.Districts.Add(district);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, district);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = district.DistrictId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Districts/5
        public HttpResponseMessage DeleteDistrict(int id)
        {
            District district = db.Districts.Find(id);
            if (district == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Districts.Remove(district);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, district);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}