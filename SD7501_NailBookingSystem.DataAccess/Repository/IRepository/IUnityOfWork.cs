using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.DataAccess.Repository.IRepository
{
    public interface IUnityOfWork
    {
        IBookingRepository Booking { get; }
        IServiceRepository Service { get; }
        IShoppingCartRespository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        //IAddOnRepository AddOn { get; }
        void Save();

    }
}
