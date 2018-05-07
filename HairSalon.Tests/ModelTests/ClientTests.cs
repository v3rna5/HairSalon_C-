using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;

namespace HairSalon.Models.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public ClientTests()
    {
     DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon_test;";
    }

    public void Dispose()
    {
      Client.ClearAll();
    }

    [TestMethod]
    public void GetAll_ClientEmptyAtFirst_0()
    {
      int result = Client.GetAll().Count;
      Assert.AreEqual(0, result);
    }


      [TestMethod]
      public void Save_SavesClientToDatabase_ClientList()
      {
        Client testClient = new Client("Verna Santos", 0);
        testClient.Save();

        List<Client> result = Client.GetAll();
        List<Client> testList = new List<Client>{testClient};

        CollectionAssert.AreEqual(testList, result);
      }


    [TestMethod]
    public void StylistAreTheSame()
    {
      // Arrange
      Stylist firstClient = new Stylist("Nick");
      Stylist secondClient = new Stylist("Nick");

      Assert.AreEqual(firstClient, secondClient);

    }
  }
}
