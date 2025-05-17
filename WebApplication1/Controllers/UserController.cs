using Business;
using Business.Implements; // Agregar esta línea
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : GenericController<UserDto, User>
    {
        private readonly UserBusiness _userBusiness;

        public UserController(UserBusiness userBusiness, ILogger<UserController> logger)
            : base(
                userBusiness,
                logger,
                "usuario",
                dto => dto.Id,
                new[] { "/Name", "/Email" } // Campos permitidos para PATCH
            )
        {
            _userBusiness = userBusiness ?? throw new ArgumentNullException(nameof(userBusiness));
        }

        // No se requieren endpoints adicionales
    }
}