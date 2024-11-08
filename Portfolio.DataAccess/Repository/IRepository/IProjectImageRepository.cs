﻿using Portfolio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.DataAccess.Repository.IRepository
{
    public interface IProjectImageRepository : IRepository<ProjectImage> 
    {
        void Update(ProjectImage projectImage);
    }
}
