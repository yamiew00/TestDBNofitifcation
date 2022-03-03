using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using TestDBNotification.QuestionBank.Models.QuestionDerivatives;

namespace TestDBNotification.QuestionBank.Models
{
    [BsonIgnoreExtraElements]
    public class Question : MongoDocument
    {
        [BsonElement("userUID")]
        public string UserUID;

        //[BsonElement("fileUID")]
        //public string FileUID;

        [BsonElement("bookID")]
        public string BookID;

        //[BsonElement("UID")]
        //public string UID;

        //[BsonElement("createTime")]
        //public DateTime? CreateTime;

        //[BsonElement("updateTime")]
        //public DateTime? UpdateTime;

        //[BsonElement("department")]
        //public string Department;

        //[BsonElement("key")]
        //public IEnumerable<string> Key;

        //[BsonElement("questionImage")]
        //public string QuestionImage;

        //[BsonElement("questionPdf")]
        //public string QuestionPdf;

        //[BsonElement("detailImage")]
        //public IEnumerable<QuestionDetail> DetailImage;

        //[BsonElement("detailDoc")]
        //public IEnumerable<QuestionDetail> DetailDoc;

        //[BsonElement("soundtracks")]
        //public object SoundTracks;

        //[BsonElement("metadata")]
        //public IEnumerable<QuestionMetaData> MetaData;

        //研究bsonSerializer
        [BsonElement("metadata")]
        [BsonSerializer(typeof(MetaDataDeserializer))]
        public MetaData MetaData;

        //[BsonElement("content")]
        //public string Content;

        //[BsonElement("answerInfos")]
        //public IEnumerable<QuestionAnswerInfo> AnswerInfos;

        //[BsonElement("htmlParts")]
        //public QuestionHtmlPart HtmlParts;

        
    }
}
