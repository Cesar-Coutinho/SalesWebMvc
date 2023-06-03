using Microsoft.EntityFrameworkCore.Internal;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;

namespace SalesWebMvc.Data
{
    public class SeedingService
    {
        private SalesWebMvcContext _context;

        public SeedingService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Department.Any() || _context.Seller.Any() || _context.SalesRecord.Any())
                return;

            Department department1 = new Department(1, "Computer");
            Department department2 = new Department(2, "Electronics");

            Seller seller1 = new Seller(1, "BobBrown", "Bob@gmail.com", new System.DateTime(1998,1,2), 2500, department1);
            Seller seller2 = new Seller(2, "AmandaSeili", "Amanda@gmail.com", new System.DateTime(1997,5,10), 2500, department2);

            SalesRecord sales1 = new SalesRecord(1,new System.DateTime(2018, 11,20), 11000.00, SalesStatus.BILLED, seller1);
            SalesRecord sales2 = new SalesRecord(2,new System.DateTime(2019, 07,1), 15600.00, SalesStatus.PENDING, seller2);


            _context.Department.AddRange(department1,department2);
            _context.Seller.AddRange(seller1, seller2);
            _context.SalesRecord.AddRange(sales1, sales2);

            _context.SaveChanges();
        }
    }
}
