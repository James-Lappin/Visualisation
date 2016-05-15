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

			var restClient = new RestClient("http://maps.rebtabpyik.eu-west-1.elasticbeanstalk.com");
			//var restClient = new RestClient("http://localhost/Visualisation");

			for (var i = 0; i < 5; i++)
			{
				var request = new RestRequest("TransactionRequest/Location", Method.POST);
				var lat = random.Next(50, 60);
				var longatude = random.Next(0, 10);
				request.AddParameter("latitude", lat);
				request.AddParameter("longitude", longatude);
				request.AddParameter("title", "Test transaction");

				var response = restClient.Post(request);

				Assert.That(response, Is.Not.Null);
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			}

			var postcodes = new[] { "HG1 5NA", "SE10 0QG", "N1 9HF", "SL4 1NJ", "ls10 3tp" };
			for (var i = 0; i < 5; i++)
			{
				var request = new RestRequest("TransactionRequest/Location", Method.POST);
				request.AddParameter("Postcode", postcodes[i]);
				request.AddParameter("title", "Postcode Test transaction");

				var response = restClient.Post(request);

				Assert.That(response, Is.Not.Null);
				Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
			}
		}
	}
}
