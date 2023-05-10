using HorecaSwagger.BL.Interfaces;
using HorecaSwagger.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Services;

public class CustomerService
{
    private ICustomerRepository repo;

	public CustomerService(ICustomerRepository repo)
	{
		this.repo = repo;
	}

	public void Create(Customer c)
	{
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
		repo.UpdateCustomer(c);
	}

	public void Delete(Customer c)
	{
		repo.DeleteCustomer(c);
	}
}
