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


        public ReservationsController(IReservationService reservationService, IContactService contactService)
        {
            this._reservationService = reservationService;
            this._contactService = contactService;
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

        // GET: Reservations/Create
        public ActionResult Create(int page, string sort)
        {
            this.ViewBag.page = page;
            this.ViewBag.sort = sort;
            this.ViewBag.contactTypeList = Helper.EnumHelper.ToListSelectListItem<ContactTypeEnum>();
            var reservationDto = new ReservationDto()
            {
                Contact = new Contact()
            };
            return View(reservationDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReservationDto reservationDto, int page, string sort)
        {
            this.ViewBag.page = page;
            this.ViewBag.sort = sort;

            if (ModelState.IsValid)
            {
                reservationDto.CreateDate = DateTime.Now;
                try
                {
                    var reservation = Mapper.Map<Reservation>(reservationDto);
                    if (reservationDto.Contact.Id > 0)
                    {
                        this._contactService.Update(reservationDto.Contact);
                    }
                    else
                    {
                        Contact newContact = this._contactService.Add(reservationDto.Contact);
                        reservationDto.Contact = newContact;
                    }

                    reservation.ContactId = reservation.Contact.Id;
                    this._reservationService.Add(reservation);
                    return RedirectToAction("Index", new {page, sort});
                }
                catch
                {
                    return this.View();
                }
            }
            this.ViewBag.contactTypeList = Helper.EnumHelper.ToListSelectListItem<ContactTypeEnum>();
            return View(reservationDto);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int page, string sort, int id = 0)
        {
            this.ViewBag.page = page;
            this.ViewBag.sort = sort;
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            this.ViewBag.contactTypeList = Helper.EnumHelper.ToListSelectListItem<ContactTypeEnum>();
            var res = this._reservationService.Get(id);
            var reservationDto = Mapper.Map<ReservationDto>(res);
            return this.View(reservationDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReservationDto reservationDto, int page, string sort)
        {
            this.ViewBag.page = page;
            this.ViewBag.sort = sort;
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            try
            {
                var reservation = Mapper.Map<Reservation>(reservationDto);
                if (reservationDto.Contact.Id > 0)
                {
                    this._contactService.Update(reservationDto.Contact);
                }
                else
                {
                    Contact newContact = this._contactService.Add(reservationDto.Contact);
                    reservationDto.Contact = newContact;
                }

                this._reservationService.Update(reservation);
                return RedirectToAction("Index", new {page, sort});
            }
            catch
            {
                return this.View();
            }
        }


        [HttpPost]
        public ActionResult UpdateRating(int id, string value)
        {
            if (id == 0)
            {
                return null;
            }

            var reservation = this._reservationService.Get(id);
            if (reservation != null)
            {
                reservation.RanKing = Double.Parse(value);
                this._reservationService.Update(reservation);
            }

            return null;
        }

        public JsonResult GetById(int id = 0)
        {
            if (id == 0)
            {
                return null;
            }

            var reservation = this._reservationService.Get(id);
            var reservationDto = Mapper.Map<ReservationDto>(reservation);

            ContactDto contact = Mapper.Map<ContactDto>(reservationDto.Contact);
            contact.BirthdateText = contact.BirthdateString();
            reservationDto.Contact = null;

            return this.Json(new List<Object>
                {
                    reservationDto,
                    contact
                }
                , JsonRequestBehavior.AllowGet);
        }

        private List<ReservationDto> ReservationDetoList(int page, string sort)
        {
            const int maxPage = 5;
            var skipCount = (page - 1) * maxPage;
            OrderByEnum.TryParse(sort, out OrderByEnum sortBy);

            var response =
                this._reservationService.OrderBy(sortBy, new PageResult(skipCount, maxPage));

            var reservationDetoList = Mapper.Map<List<Reservation>, List<ReservationDto>>(response.Items);
            this.ViewBag.PageCount = (int) Math.Ceiling((double) response.SourceTotal / maxPage);
            this.ViewBag.page = page;
            this.ViewBag.sort = sort;
            this.ViewBag.listSorts = Helper.EnumHelper.ToListSelectListItem<OrderByEnum>();
            return reservationDetoList;
        }
    }
}