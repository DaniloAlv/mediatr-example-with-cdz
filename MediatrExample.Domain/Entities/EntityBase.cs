using Amazon.DynamoDBv2.DataModel;

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
