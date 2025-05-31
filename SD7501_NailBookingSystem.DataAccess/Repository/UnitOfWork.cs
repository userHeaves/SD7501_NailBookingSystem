using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.DataAccess.Repository
{
    public class UnitOfWork : IUnityOfWork
    {
        private ApplicationDbContext _db;
        public IBookingRepository Booking { get; private set; }
        public IServiceRepository Service { get; private set; }
        public IShoppingCartRespository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        //public IAddOnRepository AddOn { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Booking = new BookingRepository(_db);
            Service = new ServiceRepository(_db);
            ShoppingCart = new ShoppingCartRespository(_db);
            ApplicationUser = new ApplicationUserRespository(_db);
            //AddOn = new AddOnRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
