using AutoMapper;
using RouteProvider.API.Model;
using RouteProvider.API.Model.Requests;

namespace RouteProvider.API.Mapping;

public sealed class FiltersMappingProfile : Profile
{
    public FiltersMappingProfile()
    {
        CreateMap<Filter, ProviderOneSearchRequest>();
        CreateMap<Filter, ProviderTwoSearchRequest>();
    }
}