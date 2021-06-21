using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using PierresSweetAndSavoryTreats.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace PierresSweetAndSavoryTreats.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    private readonly PierresSweetAndSavoryTreatsContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, PierresSweetAndSavoryTreatsContext db)
    {
      _userManager = userManager;
      _db = db;
    }
    
    [AllowAnonymous]
    public ActionResult Index()
    {
      var treats = _db.Treats.ToList();
      return View (treats);
    }  

    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Description");
      return View();
    } 

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        treat.User = currentUser;
        _db.Treats.Add(treat);
        _db.SaveChanges();
        if (FlavorId != 0)
        {
            _db.FlavorTreat.Add(new FlavorTreat() { FlavorId = FlavorId, TreatId = treat.TreatId });
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  }
}