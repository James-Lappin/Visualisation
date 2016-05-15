using System.Web.Mvc;
using Visualisation.Core.Domain;

namespace Visualisation.Web.Controllers
{
	public class TransactionRequestController : Controller
	{
		[HttpPost]
		public void Location(TransactionRequest transaction)
		{
			transaction?.Map();
		}
	}
}
