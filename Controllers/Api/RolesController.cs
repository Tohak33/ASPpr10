using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using para.DTOs;
using para.Services;

namespace para.Controllers.Api;

[Route("api/roles")]
public class RolesController(IService<RoleDTO, CreateRoleDTO, UpdateRoleDTO, PatchRoleDTO> service)
    : CrudController<RoleDTO, CreateRoleDTO, UpdateRoleDTO, PatchRoleDTO>(service)
{
}
