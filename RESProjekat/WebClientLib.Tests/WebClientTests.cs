﻿using Newtonsoft.Json;
using NUnit.Framework;
using SharedResources;
using SharedResources.Interfaces;

namespace WebClientLib.Tests
{
    [TestFixture]
    public class WebClientTests
    {
        private WebClient webClient = null;
        private Response response = null;
        private Request request = null;
       

        [SetUp]
        public void SetUp()
        {
            webClient = new WebClient();
            response = new Response();
            request = new Request();
        }

        [Test]
        public void WC_Constructor_Good()
        {
            WebClient webClient = new WebClient();
            Assert.IsNotNull(webClient);
        }

        [Test]
        [TestCase("GET /")]
        [TestCase("PATCH /resource")]
        [TestCase("PATCH /resource/2")]
        [TestCase("DELETE/resource/2")]
        public void WC_SendRequest_BadRequests(string request)
        {
            string result = webClient.SendRequest(request);

            response.Payload = new Payload();
            response.Payload.Resource = null;
            response.Payload.ErrorMessage = "Request is not valid.";
            response.Status = Status.BAD_FORMAT.ToString();
            response.StatusCode = StatusCode.BAD_FORMAT_CODE;
            string expected = JsonConvert.SerializeObject(response);

            Assert.AreEqual(expected, result);

        }

        [Test]
        [TestCase("GET /connection/2")]
        [TestCase("PUT /resource/2")]
        public void WC_SendRequest_BadVerbRequests(string request)
        {
            string result = webClient.SendRequest(request);

            response.Payload = new Payload();
            response.Payload.Resource = null;
            response.Payload.ErrorMessage = "Method not supported.";
            response.Status = Status.BAD_FORMAT.ToString();
            response.StatusCode = StatusCode.BAD_FORMAT_CODE;
            string expected = JsonConvert.SerializeObject(response);

            Assert.AreEqual(expected, result);

        }

        [Test]
        [TestCase("GET /resource {\"name\"=\"pera\"}")]
        [TestCase("GET /resource {\"name\"=\"pera\", \"type\"=\"3\"}")]
        [TestCase("GET /resource {\"name\"=\"pera\", \"connectedTo\"=\"3, 5, 7\"}, \"connectedType\"=\"3, 5\"")]
        [TestCase("GET /resource {\"name\"=\"pera\", \"connectedType\"=\"3, 5\"}")]
        [TestCase("GET /resource {\"name\"=\"pera\", \"fields\"=\"id, name, title\"}")]
        public void WC_ParseRequest_QuerySuccess(string request)
        {
            IRequest result = webClient.ParseRequest(request);

            Assert.IsNotNull(result.Query);

        }

        [TearDown]
        public void TearDown()
        {
            webClient = null;
            response = null;
            request = null;
        }

    }
}
