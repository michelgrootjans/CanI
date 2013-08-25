﻿using System.Web.Mvc;
using CanI.Demo.Authorization;
using CanI.Mvc;

namespace CanI.Demo.App_Start
{
    public static class AuthorizationConfig
    {
        public static void Configure()
        {
            CanIMvcConfiguration.ConfigureWith(
                config => new AbilityConfigurator(config),
                () => new RedirectResult("/") // ActionResult on failed authorization
                );
        }
    }
}