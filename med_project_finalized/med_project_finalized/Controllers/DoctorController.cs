using med_project_finalized.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace med_project_finalized.Controllers
{
    public class DoctorController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
      
        ApplicationDbContext context;


        public DoctorController()
        {
            context = new ApplicationDbContext();
        }


      


        public ActionResult Index()
        {
            var allusers = db.Users.ToList();
            ViewBag.Pacient = "Pacient";
            var users = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains("7c4f93b4-e82e-4736-a4b2-7a76116da262")).ToList();
            var userVM = users.Select(user => new UserViewModel { Username = user.Email, Roles = string.Join(",", user.Roles.Select(role => role.RoleId)) }).ToList();
            var admins = allusers.Where(x => x.Roles.Select(role => role.RoleId).Contains("df265882-060a-45a9-a3db-bd19622b8826")).ToList();
            var adminsVM = admins.Select(user => new UserViewModel { Username = user.Email, Roles = string.Join(",", user.Roles.Select(role => role.RoleId)) }).ToList();
            var model = new GroupedUserViewModel { Pacient = userVM, Doctor = adminsVM };
            //

            //
            ViewBag.Pacients = new SelectList(users.ToList(), "Email", "Email");
            //
            return View(model);
        }
        //  Doctor/EditPacientInfo/test_pacient@gmail.com
        //EDITNG
        //
        //
        //
        //
        [Route("Doctor/EditPacientInfo/{name}")]
        public ActionResult EditPacientInfo(string name)
        {
            string diagnosis = name;
            // Fetch the userprofile
            Diagnoses diagnose = db.Diagnoses.FirstOrDefault(u => u.Diagnosis.Equals(diagnosis));
            // Construct the viewmodel
            Diagnoses diagnoses = new Diagnoses()
            {
                Diagnosis = diagnose.Diagnosis,
                Details = diagnose.Details,
                IsCured = diagnose.IsCured
            };
            return View(diagnose);
        }
        [HttpPost]
        public ActionResult EditPacientInfo([Bind(Include = "Id,Diagnosis,Details,TimeAdded,IsCured,UserId")] Diagnoses diagnoses)
        {
            if (ModelState.IsValid)
            {
                
                string diagnosis = diagnoses.Diagnosis;
                Diagnoses diagnose = db.Diagnoses.FirstOrDefault(u => u.Diagnosis.Equals(diagnosis));
                //
                diagnose.Diagnosis = diagnoses.Diagnosis;
                diagnose.Details = diagnoses.Details;
                diagnose.IsCured = diagnoses.IsCured;
                db.Entry(diagnose).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(diagnoses);
        }
        //SEARCHING_PACIENT
        //
        //
        //
        //
        [HttpPost]
        public ActionResult PacientSearch(string name)
        {
            var all_pacients = db.Users.Where(a => a.Email.Contains(name)).ToList();
            return PartialView(all_pacients);
        }
        //GETTING_DETATILS
        //
        //
        //
        //
        [Route("Doctor/Details/{name}")]
        public ActionResult Details(string name)
        {
            string username = name;
            // Fetch the userprofile
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            //var diagnoses = db.Diagnoses.Where(a=> a.Userid.Contains(user.Id)).ToList();
            var diagnoses = db.Diagnoses.Where(a => a.Userid.Contains(user.Id)).ToList();
            ViewBag.Email = user.Email;
            // Construct the viewmodel
            if (diagnoses == null)
            {
                return HttpNotFound();
            }
            return PartialView(diagnoses);

        }
        //CREATING_DIAGNOSES
        //
        //
        //
        //
        [Route("Doctor/AddDiagnosis/{name}")]
        public ActionResult AddDiagnosis(string name)
        {
            string username = name;
            // Fetch the userprofile
            ApplicationUser user = db.Users.FirstOrDefault(u => u.UserName.Equals(username));
            ViewBag.UserId = user.Id;
            // Construct the viewmodel
            Diagnoses diagnose = new Diagnoses()
            {
                User = user,
                Userid = user.Id,
                TimeAdded = DateTime.Now
            };
            ViewBag.UserName = user.Email;
            return PartialView(diagnose);
        }
        [HttpPost]
        public ActionResult AddDiagnosis([Bind(Include = "Id,Diagnosis,Details,TimeAdded,IsCured,UserId")] Diagnoses diagnoses)
        {
            if (ModelState.IsValid)
            {
                db.Diagnoses.Add(diagnoses);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diagnoses);
        }
        /////DELETE_DIAGNOSIS
        //
        //
        //
        [HttpPost]
        [Route("Doctor/EditPacientInfo/{id}")]
        public JsonResult Delete(int id)
        {
           
            Diagnoses diagnose = (from item in db.Diagnoses
                                  where item.Id == id
                                  select item).SingleOrDefault();
            db.Diagnoses.Remove(diagnose);
            db.SaveChanges();
            return Json("Record deleted successfully!");
        }
       
    }
}