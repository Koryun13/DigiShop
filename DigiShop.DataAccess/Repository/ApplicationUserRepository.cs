﻿using DigiShop.DataAccess.Data;
using DigiShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigiShop.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser> , IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;    
        }
    }
}
