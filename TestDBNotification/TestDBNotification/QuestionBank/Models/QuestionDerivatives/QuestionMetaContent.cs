using MongoDB.Bson.Serialization.Attributes;

namespace TestDBNotification.QuestionBank.Models.QuestionDerivatives
{
    [BsonIgnoreExtraElements]
    public class QuestionMetaContent
    {
        [BsonElement("code")]
        public string Code;

        [BsonElement("name")]
        public string Name;
    }
}
