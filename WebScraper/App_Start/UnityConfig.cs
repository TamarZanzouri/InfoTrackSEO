using System;
using Unity;
using Unity.Mvc4;
using Unity.Injection;
using WebScraper.Controllers;
using WebScraper.Repository;
using System.Web.Mvc;
//using Microsoft.Practices.Unity.Configuration;



namespace WebScraper
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes();
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static IUnityContainer RegisterTypes()
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            var container = new UnityContainer();
            //container.RegisterType<ISiteRepository, SiteRepository>();
            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }
    }
}