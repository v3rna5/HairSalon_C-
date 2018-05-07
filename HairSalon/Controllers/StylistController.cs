// using System.Collections.Generic;
// using System;
// using Microsoft.AspNetCore.Mvc;
// using HairSalon.Models;
//
// namespace HairSalon.Controllers
// {
//   public class StylistController : Controller
//   {
//     [HttpGet("/stylist")]
//     public ActionResult Index()
//     {
//       List<Stylist> allStylist = Stylist.GetAll();
//       return View(allStylist);
//     }
//
//     [HttpGet("/stylist/new")]
//     public ActionResult CreateForm()
//     {
//       return View();
//     }
//
//     [HttpPost("/stylist")]
//     public ActionResult Create()
//     {
//       Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
//       List<Stylist> allStylist = Stylist.GetAll();
//       return View("Index", allStylist);
//     }
//
//     [HttpGet("/stylist/{id}")]
//     public ActionResult Details(int id)
//     {
//         Dictionary<string, object> model = new Dictionary<string, object>();
//         Stylist selectedStylist = Stylist.Find(id);
//         List<Client> stylistClients = selectedStylist.GetClients();
//         model.Add("stylist", selectedStylist);
//         model.Add("clients", stylistClients);
//         return View(model);
//     }
//
//     [HttpPost("/client")]
//     public ActionResult CreateClient()
//     {
//       Dictionary<string, object> model = new Dictionary<string, object>();
//       Stylist foundStylist = Stylist.Find(Int32.Parse(Request.Form["stylist-id"]));
//       string clientDescription = Request.Form["client-description"];
//       Client newClient = new Client(clientDescription);
//       foundStylist.AddClient(newClient);
//       List<Client> stylistClients = foundStylist.GetClients();
//       model.Add("clients", stylistClients);
//       model.Add("stylist", foundStylist);
//       return View("Details", model);
//     }
//   }
// }
