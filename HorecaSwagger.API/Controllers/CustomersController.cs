using HorecaSwagger.API.DTO;
using HorecaSwagger.API.Mappers;
using HorecaSwagger.BL.Model;
using HorecaSwagger.BL.Services;
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
        catch (Exception ex)
        {
            return StatusCode(500);
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
        catch (Exception ex)
        {
            return StatusCode(500);
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
        catch (Exception ex)
        {
            return StatusCode(500);
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
        catch (Exception ex)
        {
            return StatusCode(500);
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
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}
