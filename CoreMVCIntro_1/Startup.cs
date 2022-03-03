﻿using CoreMVCIntro_1.Models.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro_1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //Hangi servisin eklenecegini belirliyorsunuz...Bazı servisler eklendiklerinde otomatik olarak kullanımı alınırken bazı servisleri de ekledikten sonra alttaki configure metodunda özellikle kullanacagınızı belirtmeniz gerekiyor...

            //BUrada standart bir Sql baglantısı belirtmek istiorsanız(sınıf icerisinde optionsBuilder'in belirtilmesindense bu tercih edilir) asagıdaki kodla belirtmelisiniz...

            //Pool kullanmak bir Singleton Pattern görevi görür...
            services.AddDbContextPool<MyContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection")).UseLazyLoadingProxies());//böylece baglantı ayarlarını burada belirlemiş olduk ve Lazy Loading'i de aktif hale getirdik...
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
