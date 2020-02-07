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
    public class StudentDatasController : ApiController
    {
        private StudentsLoggerAppEntities db = new StudentsLoggerAppEntities();

        // GET: api/StudentDatas
        public IQueryable<StudentData> GetStudentDatas()
        {
            return db.StudentDatas;
        }

        // GET: api/StudentDatas/5
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult GetStudentData(int id)
        {
            StudentData studentData = db.StudentDatas.Find(id);
            if (studentData == null)
            {
                return NotFound();
            }

            return Ok(studentData);
        }

        // PUT: api/StudentDatas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStudentData(int id, StudentData studentData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != studentData.ID)
            {
                return BadRequest();
            }

            db.Entry(studentData).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentDataExists(id))
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

        // POST: api/StudentDatas
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult PostStudentData(StudentData studentData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StudentDatas.Add(studentData);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = studentData.ID }, studentData);
        }

        // DELETE: api/StudentDatas/5
        [ResponseType(typeof(StudentData))]
        public IHttpActionResult DeleteStudentData(int id)
        {
            StudentData studentData = db.StudentDatas.Find(id);
            if (studentData == null)
            {
                return NotFound();
            }

            db.StudentDatas.Remove(studentData);
            db.SaveChanges();

            return Ok(studentData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StudentDataExists(int id)
        {
            return db.StudentDatas.Count(e => e.ID == id) > 0;
        }
    }
}