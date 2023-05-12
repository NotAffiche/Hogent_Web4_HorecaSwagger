using HorecaSwagger.API.DTO;
using HorecaSwagger.API.Mappers;
using HorecaSwagger.BL.Services;
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
        orderService = new OrderService(new OrderRepositoryEF(config.GetValue<string>("ConnectionStrings:MySqlConn")!));
        customerService = new CustomerService(new CustomerRepositoryEF(config.GetValue<string>("ConnectionStrings:MySqlConn")!));
        dishService = new DishService(new DishRepositoryEF(config.GetValue<string>("ConnectionStrings:MySqlConn")!));
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
        catch (Exception ex)
        {
            return StatusCode(500);
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
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] OrderDTO dto)
    {
        try
        {
            orderService.Create(OrderMapper.MapToDomain(dto, customerService, dishService));
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [HttpPut]
    public ActionResult Put([FromBody] OrderDTO dto)
    {
        try
        {
            orderService.Update(OrderMapper.MapToDomain(dto, customerService, dishService));
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
            OrderDTO o = OrderMapper.MapToDTO(orderService.Read(id));
            if (o == null) return NotFound();
            orderService.Delete(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}
