using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;
using System.IO;
using System.Numerics;
using System.Xml.Linq;
using HorecaSwagger.BL.Exceptions;

namespace HorecaSwagger.BL.Model;

public class Customer
{
    private int _customerUUID;
    private string _name;
    private string _firstName;
    private string _street;
    private int _nr;
    private string _city;
    private int _postalCode;
    private string _country;
    private string _phone;
    private string _email;
    private string _password;

    public Customer(string name, string firstName, string street, int nr, string? nrAddition, string city, int postalCode, string country, string phone, string email, string password)
    {
        Name = name;
        FirstName = firstName;
        Street = street;
        Nr = nr;
        NrAddition = nrAddition;
        City = city;
        PostalCode = postalCode;
        Country = country;
        Phone = phone;
        Email = email;
        Password = password;
    }
    public Customer(int customerUUID, string name, string firstName, string street, int nr, string? nrAddition, string city, int postalCode, string country, string phone, string email, string password) : this(name, firstName,
        street, nr, nrAddition, city, postalCode, country, phone, email, password)
    {
        CustomerUUID = customerUUID;
    }

    public int CustomerUUID
    {
        get { return _customerUUID; }
        set
        {
            if (value < 0) throw new DomainException("Invalid CustomerUUID", new ArgumentException());
            _customerUUID = value;
        }
    }
    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid Name", new ArgumentException());
            _name = value;
        }
    }
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid FirstName", new ArgumentException());
            _firstName = value;
        }
    }
    public string Street
    {
        get { return _street; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid Street", new ArgumentException());
            _street = value;
        }
    }
    public int Nr
    {
        get { return _nr; }
        set
        {
            if (value <= 0) throw new DomainException("Invalid Nr", new ArgumentException());
            _nr = value;
        }
    }
    public string? NrAddition { get; set; }
    public string City
    {
        get { return _city; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid City", new ArgumentException());
            _city = value;
        }
    }
    public int PostalCode
    {
        get { return _postalCode; }
        set
        {
            if (value < 1000 || value > 9999) throw new DomainException("Invalid PostalCode", new ArgumentException());
            _postalCode = value;
        }
    }
    public string Country
    {
        get { return _country; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid Country", new ArgumentException());
            _country = value;
        }
    }
    public string Phone
    {
        get { return _phone; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid Phone", new ArgumentException());
            _phone = value;
        }
    }
    public string Email
    {
        get { return _email; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid Email", new ArgumentException());
            _email = value;
        }
    }
    public string Password
    {
        get { return _password; }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new DomainException("Invalid Password", new ArgumentException());
            _password = value;
        }
    }
}
