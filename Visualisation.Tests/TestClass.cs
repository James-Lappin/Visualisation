using NUnit.Framework;
using RestSharp;
using System;
using Visualisation.Web.Models;

namespace Visualisation.Tests
{
	[TestFixture]
	public class TestClass
	{
		[Test]
		public void TestMethod()
		{
			var random = new Random();

			for (int i = 0; i < 5; i++)
			{
				var lat = random.Next(50, 60);
				var longatude = random.Next(0, 10);
				var transactionRequest = new TransactionRequest { Lat = lat, Long = -longatude, Title = "Test transaction" };
				var restClient = new RestClient("http://localhost/");
				var request = new RestRequest("Visualisation/TransactionRequest/Location", Method.POST);
				request.AddParameter("lat", transactionRequest.Lat);
				request.AddParameter("long", transactionRequest.Long);
				request.AddParameter("title", transactionRequest.Title);

				var response = restClient.Post(request);
			}



			//Assert.That(response, Is.Not.Null);
			//Assert.That(response.StatusCode, Is.EqualTo((int)HttpStatusCode.OK));
		}
	}
}
