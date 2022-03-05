using MongoDB.Bson;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDBNotification.QuestionBank.Models;

namespace TestDBNotification.QuestionBank.Indexes
{
    public class QuestionIndex
    {

        /// <summary>
        /// 需要存的最少資料
        /// </summary>
        private ConcurrentDictionary<ObjectId, Question> RawData;

        private ConcurrentDictionary<string, ConcurrentBag<ObjectId>> BookIdIndex = new ConcurrentDictionary<string, ConcurrentBag<ObjectId>>();

        private ConcurrentDictionary<string, ConcurrentBag<ObjectId>> KnowledgeIndex = new ConcurrentDictionary<string, ConcurrentBag<ObjectId>>();

        private ConcurrentDictionary<string, ConcurrentBag<ObjectId>> SourceIndex = new ConcurrentDictionary<string, ConcurrentBag<ObjectId>>();

        private QuestionIndex()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="questions"></param>
        /// <returns></returns>
        public static QuestionIndex CreateIndexes(IEnumerable<Question> questions, string subject)
        {
            QuestionIndex indexInstance = new QuestionIndex();

            indexInstance.RawData = new ConcurrentDictionary<ObjectId, Question>(questions.ToDictionary(x => x._id));

            foreach (var question in questions)
            {
                //BookId
                if (indexInstance.BookIdIndex.TryGetValue(question.BookID, out var bookIdBag))
                {
                    bookIdBag.Add(question._id);
                }
                else
                {
                    indexInstance.BookIdIndex[question.BookID] = new ConcurrentBag<ObjectId>() { question._id };
                }

                //KnowledgeIndex
                if (indexInstance.KnowledgeIndex.TryGetValue(question.MetaData.Knowledge, out var knowledgeBag))
                {
                    knowledgeBag.Add(question._id);
                }
                else
                {
                    indexInstance.KnowledgeIndex[question.MetaData.Knowledge] = new ConcurrentBag<ObjectId>() { question._id };
                }

                //Source
                if (indexInstance.SourceIndex.TryGetValue(question.MetaData.Source, out var sourceBag))
                {
                    sourceBag.Add(question._id);
                }
                else
                {
                    indexInstance.SourceIndex[question.MetaData.Source] = new ConcurrentBag<ObjectId>() { question._id };
                }
            }
            return indexInstance;
        }

        public void InsertIndex(Question question)
        {
            RawData.TryAdd(question._id, question);

            //重複性很高

            //BookId
            if (BookIdIndex.TryGetValue(question.BookID, out var bookIdBag))
            {
                bookIdBag.Add(question._id);
            }
            else
            {
                BookIdIndex[question.BookID] = new ConcurrentBag<ObjectId>() { question._id };
            }

            //KnowledgeIndex
            if (KnowledgeIndex.TryGetValue(question.MetaData.Knowledge, out var knowledgeBag))
            {
                knowledgeBag.Add(question._id);
            }
            else
            {
                KnowledgeIndex[question.MetaData.Knowledge] = new ConcurrentBag<ObjectId>() { question._id };
            }

            //Source
            if (SourceIndex.TryGetValue(question.MetaData.Source, out var sourceBag))
            {
                sourceBag.Add(question._id);
            }
            else
            {
                SourceIndex[question.MetaData.Source] = new ConcurrentBag<ObjectId>() { question._id };
            }
        }

        public IEnumerable<ObjectId> GetByKnowledges(IEnumerable<string> bookIDs, IEnumerable<string> knowledges)
        {
            var books = BookIdIndex.Where(x => bookIDs.Contains(x.Key))
                                   .SelectMany(x => x.Value);
            var knows = KnowledgeIndex.Where(x => knowledges.Contains(x.Key))
                                      .SelectMany(x => x.Value);
            return books.Intersect(knows);
        }

        public IEnumerable<Question> GetByKnowledges2(IEnumerable<string> bookIDs, IEnumerable<string> knowledges)
        {
            var ids = GetByKnowledges(bookIDs, knowledges);
            return ids.Select(x => RawData[x]);
        }



        public int GetInt()
        {
            return RawData.Count;
        }

        public IEnumerable<ObjectId> GetByBook(string bookId)
        {
            return BookIdIndex[bookId];
        }

        public string GetIndexContent(string index)
        {
            return $"{index} 有 {KnowledgeIndex[index].Count} 個";
        }

        public string GetSourceIndex(string index)
        {
            return $"{index} 有 {SourceIndex[index].Count} 個";
        }
    }
}
