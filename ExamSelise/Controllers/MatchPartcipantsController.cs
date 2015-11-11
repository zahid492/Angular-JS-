using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using ExamSelise;

namespace ExamSelise.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using ExamSelise;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<MatchPartcipant>("MatchPartcipants");
    builder.EntitySet<MatchDetail>("MatchDetails"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MatchPartcipantsController : ODataController
    {
        private CricketMatchModel db = new CricketMatchModel();

        // GET: odata/MatchPartcipants
        [EnableQuery]
        public IQueryable<MatchPartcipant> GetMatchPartcipants()
        {
            return db.MatchPartcipants;
        }

        // GET: odata/MatchPartcipants(5)
        [EnableQuery]
        public SingleResult<MatchPartcipant> GetMatchPartcipant([FromODataUri] int key)
        {
            return SingleResult.Create(db.MatchPartcipants.Where(matchPartcipant => matchPartcipant.GID == key));
        }

        // PUT: odata/MatchPartcipants(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<MatchPartcipant> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MatchPartcipant matchPartcipant = await db.MatchPartcipants.FindAsync(key);
            if (matchPartcipant == null)
            {
                return NotFound();
            }

            patch.Put(matchPartcipant);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchPartcipantExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(matchPartcipant);
        }

        // POST: odata/MatchPartcipants
        public async Task<IHttpActionResult> Post(MatchPartcipant matchPartcipant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MatchPartcipants.Add(matchPartcipant);
            await db.SaveChangesAsync();

            return Created(matchPartcipant);
        }

        // PATCH: odata/MatchPartcipants(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<MatchPartcipant> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MatchPartcipant matchPartcipant = await db.MatchPartcipants.FindAsync(key);
            if (matchPartcipant == null)
            {
                return NotFound();
            }

            patch.Patch(matchPartcipant);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchPartcipantExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(matchPartcipant);
        }

        // DELETE: odata/MatchPartcipants(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            MatchPartcipant matchPartcipant = await db.MatchPartcipants.FindAsync(key);
            if (matchPartcipant == null)
            {
                return NotFound();
            }

            db.MatchPartcipants.Remove(matchPartcipant);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/MatchPartcipants(5)/MatchDetails
        [EnableQuery]
        public IQueryable<MatchDetail> GetMatchDetails([FromODataUri] int key)
        {
            return db.MatchPartcipants.Where(m => m.GID == key).SelectMany(m => m.MatchDetails);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchPartcipantExists(int key)
        {
            return db.MatchPartcipants.Count(e => e.GID == key) > 0;
        }
    }
}
