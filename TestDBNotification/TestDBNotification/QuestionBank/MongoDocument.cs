using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDBNotification.QuestionBank
{
    [BsonIgnoreExtraElements]
    public class MongoDocument
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId _id;
    }
}
