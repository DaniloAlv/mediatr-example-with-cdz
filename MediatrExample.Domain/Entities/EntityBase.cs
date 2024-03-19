using Amazon.DynamoDBv2.DataModel;

namespace MediatrExample.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        [DynamoDBHashKey("id")]
        public Guid Id { get; private set; }
    }
}
