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

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Booking = new BookingRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
