using CoreMVCIntro_1.DBConfiguration;
using CoreMVCIntro_1.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCIntro_1.Models.Context
{
    //EntityFrameworkCore.SqlServer kütüphanesini indirmeyi unutmayın...Options ayarları yoksa gelmeyecektir...

    public class MyContext:DbContext
    {
        //Eger Startup'ta gerekli emir verilmiş ise constructor'a  koydugunuz DbContextOptionsBuilder tipindeki parametre otomatik olarak algılanarak sizin hic ek bir şey yapmanıza gerek kalmadan instance'lanacaktır... Dolayısıyla siz aslında bir veritabanı sınıfınızın constructor'ina parametre olarak bir DBContextOptions<> tipinde bir yapı verirseniz bu parametreye argüman DI sayesinde Startup'tan gönderilir...

        //public MyContext(DbContextOptionsBuilder options)
        //{
        //    options.UseSqlServer(connectionString: "server=.;database=CoreDB;uid=sa;pwd=123");
        //}

        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>().Property(x => x.ProductName).HasColumnName("Urun Ismi");
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }

        //.Net Core üzerinden migrate yapmak istediginiz takdirde add-migration <isim> ve sonrasında update-database demeniz gerekir...

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<UserProfile> Profiles { get; set; }

    }
}
