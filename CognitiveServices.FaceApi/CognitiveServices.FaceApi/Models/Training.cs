using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Models
{
    public class Training
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "createdDateTime")]
        public string CreatedDateTime { get; set; }
        [JsonProperty(PropertyName = "lastActionDateTime")]
        public string LastActionDateTime { get; set; }
        [JsonProperty(PropertyName = "message")]
        public object Message { get; set; }

        public string GetStatus()
        {
            switch (Status)
            {
                case "notstarted":
                    return "Não inicializado";
                case "running":
                    return "Em treinamento";
                case "succeeded":
                    return "Sucesso";
                default:
                    return "Falhou";
            }
        }
    }
}
