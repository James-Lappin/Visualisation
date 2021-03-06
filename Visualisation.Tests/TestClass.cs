﻿using NUnit.Framework;
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

            //var restClient = new RestClient("http://maps.rebtabpyik.eu-west-1.elasticbeanstalk.com");
            var restClient = new RestClient("http://localhost/Visualisation");

            for (var i = 0; i < 10; i++)
            {
                var request = new RestRequest("Map/Transaction", Method.POST);
                var lat = random.Next(5000, 6000) / (double)100;
                var longatude = random.Next(000, 1000) / (double)100;
                request.AddParameter("latitude", lat);
                request.AddParameter("longitude", -longatude);
                request.AddParameter("title", "Test transaction");
                request.AddParameter("PaymentType", "Payment");

                var response = restClient.Post(request);

                Assert.That(response, Is.Not.Null);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), response.Content);
            }

            var postcodes = new[] { "HG1 5NA", "SE10 0QG", "N1 9HF", "SL4 1NJ", "ls10 3tp" };
            for (var i = 0; i < 5; i++)
            {
                var request = new RestRequest("Map/Transaction", Method.POST);
                request.AddParameter("Postcode", postcodes[i]);
                request.AddParameter("title", "Postcode Test transaction");

                var response = restClient.Post(request);

                Assert.That(response, Is.Not.Null);
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }
    }
}
