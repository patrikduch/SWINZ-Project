﻿

using PersistenceLib.Domains.OrderApi;

namespace UserApi.QueryObjects
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using PersistenceLib.Domains.UserApi;
    using Dto.Customers;

    public class CustomerQuery : ICustomerQuery
    {
        public bool FilterById { get; set; } = false;
        public int  CustomerId { get; set; }
        public bool FilterByName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public async Task<CustomerDto> Execute(DbContext context)
        {
            Customer entity = null;

            if (FilterById)
            {
                entity = await context.Set<Customer>().Where(c => c.Id == CustomerId).FirstOrDefaultAsync();

                var res = context.Set<Order>().Where(c => c.CustomerId == entity.Id).Include(c=>c.Discount).ToList();
            }

            if (FilterByName)
            {
                entity = await context.Set<Customer>().Where(c => c.FirstName == FirstName && c.LastName == LastName)
                    .FirstOrDefaultAsync();
            }


            if (entity == null) return null; // No data was found

            

            return new CustomerDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Discount = entity.Discount
            };
        }
    }
}
