﻿using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepo;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        
        public ICategory Category { get; private set; }
        public IProduct Product { get; private set; }

        private readonly ApllicationDbContext _db;
        public UnitOfWork(ApllicationDbContext db,ICategory cat,IProduct prod)
        {
            _db = db;
            Category = cat;
            Product = prod;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
