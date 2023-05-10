using HorecaSwagger.BL.Interfaces;
using HorecaSwagger.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Services;

public class DishService
{
    private IDishRepository repo;

	public DishService(IDishRepository repo)
	{
		this.repo = repo;
	}

	public void Create(Dish d)
	{
		repo.CreateDish(d);
	}

	public Dish Read(int id)
	{
		return repo.Read(id);
	}

	public ICollection<Dish> ReadAll()
	{
		return repo.ReadAll();
	}

	public void Update(Dish d)
	{
		repo.UpdateDish(d);
	}

	public void Delete(Dish d)
	{
		repo.DeleteDish(d);
	}
}
