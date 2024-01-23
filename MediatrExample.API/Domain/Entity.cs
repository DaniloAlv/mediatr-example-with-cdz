using Amazon.DynamoDBv2.DataModel;

namespace MediatrExample.API.Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        [DynamoDBHashKey("id")]
        public Guid Id { get; private set; }
    }
}
