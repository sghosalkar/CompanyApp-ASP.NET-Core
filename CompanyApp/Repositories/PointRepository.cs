using CompanyApp.Data;
using CompanyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyApp.Repositories
{
    public class PointRepository
    {
        private readonly CompanyDbContext context;

        public PointRepository(CompanyDbContext context)
        {
            this.context = context;
        }

        public void Add(Point point)
        {
            context.Point.Add(point);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
