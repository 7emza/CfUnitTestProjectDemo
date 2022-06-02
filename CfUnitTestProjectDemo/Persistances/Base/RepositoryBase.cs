using CfUnitTestProjectDemo.Services;
using CfUnitTestProjectDemo.Services.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.Persistances.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class 
    {
        protected const string ACCEPTED_FILE_PATH = @"C:\Users\Shadi\Desktop\Hamza\.NET\CfUnitTestProjectDemo\AcceptedMembers.txt";
        protected const string REJECTED_FILE_PATH = @"C:\Users\Shadi\Desktop\Hamza\.NET\CfUnitTestProjectDemo\RejectedMembers.txt";

        public async Task<TEntity> AddEntity(TEntity entity)
        {
             throw new NotImplementedException();
        }

        public Task DeleteEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllEntities()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> UpdateEntity(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
