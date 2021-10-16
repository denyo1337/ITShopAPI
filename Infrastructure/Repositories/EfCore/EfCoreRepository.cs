using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.EfCore
{
    public abstract class EfCoreRepository<TEnity, Context> : IRepository<TEnity>
        where TEnity : class, IEntity
        where Context : DbContext
    {
        protected readonly Context _context;
        public EfCoreRepository(Context context)
        {
            _context = context;
        }

        public virtual async Task<TEnity> Add(TEnity entity)
        {
            _context.Set<TEnity>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEnity> Delete(int id)
        {
            var entity = await _context.Set<TEnity>().FirstOrDefaultAsync(x => x.Id == id);
            if(entity == null)
            {
                return entity;
            }
            _context.Set<TEnity>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEnity> Get(int id)
        {
            return await _context.Set<TEnity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<TEnity>> GetAll()
        {
            return await _context.Set<TEnity>().ToListAsync();
        }

        public virtual async Task<TEnity> Update(TEnity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
