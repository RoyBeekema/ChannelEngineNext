using ChannelEngineMvc.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MerchantDomain;
using System.Linq;
using MerchantData;
using MerchantLogic;

namespace ChannelEngineTest
{
    public class Tests
    {
        readonly List<Order> orders = new List<Order>();

        [SetUp]
        public void Setup()
        {
            orders.Add(new Order
            {
                Id = 1,
                ChannelOrderNo = "1",
                TotalInclVat = 10,
                Lines = new List<OrderLine>
                { new OrderLine { Description = "One", Gtin = "11", MerchantProductNo = "1111", Quantity = 1 } }
            });
            orders.Add(new Order
            {
                Id = 2,
                ChannelOrderNo = "2",
                TotalInclVat = 10,
                Lines = new List<OrderLine>
                { new OrderLine { Description = "Two", Gtin = "22", MerchantProductNo = "2222", Quantity = 9 } }
            });
            orders.Add(new Order
            {
                Id = 3,
                ChannelOrderNo = "3",
                TotalInclVat = 10,
                Lines = new List<OrderLine>
                { new OrderLine { Description = "Thre", Gtin = "33", MerchantProductNo = "3333", Quantity = 25 } }
            });
            orders.Add(new Order
            {
                Id = 4,
                ChannelOrderNo = "4",
                TotalInclVat = 10,
                Lines = new List<OrderLine>
                { new OrderLine { Description = "Four", Gtin = "44", MerchantProductNo = "4444", Quantity = 4 } }
            });
            orders.Add(new Order
            {
                Id = 5,
                ChannelOrderNo = "5",
                TotalInclVat = 10,
                Lines = new List<OrderLine>
                { new OrderLine { Description = "Five", Gtin = "55", MerchantProductNo = "5555", Quantity = 0 } }
            });
            orders.Add(new Order
            {
                Id = 6,
                ChannelOrderNo = "6",
                TotalInclVat = 10,
                Lines = new List<OrderLine>
                { new OrderLine { Description = "Siz", Gtin = "66", MerchantProductNo = "6666", Quantity = 3 } }
            });
        }

        public IEnumerable<OrderModel> NewTestData(List<Order> testData)
        {
            var loggerMock = new Mock<ILogger<OrdersController>>();
            var repositoryMock = new Mock<IBaseOrderRepository>();
            repositoryMock.Setup(r => r.Orders).Returns(testData);
            var controller = new OrdersController(repositoryMock.Object, loggerMock.Object);

            return ((ViewResult)controller.Index().Result).Model as IEnumerable<OrderModel>;
        }

        [Test(Description = "Test basic functionality")]
        public void TestEmpty()
        {
            var models = NewTestData(new List<Order>());

            Assert.IsNotNull(models);
            Assert.Zero(models.Count());

            Assert.Pass();
        }

        [Test(Description = "Test page size is 5")]
        public void TestPageSize()
        {
            var models = NewTestData(orders);

            Assert.IsNotNull(models);
            Assert.IsTrue(models.Count() == 5);

            Assert.Pass();
        }

        [Test(Description = "Test order correctness")]
        public void TestOrder()
        {
            var models = NewTestData(orders);
            OrderModel previousModel = null;

            Assert.IsNotNull(models);

            foreach(var model in models)
            {
                if (previousModel != null)
                {
                    Assert.True(model.Quantity <= previousModel.Quantity);
                 }

                previousModel = model;
            }

            Assert.Pass();
        }
    }
}