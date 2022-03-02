using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace TestDBNotification.QuestionBank
{
    public class MongoChangeStream<T> where T : MongoDocument
    {
        private readonly IMongoCollection<T> MongoCollection;

        private readonly List<Action<T>> InsertActions;

        private readonly List<Action<T>> UpdateActions;

        public MongoChangeStream(IMongoCollection<T> mongoCollection)
        {
            MongoCollection = mongoCollection;
            InsertActions = new List<Action<T>>();
            UpdateActions = new List<Action<T>>();
        }

        public void OnInsert(Action<T> action)
        {
            InsertActions.Add(action);
        }

        public void OnUpdate(Action<T> action)
        {
            UpdateActions.Add(action);
        }

        public async Task ChangeStreamOnSingleCollection() 
        {
            var cursor = await MongoCollection.WatchAsync();
            
            await cursor.ForEachAsync(change =>
            {
                //insert
                if (change.OperationType == ChangeStreamOperationType.Insert)
                {
                    T insertValue = change.FullDocument;

                    foreach (var action in InsertActions)
                    {
                        action.Invoke(insertValue);
                    }
                }

                //update
                if (change.OperationType == ChangeStreamOperationType.Update)
                {
                    var key = BsonSerializer.Deserialize<T>(change.DocumentKey);
                    var updateValue = MongoCollection.Find<T>(x => x._id == key._id).Limit(1).ToList().FirstOrDefault();

                    foreach (var action in UpdateActions)
                    {
                        action.Invoke(updateValue);
                    }
                }
            });
        }
    }
}
