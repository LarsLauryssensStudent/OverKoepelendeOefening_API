﻿namespace OverKoepelendeOefening_LarsLauryssens.Models
{
    public interface IStudent
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }


    }
}
