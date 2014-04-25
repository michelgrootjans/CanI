#Can I ?
##Goals:
A .net authorization component that decouples authorization from User, IPrincipal or Roles. 
This assumes a few conventions over configuration.

Inspired by ruby's [cancan gem](https://github.com/ryanb/cancan).

##Usage
Create a new class where you'll configure the authorization. In the demo application, I called it the ```AbiltiyConfigurator```:
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

There is also a helper to easily check for authorization. The next piece of code only checks if the action is allowed for the current user. This can be called in a view to show/hide parts of the view.
<pre lang='csharp'>
	if (I.Can("edit", "customer"))
	{
		// with the above configuration, this will only execute
		// for users with a role of admin, manager or callcenter
		...some code
	}
</pre>

The next piece of code also verifies if the action is allowed on the Model. In this case, two checks will be applied:
- is the user allowed to 'edit' a 'customer'
- if the Model has a property 'CanEdit' that returns a boolean, this property has to be true

If both these conditions are met, the HTML will be rendered
<pre lang='csharp'>
	if (I.Can("edit", Model)) // where Model is any .net class
	{
		...some html
	}
</pre>

## Configuration
### Telling CanI where to find the authorization config
To configure an asp.net application (mvc or not), just execute the following at startup (typically in global.asax):
<pre lang='csharp'>
	AbilityConfiguration.ConfigureWith(
		config => new AbilityConfigurator(config, System.Web.HttpContext.Current.User)
	);
</pre>

### Logging
If you want to know why an authorization succeeds or fails, you can configure the logging of CanI. There are no logging frameworks dependencies, just insert your preference. In the demo application, logging is done with plain System.Diagnostics. It is configured like this:
<pre lang='csharp'>
    AbilityConfiguration.Debug(message => Trace.Write(string.Format("Authorization: {0}", message))).Verbose();
</pre>
This allows me to view the debug information in realtime using [SysInternals Suites](http://technet.microsoft.com/en-us/sysinternals/bb842062.aspx) excellent [DebugView](http://technet.microsoft.com/en-us/sysinternals/bb896647). ![Dbgview](https://raw.githubusercontent.com/michelgrootjans/CanI/master/img/DebugInformation.png)

### Caching
When you check an authorization with ```if(I.Can("edit", "customer"))```, the default implementation gets run every time. If you want to run it only once, you can add configuration caching like this:
<pre lang='csharp'>
    AbilityConfiguration.ConfigureCache(new StaticHttpCache());
</pre>
You would typically use ```StaticCache``` for a desktop app or for tests. In an asp.net application, you would probably want to use a ```PerRequestHttpCache```.

### Extra configuration for an asp.net mvc application
#### Filters
To add a generic filter over all the controllers, register the filter globally
<pre lang='csharp'>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
			// this is the default ActionResult on failed authorization
            filters.Add(new AuthorizeWithCanIFilter(new RedirectResult("/")));
        }
</pre>
Now each request is automatically filtered based on the content of the AbilityConfigurator. When a request is not authorized, the user will be redirected to the configured URL. In this case, this will be the root of the site: "/" (the default).

If you do not want to add a generic filter over all the controllers, you can add them individually to each controller
<pre lang='csharp'>
    [AuthorizeWithCanIFilter]
    public class CustomersController : Controller
    {
		// controller actions ...
	}
</pre>

You can also apply the authorization filter to individual controller actions:
<pre lang='csharp'>
    public class CustomersController : Controller
    {
        [AuthorizeWithCanIFilter]
        public ActionResult Delete(int id)
        {
			// code ...
        }
	}
</pre>

If some controller action doesn't follow naming conventions, you can still indicate what the rules are with a custom attribute like this:
<pre lang='csharp'>
    public class CustomerController : Controller
    {
		// Without the attribute, this would check if you can "discombobulate" a "customer"
		// With the attribute, this will check if you can "eat" a "hamburger"
        [AuthorizeIfICan("eat", "hamburger")] 
        public ActionResult Discombobulate(int id)
        {
			// code ...
        }
	}
</pre>

#### Working with asp.net mvc areas
If you are using areas, there are some extra things you want to watch out for...
TO BE EDITED LATER

#### Caching
There is an optimized caching for per-request caching of the configuration. You can apply it with this line of code:
<pre lang='csharp'>
    AbilityConfiguration.ConfigureCache(new PerRequestHttpCache());
</pre>

Where can I get it?
--------------------------------
First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install CanI from the package manager console:

    PM> Install-Package CanI.Core
    PM> Install-Package CanI.Mvc


##Features:
- Action-based authorization filter for http requests (mvc only at the moment)
- Simple viewhelper to show/hide html based on the authorization
- Convention-based subject state authorization
- Authorize command objects based on conventions
- Authorization based on external state
- Contains a very simple demo project. Explore at leisure
- Attribute-based custom authorization
- Downloadable as a NuGet package

##Roadmap:
This project is written in the RDD fashion: Readme Driven Development. These are the features I'm planning:

- Authorize MVC actions based on verb
  - GET => authorize on view
  - POST => authorize on create
  - PUT => authorize on update
  - DELETE => authorize on delete
- Make the demo site a little more appealing
- fix hack, where an url can bypass state based authorization
- Think about a plugin for NHibernate
