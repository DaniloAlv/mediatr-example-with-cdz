using Amazon.DynamoDBv2.DataModel;

namespace MediatrExample.Domain.Entities
{
    [DynamoDBTable("cavaleiros")]
    public class Cavaleiro : EntityBase
    {
        [DynamoDBProperty("nome")]
        public string Nome { get; set; }

        [DynamoDBProperty("local_treinamento")]
        public string LocalDeTreinamento { get; set; }

        [DynamoDBProperty("armadura")]
        public string Armadura { get; set; }

        [DynamoDBProperty("constelacao")]
        public string Constelacao { get; set; }

        [DynamoDBProperty("golpe_principal")]
        public string GolpePrincipal { get; set; }

        [DynamoDBProperty("referencia_imagem")]
        public string ReferenciaImagem { get; set; }
    }
}
