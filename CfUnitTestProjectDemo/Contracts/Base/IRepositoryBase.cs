using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfUnitTestProjectDemo.Services.Base
{
    public interface IRepositoryBase<Entity>
    {
        Task<IEnumerable<Entity>> GetAllEntities();
        Task<Entity> AddEntity(Entity entity);
        Task<Entity> UpdateEntity(Entity entity);
        Task DeleteEntity(Entity entity);
    }
}
