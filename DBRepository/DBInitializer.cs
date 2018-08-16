using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepository
{

    public static class DBInitializer
    {
        public static void Initialize(RepositoryContext context)
        {
            context.Database.Migrate();
            context.SaveChanges();
            // var userCount = await context.Owners.CountAsync().ConfigureAwait(false);
        }
    }
}
