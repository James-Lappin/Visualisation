using System.Web.Mvc;
using Visualisation.Web.Models;

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
