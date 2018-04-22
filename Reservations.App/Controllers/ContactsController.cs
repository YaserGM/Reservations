using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Reservations.App.Models;
using Reservations.Business.Dto;
using Reservations.Business.Services.Contacts;
using Reservations.Core.Entities;
using Reservations.DataAccess.DataContext;

namespace Reservations.App.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService contactService;

        //Quitar
        private EfDbContext db = new EfDbContext();


        public ContactsController(IContactService contactService)
        {
            this.contactService = contactService;
        }


        // GET: Contacts
        public ActionResult Index()
        {
            /*const int MaxCount = 5;
            var skipCount = (page - 1) * MaxCount;
            var response = this.contactService.GetAll(new PaginationResult(skipCount, MaxCount));
            var entityList = response.Items;
            var dtoList = Mapper.Map<List<Contact>, List<ContactDto>>(entityList);
            this.ViewBag.PageCount = (int)Math.Ceiling((double)response.SourceTotal / MaxCount);
            this.ViewBag.CurrentPage = page;
            return this.View(dtoList);*/


            var response = this.contactService.GetAll();
            var dtoList = Mapper.Map<List<Contact>, List<ContactDto>>(response.Items);

            return this.View(dtoList);


            //return View(db.Contacts.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PhoneNumber,Birthdate,Contacttype")]
            Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PhoneNumber,Birthdate,Contacttype")]
            Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetById(int id = 0)
        {
            if (id == 0)
            {
                return null;
            }

            var contact = this.contactService.Get(id);
            var contactDto = Mapper.Map<ContactDto>(contact);


            return this.Json(contact, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByName(string name)
        {
            if (name == null)
            {
                return null;
            }

            var contact = this.contactService.Get(name);
            var contactDto = new ContactDto();
            if (contact != null)
            {
                contactDto = Mapper.Map<ContactDto>(contact);
                contactDto.BirthdateText = contactDto.BirthdateString();
            }

            return this.Json(contactDto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult FindByName(string name)
        {
            if (name == null)
            {
                return null;
            }

            var contact = this.contactService.Get(name);
            var contactDto = new ContactDto();
            if (contact != null)
            {
                contactDto = Mapper.Map<ContactDto>(contact);
                contactDto.BirthdateText = contactDto.BirthdateString();
            }

            ViewBag.Contact = contactDto;

            return RedirectToAction("Edit");
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