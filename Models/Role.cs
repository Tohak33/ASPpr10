using System;
using System.Collections.Generic;

namespace para.Models;

public partial class Role
{
    public int RoleID { get; set; }

    public string RoleName { get; set; } = null!;
    public ICollection<User> Users { get; set; } = [];

   
}
