using System;
using Newtonsoft.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDotnet.Model
{
    public class Chore
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Text { get; set; }
        public int FrequencyInDays { get; set; }
        public User AssignedTo { get; set; }
        public DateTime? CompletedOn { get; set; }
        
        public override string ToString() => JsonConvert.SerializeObject(this);     
    }
}