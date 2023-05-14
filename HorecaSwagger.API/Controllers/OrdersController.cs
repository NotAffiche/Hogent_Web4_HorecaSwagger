using HorecaSwagger.API.DTO;
using HorecaSwagger.API.Exceptions;
using HorecaSwagger.API.Mappers;
using HorecaSwagger.BL.Exceptions;
using HorecaSwagger.BL.Services;
using HorecaSwagger.DLEF.Exceptions;
using HorecaSwagger.DLEF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HorecaSwagger.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : Controller
{
    private OrderService orderService;
    private CustomerService customerService;
    private DishService dishService;

    public OrdersController(IConfiguration config)
    {
        string conn = config.GetValue<string>("ConnectionStrings:MySqlConn")!;
        orderService = new OrderService(new OrderRepositoryEF(conn));
        customerService = new CustomerService(new CustomerRepositoryEF(conn));
        dishService = new DishService(new DishRepositoryEF(conn));
    }

    [HttpGet]
    public ActionResult<List<OrderDTO>> Get()
    {
        try
        {
            List<OrderDTO> orders = orderService.ReadAll().Select(x => OrderMapper.MapToDTO(x)).ToList();
            if (orders == null) return NotFound();
            return Ok(orders);
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

    [HttpGet("{id}")]
    public ActionResult<List<OrderDTO>> Get(int id)
    {
        try
        {
            List<OrderDTO> orders = orderService.ReadOrdersByCustomer(id).Select(x => OrderMapper.MapToDTO(x)).ToList();
            if (orders == null) return NotFound();
            return Ok(orders);
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
    public ActionResult Post([FromBody] OrderDTO dto)
    {
        try
        {
            orderService.Create(OrderMapper.MapToDomain(dto, customerService, dishService), dishService);
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
            if (ex.InnerException != null && ex.InnerException.Message.Contains("order too high (not enough available)"))
            {
                return StatusCode(409, ex.InnerException?.Message);
            }
            return StatusCode(500, ex.Message + " " + ex.InnerException?.Message);
        }
    }

    [HttpPut]
    public ActionResult Put([FromBody] OrderDTO dto)
    {
        try
        {
            orderService.Update(OrderMapper.MapToDomain(dto, customerService, dishService), dishService);
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
            if (ex.InnerException != null && ex.InnerException.Message.Contains("order too high (not enough available)"))
            {
                return StatusCode(409, ex.InnerException?.Message);
            }
            return StatusCode(500, ex.Message + " " + ex.InnerException?.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            OrderDTO o = OrderMapper.MapToDTO(orderService.Read(id));
            if (o == null) return NotFound();
            orderService.Delete(id);
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
