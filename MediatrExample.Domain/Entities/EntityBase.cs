using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;

namespace MediatrExample.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase(string id)
        {
            Id = id;
        }

        protected EntityBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        [JsonPropertyName("pk")]
        public string Id { get; protected set; }
    }
}
