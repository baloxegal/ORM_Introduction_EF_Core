using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Introduction_EF_Core
{
    class DataBaseConfig
    {
        public DataBaseConfig()
        {

        }



        var builder = new ConfigurationBuilder();

        builder.SetBasePath(Directory.GetCurrentDirectory());
       
            builder.AddJsonFile("appsettings.json");
       
            var config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var options = optionsBuilder
            .UseSqlServer(connectionString)
            .Options;

    }
}
