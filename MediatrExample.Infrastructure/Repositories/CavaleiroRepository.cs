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

        public async Task Adicionar(Cavaleiro cavaleiro)
        {
            string cavaleiroComoJson = JsonConvert.SerializeObject(cavaleiro);
            Document cavaleiroComoDocumento = Document.FromJson(cavaleiroComoJson);
            var itemRequest = cavaleiroComoDocumento.ToAttributeMap();

            var request = new PutItemRequest()
            {
                TableName = tableName, 
                Item = itemRequest
            };

            var putItemResponse = await _dynamoDb.PutItemAsync(request);

            if (putItemResponse.HttpStatusCode != System.Net.HttpStatusCode.OK &&
                putItemResponse.HttpStatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new AmazonDynamoDBException("Não foi possível cadastrar esse cavaleiro!");
            }
        }

        public async Task Atualizar(Cavaleiro cavaleiro)
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

            var updateItemResponse = await _dynamoDb.UpdateItemAsync(putItemRequest);

            if (updateItemResponse.HttpStatusCode != System.Net.HttpStatusCode.OK &&
                updateItemResponse.HttpStatusCode != System.Net.HttpStatusCode.Created)
            {
                throw new AmazonDynamoDBException("Não foi possível atualizar os dados deste cavaleiro!");
            }
        }

        public async Task Remover(Cavaleiro cavaleiro)
        {
            var deleteItemRequest = new DeleteItemRequest()
            {
                TableName = tableName, 
                Key = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue(cavaleiro.Id.ToString()) }
                }
            };

            var deleteItemResponse = await _dynamoDb.DeleteItemAsync(deleteItemRequest);

            if (deleteItemResponse.HttpStatusCode != System.Net.HttpStatusCode.OK &&
                deleteItemResponse.HttpStatusCode != System.Net.HttpStatusCode.NoContent)
            {
                throw new AmazonDynamoDBException("Não foi possível cadastrar esse cavaleiro!");
            }
        }

        public async Task<Cavaleiro> ObterPorId(Guid id)
        {
            var getItemRequest = new GetItemRequest()
            {
                TableName = tableName, 
                Key = new Dictionary<string, AttributeValue>
                {
                    { "id", new AttributeValue(id.ToString()) }
                }
            };

            var getItemResponse = await _dynamoDb.GetItemAsync(getItemRequest);

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
