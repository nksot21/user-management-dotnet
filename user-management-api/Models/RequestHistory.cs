using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace user_management_api.Models
{
    public class RequestHistory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public DateTime RequestDateTime { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public RequestHistory() { }
    }
}
