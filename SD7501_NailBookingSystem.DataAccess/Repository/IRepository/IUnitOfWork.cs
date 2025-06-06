﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD7501_NailBookingSystem.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBookingRepository Booking { get; }
        IServiceRepository Service { get; }
        IShoppingCartRespository ShoppingCart { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailRepository OrderDetail { get; }
        //IAddOnRepository AddOn { get; }
        void Save();

    }
}
