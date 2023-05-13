using HorecaSwagger.API.DTO;
using HorecaSwagger.API.Exceptions;
using HorecaSwagger.API.Mappers;
using HorecaSwagger.BL.Exceptions;
using HorecaSwagger.BL.Model;
using HorecaSwagger.BL.Services;
using HorecaSwagger.DLEF.Exceptions;
using HorecaSwagger.DLEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HorecaSwagger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : Controller
{
    private CustomerService customerService;

    public CustomersController(IConfiguration config)
    {
        customerService = new CustomerService(new CustomerRepositoryEF(config.GetValue<string>("ConnectionStrings:MySqlConn")));
    }

    [HttpGet]
    public ActionResult<List<CustomerDTO>> Get()
    {
        try
        {
            List<CustomerDTO> customerDTOs = customerService.ReadAll().Select(x=>CustomerMapper.MapToDTO(x)).ToList();
            if (customerDTOs == null) return NotFound();
            return Ok(customerDTOs);
        }
        catch(APIException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (DomainException ex)
        {
            return StatusCode(422, ex.Message);
        }
        catch (Exception ex) when (ex is MapperException || ex is RepositoryException)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<CustomerDTO> Get(int id)
    {
        try
        {
            CustomerDTO c = CustomerMapper.MapToDTO(customerService.Read(id));
            if (c == null) return NotFound();
            return Ok(c);
        }
        catch (APIException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (DomainException ex)
        {
            return StatusCode(422, ex.Message);
        }
        catch (Exception ex) when (ex is MapperException || ex is RepositoryException)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] CustomerDTO dto)
    {
        try
        {
            customerService.Create(CustomerMapper.MapToDomain(dto));
            return Ok();
        }
        catch (APIException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (DomainException ex)
        {
            return StatusCode(422, ex.Message);
        }
        catch (Exception ex) when (ex is MapperException || ex is RepositoryException)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    public ActionResult Put([FromBody] CustomerDTO dto)
    {
        try
        {
            customerService.Update(CustomerMapper.MapToDomain(dto));
            return Ok();
        }
        catch (APIException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (DomainException ex)
        {
            return StatusCode(422, ex.Message);
        }
        catch (Exception ex) when (ex is MapperException || ex is RepositoryException)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            CustomerDTO c = CustomerMapper.MapToDTO(customerService.Read(id));
            if (c == null) return NotFound();
            customerService.Delete(id);
            return Ok();
        }
        catch (APIException ex)
        {
            return StatusCode(400, ex.Message);
        }
        catch (DomainException ex)
        {
            return StatusCode(422, ex.Message);
        }
        catch (Exception ex) when (ex is MapperException || ex is RepositoryException)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
