using Microsoft.AspNetCore.Routing;

namespace ApplicationInfra.Api.Abstract;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
