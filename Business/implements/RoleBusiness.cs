using Business.Interfaces;
using Data.Interfaces;
using Entity;
using System;
using System.Threading.Tasks;

namespace Business.Implements
{
    public class RoleBusiness : BaseBusiness<Role>, IRoleBusiness
    {
        public RoleBusiness(IGenericRepository<Role> repository) : base(repository)
        {
        }

        
    }
}