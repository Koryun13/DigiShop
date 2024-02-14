using DigiShop.DataAccess.Data;
using DigiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiShop.DataAccess.Repository
{
    public class ShoppingCardRepository : Repository<ShoppingCard> , IShoppingCardRepository
    {
        private ApplicationDbContext _db;

        public ShoppingCardRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;    
        }

        public void Update(ShoppingCard obj)
        {
            _db.ShoppingCards.Update(obj);
        }
    }
}
