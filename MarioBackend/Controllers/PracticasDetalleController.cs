using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using MarioBackend.DataObjects;
using MarioBackend.Models;

namespace MarioBackend.Controllers
{
    public class PracticasDetalleController : TableController<PracticasDetalle>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<PracticasDetalle>(context, Request);
        }

        // GET tables/PracticasDetalle
        public IQueryable<PracticasDetalle> GetAllPracticasDetalle()
        {
            return Query(); 
        }

        // GET tables/PracticasDetalle/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<PracticasDetalle> GetPracticasDetalle(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PracticasDetalle/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<PracticasDetalle> PatchPracticasDetalle(string id, Delta<PracticasDetalle> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/PracticasDetalle
        public async Task<IHttpActionResult> PostPracticasDetalle(PracticasDetalle item)
        {
            PracticasDetalle current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PracticasDetalle/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePracticasDetalle(string id)
        {
             return DeleteAsync(id);
        }
    }
}
