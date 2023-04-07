using Application.Abstractions.Repositories;
using Domain.Entities;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class CitiesWriteRepository : WriteRepository<Cities>, ICitiesWriteRepository
    {
        public CitiesWriteRepository(OzkaDbContext context) : base(context)
        {
        }
    }
}
