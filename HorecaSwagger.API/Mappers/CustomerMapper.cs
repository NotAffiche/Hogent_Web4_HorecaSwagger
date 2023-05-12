using HorecaSwagger.API.DTO;
using HorecaSwagger.API.Exceptions;
using HorecaSwagger.BL.Model;

namespace HorecaSwagger.API.Mappers;

public static class CustomerMapper
{
    public static Customer MapToDomain(CustomerDTO dto)
    {
        try
        {
            return new Customer(dto.CustomerUUID, dto.Name, dto.FirstName, dto.Street, dto.Nr, dto.NrAddition, dto.City, dto.PostalCode, dto.Country, dto.Phone, dto.Email, dto.Password);
        }
        catch(Exception ex)
        {
            throw new APIException("Customer To Domain", ex);
        }
    }

    public static CustomerDTO MapToDTO(Customer dom)
    {
        try
        {
            return new CustomerDTO(dom.CustomerUUID, dom.Name, dom.FirstName, dom.Street, dom.Nr, dom.NrAddition, dom.City, dom.PostalCode, dom.Country, dom.Phone, dom.Email, dom.Password);
        }
        catch(Exception ex)
        {
            throw new APIException("Customer To DTO", ex);
        }
    }
}
