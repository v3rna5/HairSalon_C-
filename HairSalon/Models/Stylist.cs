using System.Collections.Generic;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    private string _description;
    private int _id;
    private static List<Client> _instances = new List<Client>{};

    public Client (string description)
    {
      _description = description;
      _instances.Add(this);
      _id = _instances.Count;
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
    public static List<Client> GetAll()
    {
      return _instances;
    }
    public void Save()
    {
      _instances.Add(this);
    }
        public static void ClearAll()
    {
      _instances.Clear();
    }
    public static Client Find(int searchId)
    {
      return _instances[searchId-1];
    }
  }
}