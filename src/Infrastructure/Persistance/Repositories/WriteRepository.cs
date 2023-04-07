using Application.Abstractions.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly OzkaDbContext _context;
        public WriteRepository(OzkaDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                if (entity == null)
                    return null;
                {
                 
                    await Table.AddAsync(entity);
                    await _context.SaveChangesAsync();
                    return entity;
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> AddRangeAsync(List<T> entites)
        {
            if (entites.Count == 0)
                return false;
            else
            {
                await Table.AddRangeAsync(entites);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<T> RemoveAsync(T entity)
        {
            if (entity == null)
                return null;
            else
            {
                await _context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<T> RemoveAsync(int id)
        {
            var entity = await Table.FindAsync(id);
            if (entity == null)
                return null;
            else
            {
                Table.Remove(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<bool> RemoveRangeAsync(List<T> entites)
        {
            if (entites.Count == 0)
                return false;
            else
            {
                Table.RemoveRange(entites);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                return null;
            else
            {
                Table.Update(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
        }
    }
}
