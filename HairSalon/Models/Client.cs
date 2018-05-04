using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private string _description;
    private int _id;
    // private static List<Client> _instances = new List<Client>{};
    public Client(string description, int id = 0)
    // public Client (string description)
    {
      _description = description;
      _id = id;
      // _instances.Add(this);
      // _id = _instances.Count;
    }
    public string GetDescription()
    {
      return _description;
    }

    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }


    public static List<Client> GetAll()
    {
      // return _instances;
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM client;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientDescription = rdr.GetString(1);
        Client newClient = new Client(clientDescription, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }
    // public void Save()
    // {
    //   _instances.Add(this);
    // }
  
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `client` (`description`);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@ClientDescription";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    //     public static void ClearAll()
    // {
    //   _instances.Clear();
    // }
    // public static Client Find(int searchId)
    // {
    //   return _instances[searchId-1];
    // }
    public static Client Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `client` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      int clientId = 0;
      string clientDescription = "";

      while (rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientDescription = rdr.GetString(1);
      }
      Client foundClient= new Client(clientDescription);
      foundClient.SetId(clientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }
  }
}
