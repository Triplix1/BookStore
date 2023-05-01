using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private ApplicationDbContext _context;

        public CoverTypeRepository(ApplicationDbContext ctx) : base(ctx)
        {
            _context = ctx; 
        }

        public void Update(CoverType obj)
        {
            _context.CoverTypes.Update(obj);
        }
    }
}
