using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace TestDBNotification.QuestionBank.Models.QuestionDerivatives
{
    [BsonIgnoreExtraElements]
    public class QuestionMetaData
    {
        [BsonElement("code")]
        public string Code;

        [BsonElement("name")]
        public string Name;

        [BsonElement("content")]
        public IEnumerable<QuestionMetaContent> Content;
    }
}
