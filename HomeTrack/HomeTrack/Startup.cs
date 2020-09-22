using DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace HomeTrack
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new HomeTrackConfiguration();
            Configuration.Bind(config);

            services.AddSingleton(config.DatabaseConfiguration);

            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opts =>
                {
                    opts.Cookie.Name = "Cookie";
                    opts.ExpireTimeSpan = TimeSpan.FromDays(1);
                    opts.LoginPath = "/auth/login";
                    opts.LogoutPath = "/auth/logout";
                    opts.ReturnUrlParameter = "returnUrl";
                });

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });

            services.Configure<RequestLocalizationOptions>(
                opts =>
                {
                    var list = new List<CultureInfo>(0);
                    list.Add(new CultureInfo("ru"));
                    list.Add(new CultureInfo("en"));

                    opts.DefaultRequestCulture = new RequestCulture("ru");
                    opts.SupportedCultures = list;
                    opts.SupportedUICultures = list;
                });

            var mvcBuilder = services
                .AddMvc(options =>
                {
                    var stringLocalizerFactory = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                    var localizer = stringLocalizerFactory.Create("ModelBinder", "HomeTrack");
                    options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => 
                    localizer["ModelBinder_AttemptedValueIsInvalidAccessor", x, y]);
                    options.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => 
                    localizer["ModelBinder_MissingBindRequiredValueAccessor", x]);
                    options.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => 
                    localizer["ModelBinder_MissingKeyOrValueAccessor"]);
                    options.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => 
                    localizer["ModelBinder_MissingRequestBodyRequiredValueAccessor"]);
                    options.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(x => 
                    localizer["ModelBinder_NonPropertyAttemptedValueIsInvalidAccessor", x]);
                    options.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => 
                    localizer["ModelBinder_NonPropertyUnknownValueIsInvalidAccessor"]);
                    options.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => 
                    localizer["ModelBindder_NonPropertyValueMustBeANumberAccessor"]);
                    options.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(x => 
                    localizer["ModelBinder_UnknownValueIsInvalidAccessor", x]);
                    options.ModelBindingMessageProvider.SetValueIsInvalidAccessor(x => 
                    localizer["ModelBinder_ValueIsInvalidAccessor", x]);
                    options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => 
                    localizer["ModelBinder_ValueMustBeANumberAccessor", x]);
                    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => 
                    localizer["ModelBinder_ValueMustNotBeNullAccessor", x]);
                }
                );

            services.AddSession();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<AppDbContextFactory>();

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Auth}/{action=Login}/{id?}");
            });
        }
    }
}
