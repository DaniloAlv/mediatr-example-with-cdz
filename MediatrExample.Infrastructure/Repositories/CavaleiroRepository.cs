using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MediatrExample.API.Repositories;
using MediatrExample.Domain.Entities;
using Newtonsoft.Json;

namespace MediatrExample.Infrastructure.Repositories
{
    public class CavaleiroRepository : ICavaleiroRepository
    {
        private const string tableName = "cavaleiros";
        private readonly IAmazonDynamoDB _dynamoDb;

        public CavaleiroRepository(IAmazonDynamoDB dynamoDb)
        {
            _dynamoDb = dynamoDb;
        }

        public async Task Adicionar(Cavaleiro cavaleiro, CancellationToken cancellationToken)
        {
            string cavaleiroComoJson = JsonConvert.SerializeObject(cavaleiro);
            Document cavaleiroComoDocumento = Document.FromJson(cavaleiroComoJson);
            var itemRequest = cavaleiroComoDocumento.ToAttributeMap();

            var request = new PutItemRequest()
            {
                TableName = tableName, 
                Item = itemRequest
            };

            var putItemResponse = await _dynamoDb.PutItemAsync(request, cancellationToken);

            if (putItemResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AmazonDynamoDBException("Não foi possível cadastrar esse cavaleiro!");
            }
        }

        public async Task Atualizar(Cavaleiro cavaleiro, CancellationToken cancellationToken)
        {
            var putItemRequest = new UpdateItemRequest()
            {
                TableName = tableName, 
                Key = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue(cavaleiro.Id.ToString()) }
                }, 
                UpdateExpression = $"SET referencia_imagem = {cavaleiro.ReferenciaImagem}",
            };

            var updateItemResponse = await _dynamoDb.UpdateItemAsync(putItemRequest, cancellationToken);

            if (updateItemResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AmazonDynamoDBException("Não foi possível atualizar os dados deste cavaleiro!");
            }
        }

        public async Task Remover(Guid id, CancellationToken cancellationToken)
        {
            var deleteItemRequest = new DeleteItemRequest()
            {
                TableName = tableName, 
                Key = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue(id.ToString()) }
                }
            };

            var deleteItemResponse = await _dynamoDb.DeleteItemAsync(deleteItemRequest, cancellationToken);

            if (deleteItemResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AmazonDynamoDBException("Não foi possível remover esse cavaleiro!");
            }
        }

        public async Task<Cavaleiro> ObterPorId(Guid id, CancellationToken cancellationToken)
        {
            var getItemRequest = new GetItemRequest()
            {
                TableName = tableName, 
                Key = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue(id.ToString()) }
                }
            };

            var getItemResponse = await _dynamoDb.GetItemAsync(getItemRequest, cancellationToken);

            if (getItemResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AmazonDynamoDBException("Não foi possível cadastrar esse cavaleiro!");
            }

            Document cavaleiroComoDocumento = Document.FromAttributeMap(getItemResponse.Item);
            string cavaleiroComoJson = cavaleiroComoDocumento.ToJson();

            Cavaleiro cavaleiro = JsonConvert.DeserializeObject<Cavaleiro>(cavaleiroComoJson);

            return await Task.FromResult(cavaleiro);
        }
    }
}
