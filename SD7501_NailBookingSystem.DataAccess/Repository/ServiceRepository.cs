using SD7501_NailBookingSystem.Data;
using SD7501_NailBookingSystem.DataAccess.Repository.IRepository;
using SD7501_NailBookingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.DataAccess.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private ApplicationDbContext _db;

        public ServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Service obj)
        {
            //_db.Services.Update(obj);
            var objFromDb = _db.Services.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Type = obj.Type;
                objFromDb.Cost = obj.Cost;
                objFromDb.Description = obj.Description;
                objFromDb.BookingId = obj.BookingId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
