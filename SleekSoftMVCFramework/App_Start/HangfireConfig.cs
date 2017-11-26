using Hangfire;
using Owin;
using SleekSoftMVCFramework.HangfireJob;
using SleekSoftMVCFramework.HangfireSetting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SleekSoftMVCFramework.App_Start
{
    public class HangfireConfig
    {
        public static void ConfigureHangfire(IAppBuilder app)
        {
            Hangfire.GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection").UseFilter(new HangfireLoggerAttribute());

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangireDashboardAuthorizationFilter() }
            });
            app.UseHangfireServer();
            GlobalJobFilters.Filters.Add(new HangfireLoggerAttribute());
        }
        public static void InitializeJobs()
        {
            RecurringJob.AddOrUpdate<HangfireEmailJob>(j => j.Execute(),Cron.Minutely());
        }
        }
}