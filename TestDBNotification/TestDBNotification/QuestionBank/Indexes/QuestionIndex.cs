using MongoDB.Bson;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestDBNotification.QuestionBank.Models;

namespace TestDBNotification.QuestionBank.Indexes
{
    public class QuestionIndex
    {
        private ConcurrentDictionary<string, List<ObjectId>> BookIdIndex = new ConcurrentDictionary<string, List<ObjectId>>();

        private ConcurrentDictionary<string, List<ObjectId>> KnowledgeIndex = new ConcurrentDictionary<string, List<ObjectId>>();

        private ConcurrentDictionary<string, List<ObjectId>> SourceIndex = new ConcurrentDictionary<string, List<ObjectId>>();

        private QuestionIndex()
        {
        }

        public static QuestionIndex CreateIndexes(IEnumerable<Question> questions)
        {
            var index = new QuestionIndex();

            foreach (var question in questions)
            {
                //BookId
                if (index.BookIdIndex.TryGetValue(question.BookID, out var list))
                {
                    list.Add(question._id);
                }
                else
                {
                    index.BookIdIndex[question.BookID] = new List<ObjectId>() { question._id };
                }

                //KnowledgeIndex
                if (index.KnowledgeIndex.TryGetValue(question.MetaData.Knowledge, out var knowledgeList))
                {
                    knowledgeList.Add(question._id);
                }
                else
                {
                    index.KnowledgeIndex[question.MetaData.Knowledge] = new List<ObjectId>() { question._id };
                }

                //Source
                if (index.SourceIndex.TryGetValue(question.MetaData.Source, out var sourceList))
                {
                    sourceList.Add(question._id);
                }
                else
                {
                    index.SourceIndex[question.MetaData.Source] = new List<ObjectId>() { question._id };
                }
            }
            return index;
        }

        public int GetInt()
        {
            return BookIdIndex.Count;
        }

        public List<ObjectId> GetByBook(string bookId)
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
