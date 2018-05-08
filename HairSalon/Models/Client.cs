using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Client
  {
    public string Name;
    public int Id;
    public int StylistId;
  

    public Client(string name, int stylistId, int id = 0)
    {
      Name = name;
      Id = id;
      StylistId = stylistId;

    }
    public string GetName()
    {
      return Name;
    }
    public void SetName(string NewName)
    {
      Name = NewName;
    }
    public int GetId()
    {
      return Id;
    }
    public void SetId(int NewId)
    {
      Id = NewId;
    }
    public int GetStylistId()
    {
      return StylistId;
    }
    public void SetStylistId(int NewStylistId)
    {
      StylistId = NewStylistId;
    }



    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM clients;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int stylistId = rdr.GetInt32(2);


        Client newClient = new Client(clientName, stylistId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public static List<Client> GetAllClientsByStylist(int inputId)
    {
      List<Client> stylistClients = new List<Client>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId;";

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@stylistId";
      stylistId.Value = inputId;
      cmd.Parameters.Add(stylistId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int returnId = rdr.GetInt32(0);
        string returnName = rdr.GetString(1);
        int returnStylistId = rdr.GetInt32(2);

        Client returnClient = new Client(returnName, returnStylistId, returnId);
        stylistClients.Add(returnClient);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return stylistClients;
    }

    // public override bool Equals(System.Object otherClient)
    // {
    //   if (!(otherClient is Client))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Client newClient = (Client) otherClient;
    //     bool idEquality = (this.Id == newClient.Id);
    //     bool nameEquality = (this.Name == newClient.Name);
    //     bool stylistIdEquality = (this.StylistId == newClient.StylistId);
    //     return (idEquality && nameEquality && stylistIdEquality);
    //   }
    // }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO clients (name, stylist_id) VALUES (@ClientName, @ClientStylistId);";
      //name
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@ClientName";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@ClientStylistId";
      stylistId.Value = this.StylistId;
      cmd.Parameters.Add(stylistId);

      cmd.ExecuteNonQuery();
      Id = (int)cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Client Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;

      cmd.Parameters.Add(thisId);

      int clientId = 0;
      string clientName = "";

      int clientStylistId = 0;

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        clientId = rdr.GetInt32(0);
        clientName = rdr.GetString(1);
        clientStylistId = rdr.GetInt32(2);

      }
      Client foundClient = new Client(clientName, clientStylistId, clientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundClient;
    }
  }
}
