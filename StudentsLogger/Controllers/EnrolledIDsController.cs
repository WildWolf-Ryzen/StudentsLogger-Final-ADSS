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
using StudentsLogger.Models;

namespace StudentsLogger.Controllers
{
    public class EnrolledIDsController : ApiController
    {
        private StudentsLoggerAppEntities db = new StudentsLoggerAppEntities();

        // GET: api/EnrolledIDs
        public IQueryable<EnrolledID> GetEnrolledIDs()
        {
            return db.EnrolledIDs;
        }

        // GET: api/EnrolledIDs/5
        [ResponseType(typeof(EnrolledID))]
        public IHttpActionResult GetEnrolledID(int id)
        {
            EnrolledID enrolledID = db.EnrolledIDs.Find(id);
            if (enrolledID == null)
            {
                return NotFound();
            }

            return Ok(enrolledID);
        }

        // PUT: api/EnrolledIDs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnrolledID(int id, EnrolledID enrolledID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrolledID.EnrollID)
            {
                return BadRequest();
            }

            db.Entry(enrolledID).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrolledIDExists(id))
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

        // POST: api/EnrolledIDs
        [ResponseType(typeof(EnrolledID))]
        public IHttpActionResult PostEnrolledID(EnrolledID enrolledID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.EnrolledIDs.Add(enrolledID);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EnrolledIDExists(enrolledID.EnrollID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = enrolledID.EnrollID }, enrolledID);
        }

        // DELETE: api/EnrolledIDs/5
        [ResponseType(typeof(EnrolledID))]
        public IHttpActionResult DeleteEnrolledID(int id)
        {
            EnrolledID enrolledID = db.EnrolledIDs.Find(id);
            if (enrolledID == null)
            {
                return NotFound();
            }

            db.EnrolledIDs.Remove(enrolledID);
            db.SaveChanges();

            return Ok(enrolledID);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnrolledIDExists(int id)
        {
            return db.EnrolledIDs.Count(e => e.EnrollID == id) > 0;
        }
    }
}