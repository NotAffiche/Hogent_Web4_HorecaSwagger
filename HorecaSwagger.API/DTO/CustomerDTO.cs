namespace HorecaSwagger.API.DTO;

public class CustomerDTO
{
    public CustomerDTO()
    {

    }
    public CustomerDTO(string name, string firstName, string street, int nr, string? nrAddition, string city, int postalCode, string country, string phone, string email, string password) : this()
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
    public CustomerDTO(int customerUUID, string name, string firstName, string street, int nr, string? nrAddition, string city, int postalCode, string country, string phone, string email, string password) : this(name, firstName, street, nr,
        nrAddition, city, postalCode, country, phone, email, password)
    {
        CustomerUUID = customerUUID;
    }

    public int CustomerUUID { get; set; }
    public string Name { get; set; }
    public string FirstName { get; set; }
    public string Street { get; set; }
    public int Nr { get; set; }
    public string? NrAddition { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
