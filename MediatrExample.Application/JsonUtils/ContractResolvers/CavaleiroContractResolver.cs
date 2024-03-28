using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MediatrExample.Application.JsonUtils.ContractResolvers;

public class CavaleiroContractResolver : DefaultContractResolver
{
    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    {
        var properties = base.CreateProperties(type, memberSerialization);

        foreach (JsonProperty property in properties)
            property.PropertyName = property.UnderlyingName;

        return properties;
    }
}