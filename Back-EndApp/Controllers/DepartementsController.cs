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
using Model;
using ORM_GSB;

namespace Back_EndApp.Controllers
{
    public class DepartementsController : ApiController
    {
        private GSB_DATA_Model db = new GSB_DATA_Model();   

       // [AuthentificationFilter]
        // GET: api/Departements
        public IQueryable<Departement> GetDepartements()
        {
            return db.Departements;
        }

        // GET: api/Departements/5
        //[AuthentificationFilter]
        [ResponseType(typeof(Departement))]
        public IHttpActionResult GetDepartement(int id)
        {
            Departement departement = db.Departements.Find(id);
            if (departement == null)
            {
                return NotFound();
            }

            return Ok(departement);
        }

        //// PUT: api/Departements/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutDepartement(int id, Departement departement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != departement.num_dep)
        //    {
        //        return BadRequest();
        //    }
                
        //    db.Entry(departement).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DepartementExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Departements
        //[ResponseType(typeof(Departement))]
        //public IHttpActionResult PostDepartement(Departement departement)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Departements.Add(departement);

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (DepartementExists(departement.num_dep))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = departement.num_dep }, departement);
        //}

        //// DELETE: api/Departements/5
        //[ResponseType(typeof(Departement))]
        //public IHttpActionResult DeleteDepartement(int id)
        //{
        //    Departement departement = db.Departements.Find(id);
        //    if (departement == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Departements.Remove(departement);
        //    db.SaveChanges();

        //    return Ok(departement);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //        private bool DepartementExists(int id)
        //        {
        //            return db.Departements.Count(e => e.num_dep == id) > 0;
        //        }
    }
}