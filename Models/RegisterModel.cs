using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityApi.Models
{
  public class RegisterModel : BaseModel
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

}