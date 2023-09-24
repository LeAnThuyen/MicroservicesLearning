using AutoMapper;
using System.Reflection;

namespace Ordering.Application.Common.Mappings
{
    public class MappingProfile
    {

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);
            const string mappingMethodName = nameof(IMapFrom<Object>.Mapping);

            bool HasInterfaces(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
            var types = assembly.GetExportedTypes().Where(c => c.GetInterfaces().Any(HasInterfaces)).ToList();
            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces()
                        .Where(HasInterfaces).ToList();

                    if (interfaces.Count <= 0) continue;

                    foreach (var @interface in interfaces)
                    {
                        var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                        interfaceMethodInfo?.Invoke(instance, new object[] { this });
                    }
                }
            }
        }
    }
}
