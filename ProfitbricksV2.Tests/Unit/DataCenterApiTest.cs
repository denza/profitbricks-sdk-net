using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Api;
using Client;
using Model;
using System.Collections.Generic;
using System.Threading;

namespace ProfitBricksSDK.Tests
{
    [TestClass]
    public class DataCenterTest
    {
        DataCenterApi dcApi = new DataCenterApi(Config.Configuration);
        static Datacenter datacenter;
        static string instanceType = "datacenter";
        static string simpleDesc = ".NET SDK test datacenter";
        static string compositeName = ".NET SDK Test Composite";
        static string compositeDesc = ".NET SDK test composite datacenter";

        [TestMethod]
        public void DataCenterCreateSimple()
        {
            datacenter = TestHelper.CreateDatacenter(description: simpleDesc);

            Assert.AreEqual(instanceType, datacenter.Type);
            Assert.AreEqual(Config.DefaultName, datacenter.Properties.Name);
            Assert.AreEqual(simpleDesc, datacenter.Properties.Description);
            Assert.AreEqual(Config.DefaultLocation, datacenter.Properties.Location);
        }

        [TestMethod]
        public void DataCenterCreateComposite()
        {
            var composite = new Datacenter
            {
                Properties = new DatacenterProperties
                {
                    Name = compositeName,
                    Description = compositeDesc,
                    Location = Config.DefaultLocation
                },
                Entities = new DatacenterEntities
                {
                    Servers = TestHelper.BuildServers(),
                    Volumes = TestHelper.BuildVolumes()
                }
            };

            composite = dcApi.Create(composite);
            Config.DoWait(composite.Request);
            composite = dcApi.FindById(composite.Id, depth: 5);
            Assert.AreEqual(instanceType, composite.Type);
            Assert.AreEqual(compositeName, composite.Properties.Name);
            Assert.AreEqual(compositeDesc, composite.Properties.Description);
            Assert.AreEqual(Config.DefaultLocation, composite.Properties.Location);
            Assert.IsTrue(composite.Entities.Servers.Items.Count > 0);
            Assert.IsTrue(composite.Entities.Volumes.Items.Count > 0);

            // wait before cleanup
            Thread.Sleep(30000);
            Assert.IsNull(dcApi.Delete(composite.Id));
        }

        [TestMethod]
        public void DataCenterGet()
        {
            var dc = dcApi.FindById(datacenter.Id, depth: 5);
            Assert.AreEqual(datacenter.Id, dc.Id);
            Assert.AreEqual(instanceType, dc.Type);
            Assert.AreEqual(datacenter.Properties.Name, dc.Properties.Name);
            Assert.AreEqual(datacenter.Properties.Description, dc.Properties.Description);
            Assert.AreEqual(datacenter.Properties.Location, dc.Properties.Location);
        }

        [TestMethod]
        [ExpectedException(typeof(ApiException))]
        public void DataCenterGetFail()
        {
            try
            {
                dcApi.FindById("00000000-0000-0000-0000-000000000000");
            }
            catch (ApiException e)
            {
                Assert.IsTrue(e.Message.Contains(Config.NotFoundError));
                throw;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ApiException))]
        public void DataCenterCreateFail()
        {
            try
            {
                var dc = new Datacenter
                {
                    Properties = new DatacenterProperties
                    {
                        Name = Config.DefaultName,
                    }
                };
                dcApi.Create(dc);
            }
            catch (ApiException e)
            {
                Assert.IsTrue(e.Message.Contains(string.Format(Config.MissingAttributeError, "location")));
                throw;
            }
        }

        [TestMethod]
        public void DataCenterList()
        {
            var list = dcApi.FindAll(depth: 1);
            Assert.IsTrue(list.Items.Count > 0);
            Assert.AreEqual(instanceType, list.Items[0].Type);
        }

        [TestMethod]
        public void DataCenterUpdate()
        {
            string newName = Config.DefaultName + " - RENAME";
            var resp = dcApi.Update(datacenter.Id, new Datacenter { Properties = new DatacenterProperties { Name = newName } });

            Assert.AreEqual(datacenter.Id, resp.Id);
            Assert.AreEqual(newName, resp.Properties.Name);

            Thread.Sleep(5000);
            resp = dcApi.FindById(datacenter.Id, depth: 1);

            Assert.IsTrue(resp.Properties.Version > 1);
        }

        [TestMethod]
        public void DataCenterDelete()
        {
            var resp = dcApi.Delete(datacenter.Id);
            Assert.IsNull(resp);
        }
    }
}
