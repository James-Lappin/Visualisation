using System.Web.Mvc;
using Visualisation.Core.Domain;
using Visualisation.Core.Services;

namespace Visualisation.Web.Controllers
{
    public class MapController : Controller
    {
        private readonly MapService _mapService;

        public MapController(MapService service)
        {
            _mapService = service;
        }

        [HttpPost]
        public void Transaction(TransactionRequest transaction)
        {
            if (transaction == null) { return; }

            _mapService.MapTransaction(transaction);
        }
    }
}
