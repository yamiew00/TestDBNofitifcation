using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDBNotification
{
    public class TestMongoChangeStream
    {
        public void Test()
        {
            var client = new MongoClient("mongodb+srv://jerry:N5X13QcqYEAiaCtT@cluster0.wlr5j.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

            var database = client.GetDatabase("TestDB");
            var collection = database.GetCollection<A>("TestCollection1");

            var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };
            var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<A>>().Match("{ operationType: { $in: [ 'insert', 'delete' ] } }");

            var cursor = collection.Watch<ChangeStreamDocument<A>>(pipeline, options);

            var enumerator = cursor.ToEnumerable().GetEnumerator();
            while (enumerator.MoveNext())
            {
                ChangeStreamDocument<A> doc = enumerator.Current;
                // Do something here with your document

                Console.WriteLine("yes");
                Console.WriteLine(doc.DocumentKey);
            }
        }

        public async Task ChangeStreamOnSingleCollection()
        {
            var client = new MongoClient("mongodb+srv://apiService:x5CckmsVwRzAucSm@clusters.4tbts.mongodb.net/{0}?retryWrites=true&w=majority");

            var database = client.GetDatabase("TestDB");
            var collection = database.GetCollection<A>("TestCollection1");


            var list = collection.Find(x => true).ToEnumerable().ToList();

            using (var cursor = await collection.WatchAsync())
            {
                await cursor.ForEachAsync(change =>
                {
                    //update
                    if (change.OperationType == ChangeStreamOperationType.Update)
                    {
                        var key = change.DocumentKey;

                        var key2 = BsonSerializer.Deserialize<A>(change.DocumentKey);

                        var updateValue = collection.Find<A>(x => x._id == key2._id).Limit(1).ToList().FirstOrDefault();
                        Console.WriteLine($"update: {updateValue._id}");
                        Console.WriteLine($"    number1:{updateValue.Number1}, string1:{updateValue.String1} ");
                        Console.WriteLine("------------------");
                    }

                    //insert
                    if (change.OperationType == ChangeStreamOperationType.Insert)
                    {
                        var insertValue = change.FullDocument;

                        Console.WriteLine($"insert: {insertValue._id}");
                        Console.WriteLine($"    number1:{insertValue.Number1}, string1:{insertValue.String1} ");
                        Console.WriteLine("------------------");
                    }
                });
            }
        }

        public async Task ChangeStreamOnSingleDatabase()
        {
            var client = new MongoClient("mongodb+srv://apiService:x5CckmsVwRzAucSm@clusters.4tbts.mongodb.net/{0}?retryWrites=true&w=majority");

            var database = client.GetDatabase("TestDB");
            var collection = database.GetCollection<A>("TestCollection1");


            using (var cursor = await database.WatchAsync())
            {
                foreach (var change in cursor.ToEnumerable())
                {
                    var collectionName = change.CollectionNamespace.CollectionName;
                    if(collectionName == "TestCollection1")
                    {
                        //update
                        if (change.OperationType == ChangeStreamOperationType.Update)
                        {
                            var key = change.DocumentKey;

                            var key2 = BsonSerializer.Deserialize<A>(change.DocumentKey);

                            var updateValue = collection.Find<A>(x => x._id == key2._id).Limit(1).ToList().FirstOrDefault();
                            Console.WriteLine($"update: {updateValue._id}");
                            Console.WriteLine($"    number1:{updateValue.Number1}, string1:{updateValue.String1} ");
                            Console.WriteLine("------------------");
                        }

                        //insert
                        if (change.OperationType == ChangeStreamOperationType.Insert)
                        {
                            var insertValue = BsonSerializer.Deserialize<A>(change.FullDocument);

                            Console.WriteLine($"insert: {insertValue._id}");
                            Console.WriteLine($"    number1:{insertValue.Number1}, string1:{insertValue.String1} ");
                            Console.WriteLine("------------------");
                        }
                    }
                    else if (collectionName == "TestCollection2")
                    {

                    }
                }
            }
        }


        [BsonIgnoreExtraElements]
        public class A
        {
            [BsonId]
            public ObjectId _id;

            [BsonElement("number1")]
            public int Number1;

            [BsonElement("string1")]
            public string String1;
        }
    }
}
