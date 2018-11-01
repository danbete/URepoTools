using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DBTools
{
    public sealed class UnitOfWork
    {
        public DbContext _dbcontext;
        public Dictionary<Type, dynamic> DictionaryRepos = new Dictionary<Type, dynamic>();
        public UnitOfWork(DbContext dbContext)
        {
            _dbcontext = dbContext;
            
        }
        private void _addRepository<K>() where K : EntityBase
        {
            Repository<K> Repository = new Repository<K>(_dbcontext);
            DictionaryRepos.Add(typeof(K), Repository);
        }
        public Repository<K> GetRepository<K>() where K : EntityBase
        {
            dynamic repositoryOutout;
            bool exist = DictionaryRepos.TryGetValue(typeof(K), out repositoryOutout);
            if (exist == false)
            {
                _addRepository<K>();
                DictionaryRepos.TryGetValue(typeof(K), out repositoryOutout);
            }
            Repository<K> _repository = repositoryOutout as Repository<K>;
            return _repository;
        }

        public void Save()
        {
            _dbcontext.SaveChanges();
        }
    }
}
