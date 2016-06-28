using System;
using Visualisation.Core.Enums;
using Visualisation.Core.Responsitories;

namespace Visualisation.Core.Domain
{
	public class TransactionRequest : EntityBase
	{
		public string Title { get; set; }
		public PaymentType PaymentType { get; set; }
		public double? Longitude { get; set; }
		public double? Latitude { get; set; }
		public string Postcode { get; set; }
		public DateTimeOffset CreatedDate { get; set; }

	    public TransactionRequest()
	    {
            CreatedDate = DateTimeOffset.Now;
        }
	}
}