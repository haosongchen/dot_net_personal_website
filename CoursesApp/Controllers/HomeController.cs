/////////////////////////////////////////////////////////////////
// HomeController.cs - Controller for CourseApplication Demo   //
//                                                             //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019     //
/////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using CoursesApp.Models;
using CoursesApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace CoursesApp.Controllers
{
  public class HomeController : Controller
  {
    private readonly ApplicationDbContext context_;
    private const string sessionId_ = "SessionId";

    public HomeController(ApplicationDbContext context)
    {
      context_ = context;
    }



        public IActionResult Index()
        {
            return View(context_.Bios.ToList<MyBio>());
        }

        public IActionResult ErrorHandle()
        {
            return View();
        }

        public IActionResult UploadErrorHandle()
        {
            return View();
        }

        public IActionResult HandleAll()
        {

            return RedirectToAction("ErrorHandle");
        }



        public IActionResult project1()
        {
            return View();
        }

        public IActionResult project2()
        {
            return View();
        }

        public IActionResult viewResume()
        {

            return View();
        }
        //----< show list of lectures, ordered by Title >------------

        public IActionResult CommentPage()
        {

            return View(context_.Comments.ToList<Comment>());
        }

        [HttpGet]
        public IActionResult CreateBio(int id)
        {
            var model = new MyBio();
            return View(model);
        }

        //----< posts back new about paragraph >---------------------

        [HttpPost]
        public IActionResult CreateBio(int id, MyBio abt)
        {
            context_.Bios.Add(abt);
            context_.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateComment(int id)
        {
            var model = new Comment();
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateComment(int id, Comment cmt)
        {
            try
            {
                context_.Comments.Add(cmt);
                context_.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("CommentPage");

            }
            return RedirectToAction("CommentPage");
        }
        //----< gets form to edit a specific bio via id >---------

        [HttpGet]
        public IActionResult EditBio(int? id)
        {
            if (id == null)
            {
                return StatusCode(Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest);
            }
           MyBio abt = context_.Bios.Find(id);
            if (abt == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(abt);
        }

        //----< posts back edited results for specific bio >------

        [HttpPost]
        public IActionResult EditBio(int? id, MyBio abt)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var about = context_.Bios.Find(id);
            if (about != null)
            {
                about.MyBioId= abt.MyBioId;
                about.Bio= abt.Bio;
                try
                {
                    context_.SaveChanges();
                }
                catch (Exception)
                {
                    // do nothing for now
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            try
            {
                var comment = context_.Comments.Find(id);
                if (comment != null)
                {
                    context_.Remove(comment);
                    context_.SaveChanges();
                }
            }
            catch (Exception)
            {
                // nothing for now
            }
            return RedirectToAction("CommentPage");
        }



        //----< wizard generated actions >---------------------------

        public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}


