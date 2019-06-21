using Lingva.DAL.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Lingva.DAL.CosmosSqlApi
{
    public abstract class CosmosSqlApiSet
    {

    }

    public class CosmosSqlApiSet<T> : CosmosSqlApiSet 
        where T : BaseBE, new()
    {
        private readonly CosmosSqlApiContext _dbContext;       
        private string _collectionName;

        public CosmosSqlApiSet(CosmosSqlApiContext dbContext, string collectionName)
        {
            _dbContext = dbContext;
            _collectionName = collectionName;
        }

        public async Task<IEnumerable<T>> SelectAllAsync()
        {
            Uri uri = UriFactory.CreateDocumentCollectionUri("lingva", _collectionName);
            var document = await _dbContext.Client.ReadDocumentCollectionAsync(uri);
            return (IEnumerable<T>)(dynamic)document;
        }
        public async Task<T> FindAsync(int id)
        {
            Uri uri = UriFactory.CreateDocumentUri("lingva", _collectionName, id.ToString());
            Document document = await _dbContext.Client.ReadDocumentAsync(uri);
            return (T)(dynamic)document;
        }
        public async Task<ResourceResponse<Document>> AddAsync(T entity)
        {
            Uri uri = UriFactory.CreateDocumentCollectionUri("lingva", _collectionName);
            return await _dbContext.Client.CreateDocumentAsync(uri, entity);       
        }
        public async Task<ResourceResponse<Document>> UpdateAsync(T entity, IDbTransaction transaction = null)
        {
            Uri uri = UriFactory.CreateDocumentUri("lingva", _collectionName, entity.Id.ToString());
            return await _dbContext.Client.ReplaceDocumentAsync(uri, entity);
        }
        public async Task<ResourceResponse<Document>> RemoveAsync(int id, IDbTransaction transaction = null)
        {
            Uri uri = UriFactory.CreateDocumentUri("lingva", _collectionName, id.ToString());
            return await _dbContext.Client.DeleteDocumentAsync(uri);
        }
    }
}
