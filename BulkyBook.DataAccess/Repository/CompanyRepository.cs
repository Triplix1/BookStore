using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    internal class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _context = ctx;
        }

        public void Update(Company obj)
        {
            _context.Companies.Update(obj);
        }
    }
}
