using CfUnitTestProjectDemo.Services;
using CfUnitTestProjectDemo.Services.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace CfUnitTestProjectDemo.Persistances.Base
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class 
    {

        protected  string ACCEPTED_FILE_PATH = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}/CfUnitTestProjectDemo/AcceptedMembers.txt";
        protected  string REJECTED_FILE_PATH = $@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName}/CfUnitTestProjectDemo/RejectedMembers.txt";

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
