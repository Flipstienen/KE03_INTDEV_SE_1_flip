using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class OrderProductsRepository : IOrderProductsRepository
    {
        private readonly MatrixIncDbContext _context;
        public OrderProductsRepository(MatrixIncDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> GetAllMostSoldProducts()
        {
            return _context.OrderProduct.GroupBy(op => op.ProductId)
                .OrderByDescending(g => g.Sum(op => op.Amount))
                .Take(2)
                .Select(g => g.Key)
                .Join(_context.Products,
                      productId => productId,
                      p => p.Id,
                      (productId, product) => product)
                .ToList();
        }
    }
}
