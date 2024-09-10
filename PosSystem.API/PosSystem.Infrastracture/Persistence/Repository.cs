﻿using Microsoft.EntityFrameworkCore;
using PosSystem.Core.Interfaces;
using PosSystem.Infrastracture.Persistence.Data;
using System.Collections.Generic;

namespace PosSystem.Infrastracture.Persistence
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        private readonly PosDbContext _context;
        private readonly DbSet<Entity> _dbSet;

        public Repository(PosDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Entity>();
        }

        public List<Entity> GetAll()
        {
            return _dbSet.ToList();
        }

        public Entity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Insert(Entity entity)
        {
            _dbSet.Add(entity);
        }
        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
        public void Update(Entity entity)
        {
            _dbSet.Update(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }


    }
}