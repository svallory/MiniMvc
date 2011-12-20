## Careful: It's still under development

## About the MiniMvc Framework
Actually, the term "mini" isn't accurate. It should be [micro](http://en.wikipedia.org/wiki/Micro-), [nano](http://en.wikipedia.org/wiki/Nano-) or even [yocto](http://en.wikipedia.org/wiki/Yocto-)! But I like the sound of MiniMvc. To give you an idea of how small it is, the project has 7 code files (I like it, seven is the number of perfection), and about 10 classes. You are probably wondering: it can't be good if it has only 10 classes! And, probably the question bugging you more is...

## Why the hell would someone create this?
**Quick answer:** To gradually migrate an old, big project, either in Web Forms or Web Site structure, into the new shiny MVC 3 framework.
With MiniMvc you can add some features to your application while keeping the old code running. So, all the new code you write goes into the right place and you can piece by piece move pages into controllers and views.

## How does this little thing help with that?
Basically, it adds the following capabilities to your application:

* URL Routing to route requests to controller methods
* A base Controller class to easy up some repetitive work
* Razor views to render your output (Thanks to [RazorEngine](https://github.com/Antaris/RazorEngine))

Which is actually most of what MVC is all about. The models aren't covered just so you can use wherever you want.
## Interested? Let's see how the process works...

### 1. Configuration

2. Add a references to the MiniMvc.dll and RazorEngine.dll
2. Configure the Razor views on your `Web.config`
		<configuration>
			<configSections>
				<section name="razorEngine" type="RazorEngine.Configuration.RazorEngineConfigurationSection, RazorEngine" requirePermission="false"/>
				<sectionGroup name="system.web.webPages.razor" type="System.Web.WebPages.Razor.Configuration.RazorWebSectionGroup, System.Web.WebPages.Razor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35">
					<section name="host" type="System.Web.WebPages.Razor.Configuration.HostSection, System.Web.WebPages.Razor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" requirePermission="false" />
					<section name="pages" type="System.Web.WebPages.Razor.Configuration.RazorPagesSection, System.Web.WebPages.Razor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" requirePermission="false" />
				</sectionGroup>
			</configSections>
			<system.web.webPages.razor>
				<pages pageBaseType="System.Web.Mvc.WebViewPage">
					<namespaces>
						<add namespace="System.Web.Mvc" />
						<add namespace="System.Web.Mvc.Ajax" />
						<add namespace="System.Web.Mvc.Html" />
						<add namespace="System.Web.Routing" />
						<add namespace="San.Model" />
					</namespaces>
				</pages>
			</system.web.webPages.razor>
		</configuration>

2. Instantiate the MiniMvc inside your Global.asax file like this
    
    > **VB**
    
        Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
            MiniMvcSystem.GetInstance(System.Reflection.[Assembly].GetExecutingAssembly(), "~/Views")
        End Sub
    
    > **C#**
    
        void Application_Start(Object sender, EventArgs e)
        {
            MiniMvcSystem.GetInstance(System.Reflection.Assembly.GetExecutingAssembly(), "~/Views");
        }
    > Note: the second parameter, if you didn't guess it yet, is your Views folder :)

2. Create `Controllers` and `Views` folders. The `Controllers` folders can be anywhere, e.g. `~/App_Code/Controllers` (if your project is of type Web Site, you have to put the folder somewhere inside `App_Code`. If you used the above config, you have to put the Views folder at the root of your site / application.
