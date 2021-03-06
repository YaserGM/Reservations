﻿using AutoMapper;
using Reservations.App.Models;
using Reservations.Core.Entities;

namespace Reservations.App
{
    /// <summary>
    /// The auto mapper config.
    /// </summary>
    public class AutoMapperConfig
    {
        #region Public Static Methods

        /// <summary>
        /// The initialize mapper.
        /// </summary>
        public static void InitializeMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Contact, ContactViewModel>();
                cfg.CreateMap<Reservation, ReservationViewModel>();
                cfg.CreateMap<ContactType, ContactTypeViewModel>();
            });
        }

        #endregion
    }
}