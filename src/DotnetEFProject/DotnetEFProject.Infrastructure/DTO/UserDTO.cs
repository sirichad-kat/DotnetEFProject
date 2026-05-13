using DotnetEFProject.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.DTO
{
    public class AddUserInput
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public DateOnly? BirthDate { get; set; }
    }

    public class GetUserListOutput
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!; 
        public DateOnly? BirthDate { get; set; }
    }

    public class GetUserInput
    {
        public int Id { get; set; }
    }
    public class GetUserOutput
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Email { get; set; } 
    }
     

}
