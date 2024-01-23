using Amazon.DynamoDBv2.DataModel;
using MediatrExample.API.ViewModels;

namespace MediatrExample.API.Domain
{
    [DynamoDBTable("cavaleiros")]
    public class Cavaleiro : Entity
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


        public CavaleiroViewModel ParaViewModel()
        {
            return new CavaleiroViewModel
            {
                Id = Id, 
                Nome = Nome,
                Armadura = Armadura,
                Constelacao = Constelacao, 
                GolpePrincipal = GolpePrincipal, 
                LocalDeTreinamento = LocalDeTreinamento
            };
        }
    }
}
