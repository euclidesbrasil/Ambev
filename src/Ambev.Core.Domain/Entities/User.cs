﻿using Ambev.Core.Domain.Common;
using Ambev.Core.Domain.Enum;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;

namespace Ambev.Core.Domain.Entities;

public sealed class User : BaseEntity
{
    public string Email { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string Firstname { get; private set; }
    public string Lastname { get; private set; }
    public Address Address { get; private set; }
    public string Phone { get; private set; }
    public UserStatus Status { get; private set; }
    public UserRole Role { get; private set; }

    private User() { } // Para o EF Core
    public User(int id)
    {
         Id = id;
    }

    public User(string email, string username, string password, string firstname, string lastname,
        Address address, string phone, UserStatus status, UserRole role, IJwtTokenService _tokenService)
    {
        Email = email;
        Username = username;
        PasswordHash = _tokenService.HashPassword(password);
        Firstname = firstname;
        Lastname = lastname;
        Address = address;
        Phone = phone;
        Status = status;
        Role = role;
    }

    public void UpdateUserInfo(string firstname, string lastname, Address address, string phone, UserStatus status, UserRole role)
    {
        Firstname = firstname;
        Lastname = lastname;
        Address = address;
        Phone = phone;
        Status = status;
        Role = role;
    }

    public void ChangePassword(string newPassword, IJwtTokenService _tokenService)
    {
        PasswordHash = _tokenService.HashPassword(newPassword);
    }

    public bool ValidatePassword(string password, string hashedPassword, IJwtTokenService _tokenService)
    {
        return _tokenService.VerifyPassword(password, hashedPassword);
    }

    public void UpdateName(string first, string last)
    {
        Firstname = first;
        Lastname = last;
    }

    public void UpdateAdress(Address newAdress)
    {
        Address = newAdress;
    }

    /// <summary>
    /// Activates the user account.
    /// Changes the user's status to Active.
    /// </summary>
    public void Activate()
    {
        Status = UserStatus.Active;
    }

    /// <summary>
    /// Deactivates the user account.
    /// Changes the user's status to Inactive.
    /// </summary>
    public void Deactivate()
    {
        Status = UserStatus.Inactive;
    }

    /// <summary>
    /// Blocks the user account.
    /// Changes the user's status to Blocked.
    /// </summary>
    public void Suspend()
    {
        Status = UserStatus.Suspended;
    }
}