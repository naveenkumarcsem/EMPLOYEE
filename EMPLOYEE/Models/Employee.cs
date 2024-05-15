using System;

public class Employee
{
    public int EmployeeID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public decimal Salary { get; set; }
    public string Phone { get; set; }
    public Address PrimaryAddress { get; set; }
    public Address TemporaryAddress { get; set; }
}

public class Address
{
    public int AddressID { get; set; }
    public string DoorNo { get; set; }
    public string StreetName { get; set; }
    public string StreetName2 { get; set; }
    public string Landmark { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string AddressType { get; set; } // 'Primary' or 'Temporary'
}
