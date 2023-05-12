using HorecaSwagger.BL.Model;
using HorecaSwagger.DLEF.Exceptions;
using HorecaSwagger.DLEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.DLEF.Mappers;

public static class CustomerMapper
{
    public static Customer MapToDomain(CustomerEF db)
    {
        try
        {
            return new Customer(db.CustomerUUID, db.Name, db.FirstName, db.Street, db.Nr, db.NrAddition, db.City, db.PostalCode, db.Country, db.Phone, db.Email, db.Password);
        }
        catch (Exception ex)
        {
            throw new MapperException("Customer To Domain", ex);
        }
    }

    public static CustomerEF MapToDB(Customer dom, bool deleted)
    {
        try
        {
            return new CustomerEF()
            {
                CustomerUUID = dom.CustomerUUID,
                Name = dom.Name,
                FirstName = dom.FirstName,
                Street = dom.Street,
                Nr = dom.Nr,
                NrAddition = dom.NrAddition,
                City = dom.City,
                PostalCode = dom.PostalCode,
                Country = dom.Country,
                Phone = dom.Phone,
                Email = dom.Email,
                Password = dom.Password,
                Deleted = deleted
            };
        }
        catch (Exception ex)
        {
            throw new MapperException("Customer To DB", ex);
        }
    }
}
