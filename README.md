Can I ?
=======

Goals:
------
A .net authorization component that decouples authorization from roles/actions/...
This assumes a few conventions over configuration.

Loosely based on ruby's [cancan gem](https://github.com/ryanb/cancan).

Usage for an MVC application
----------------------------
Create a new class where you'll configure the authorization. In the demo application, I called it the AbiltiyConfigurator:
<pre lang='csharp'>
    public class AbilityConfigurator
    {
        public AbilityConfigurator(IAbilityConfiguration config, IPrincipal principal)
        {
            config.Allow("Login", "LogOff").On("Account");
            config.AllowTo("View", "Home");

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

There is also a view helper to easily check for authorization. The next piece of code only checks if the action is allowed for the current user:
<pre lang='csharp'>
	@if (I.Can("edit", "customer")) // doesn't check state
	{
		...some html
	}
</pre>

The next piece of code also verifies if the action is allowed on the @Model. In this case, two checks will be applied:
- is the user allowed to 'edit' a 'customer'
- if the @Model has a property 'CanEdit' that returns a boolean, this property has to be true

If both these conditions are met, the HTML will be rendered
<pre lang='csharp'>
	@if (I.Can("edit", @Model)) // where Model is a viewmodel, also checks state
	{
		...some html
	}
</pre>

How to configure an MVC application
-----------------------------------
In the global.asax, intialize this new configuration class like this:
<pre lang='csharp'>
	AbilityConfiguration.ConfigureWith(
		config => new AbilityConfigurator(config, System.Web.HttpContext.Current.User)
	);
</pre>

To add a generic filter over all the controllers, register the filter globally
<pre lang='csharp'>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
			// this is the default ActionResult on failed authorization
            filters.Add(new AuthorizeWithCanIFilter(new RedirectResult("/")));
        }
</pre>
Now each request is automatically filtered based on the content of the AbilityConfigurator.


Features:
---------
- Action-based authorization filter for http requests (mvc only at the moment)
- Simple viewhelper to show/hide html based on the authorization
- State based authorization
- Contains a very simple demo project. Explore at leisure

Roadmap:
--------
This project is written in the RDD fashion: Readme Driven Development. These are the features I'm planning:

Near Future
***********
- Make the demo site a little more appealing
- fix hack, where an url can bypass state based authorization

Far Future
**********
- Create NuGet package
- Think about a plugin for NHibernate
