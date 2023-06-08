using Microsoft.EntityFrameworkCore.Internal;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Services.Exceptions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService() { }
        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync() 
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller seller)
        {
            _context.Add(seller);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            return await _context.Seller.Include(x => x.Department).FirstOrDefaultAsync(seller => seller.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Seller.FindAsync(id);
            _context.Seller.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task  UpdateAsync(Seller obj)
        {
            if (! (await _context.Seller.AnyAsync(x => x.Id == obj.Id)))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
