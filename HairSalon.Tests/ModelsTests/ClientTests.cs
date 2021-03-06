using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTests : IDisposable
    {

        public void Dispose()
        {
            Client.DeleteAll();
        }

        public ClientTests()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=nikki_boyd_test;";
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfDetailsAreTheSame_True()
        {
            Client firstClient = new Client(1, "test name", "test phone", "test email");
            Client secondClient = new Client(1, "test name", "test phone", "test email");

            Assert.AreEqual(firstClient, secondClient);
        }

        [TestMethod]
        public void GetAll_DbStartsEmpty_0()
        {
            int result = Client.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            Client newClient = new Client(1, "test name", "test phone", "test email");

            newClient.Save();
            List<Client> result = Client.GetAll();
            List<Client> testClientList = new List<Client> { newClient };

            CollectionAssert.AreEqual(testClientList, result);
        }

        [TestMethod]
        public void Find_FindsClientInDatabase_Client()
        {
            Client newClient = new Client(1, "test name", "test phone", "test email");
            newClient.Save();

            Client foundClient = Client.Find(newClient.Id);

            Assert.AreEqual(newClient, foundClient);
        }

        [TestMethod]
        public void Update_UpdatesClientNameInDatabase_Client()
        {
            Client newClient = new Client(1, "test name", "test phone", "test email", 0);
            newClient.Save();
            newClient.Update(1, "new name", "test phone", "test email", 0);
            Assert.AreEqual(newClient.Name, "new name");
        }
    }
}
