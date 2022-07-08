using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using TFLWebApp.Models;
using System.Net;
using TFLWebApp.DAL;

namespace TFLWebApp.Controllers
{
    public class PlayersController : Controller
    {
        // GET: Players
        DbOrmContext entities = new DbOrmContext();
        public ActionResult Index()
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;

            List<players> Players = entities.Players.ToList();
            this.ViewBag.Players = Players;
            return View();
        }

        public ActionResult List()
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;

            List<players> Players = entities.Players.ToList();
            // this.ViewBag.Players = Players;
            return Json(Players, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PlayerDetails(int id)
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;
            var Player = entities.Players.SingleOrDefault(P => P.id == id);
            return Json(Player, JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult Details(int id)
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;

            var Player = entities.Players.SingleOrDefault(P => P.id == id);
            if(Player!=null)
            {
                this.ViewData["Player"] = Player;
            }
            return View();
        }
        [HttpGet]
        public ActionResult Insert()
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;

            var player = new players();
            return View(player);
        }
        public ActionResult Insert(players player)
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;

            if(ModelState.IsValid)
            {
                entities.Players.Add(player);
                entities.SaveChanges();
                return RedirectToAction("index");
            }
            else
            {
                return View(player);
            }
            
        }
        public ActionResult Update(int? id)
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var player = entities.Players.SingleOrDefault(P => P.id == id);
            if(player==null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        [HttpPost]
        public ActionResult Update(players player)
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;
            if(ModelState.IsValid)
            {
                entities.Entry(player).State = System.Data.Entity.EntityState.Modified;
                entities.SaveChanges();
                return RedirectToAction("index");
            }
            return View();
           
        }
        public ActionResult Delete(int id)
        {
            int VisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = VisitCount + 1;

            var player = entities.Players.SingleOrDefault(P=>P.id == id);
            entities.Players.Remove(player ?? throw new InvalidOperationException());
            entities.SaveChanges();
            return RedirectToAction("Index");

            /*
             int currentVisitCount = int.Parse(this.HttpContext.Session["visits"].ToString());
            this.HttpContext.Session["visits"] = currentVisitCount + 1;

            var product = entities.Products.SingleOrDefault(p => p.Id == id);
            entities.Products.Remove(product ?? throw new InvalidOperationException());
            entities.SaveChanges();   // to reflect
            return RedirectToAction("Index");
            */
        }
    }
}