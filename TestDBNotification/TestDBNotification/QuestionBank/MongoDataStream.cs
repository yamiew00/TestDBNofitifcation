using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace TestDBNotification.QuestionBank
{
    /// <summary>
    /// 會在初始化的時候決定了要監聽哪些collection。
    /// 重要:若在初始化之後新增collection的話將不會被監聽到。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MongoDataStream<T> where T : MongoDocument
    {
        private readonly Dictionary<string, MongoChangeStream<T>> Dictionary;

        private readonly IMongoDatabase MongoDatabase;

        private readonly IEnumerable<string> CollectionNames;

        public MongoDataStream(IMongoDatabase mongoDatabase, IEnumerable<string> collectionNames)
        {
            MongoDatabase = mongoDatabase;
            CollectionNames = collectionNames;
            Dictionary = new Dictionary<string, MongoChangeStream<T>>();
            Refresh();
        }

        /// <summary>
        /// 功能不完全。無法dispose
        /// </summary>
        public void Refresh()
        {
            //should Dispose first
            Dispose();

            foreach (var collectionName in CollectionNames)
            {
                //取名的部分「是寫死的」！因為資料庫名稱都是「Question」 + 科目
                var collection = MongoDatabase.GetCollection<T>("Question" + collectionName);
                if (collection != null)
                {
                    Dictionary[collectionName] = new MongoChangeStream<T>(collection);
                }
            }
        }

        /// <summary>
        /// 還沒做
        /// </summary>
        public void Dispose()
        {

        }

        public IMongoCollection<T> GetCollection(string collectionName)
        {
            //名字是寫死的，Question + 科目
            return MongoDatabase.GetCollection<T>("Question" + collectionName);
        }

        /// <summary>
        /// 訂閱Insert事件
        /// </summary>
        /// <param name="collectionName"></param>
        /// <param name="action"></param>
        public void SubscribeInsert(string collectionName, Action<T> action)
        {
            if(Dictionary.TryGetValue(collectionName, out var mongoChangeStream))
            {
                mongoChangeStream.OnInsert(action);
            }
            else
            {
                //還沒有的話就不訂閱

            }
        }

        public void Activate()
        {
            foreach (var changeStream in Dictionary.Values)
            {
                changeStream.ChangeStreamOnSingleCollection();
            }
        }
    }
}
