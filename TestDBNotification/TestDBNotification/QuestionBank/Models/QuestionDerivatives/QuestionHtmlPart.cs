using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDBNotification.QuestionBank.Models.QuestionDerivatives
{
    [BsonIgnoreExtraElements]
    public class QuestionHtmlPart
    {
        [BsonElement("content")]
        public string Content;

        [BsonElement("answer")]
        public string Answer;

        [BsonElement("analyze")]
        public string Analyze;
    }
}
