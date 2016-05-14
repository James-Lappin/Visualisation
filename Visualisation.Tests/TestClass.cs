using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace Visualisation.Tests
{
	[TestFixture]
	public class TestClass
	{
		//This not a test!!!! Just manually check.
		[Test]
		public void TestMethod()
		{
			var random = new Random();

			for (int i = 0; i < 5; i++)
			{
				var lat = random.Next(50, 60);
				var longatude = random.Next(0, 10);
				var restClient = new RestClient("http://localhost/");
				var request = new RestRequest("Visualisation/TransactionRequest/Location", Method.POST);
				request.AddParameter("latitude", lat);
				request.AddParameter("longitude", longatude);
				request.AddParameter("title", "Test transaction");

				var response = restClient.Post(request);

				Assert.That(response, Is.Not.Null);
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			}
		}
	}
}
