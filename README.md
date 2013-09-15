Can I ?
=======

Goals:
------
A .net authorization component that decouples authorization from User, IPrincipal or Roles. 
This assumes a few conventions over configuration.

Inspired by ruby's [cancan gem](https://github.com/ryanb/cancan).

Usage for an MVC application
----------------------------
Create a new class where you'll configure the authorization. In the demo application, I called it the AbiltiyConfigurator:
<pre lang='csharp'>
    public class AbilityConfigurator
    {
        public AbilityConfigurator(IAbilityConfiguration config, IPrincipal principal)
        {
            if (principal.IsInRole("admin"))
                config.AllowAnything().OnEverything();

            if (principal.IsInRole("manager"))
                config.AllowAnything().On("Customer");

            if (principal.IsInRole("callcenter"))
                config.Allow("View", "Edit").On("Customer");

            if (principal.IsInRole("viewer"))
                config.Allow("View").On("Customer");

            config.ConfigureSubjectAliases("Customer", "Customers");
        }
    }
</pre>
You can implement this class however you want. If you want to have dynamic rules, you could get your configuration from the database.

There is also a view helper to easily check for authorization. The next piece of code only checks if the action is allowed for the current user:
<pre lang='csharp'>
	@if (I.Can("edit", "customer"))
	{
		// with the above configuration, this will only render
		// for users with a role of 
		// admin, manager or callcenter
		...some html
	}
</pre>

The next piece of code also verifies if the action is allowed on the @Model. In this case, two checks will be applied:
- is the user allowed to 'edit' a 'customer'
- if the @Model has a property 'CanEdit' that returns a boolean, this property has to be true

If both these conditions are met, the HTML will be rendered
<pre lang='csharp'>
	@if (I.Can("edit", @Model))
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
Now each request is automatically filtered based on the content of the AbilityConfigurator. When a request is not authorized, the user will be redirected to the configured URL. In this case, this will be the root of the site: "/".

If you do not want to add a generic filter over all the controllers, you can add them individually to each controller
<pre lang='csharp'>
    [AuthorizeWithCanIFilter("/")]
    public class CustomersController : Controller
    {
		// controller actions ...
	}
</pre>

You can also apply the authorization filter to individual controller actions:
<pre lang='csharp'>
    public class CustomersController : Controller
    {
        [AuthorizeWithCanIFilter("/")]
        public ActionResult Delete(int id)
        {
			// code ...
        }
	}
</pre>

Features:
---------
- Action-based authorization filter for http requests (mvc only at the moment)
- Simple viewhelper to show/hide html based on the authorization
- Convention-based subject state authorization
- Authorize command objects based on conventions
- Authorization based on external state
- Contains a very simple demo project. Explore at leisure

Roadmap:
--------
This project is written in the RDD fashion: Readme Driven Development. These are the features I'm planning:

- Attribute-based custom authorization
- Authorize MVC actions based on verb
  - GET => authorize on view
  - POST => authorize on create
  - PUT => authorize on update
  - DELETE => authorize on delete
- Make the demo site a little more appealing
- fix hack, where an url can bypass state based authorization
- Create NuGet package
- Think about a plugin for NHibernate
