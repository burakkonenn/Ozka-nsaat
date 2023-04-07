using Application.Abstractions.Repositories;
using Application.DTOs.Cities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class CitiesReadRepository : ReadRepository<Cities>, ICitiesReadRepository
    {
        private readonly OzkaDbContext _context;
        public CitiesReadRepository(OzkaDbContext context) : base(context)
        {
            _context = context; 
        }

        public User Get()
        {
          var s = _context.Users.FirstOrDefault(x=> x.Id==1);
            return s;
        }
    }
}
