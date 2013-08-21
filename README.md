Can I ?
=======

Goals:
------
A .net authorization component that decouples authorization from roles/actions/...
This assumes a few conventions over configuration.

Loosely based on ruby's cancan gem.

Usage for an MVC application
----------------------------
In the global.asax, intialize the authorization like this:
<pre lang='csharp'>
    CanIMvcConfiguration.ConfigureWith(
        () => new AbilityConfigurator(HttpContext.Current.User), // we'll get to this class
        () => new RedirectResult("/") //action if authorization fails
  );
</pre>

Create a new class where you'll configure the authorization. In the demo application, I called it the AbiltiyConfigurator:
<pre lang='csharp'>
    public class AbilityConfigurator : IAbilityConfigurator
    {
        private readonly IPrincipal principal;

        public AbilityConfigurator(IPrincipal principal)
        {
            this.principal = principal;
        }

        public void Configure(IAbilityConfiguration configuration)
        {
            configuration.AllowTo("view", "home");

            if (principal.IsInRole("admin"))
                configuration.AllowTo("manage", "all");

            if (principal.IsInRole("home-owner"))
                configuration.AllowTo("manage", "home");
        }
    }
</pre>

To add a generic filter over all the controllers, register the filter globally
<pre lang='csharp'>
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new AuthorizeWithCanIFilter());
    }
</pre>
Now each request is automatically filtered based on the content of the DemoAbilityConfigurator.

There is also a view helper to easily check for authorization.
<pre lang='csharp'>
	@if (Html.ICan("View", "Home"))
	{
		...some html
	}
</pre>

Features:
---------
- Action-based authorization filter for http requests (mvc only at the moment)
- Simple viewhelper to show/hide controls
- Contains a very simple demo project. Explore at leasure.

Roadmap:
--------
This project is written in the RDD fashion: ReadMe Driven Development. These are the features I'm planning:

Near Future
***********
- Add authorization on ViewModels

Far Future
**********
- Create NuGet package
- Think about a plugin for NHibernate
