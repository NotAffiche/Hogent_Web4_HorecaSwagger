using HorecaSwagger.BL.Interfaces;
using HorecaSwagger.BL.Model;
using HorecaSwagger.BL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Services;

public class CustomerService
{
    private ICustomerRepository repo;
	private const int _ITER = 3;

	public CustomerService(ICustomerRepository repo)
	{
		this.repo = repo;
	}

	public void Create(Customer c)
	{
		c.PasswordSalt = PasswordHasher.GenerateSalt();
		c.Password = PasswordHasher.ComputeHash(c.Password, c.PasswordSalt, _ITER);
		repo.CreateCustomer(c);
	}

	public Customer Read(int id)
	{
		return repo.Read(id);
	}

	public ICollection<Customer> ReadAll()
	{
		return repo.ReadAll();
	}

	public void Update(Customer c)
	{
		c.Password = PasswordHasher.ComputeHash(c.Password, c.PasswordSalt, _ITER);
		repo.UpdateCustomer(c);
	}

	public void Delete(int id)
	{
		repo.DeleteCustomer(id);
	}
}
