using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PierresSweetAndSavoryTreats.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PierresSweetAndSavoryTreats.Controllers
{
    public class HomeController : Controller
    {
        private readonly PierresSweetAndSavoryTreatsContext _db;
        public HomeController(PierresSweetAndSavoryTreatsContext db)
        {
        _db = db;
        }
        [HttpGet("/")]
        public ActionResult Index()
        {
        List<Flavor> flavors = _db.Flavors.ToList();
        List<Treat> treats = _db.Treats.ToList();

        ViewBag.Treats = treats;
        return View(flavors);
        }
    }
}
