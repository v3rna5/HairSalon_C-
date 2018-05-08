using System;
using System.Collections.Generic;
using HairSalon;
using MySql.Data.MySqlClient;


namespace HairSalon.Models
{
  public class Stylist
  {
    public string Name;
    public int Id;
    public string StartDate;

    public Stylist (string name, string startDate, int id = 0)
    {
      Name = name;
      Id = id;
      StartDate = startDate;

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
    public string GetStartDate()
    {
      return StartDate;
    }
    public void SetStartDate(string NewStartDate)
    {
      StartDate = NewStartDate;
    }


    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Stylist> GetAll()
    {
      List<Stylist> allStylists = new List<Stylist>{};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        string startDate = rdr.GetString(2);

        Stylist newStylist = new Stylist(stylistName, startDate, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }

    // public override bool Equals(System.Object otherStylist)
    // {
    //   if (!(otherStylist is Stylist))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Stylist newStylist = (Stylist) otherStylist;
    //     bool idEquality = (this.Id == newStylist.Id);
    //     bool nameEquality = (this.Name == newStylist.Name);
    //
    //     return (idEquality && nameEquality);
    //   }
    // }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists (name, startdate) VALUES (@StylistName, @StylistStartDate);";


      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@StylistName";
      name.Value = this.Name;
      cmd.Parameters.Add(name);

      MySqlParameter startDate = new MySqlParameter();
      startDate.ParameterName = "@StylistStartDate";
      startDate.Value = this.StartDate;
      cmd.Parameters.Add(startDate);


      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Stylist Find(int searchId)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = searchId;
      cmd.Parameters.Add(thisId);

      int stylistId = 0;
      string stylistName = "";
      string startDate = "";

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
        startDate = rdr.GetString(2);

      }

      Stylist foundStylist = new Stylist(stylistName, startDate, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }



    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists where id = @searchId;DELETE FROM clients WHERE stylist_id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = this.Id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();
      conn.Close();

      if (conn != null)
      {
        conn.Dispose();
      }
    }
    // public override int GetHashCode()
    // {
    //   return this.Id.GetHashCode();
    // }
  }
}
