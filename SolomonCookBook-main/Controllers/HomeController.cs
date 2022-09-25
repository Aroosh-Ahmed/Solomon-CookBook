﻿using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using SolomonCookBook.Models;
using SolomonCookBook.Data;
using Microsoft.EntityFrameworkCore;

namespace SolomonCookBook.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IWebHostEnvironment env;
    private TheDbContext db;

    public HomeController(ILogger<HomeController> logger, TheDbContext context, IWebHostEnvironment environment)
    {
        _logger = logger;
        db = context;
        env = environment;
    }

    public ViewResult Index()
    {
        var recepiedata = db.Recepies.ToList();
        return View(recepiedata);
    }


    // SignIn
    [HttpGet]
    public ViewResult SignIn()
    {
        return View();
    }
    
    [HttpPost]
    public ActionResult SignIn(User usr)
    {
        var obj = db.Users.Single(user => user.Email == usr.Email && user.Password == usr.Password); 
        if (obj != null)
        {
            return RedirectToAction("MainPage");
        }
        else{
            return View();
        }
        
    }
    [HttpPost]
    public ActionResult AdminSignin(Admin admin)
    {
        var obj = db.Admins.Single(a => a.Name == admin.Name && a.Password == admin.Password);
        if (obj != null)
        {
            return RedirectToAction("StaffRecepiePage");
        }
        else
        {
            return View();
        }

    }

    [HttpGet]
    public ViewResult Signup()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Signup(User usr)
    {
        var data = usr;
        db.Users.Add(usr);
        db.SaveChanges();
        var msg = "User Created Succeddfully !";
        return RedirectToAction("SignIn", "Home", new { message = msg });
    }

    public ActionResult MainPage(string category)
    {
        var recepiedata = db.Recepies.ToList();
        var categories = recepiedata.Select(a => a.Category).ToList();
        if (!String.IsNullOrEmpty(category))
        {
            if(category == "clear")
            {
                recepiedata = recepiedata;
            }
            else
            {
                recepiedata = recepiedata.Where(s => s.Category.Contains(category)).ToList();
            }
            
        }
        RecepieswithCategoryListVM data = new RecepieswithCategoryListVM();
        data.recepies = recepiedata;
        data.categories = categories;
        return View(data);
    }

    public ViewResult test()
    {
        var recepiedata = db.Recepies.ToList();
        var categories = recepiedata.Select(a => a.Category).ToList();
        RecepieswithCategoryListVM data = new RecepieswithCategoryListVM();
        data.recepies = recepiedata;
        data.categories = categories;
        return View(data);
    }


    //// Browse By Category
    //[HttpGet]
    //public async  Task<IActionResult> MainPage(string SearchTerm)
    //{
    //    var searchquery = from r in db.Recepies select r;
    //    if (!string.IsNullOrEmpty(SearchTerm))
    //    {
    //        searchquery = searchquery.Where(r => r.Category.ToLower().Contains(SearchTerm) || r.Category.ToUpper().Contains(SearchTerm) ||  r.Recepie_Name.Contains(SearchTerm));
    //    }
    //   return View(await searchquery.AsNoTracking().ToListAsync());
    //}

    //public IActionResult MainPage(string category)
    //{
    //    var alldata = db.Recepies.ToList();
    //    var recepiedata = alldata.Where(r => r.Category.Contains("HEALTHY") || r.Category.ToLower().Contains("healthy")).ToList();
    //    var categories = recepiedata.Select(a => a.Category).ToList();
    //    RecepieswithCategoryListVM data = new RecepieswithCategoryListVM();
    //    data.recepies = recepiedata;
    //    data.categories = categories;
    //    return View(data);
    //    //var searchquery = from r in db.Recepies select r;
    //    //if (!string.IsNullOrEmpty(category))
    //    //{
    //    //    searchquery = searchquery.Where(r => r.Category.ToLower().Contains(category));
    //    //}
    //    //return View(await searchquery.AsNoTracking().ToListAsync());
    //}

    public ViewResult Recepies()
    {
        var data = db.Recepies.ToList();
        return View(data);
    }

    [HttpPost] 
    public IActionResult CommentonRecepie (Recepie_Comments newcomment)
    {
        var data = newcomment;
        data.User_ID = null;
        db.Recepie_Comments.Add(newcomment);
        db.SaveChanges();
        return Ok("Commented Successfully");
    }

    [HttpPost]
    public IActionResult LikeRecepie(Recepie_Comments newcomment)
    {
        var data = newcomment;
        data.User_ID = null;
        db.Recepie_Comments.Add(newcomment);
        db.SaveChanges();
        return Ok("Commented Successfully");
    }




    [HttpGet]
    public ViewResult StaffRecepiePage()
    {
        var data = db.Recepies.ToList();
        return View(data);
    }
    [HttpGet]
    public ViewResult StaffRecepieDetails(int id)
    {
        ViewBag.Id = id;
        var recepie = db.Recepies.Where(x => x.Recepie_ID == id).FirstOrDefault();
        var comments = db.Recepie_Comments.Where(c => c.Recepie_ID == id);
        ViewBag.Comments = comments;
        return View(recepie);
    }
    public IActionResult ApproveRecepie(int id)
    {
        var recepie = db.Recepies.Where(x => x.Recepie_ID == id).FirstOrDefault();
        recepie.status = "approved";
        db.Entry(recepie).State = EntityState.Modified ;
        db.SaveChanges();
        return RedirectToAction("StaffRecepiePage");

    }

    [HttpGet]
    public ViewResult StaffRecepieEditingPage(int id)
    {
        var recepie = db.Recepies.Where(x => x.Recepie_ID == id).FirstOrDefault();
        ViewBag.Recepie = recepie;
        return View();
    }

    [HttpPost]
    public IActionResult StaffRecepieEditingPage(RecepieImg model)
    {
        var path = env.WebRootPath;
        var filepath = "Images/" + model.MyImage.Img.FileName;
        var fullpath = Path.Combine(path, filepath);
        UploadImage(model.MyImage.Img, fullpath);
        string imgPath = filepath;
        var data = new Recepies
        {
            Recepie_Name = model.MyRecepie.Recepie_Name,
            Category = model.MyRecepie.Category,
            video_url = model.MyRecepie.video_url,
            image_url = imgPath,
            Ingredients = model.MyRecepie.Ingredients,
            Directions = model.MyRecepie.Directions,
            //Comments = "No Comments for Now"

        };

        db.Recepies.Add(data);
        db.SaveChanges();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public ViewResult CreateRecepie()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CreateRecepie(RecepieImg model)
    {
        var path =  env.WebRootPath;
        var filepath = "Images/"+ model.MyImage.Img.FileName;
        var fullpath = Path.Combine(path,filepath);
        UploadImage(model.MyImage.Img,fullpath);
        string imgPath = filepath; 
        var data = new Recepies{
            Recepie_Name = model.MyRecepie.Recepie_Name,
            Category = model.MyRecepie.Category,
            video_url = model.MyRecepie.video_url,
            image_url = imgPath,
            Ingredients = model.MyRecepie.Ingredients,
            Directions = model.MyRecepie.Directions,
            //Comments = "No Comments for Now"

        };

        db.Recepies.Add(data);
        db.SaveChanges();
        return RedirectToAction("MainPage");
    }

    public void UploadImage(IFormFile file, string path)
    {
        FileStream stream =  new FileStream(path, FileMode.Create);
        file.CopyTo(stream);
    }
    [HttpGet]
    public ViewResult recepieDetails(int id)
    {
        ViewBag.Id = id;
        var recepie = db.Recepies.Where(x => x.Recepie_ID == id).FirstOrDefault();
        var comments = db.Recepie_Comments.Where(c => c.Recepie_ID == id);
        ViewBag.Comments = comments;
        return View(recepie);
    }

    public ViewResult Shop()
    {
        return View();
    }

    public ViewResult About()
    {
        return View();
    }

  


}
