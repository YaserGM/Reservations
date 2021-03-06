﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using Reservations.Business.Dto;
using Reservations.Core;
using Reservations.Core.Entities;
using Reservations.Core.Enums;
using Reservations.DataAccess.Contracts;
using static Reservations.Core.Enums.OrderByEnum;

namespace Reservations.Business.Services.Reservations
{
    public class ReservationService : IReservationService
    {
        #region Fields

        /// <summary>
        ///     The repository.
        /// </summary>
        private readonly IRepository<Reservation> repository;

        /// <summary>
        ///     The unit of work.
        /// </summary>
        private readonly IUnitOfWork unitOfWork;

        //private readonly object Localization;

        #endregion

        #region Constructors and Desctructors

        public ReservationService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = unitOfWork.Reservations;
        }

        #endregion


        public Reservation Add(Reservation input)
        {
            var contact = input.Contact;
            input.Contact = null;
            input.CreateDate = DateTime.Now;
            this.repository.Add(input);
            this.unitOfWork.SaveChanges();
            input.Contact = contact;
            return input;
        }


        public Reservation Get(int id)
        {
            return this.repository.Get(id);
        }


        public CollectionResponse<Reservation> GetAll(PageResult input)
        {
            var entities = this.repository.GetAll();
            var response = GetPageReservation(entities.ToList(), input);
            return response;
        }

        public CollectionResponse<Reservation> GetAll()
        {
            var entities = this.repository.GetAll();
            var result = entities.ToList();
            var response = new CollectionResponse<Reservation> {SourceTotal = entities.Count()};
            response.Items.AddRange(result);
            return response;
        }

        public CollectionResponse<Reservation> OrderBy(OrderByEnum order, PageResult input = null)
        {
            var entities = this.repository.GetAll();

            var result = new List<Reservation>();

            switch (order)
            {
                case RanKing:
                    result = entities.OrderByDescending(t => t.RanKing).ToList();
                    break;
                case DateAscending:
                    result = entities.OrderBy(t => t.CreateDate).ToList();
                    break;
                case AlphabeticAscending:
                    result = entities.OrderBy(t => t.Contact.Name).ToList();
                    break;
                case AlphabeticDescending:
                    result = entities.OrderByDescending(t => t.Contact.Name).ToList();
                    break;
                case DateDescending:
                    result = entities.OrderByDescending(t => t.CreateDate).ToList();
                    break;
            }

            CollectionResponse<Reservation> response;
            if (input == null)
            {
                response = new CollectionResponse<Reservation> {SourceTotal = entities.Count()};
                response.Items.AddRange(result);
            }
            else
            {
                response = GetPageReservation(result, input);
            }

            return response;
        }


        public Reservation Update(Reservation input)
        {
            var found = this.ValidateReservationExists(input.Id);

            found.ContactId = input.ContactId;
            found.RanKing = input.RanKing;
            found.Descriptions = input.Descriptions;
            found.Favorite = input.Favorite;

            this.unitOfWork.SaveChanges();
            return null;
        }

        public bool UpdateRanKing(int id, double value)
        {
            var command = "sp_Reservation_Update_RanKing @id, @value";

            var reseult = unitOfWork.ExecuteStoreCommand(command,
                new SqlParameter() {ParameterName = "@id", Value = id},
                new SqlParameter() {ParameterName = "@value", Value = value});

            return reseult == 1;
        }

        public bool UpdateFavorite(int id, bool value)
        {
            var command = "sp_Reservation_Update_Favorite @id, @value";

            var reseult = unitOfWork.ExecuteStoreCommand(command,
                new SqlParameter() { ParameterName = "@id", Value = id },
                new SqlParameter() { ParameterName = "@value", Value = value });

            return reseult == 1;
        }


        public void Delete(int id)
        {
            var found = this.ValidateReservationExists(id);

            this.repository.Delete(found);
            this.unitOfWork.SaveChanges();
        }


        private Reservation ValidateReservationExists(int id)
        {
            var found = this.repository.Get(id);
            if (found != null)
            {
                return found;
            }

            var message = string.Format(Localization.ReservationNotExists, id);
            throw new Exception(message);
        }

        private CollectionResponse<Reservation> GetPageReservation(List<Reservation> entities, PageResult input)
        {
            if (input.SkipCount < 0)
            {
                input.SkipCount = 0;
            }

            if (input.MaxCount < 0)
            {
                input.MaxCount = 0;
            }

            var result = entities.Skip(input.SkipCount).Take(input.MaxCount);
            var response = new CollectionResponse<Reservation> {SourceTotal = entities.Count()};
            response.Items.AddRange(result);
            return response;
        }
    }
}