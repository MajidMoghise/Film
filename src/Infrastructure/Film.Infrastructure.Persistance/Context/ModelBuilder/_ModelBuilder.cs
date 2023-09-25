using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Film.Infrastructure.Persistance.Context.ModelsBuilder
{
    public static class _ModelBuilder
    {
        public static void ModelsBuilder(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuider)
        {
            modelBuider.CategoryBuilder();
            modelBuider.FilmBuilder();
           
        }

    }
}
