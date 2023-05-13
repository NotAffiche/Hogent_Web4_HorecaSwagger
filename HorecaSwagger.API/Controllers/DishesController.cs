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
public class DishesController : Controller
{
    private DishService dishService;

    public DishesController(IConfiguration config)
    {
        dishService = new DishService(new DishRepositoryEF(config.GetValue<string>("ConnectionStrings:MySqlConn")));
    }

    [HttpGet]
    public ActionResult<List<DishDTO>> Get()
    {
        try
        {
            List<DishDTO> dishes = dishService.ReadAll().Select(x=>DishMapper.MapToDTO(x)).ToList();
            if (dishes == null) return NotFound();
            return Ok(dishes);
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
    public ActionResult<DishDTO> Get(int id)
    {
        try
        {
            DishDTO d = DishMapper.MapToDTO(dishService.Read(id));
            if (d == null) return NotFound();
            return Ok(d);
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

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost]
    public ActionResult Post([FromBody] DishDTO dto)
    {
        try
        {
            dishService.Create(DishMapper.MapToDomain(dto));
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPut]
    public ActionResult Put([FromBody] DishDTO dto)
    {
        try
        {
            dishService.Update(DishMapper.MapToDomain(dto));
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

    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        try
        {
            DishDTO d = DishMapper.MapToDTO(dishService.Read(id));
            if (d == null) return NotFound();
            dishService.Delete(id);
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
