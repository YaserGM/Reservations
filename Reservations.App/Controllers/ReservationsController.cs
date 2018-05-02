using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Newtonsoft.Json;
using Reservations.App.Helper;
using Reservations.App.Models;
using Reservations.Business.Dto;
using Reservations.Business.Services.Contacts;
using Reservations.Business.Services.ContactTypes;
using Reservations.Business.Services.Reservations;
using Reservations.Core.Entities;
using Reservations.Core.Enums;
using Reservations.DataAccess.DataContext;

namespace Reservations.App.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IContactService _contactService;
        private readonly IContactTypeService _contactTypeService;


        public ReservationsController(IReservationService reservationService, IContactService contactService,
            IContactTypeService contactTypeService)
        {
            this._reservationService = reservationService;
            this._contactService = contactService;
            this._contactTypeService = contactTypeService;
        }

        // GET: Reservations
        public ActionResult Index(int page = 1, string sort = "1")
        {
            var reservationDetoList = ReservationDetoList(page, sort);

            return this.View(reservationDetoList);
        }


        [HttpPost]
        public ActionResult Index(string sort, int page = 1)
        {
            var reservationDetoList = ReservationDetoList(page, sort);

            return this.View(reservationDetoList);
        }


        public JsonResult FilterIndex(string sort, int page = 1)
        {
            var reservationDetoList = ReservationDetoList(page, sort);
            return JsonHelper.ToJsonResult(reservationDetoList);
        }


        // GET: Reservations/Create
        public ActionResult Create(int? page, string sort)
        {
            this.ViewBag.page = page ?? 1;
            this.ViewBag.sort = sort;

            var list = _contactTypeService.GetAll().Items;
            this.ViewBag.ContactTypeId = new SelectList(list, "Id", "Description");

            var reservationDto = new ReservationViewModel();
            return View(reservationDto);
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(ReservationViewModel reservationViewModel, int page, string sort)
        {
            this.ViewBag.page = page;
            this.ViewBag.sort = sort;

            if (ModelState.IsValid)
            {
                try
                {
                    
                    var contact = Mapper.Map<Contact>(reservationViewModel.Contact);
                    if (contact.Id > 0)
                    {
                        this._contactService.Update(contact);
                    }
                    else
                    {
                        contact = this._contactService.Add(contact);
                        reservationViewModel.Contact = Mapper.Map<ContactViewModel>(contact);
                    }
                    reservationViewModel.ContactId = contact.Id;
                    var reservation = Mapper.Map<Reservation>(reservationViewModel);
                    this._reservationService.Add(reservation);
                    return RedirectToAction("Index", new {page, sort});
                }
                catch (Exception)
                {
                }
            }

            var list = _contactTypeService.GetAll().Items;
            this.ViewBag.ContactTypeId = new SelectList(list, "Id", "Description");
            return View(reservationViewModel);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? page, string sort, int id = 0)
        {
            this.ViewBag.page = page ?? 1;
            this.ViewBag.sort = sort;
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var list = _contactTypeService.GetAll().Items;
            this.ViewBag.ContactTypeId = new SelectList(list, "Id", "Description", id);
            var res = this._reservationService.Get(id);
            var reservationDto = Mapper.Map<ReservationViewModel>(res);
            return this.View(reservationDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservationViewModel reservationViewModel, int page, string sort)
        {
            this.ViewBag.page = page;
            this.ViewBag.sort = sort;
            if (this.ModelState.IsValid)
            {
                try
                {
                    var contact = Mapper.Map<Contact>(reservationViewModel.Contact);
                    if (contact.Id > 0)
                    {
                        this._contactService.Update(contact);
                    }
                    else
                    {
                        contact = this._contactService.Add(contact);
                        reservationViewModel.Contact = Mapper.Map<ContactViewModel>(contact);
                    }
                    reservationViewModel.ContactId = contact.Id;
                    var reservation = Mapper.Map<Reservation>(reservationViewModel);
                    this._reservationService.Update(reservation);
                    return RedirectToAction("Index", new {page, sort});
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            var list = _contactTypeService.GetAll().Items;
            this.ViewBag.ContactTypeId = new SelectList(list, "Id", "Description");
            return View(reservationViewModel);
        }


        [HttpPost]
        public ActionResult UpdateRating(int id, string value)
        {
            if (id == 0) return null;

            var reservation = this._reservationService.UpdateRanKing(id, Double.Parse(value));
            return JsonHelper.ToJsonResult(reservation);
        }

        [HttpPost]
        public ActionResult UpdateFavorite(int id, bool value)
        {
            if (id == 0) return null;

            var reservation = this._reservationService.UpdateFavorite(id, value);
            return JsonHelper.ToJsonResult(reservation);
        }

        public JsonResult GetById(int id = 0)
        {
            if (id == 0)
            {
                return null;
            }

            var reservation = this._reservationService.Get(id);
            var reservationDto = Mapper.Map<ReservationViewModel>(reservation);

            ContactViewModel contact = Mapper.Map<ContactViewModel>(reservationDto.Contact);
            reservationDto.Contact = null;

            return this.Json(new List<Object>
                {
                    reservationDto,
                    contact
                }
                , JsonRequestBehavior.AllowGet);
        }

        private List<ReservationViewModel> ReservationDetoList(int page, string sort)
        {
            const int maxPage = 5;
            var skipCount = (page - 1) * maxPage;
            OrderByEnum.TryParse(sort, out OrderByEnum sortBy);

            var response =
                this._reservationService.OrderBy(sortBy, new PageResult(skipCount, maxPage));

            var reservationDetoList = Mapper.Map<List<Reservation>, List<ReservationViewModel>>(response.Items);
            this.ViewBag.PageCount = (int) Math.Ceiling((double) response.SourceTotal / maxPage);
            this.ViewBag.page = page;
            this.ViewBag.sort = sort;
            this.ViewBag.listSorts = Helper.EnumHelper.ToLocalizationListSelectListItem<OrderByEnum>();
            return reservationDetoList;
        }
    }
}