using HorecaSwagger.BL.Interfaces;
using HorecaSwagger.BL.Model;
using HorecaSwagger.DLEF.Mappers;
using HorecaSwagger.DLEF.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Repositories;

public class CustomerRepositoryEF : ICustomerRepository
{
    private DataContext ctx;

    public CustomerRepositoryEF(string connStr)
    {
        ctx = new DataContext(connStr);
    }

    private void SaveAndClear()
    {
        ctx.SaveChanges();
        ctx.ChangeTracker.Clear();
    }

    public void CreateCustomer(Customer customer)
    {
        if (ctx.Customers.Any(x=>x.Name==customer.Name&&x.FirstName==customer.FirstName&&x.Email==customer.Email))
        {
            var existingC = ctx.Customers.Where(x => x.Name == customer.Name&& x.FirstName == customer.FirstName&& x.Email == customer.Email).AsNoTracking().SingleOrDefault();
            existingC.Deleted = false;
            ctx.Customers.Update(existingC);
        } 
        else
        { 
            ctx.Customers.Add(CustomerMapper.MapToDB(customer, false)); 
        }
        SaveAndClear();
    }

    public void DeleteCustomer(Customer customer)
    {
        ctx.Customers.Update(CustomerMapper.MapToDB(customer, true)); 
        SaveAndClear();
    }
    
    public Customer Read(int id)
    {
        return CustomerMapper.MapToDomain(ctx.Customers.Where(x => x.CustomerUUID == id && x.Deleted==false).AsNoTracking().SingleOrDefault());
    }

    public ICollection<Customer> ReadAll()
    {
        return ctx.Customers.Where(x => x.Deleted == false).AsNoTracking().ToList().Select(x=> CustomerMapper.MapToDomain(x)).ToList();
    }

    public void UpdateCustomer(Customer customer)
    {
        ctx.Customers.Update(CustomerMapper.MapToDB(customer, false));
        SaveAndClear();
    }
}
