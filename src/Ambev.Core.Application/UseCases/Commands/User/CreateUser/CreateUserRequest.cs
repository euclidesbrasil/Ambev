﻿using Ambev.Core.Domain.Entities;

using MediatR;
using Ambev.Core.Domain.ValueObjects;
using Ambev.Core.Application.UseCases.DTOs;

namespace Ambev.Application.UseCases.Commands.User.CreateUser
{
    public class CreateUserRequest : UserDTO, IRequest<CreateUserResponse>
    {
        public int Id { get; internal set; }
    }
}
