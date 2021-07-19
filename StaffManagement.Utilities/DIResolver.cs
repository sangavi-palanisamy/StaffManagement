using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StaffManagement.Core.IRepository;
using StaffManagement.Core.IServices;
using StaffManagement.Resources.Repository;
using StaffManagement.Service.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagement.Utilities
{
    public class DIResolver
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            #region Context accesor
            // this is for accessing the http context by interface in view level
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            #region Services

            services.AddScoped<IStudentService, StudentService>();

            #endregion

            #region Repository

            services.AddScoped<IStudentRepository, StudentRepository>();

            #endregion

        }
    }
}
