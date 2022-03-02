using MongoDB.Bson.Serialization.Attributes;

namespace TestDBNotification.QuestionBank.Models.QuestionDerivatives
{
    [BsonIgnoreExtraElements]
    public class QuestionDetail
    {
        [BsonElement("code")]
        public string Code;

        [BsonElement("name")]
        public string Name;

        [BsonElement("path")]
        public string Path;

        [BsonElement("orderby")]
        public string OrderBy;
    }
}
