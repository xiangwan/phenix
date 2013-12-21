using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Phenix.Core.Repository;
using Phenix.Data.Repository;

namespace Phenix.Web
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();


            builder.RegisterControllers(typeof (MvcApplication).Assembly);

            // builder.RegisterType<WorkContext>().InstancePerHttpRequest();

            // builder.Register(c => new HaiTaoDbContext()).InstancePerHttpRequest();
            //  builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerHttpRequest();
            builder.RegisterGeneric(typeof (Repository<>)).As(typeof (IRepository<>)).InstancePerHttpRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}