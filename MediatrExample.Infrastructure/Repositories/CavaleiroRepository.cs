using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using MediatrExample.API.Repositories;
using MediatrExample.Domain.Entities;


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
            string cavaleiroComoJson = JsonSerializer.Serialize(cavaleiro);
            var itemRequest = Document.FromJson(cavaleiroComoJson).ToAttributeMap();

            var request = new PutItemRequest()
            {
                TableName = tableName, 
                Item = itemRequest,                 
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
                    { "pk", new AttributeValue(cavaleiro.Id.ToString()) }
                }, 
                UpdateExpression = @"SET nome               = :nome, 
                                         local_treinamento  = :local_treinamento, 
                                         armadura           = :armadura, 
                                         constelacao        = :constelacao, 
                                         golpe_principal    = :golpe_principal, 
                                         referencia_imagem  = :referencia_imagem", 
                ExpressionAttributeValues = new Dictionary<string, AttributeValue>
                {
                    { ":nome", new AttributeValue(cavaleiro.Nome)}, 
                    { ":local_treinamento", new AttributeValue(cavaleiro.LocalDeTreinamento)}, 
                    { ":armadura", new AttributeValue(cavaleiro.Armadura) }, 
                    { ":constelacao", new AttributeValue(cavaleiro.Constelacao)}, 
                    { ":golpe_principal", new AttributeValue(cavaleiro.GolpePrincipal)}, 
                    { ":referencia_imagem", new AttributeValue(cavaleiro.ReferenciaImagem)}
                }
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
                    { "pk", new AttributeValue(id.ToString()) }
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
                    { "pk", new AttributeValue(id.ToString()) }
                }, 
            };

            var getItemResponse = await _dynamoDb.GetItemAsync(getItemRequest, cancellationToken);

            if (getItemResponse.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new AmazonDynamoDBException("Não foi possível obter esse cavaleiro!");
            }

            Document cavaleiroComoDocumento = Document.FromAttributeMap(getItemResponse.Item);
            string cavaleiroComoJson = cavaleiroComoDocumento.ToJson();

            var cavaleiro = JsonSerializer.Deserialize<Cavaleiro>(cavaleiroComoJson);
            return cavaleiro!;
        }
    }
}
