using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

using Skeleton.Server.Infrastructure;

namespace Skeleton.Server.Api.Controllers;

public class UzivatelController : ODataController
{
    private readonly AppDbContext dbContext;

    public UzivatelController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(dbContext.Users);
    }
}
