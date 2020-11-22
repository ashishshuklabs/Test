using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Unleashed.Models;

namespace Unleashed.Tests {
    [TestFixture]
    public class APITests {
        private RestClient client;
        [SetUp]
        public void Setup() {
            client = new RestClient("https://api.unleashedsoftware.com/");
            client
                .AddDefaultHeader("api-auth-signature", APITests.GetSignature("", "UniMVNMsZJp1rEDNuHRz8tLesTJFw82E8UMKJN3xwv4vyDf4L3GD8O46pXzy6YQoqY8JG2cnbd4LlVSLJMegw=="))
                .AddDefaultHeader("api-auth-id", "b2696a86-0ee2-4567-b487-09aeb1249138")
                .AddDefaultHeader("Content-Type", "application/json")
                .AddDefaultHeader("Accept", "application/json");
        }
        [Test]
        [Category("Unleashed")]
        public void TestSomeBasicAPI() {
            
            var request = new RestRequest("Customers/1", Method.GET);
            var response = client.Execute(request);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Bad Response");
            System.Console.WriteLine(response.Content);
        }
        [Test]
        [Category("Unleashed")]
        public void TestSaleAPI() {
            var request = new RestRequest("SalesOrders/{b599e7f5-7738-4ea1-a318-6b5298766014}", Method.POST);
            var data = File.ReadAllText("../../../DeleteMeProject/Unleashed/Models/Request.json");
            var requestData = JsonConvert.DeserializeObject<Root>(data);
            requestData.Guid = new Guid().ToString();
            requestData.OrderStatus = "Parked";
            requestData.Customer = new Customer { Guid = new Guid().ToString(), CustomerCode = "MATSUP" };
            requestData.Tax = new Tax { Guid = new Guid().ToString(), TaxCode = "V.A.T.", TaxRate = 0.200000 };
            requestData.TaxRate = 0.200000;
            requestData.Total = 30.000;
            requestData.SubTotal = 25.000;
            requestData.TaxTotal = 5.000;
            requestData.SalesOrderLines = new List<SalesOrderLine> {
                new SalesOrderLine {
                    Guid = new Guid().ToString(),
                }
            };
            request.AddJsonBody(requestData);
            var response = client.Get<Root>(request);
            var responseData = response.Data;
            Console.WriteLine(responseData);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, "Bad Response");
            Assert.AreEqual(0.200000, requestData.TaxRate, "Tax Rate Invalid");
            Assert.AreEqual( "Parked",requestData.OrderStatus, "OrderStatus");
            Assert.AreEqual("MATSUP", responseData.Customer.CustomerCode, "Customer Code does not match");
            Assert.AreEqual(requestData.Tax.TaxCode, "V.A.T.", "Invalid Tax Code");

            System.Console.WriteLine(response.Content);
        }
        private static string GetSignature(string args, string privatekey) {
            var encoding = new System.Text.UTF8Encoding();
            byte[] key = encoding.GetBytes(privatekey);
            var myhmacsha256 = new HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(args));
            string hmac64 = Convert.ToBase64String(hashValue);
            myhmacsha256.Clear();
            return hmac64;
        }
    }
}
