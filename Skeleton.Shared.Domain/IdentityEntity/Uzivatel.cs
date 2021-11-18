
using Microsoft.AspNetCore.Identity;

namespace Skeleton.Shared.Domain.IdentityEntity;

public class Uzivatel : IdentityUser
{
    public string Jmeno { get; set; }
    public string Prijmeni { get; set; }
}
