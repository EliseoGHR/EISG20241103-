using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EISGG20241103.AccesoADatos
{
    public class AppDBContextFactory : IDesignTimeDbContextFactory<EISG20241103DBContext>
    {
        public EISG20241103DBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EISG20241103DBContext>();
            const string conn = "Data Source=DESKTOP-PAKLHRS;Initial Catalog=EISG20241103DB;Integrated Security=True;Encrypt=False";
            optionsBuilder.UseSqlServer(conn);

            return new EISG20241103DBContext(optionsBuilder.Options);
        }
    }
}
