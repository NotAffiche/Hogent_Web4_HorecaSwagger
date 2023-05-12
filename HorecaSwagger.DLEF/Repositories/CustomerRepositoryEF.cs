using HorecaSwagger.BL.Interfaces;
using HorecaSwagger.BL.Model;
using HorecaSwagger.DLEF.Exceptions;
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
        try
        {
            if (ctx.Customers.Any(x => x.Name == customer.Name && x.FirstName == customer.FirstName && x.Email == customer.Email))
            {
                var existingC = ctx.Customers.Where(x => x.Name == customer.Name && x.FirstName == customer.FirstName && x.Email == customer.Email).AsNoTracking().SingleOrDefault();
                existingC.Deleted = false;
                ctx.Customers.Update(existingC);
            }
            else
            {
                ctx.Customers.Add(CustomerMapper.MapToDB(customer, false));
            }
            SaveAndClear();
        }
        catch (Exception ex)
        {
            throw new DataException("Create Customer", ex);
        }
    }

    public void DeleteCustomer(Customer customer)
    {
        try
        {
            ctx.Customers.Update(CustomerMapper.MapToDB(customer, true));
            SaveAndClear();
        }
        catch(Exception ex)
        {
            throw new DataException("Delete Customer", ex);
        }
    }
    
    public Customer Read(int id)
    {
        try
        {
            return CustomerMapper.MapToDomain(ctx.Customers.Where(x => x.CustomerUUID == id && x.Deleted == false).AsNoTracking().SingleOrDefault());
        }
        catch(Exception ex)
        {
            throw new DataException($"Read Customer {id}", ex);
        }
    }

    public ICollection<Customer> ReadAll()
    {
        try
        {
            return ctx.Customers.Where(x => x.Deleted == false).AsNoTracking().ToList().Select(x => CustomerMapper.MapToDomain(x)).ToList();
        }
        catch(Exception ex)
        {
            throw new DataException("Read Customer", ex);

        }
    }

    public void UpdateCustomer(Customer customer)
    {
        try
        {
            ctx.Customers.Update(CustomerMapper.MapToDB(customer, false));
            SaveAndClear();
        }
        catch (Exception ex)
        {
            throw new DataException("Update Customer", ex);
        }
    }
}
