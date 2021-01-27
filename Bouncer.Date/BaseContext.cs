﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Bouncer.DI;
using Bouncer.Common.Exceptions;
using Bouncer.Domain;

namespace Bouncer.Data
{
    public class BaseContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        protected readonly ILoggerFactory _loggerFactory;
        protected readonly ILogger _log;
        private IDbContextTransaction _transation;

        public BaseContext() : base()
        {
            _loggerFactory = AppContainer.Resolve<ILoggerFactory>();
            _configuration = AppContainer.Resolve<IConfiguration>();

            _log = _loggerFactory.CreateLogger<BaseContext>();
        }

        public IDbContextTransaction BeginTransaction()
        {
            if(_transation == null)
                _transation = Database.BeginTransaction();
            
            return _transation;
        }

        public void Rollback()
        {
            Dispose();
        }

        public async Task Commit()
        {
            try
            {
                await SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var msg = "Error trying to commit";
                _log.LogError(msg, ex);

                Rollback();
                throw new AppPersistenceException(msg);
            }
        }

        public DbSet<TEntity> GetEntity<TEntity>() where TEntity : EntityBase<long>
        {
            return base.Set<TEntity>();
        }

        public EntityEntry EntryEntity<TEntity>(TEntity entity) where TEntity : EntityBase<long>
        {
            return base.Entry(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}