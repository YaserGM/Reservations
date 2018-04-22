using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Reservations.Business.Services.Contacts;
using Reservations.Business.Services.Reservations;
using Reservations.DataAccess.Contracts;
using Reservations.DataAccess.UnitOfWorkImpl;

namespace Reservations.App
{
    public class AutofacConfig
    {
        #region Public Methods

        public static void Initialize()
        {
            var builder = new ContainerBuilder();

            // MVC - Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // MVC - OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // MVC - OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // MVC - OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // MVC - OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register application dependencies.
            ///builder.RegisterType<DataContextLocal>().As<DataContextLocal>();

            builder.RegisterType<SqlUnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<ContactService>().As<IContactService>();
            builder.RegisterType<ReservationService>().As<IReservationService>();
            

            // MVC - Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        #endregion
    }
}