using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIAJAXLoading.Models;

namespace WebAPIAJAXLoading.Controllers
{
    public class MoviesController : ApiController
    {
        // GET ALL 
        public IEnumerable<Movy> Get() {
            using (DBEntities1 db = new DBEntities1()) {

                return db.Movies.ToList();
            
            }
        
        }

        public HttpResponseMessage Get(int id) {

            using (DBEntities1 db = new DBEntities1()) {
                var entity = db.Movies.FirstOrDefault(x => x.MovieId == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else {

                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "We did not find the Movie:" + id.ToString());
                }
            }
        
        }

        // POST 
        public HttpResponseMessage Post([FromBody] Movy J)
        {
            using (DBEntities1 db = new DBEntities1())
            {
                try
                {
                    db.Movies.Add(J);
                    db.SaveChanges();
                    var msg = Request.CreateResponse(HttpStatusCode.OK, J);
                    msg.Headers.Location = new Uri(Request.RequestUri + J.MovieId.ToString());
                    return msg;
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                }

            }



        }

        // DELETE

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (DBEntities1 db = new DBEntities1())
                {
                    var entity = db.Movies.FirstOrDefault(x => x.MovieId == id);
                    if (entity != null)
                    {
                        db.Movies.Remove(entity);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "It does not exist" + id.ToString());

                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
