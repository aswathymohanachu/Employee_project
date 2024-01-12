using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Employee_project;
using PagedList;

namespace Employee_project.Controllers
{
    public class EmployeeController : Controller
    {
        private EMPLOYEEEntities db = new EMPLOYEEEntities();

        // GET: Employee
        public ActionResult Index(string sort_order, string key_search, string filter_value, int? Page_No)
        {

            ViewBag.Name = string.IsNullOrEmpty(sort_order) ? "Name" : "";
            ViewBag.Designation = string.IsNullOrEmpty(sort_order) ? "Designation" : "";
            ViewBag.Address = string.IsNullOrEmpty(sort_order) ? "Address" : "";

            if (key_search != null)
            {
                Page_No = 1;
            }
            else
            {
                key_search = filter_value;
            }


            var emplo = from Employe_tab in db.Employe_tab select Employe_tab;

            if (!String.IsNullOrEmpty(key_search))
            {

                //filtering 
                emplo = emplo.Where(em => em.Name.ToUpper().Contains(key_search.ToUpper()) || em.Designation.ToString().Contains(key_search.ToUpper()));
            }


            switch (sort_order)
            {
                case "Name":
                    emplo = emplo.OrderBy(pro => pro.Name);
                    break;

                case "Designation":
                    emplo = emplo.OrderBy(pro => pro.Designation);
                    break;

                case "Address":
                    emplo = emplo.OrderBy(pro => pro.Address);
                    break;
                default:
                    emplo = emplo.OrderBy(pro => pro.Name);
                    break;


            }



            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);
            return View(emplo.ToPagedList(No_Of_Page, Size_Of_Page));
            //   return View(emplo.ToList());



            //return View(db.Employe_tab.ToList());
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe_tab employe_tab = db.Employe_tab.Find(id);
            if (employe_tab == null)
            {
                return HttpNotFound();
            }
            return View(employe_tab);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Designation,Address")] Employe_tab employe_tab)
        {
            if (ModelState.IsValid)
            {
                db.Employe_tab.Add(employe_tab);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employe_tab);
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe_tab employe_tab = db.Employe_tab.Find(id);
            if (employe_tab == null)
            {
                return HttpNotFound();
            }
            return View(employe_tab);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Designation,Address")] Employe_tab employe_tab)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employe_tab).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employe_tab);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employe_tab employe_tab = db.Employe_tab.Find(id);
            if (employe_tab == null)
            {
                return HttpNotFound();
            }
            return View(employe_tab);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employe_tab employe_tab = db.Employe_tab.Find(id);
            db.Employe_tab.Remove(employe_tab);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
