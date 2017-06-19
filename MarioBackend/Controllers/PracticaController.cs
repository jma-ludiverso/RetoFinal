using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using MarioBackend.DataObjects;
using MarioBackend.Models;
using System;

namespace MarioBackend.Controllers
{
    public class PracticaController : TableController<Practica>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Practica>(context, Request);
        }

        // GET tables/Practica
        public IQueryable<Practica> GetAllPractica()
        {
            try
            {
                return Query().OrderBy(p => p.Fecha);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                return null;
            }
        }

        // GET tables/Practica/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Practica> GetPractica(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Practica/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Practica> PatchPractica(string id, Delta<Practica> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Practica
        public async Task<IHttpActionResult> PostPractica(Practica item)
        {
            Practica current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Practica/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePractica(string id)
        {
            try
            {
                return DeleteAsync(id);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
                return null;
            }
        }
    }
}
