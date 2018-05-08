using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      List<Stylist> model = Stylist.GetAll();
      return View("Index", model);
    }

    [HttpGet("/index/new")]
    public ActionResult StylistForm()
    {
      return View();
    }

    [HttpPost("/index/new")]
    public ActionResult AddStylist()
    {
      Stylist newStylist = new Stylist(Request.Form["stylist-name"], Request.Form["start-date"]);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();

      return View("Index", allStylists);
    }

    [HttpGet("/index/{id}")]
    public ActionResult StylistInfo (int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>{};
      // model.Add("Client", null);
      Stylist selectedStylist = Stylist.Find(id);
      model.Add("this-stylist", selectedStylist);
      model.Add("this-startdate", selectedStylist);
      List<Client> stylistClients = Client.GetAllClientsByStylist(selectedStylist.Id);
      model.Add("stylist-clients", stylistClients);
      return View(model);
    }

    [HttpGet("/index/{id}/clients/new")]
    public ActionResult ClientForm(int id)
    {
      Stylist selectedStylist = Stylist.Find(id);
      return View(selectedStylist);
    }

    [HttpPost("/index/{id}/clients/new")]
    public ActionResult AddClient(int id)
    {
      Client newClient = new Client(Request.Form["client-name"], id);
      newClient.Save();

      Dictionary<string, object> model = new Dictionary<string, object>{};
      model.Add("Client", null);
      Stylist selectedStylist = Stylist.Find(id);
      model.Add("this-stylist", selectedStylist);
      model.Add("this-startdate", selectedStylist);
      List<Client> stylistClients = Client.GetAllClientsByStylist(selectedStylist.Id);
      model.Add("stylist-clients", stylistClients);
      return View("StylistInfo", model);
    }

    [HttpGet("/index/{id}/clients/{clientId}")]
    public ActionResult ClientDetails(int id, int clientId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>{};
      List<Client> allClients = Client.GetAllClientsByStylist(id);
      Stylist selectedStylist = Stylist.Find(id);
      Client selectedClient = Client.Find(clientId);
      model.Add("stylist-clients", allClients);
      model.Add("this-stylist", selectedStylist);
      model.Add("this-startdate", selectedStylist);
      // model.Add("Client", selectedClient);
      return View("StylistInfo", model);
    }

    [HttpPost("/index/{id}/delete")]
    public ActionResult DeleteStylist(int id)
    {
      Stylist selectedStylist = Stylist.Find(id);
      selectedStylist.Delete();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }
  }
}
