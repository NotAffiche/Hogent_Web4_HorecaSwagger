using HorecaSwagger.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaSwagger.BL.Interfaces;

public interface ICustomerRepository
{
    void CreateCustomer(Customer customer);
    ICollection<Customer> ReadAll();
    Customer Read(int id);
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
}
