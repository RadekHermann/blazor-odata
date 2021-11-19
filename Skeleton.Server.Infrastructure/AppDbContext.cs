
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Skeleton.Shared.Domain.IdentityEntity;

namespace Skeleton.Server.Infrastructure;


public class AppDbContext : IdentityDbContext<Uzivatel, Role, string>
{
    public AppDbContext()
    {

    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}
