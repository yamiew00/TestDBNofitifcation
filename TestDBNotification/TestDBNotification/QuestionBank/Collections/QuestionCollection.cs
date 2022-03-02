using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDBNotification.QuestionBank.Models;

namespace TestDBNotification.QuestionBank.Collections
{
    //test
    public class QuestionCollection
    {
        public string Subject { get; private set; }

        private Dictionary<ObjectId, Question> Dictionary;

        private Task InitTask;

        public QuestionCollection(string subject, IMongoCollection<Question> mongoCollection)
        {
            Subject = subject;
            Dictionary = new Dictionary<ObjectId, Question>();

            //所有資料先載入，並建索引
            //Dictionary = mongoCollection.Find(item => true)
            //                            .ToEnumerable()
            //                            .ToDictionary(question => question._id);
            InitTask = Init(mongoCollection);
        }

        private Task Init(IMongoCollection<Question> mongoCollection)
        {
            return Task.Run(() =>
            {
                Dictionary = mongoCollection.Find(item => true)
                                            .ToEnumerable()
                                            .ToDictionary(question => question._id);
            });
        } 

        public Action<Question> OnInsert()
        {
            return (question) =>
            {
                Dictionary[question._id] = question;
            };
        }


        public async Task<IEnumerable<Question>> Get()
        {
            await InitTask;
            return Dictionary.Values;
        }

        public async Task<int> GetInt()
        {
            await InitTask;
            return Dictionary.Count;
        }
    }
}
