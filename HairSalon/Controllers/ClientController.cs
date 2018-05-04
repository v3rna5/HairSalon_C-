using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class ClientController : Controller
    {
      [HttpGet("/clients")]
      public ActionResult Index()
      {
          List<Client> allClients = Client.GetAll();
          return View(allClients);
      }

      [HttpGet("/clients/new")]
      public ActionResult CreateForm()
      {
          return View();
      }
      [HttpGet("/stylist/{stylistId}/clients/new")]
      public ActionResult CreateForm(int stylistId)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Stylist stylist = Stylist.Find(stylistId);
        return View(stylist);
      }
      [HttpGet("/stylist/{stylistId}/clients/{clientId}")]
       public ActionResult Details(int stylistId, int clientId)
       {
           Client client = Client.Find(clientId);
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist stylist = Stylist.Find(stylistId);
          model.Add("client", client);
          model.Add("stylist", stylist);
          return View(client);
       }

    }
}
