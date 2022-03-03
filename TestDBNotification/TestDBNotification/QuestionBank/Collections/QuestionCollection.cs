using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDBNotification.QuestionBank.Indexes;
using TestDBNotification.QuestionBank.Models;

namespace TestDBNotification.QuestionBank.Collections
{
    //test
    public class QuestionCollection
    {
        public string Subject { get; private set; }

        private Dictionary<ObjectId, Question> Dictionary;

        //test
        public QuestionIndex QuestionIndex;

        private Task InitTask;

        public QuestionCollection(string subject, IMongoCollection<Question> mongoCollection)
        {
            Subject = subject;
            Dictionary = new Dictionary<ObjectId, Question>();

            InitTask = Init(mongoCollection);
        }

        /// <summary>
        /// 從資料庫拿所有資料回來
        /// </summary>
        /// <param name="mongoCollection"></param>
        /// <returns></returns>
        private async Task Init(IMongoCollection<Question> mongoCollection)
        {
            await Task.Run(() =>
            {
                var start = Environment.TickCount;
                //Dictionary = mongoCollection.Find(item => true)
                //                            .ToEnumerable()
                //                            .ToDictionary(question => question._id);

                var data = mongoCollection.Find(item => true).ToEnumerable();
                QuestionIndex = QuestionIndex.CreateIndexes(data);
                Console.WriteLine($"                               {Subject} 花了 {(Environment.TickCount - start)}毫秒");
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
            //return Dictionary.Count;

            //不同的書數量
            return QuestionIndex.GetInt();
        }

        public async Task<int> GetByBook(string bookId)
        {
            await InitTask;
            return QuestionIndex.GetByBook(bookId).Count();
        }

        public async Task<string> GetIndexContent(string index)
        {
            await InitTask;
            return QuestionIndex.GetIndexContent(index);
        }

        public async Task<string> GetSource(string index)
        {
            await InitTask;
            return QuestionIndex.GetSourceIndex(index);
        }
    }
}
