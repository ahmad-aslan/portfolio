﻿using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repository.IRepository
{
    public interface IOwnerRepository : IRepository<Owner> 
    {
        void Update(Owner owner);
    }
}
