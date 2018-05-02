using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Reservations.App.Helper;
using Reservations.App.Models;
using Reservations.Business.Dto;
using Reservations.Business.Services.Contacts;
using Reservations.Business.Services.ContactTypes;
using Reservations.Core;
using Reservations.Core.Entities;
using Reservations.Core.Enums;
using Reservations.DataAccess.DataContext;

namespace Reservations.App.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IContactTypeService _contactTypeService;

        public ContactsController(IContactService contactService, IContactTypeService contactTypeService)
        {
            this._contactService = contactService;
            this._contactTypeService = contactTypeService;
        }


        // GET: Contacts
        public ActionResult Index(int page = 1)
        {
            var contactDetoList = ContactsDetoList(page);
            return this.View(contactDetoList);
        }

        public ActionResult Create()
        {
            var list = _contactTypeService.GetAll().Items;
            this.ViewBag.ContactTypeId = new SelectList(list, "Id", "Description");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,PhoneNumber,Birthdate,ContactTypeId")]
            ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                var findContact = _contactService.Get(contact.Name);
                if (findContact == null)
                {
                    _contactService.Add(Mapper.Map<Contact>(contact));
                    return RedirectToAction("Index");
                }
                else
                {
                    this.ModelState.AddModelError("Name", Localization.ContactExist);
                }
            }

            

            contact.Name = null;
            var list = _contactTypeService.GetAll().Items;
            this.ViewBag.ContactTypeId = new SelectList(list, "Id", "Description");
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int id)
        {
            var contact = _contactService.Get(id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            var list = _contactTypeService.GetAll().Items;
            this.ViewBag.ContactTypeId = new SelectList(list, "Id", "Description", id);
            return View(Mapper.Map<ContactViewModel>(contact));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,PhoneNumber,Birthdate,ContactTypeId")]
            ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                _contactService.Update(Mapper.Map<Contact>(contact));
                return RedirectToAction("Index");
            }

            var list = _contactTypeService.GetAll().Items;
            this.ViewBag.ContactTypeId = new SelectList(list, "Id", "Description", contact.Id);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int id)
        {
            var contact = _contactService.Get(id);
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
            var contact = _contactService.Get(id);
            if (contact != null && contact.Id > 0)
            {
                _contactService.Delete(id);
            }

            return RedirectToAction("Index");
        }

        public JsonResult GetById(int id = 0)
        {
            if (id == 0)
            {
                return null;
            }

            var contact = this._contactService.Get(id);
            var contactDto = Mapper.Map<ContactViewModel>(contact);


            return this.Json(contact, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetByName(string name)
        {
            if (name == null)
            {
                return null;
            }

            var contact = this._contactService.Get(name);
            if (contact != null)
            {
                return JsonHelper.ToJsonResult(Mapper.Map<ContactViewModel>(contact));
            }

            return null;
        }

        [HttpPost]
        public ActionResult FindByName(string name)
        {
            if (name == null)
            {
                return null;
            }

            var contact = this._contactService.Get(name);
            var contactDto = new ContactViewModel();
            if (contact != null)
            {
                contactDto = Mapper.Map<ContactViewModel>(contact);
            }

            ViewBag.Contact = contactDto;

            return RedirectToAction("Edit");
        }

        private List<Contact> ContactsDetoList(int page)
        {
            const int maxPage = 5;
            var skipCount = (page - 1) * maxPage;

            var response =
                this._contactService.GetAll(new PageResult(skipCount, maxPage));


            this.ViewBag.PageCount = (int) Math.Ceiling((double) response.SourceTotal / maxPage);
            this.ViewBag.page = page;
            return response.Items;
        }
    }
}