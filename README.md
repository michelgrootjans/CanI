Can I ?
=======

Goals:
------
A .net authorization component that decouples authorization from roles/actions/...
This assumes a few conventions over configuration.

Loosely based on ruby's cancan gem.

Usage for an MVC application
----------------------------
Create a new class where you'll configure the authorization. In the demo application, I called it the AbiltiyConfigurator:
<pre lang='csharp'>
    public class AbilityConfigurator
    {
        public AbilityConfigurator(IAbilityConfiguration config, IPrincipal principal)
        {
            config.AllowTo("view", "home");

            if (principal.IsInRole("admin"))
                config.AllowTo("Manage", "All");

            if (principal.IsInRole("manager"))
                config.AllowTo("Manage", "Customer");

            if (principal.IsInRole("callcenter"))
                config.Allow("View", "Edit").On("Customer");

            if (principal.IsInRole("viewer"))
                config.Allow("View").On("Customer");

            config.IgnoreSubjectPostfix("ViewModel");
            config.ConfigureSubjectAliases("Customer", "Customers");
        }
    }
</pre>

In the global.asax, intialize this new configuration class like this:
<pre lang='csharp'>
	CanIMvcConfiguration.ConfigureWith(
		config => new AbilityConfigurator(config, new DummyUser("admin")), // admin, manager, callcenter, viewer, guest
		() => new RedirectResult("/")
	);
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
	@if (Html.ICan("edit", @Model)) //where Model is a customer
	{
		...some html
	}
</pre>

Features:
---------
- Action-based authorization filter for http requests (mvc only at the moment)
- Simple viewhelper to show/hide controls parts of the HTLM based on the authorization
- Contains a very simple demo project. Explore at leisure.
- State based authorization

Roadmap:
--------
This project is written in the RDD fashion: ReadMe Driven Development. These are the features I'm planning:

Near Future
***********
- plug in the real authenticated user, not some dummy implementation
- fix hack, where an url can bypass state based authorization

Far Future
**********
- Create NuGet package
- Think about a plugin for NHibernate
