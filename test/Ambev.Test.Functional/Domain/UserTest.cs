﻿using System;
using System.Collections.Generic;
using Ambev.Core.Domain.Entities;
using Ambev.Core.Domain.Enum;
using Ambev.Core.Domain.Interfaces;
using Ambev.Core.Domain.ValueObjects;
using NSubstitute;
using Xunit;
namespace Ambev.Test.Domain
{
    public class UserTests
    {
        [Fact]
        public void Constructor_Should_SetPropertiesCorrectly()
        {
            // Arrange
            var address = new Address();
            var tokenService = Substitute.For<IJwtTokenService>();
            tokenService.HashPassword("password").Returns("hashedPassword");

            // Act
            var user = new User("email@example.com", "username", "password", "firstname", "lastname", address, "123456789", UserStatus.Active, UserRole.Admin, tokenService);

            // Assert
            Assert.Equal("email@example.com", user.Email);
            Assert.Equal("username", user.Username);
            Assert.Equal("hashedPassword", user.PasswordHash);
            Assert.Equal("firstname", user.Firstname);
            Assert.Equal("lastname", user.Lastname);
            Assert.Equal(address, user.Address);
            Assert.Equal("123456789", user.Phone);
            Assert.Equal(UserStatus.Active, user.Status);
            Assert.Equal(UserRole.Admin, user.Role);
        }

        [Fact]
        public void UpdateUserInfo_Should_UpdatePropertiesCorrectly()
        {
            // Arrange
            var user = new User(1);
            var newAddress = new Address();

            // Act
            user.UpdateUserInfo("newFirstname", "newLastname", newAddress, "987654321", UserStatus.Inactive, UserRole.Admin);

            // Assert
            Assert.Equal("newFirstname", user.Firstname);
            Assert.Equal("newLastname", user.Lastname);
            Assert.Equal(newAddress, user.Address);
            Assert.Equal("987654321", user.Phone);
            Assert.Equal(UserStatus.Inactive, user.Status);
            Assert.Equal(UserRole.Admin, user.Role);
        }

        [Fact]
        public void ChangePassword_Should_UpdatePasswordHash()
        {
            // Arrange
            var user = new User(1);
            var tokenService = Substitute.For<IJwtTokenService>();
            tokenService.HashPassword("newPassword").Returns("newHashedPassword");

            // Act
            user.ChangePassword("newPassword", tokenService);

            // Assert
            Assert.Equal("newHashedPassword", user.PasswordHash);
        }

        [Fact]
        public void ValidatePassword_Should_ReturnTrue_When_PasswordIsValid()
        {
            // Arrange
            var user = new User(1);
            var tokenService = Substitute.For<IJwtTokenService>();
            tokenService.VerifyPassword("password", "hashedPassword").Returns(true);

            // Act
            var isValid = user.ValidatePassword("password", "hashedPassword", tokenService);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void UpdateName_Should_UpdateFirstnameAndLastname()
        {
            // Arrange
            var user = new User(1);

            // Act
            user.UpdateName("newFirst", "newLast");

            // Assert
            Assert.Equal("newFirst", user.Firstname);
            Assert.Equal("newLast", user.Lastname);
        }

        [Fact]
        public void UpdateAdress_Should_UpdateAddress()
        {
            // Arrange
            var user = new User(1);
            var newAddress = new Address();

            // Act
            user.UpdateAdress(newAddress);

            // Assert
            Assert.Equal(newAddress, user.Address);
        }

        [Fact]
        public void Activate_Should_SetStatusToActive()
        {
            // Arrange
            var user = new User(1);

            // Act
            user.Activate();

            // Assert
            Assert.Equal(UserStatus.Active, user.Status);
        }

        [Fact]
        public void Deactivate_Should_SetStatusToInactive()
        {
            // Arrange
            var user = new User(1);

            // Act
            user.Deactivate();

            // Assert
            Assert.Equal(UserStatus.Inactive, user.Status);
        }

        [Fact]
        public void Suspend_Should_SetStatusToSuspended()
        {
            // Arrange
            var user = new User(1);

            // Act
            user.Suspend();

            // Assert
            Assert.Equal(UserStatus.Suspended, user.Status);

        }
    }
}
