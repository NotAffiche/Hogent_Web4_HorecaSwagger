using HorecaSwagger.API.DTO;
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
    public ActionResult<Customer> Get()
    {
        try
        {
            List<Customer> customers = (List<Customer>)customerService.ReadAll();
            if (customers == null) return NotFound();
            return Ok(customers);
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Customer> Get(int id)
    {
        try
        {
            Customer c = customerService.Read(id);
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
            customerService.Create(new Customer(dto.CustomerUUID, dto.Name, dto.FirstName, dto.Street, dto.Nr, dto.NrAddition, dto.City, dto.PostalCode, dto.Country, dto.Phone, dto.Email, dto.Password));
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}
