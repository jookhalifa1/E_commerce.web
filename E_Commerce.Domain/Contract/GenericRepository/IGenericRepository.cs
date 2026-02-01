using E_Commerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contract.GenericRepository
{
     public interface IGenericRepository<Tentity,Tkey> where  Tentity : BaseEntity<Tkey>
    {
       Task <IEnumerable<Tentity>> GetAllAsync();
        Task<IEnumerable<Tentity>> GetAllAsync(ISpecifications<Tentity,Tkey>  specifications);
        Task<Tentity> GetById(Tkey id);
        Task<Tentity> GetById(ISpecifications<Tentity,Tkey> specifications);
        Task AddAsync(Tentity entity);

        void update(Tentity entity);
        void remove(Tentity entity);


        Task<int> CountElementAsync(ISpecifications<Tentity, Tkey> specifications);
        


    }
}
