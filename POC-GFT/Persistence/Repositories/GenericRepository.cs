using Application.Contracts.Persistence;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly MySqlContext _context;
        public GenericRepository(MySqlContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            entity.DataCadastro = DateTime.Now;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            _context.Entry(entity).State = EntityState.Deleted;

            await _context.SaveChangesAsync();
            //todo: Precisa melhorar?
            return null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int Codigo)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(cod => cod.Codigo == Codigo);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            entity.DataAlteracao = DateTime.Now;
            _context.Update(entity);
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return null;
        }
    }
}
