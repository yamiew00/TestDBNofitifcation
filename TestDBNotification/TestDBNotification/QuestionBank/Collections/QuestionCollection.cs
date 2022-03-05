using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestDBNotification.QuestionBank.Indexes;
using TestDBNotification.QuestionBank.Models;

namespace TestDBNotification.QuestionBank.Collections
{
    //test
    public class QuestionCollection
    {
        public string Subject { get; private set; }

        //test
        public QuestionIndex QuestionIndex;

        private Task InitTask;

        public QuestionCollection(string subject, IMongoCollection<Question> mongoCollection)
        {
            Subject = subject;
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
                var data = mongoCollection.Find(item => true).ToEnumerable();
                //不需要傳科目
                QuestionIndex = QuestionIndex.CreateIndexes(data, Subject);
                Console.WriteLine($"            {Subject}: {Environment.TickCount - start}毫秒");
            });
        } 

        public Action<Question> OnInsert()
        {
            return (question) =>
            {
                InitTask = Task.Run(() =>
                {
                    QuestionIndex.InsertIndex(question);
                });
            };
        }

        public async Task<int> GetQuestionAmount()
        {
            await InitTask;
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

        public async Task<IEnumerable<ObjectId>> GetByKnowledges(IEnumerable<string> bookIDs, IEnumerable<string> knowledges)
        {
            await InitTask;
            return QuestionIndex.GetByKnowledges(bookIDs, knowledges);
        }

        public async Task<IEnumerable<Question>> GetByKnowledges2(IEnumerable<string> bookIDs, IEnumerable<string> knowledges)
        {
            await InitTask;
            return QuestionIndex.GetByKnowledges2(bookIDs, knowledges);
        }

        public async Task IsComplete()
        {
             await InitTask;
        }
    }
}
