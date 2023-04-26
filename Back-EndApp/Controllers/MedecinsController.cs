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
    public class MedecinsController : ApiController
    {
        private GSB_DATA_Model db = new GSB_DATA_Model();

        // GET: api/Medecins
        public IQueryable<Medecin> GetMedecins()
        {
            return db.Medecins;
        }

        // GET: api/Medecins/5
        [ResponseType(typeof(Medecin))]
        public IHttpActionResult GetMedecin(int id)
        {
            Medecin medecin = db.Medecins.Find(id);
            if (medecin == null)
            {
                return NotFound();
            }

            return Ok(medecin);
        }

        // PUT: api/Medecins/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMedecin(int id, Medecin medecin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medecin.id_medic)
            {
                return BadRequest();
            }

            db.Entry(medecin).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedecinExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Medecins
        [Authorize]
        [ResponseType(typeof(Medecin))]
        public IHttpActionResult PostMedecin(Medecin medecin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medecins.Add(medecin);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medecin.id_medic }, medecin);
        }

        // DELETE: api/Medecins/5
        [Authorize]
        [ResponseType(typeof(Medecin))]
        public IHttpActionResult DeleteMedecin(int id)
        {
            Medecin medecin = db.Medecins.Find(id);
            if (medecin == null)
            {
                return NotFound();
            }

            db.Medecins.Remove(medecin);
            db.SaveChanges();

            return Ok(medecin);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedecinExists(int id)
        {
            return db.Medecins.Count(e => e.id_medic == id) > 0;
        }
    }
}