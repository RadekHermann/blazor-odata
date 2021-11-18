using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

using Skeleton.Shared.Domain.Entity;
using Skeleton.Shared.Domain.IdentityEntity;

namespace Skeleton.Server.Api;

public static class EdmModelBuilder
{
    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EnableLowerCamelCase();

        //foreach (var entity in typeof(Domain.Domain).Assembly.GetExportedTypes().Where(w => w.GetCustomAttributes(typeof(ODataModelAttribute)).Any()))
        //{
        //    var attr = entity.GetCustomAttribute<ODataModelAttribute>();
        //    builder.GetType().GetMethod(nameof(builder.EntitySet))?.MakeGenericMethod(entity).Invoke(builder, new[] { attr.ControllerName ?? entity.Name });
        //}

        builder.EntitySet<Person>("Person");
        builder.EntitySet<Uzivatel>("Uzivatel");

        builder.EntityType<Uzivatel>().Ignore(i => i.AccessFailedCount);
        builder.EntityType<Uzivatel>().Ignore(i => i.ConcurrencyStamp);
        builder.EntityType<Uzivatel>().Ignore(i => i.LockoutEnabled);
        builder.EntityType<Uzivatel>().Ignore(i => i.PasswordHash);

        return builder.GetEdmModel();
    }
}