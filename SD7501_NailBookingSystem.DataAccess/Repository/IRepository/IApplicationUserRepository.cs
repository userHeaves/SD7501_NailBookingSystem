using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.Models;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;

namespace SD7501_NailBookingSystem.DataAccess.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository<ApplicationUser>
    { 

    }
}
