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
    builder.EntitySet<MatchDetail>("MatchDetails");
    builder.EntitySet<MatchPartcipant>("MatchPartcipants"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class MatchDetailsController : ODataController
    {
        private CricketMatchModel db = new CricketMatchModel();

        // GET: odata/MatchDetails
        [EnableQuery]
        public IQueryable<MatchDetail> GetMatchDetails()
        {
            return db.MatchDetails;
        }

        // GET: odata/MatchDetails(5)
        [EnableQuery]
        public SingleResult<MatchDetail> GetMatchDetail([FromODataUri] int key)
        {
            return SingleResult.Create(db.MatchDetails.Where(matchDetail => matchDetail.Id == key));
        }

        // PUT: odata/MatchDetails(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<MatchDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MatchDetail matchDetail = await db.MatchDetails.FindAsync(key);
            if (matchDetail == null)
            {
                return NotFound();
            }

            patch.Put(matchDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(matchDetail);
        }

        // POST: odata/MatchDetails
        public async Task<IHttpActionResult> Post(MatchDetail matchDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MatchDetails.Add(matchDetail);
            await db.SaveChangesAsync();

            return Created(matchDetail);
        }

        // PATCH: odata/MatchDetails(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<MatchDetail> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            MatchDetail matchDetail = await db.MatchDetails.FindAsync(key);
            if (matchDetail == null)
            {
                return NotFound();
            }

            patch.Patch(matchDetail);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchDetailExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(matchDetail);
        }

        // DELETE: odata/MatchDetails(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            MatchDetail matchDetail = await db.MatchDetails.FindAsync(key);
            if (matchDetail == null)
            {
                return NotFound();
            }

            db.MatchDetails.Remove(matchDetail);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/MatchDetails(5)/MatchPartcipant
        [EnableQuery]
        public SingleResult<MatchPartcipant> GetMatchPartcipant([FromODataUri] int key)
        {
            return SingleResult.Create(db.MatchDetails.Where(m => m.Id == key).Select(m => m.MatchPartcipant));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchDetailExists(int key)
        {
            return db.MatchDetails.Count(e => e.Id == key) > 0;
        }
    }
}
