using Microsoft.AspNetCore.Routing;

namespace AppInfra.Api.Abstract;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
