using Lingva.DAL.Entities;
using Lingva.DAL.Repositories;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Lingva.DAL.Mongo.Repositories
{
    public class GroupRepository : Repository, IGroupRepository
    {
        public GroupRepository(MongoContext dbContext) : base(dbContext)
        {

        }

        public override async Task<T> CreateAsync<T>(T entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.ModifyDate = DateTime.Now;

            Group group = entity as Group;
            group.Language = await base.GetByIdAsync<Language>(group.LanguageId);

            await _dbContext.Set<T>().InsertOneAsync(entity);

            return entity;
        }

        public override async Task<T> UpdateAsync<T>(T entity)
        {
            var filter = Builders<T>.Filter.Eq("_id", entity.Id);
            Group group = entity as Group;
            group.Language = await base.GetByIdAsync<Language>(group.LanguageId);

            await _dbContext.Set<T>().ReplaceOneAsync(filter, entity);

            return entity;
        }
    }
}
