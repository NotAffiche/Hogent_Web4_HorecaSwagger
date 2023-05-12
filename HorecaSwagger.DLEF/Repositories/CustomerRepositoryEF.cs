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
                //re add deleted cust
                var existingC = ctx.Customers.Where(x => x.Name == customer.Name && x.FirstName == customer.FirstName && x.Email == customer.Email).AsNoTracking().Single();
                existingC!.Deleted = false;
                ctx.Customers.Update(existingC);
            }
            else
            {
                //add new cust
                ctx.Customers.Add(CustomerMapper.MapToDB(customer, false));
            }
            SaveAndClear();
        }
        catch (Exception ex)
        {
            throw new RepositoryException("Create Customer", ex);
        }
    }

    public void DeleteCustomer(int id)
    {
        try
        {
            
            ctx.Customers.Update(CustomerMapper.MapToDB(Read(id), true));
            SaveAndClear();
        }
        catch(Exception ex)
        {
            throw new RepositoryException("Delete Customer", ex);
        }
    }
    
    public Customer Read(int id)
    {
        try
        {
            return CustomerMapper.MapToDomain(ctx.Customers.Where(x => x.CustomerUUID == id && x.Deleted == false).AsNoTracking().Single()!);
        }
        catch(Exception ex)
        {
            throw new RepositoryException($"Read Customer {id}", ex);
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
            throw new RepositoryException("Read Customers", ex);

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
            throw new RepositoryException("Update Customer", ex);
        }
    }
}
