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
    public class AddOnRepository : Repository<AddOn>, IAddOnRepository
    {
        private ApplicationDbContext _db;

        public AddOnRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(AddOn obj)
        {
            _db.AddOns.Update(obj);
        }
    }
}
