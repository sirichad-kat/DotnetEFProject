using DotnetEFProject.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetEFProject.Infrastructure.DTO
{
    public sealed record AddUserInput(string FullName, string Email, string? Bio, DateOnly? BirthDate); 

    public sealed record GetUserListOutput(int Id, string FullName, DateOnly? BirthDate);

    public sealed record GetUserInput(int Id);
    public sealed record GetUserOutput(int Id, string FullName, string? Email);

    public sealed record DeleteUserInput(int Id); 

    public sealed record ModifyUserInput(int Id,  string? Email, string? Bio); 
}
