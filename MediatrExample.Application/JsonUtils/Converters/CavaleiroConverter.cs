using MediatrExample.Domain.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MediatrExample.Application.JsonUtils.Converters;

public class CavaleiroConverter : JsonConverter<Cavaleiro>
{
    public override Cavaleiro? ReadJson(JsonReader reader, Type objectType, Cavaleiro? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JObject jObj = JObject.Load(reader);
        var cavaleiro = new Cavaleiro(jObj["pk"].ToString());

        serializer.Populate(jObj.CreateReader(), cavaleiro);
        
        // cavaleiro.LocalDeTreinamento = jObj["local_treinamento"].ToString();
        // cavaleiro.ReferenciaImagem = jObj["referencia_imagem"].ToString();
        // cavaleiro.GolpePrincipal = jObj["golpe_principal"].ToString();

        return cavaleiro;
    }

    public override void WriteJson(JsonWriter writer, Cavaleiro? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}