using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[Route("api/users")]
public class UsersController(IService<UserDTO, CreateUserDTO, UpdateUserDTO, PatchUserDTO> service)
    : CrudController<UserDTO, CreateUserDTO, UpdateUserDTO, PatchUserDTO>(service)
{
}
