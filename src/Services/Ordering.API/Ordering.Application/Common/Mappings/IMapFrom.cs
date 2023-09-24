using AutoMapper;

namespace Ordering.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profiles) => profiles.CreateMap(typeof(T), GetType());
    }
}
