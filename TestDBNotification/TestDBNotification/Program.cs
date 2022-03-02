using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDBNotification.QuestionBank;
using TestDBNotification.QuestionBank.Collections;
using TestDBNotification.QuestionBank.Models;

namespace TestDBNotification
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var start = Environment.TickCount;


            //基本設定
            var dev = "mongodb+srv://question-bank-admin:mk+nfFx*wmBgR4G#@ruledata.tmqy1.mongodb.net/{0}?retryWrites=true&w=majority";
            var release = "mongodb+srv://question-bank-admin:PV975tjHcXgzHpUE@clusters.3w0iy.mongodb.net/{0}?retryWrites=true&w=majority";
            MongoClient mongoClient = new MongoClient(release);
            var database = mongoClient.GetDatabase("QuestionBank");

            //test2
            //MongoDataStream<Question> mongoDataStream = new MongoDataStream<Question>(database, new List<string>() { "ECH", "ELI", "EMA" }); //單例
            //QuestionCollection questionCollection = new QuestionCollection("EMA", mongoDataStream); //每個科目一個

            ////activate
            //mongoDataStream.Activate();

            //test3
            var subjects = new List<string>() { "ECH", "ELI", "EMA", "JCT", "JEA", "JEN", "JGE", "JHI", "JMA", "JPC" };
            MongoDataStream<Question> mongoDataStream = new MongoDataStream<Question>(database, subjects); //單例
            QuestionProvider questionProvider = new QuestionProvider(subjects, mongoDataStream);

            //mongoDataStream.Activate();


            //Console.WriteLine(await questionProvider.CountAll());
            Console.WriteLine(await questionProvider.GetInt("JEN"));
            Console.WriteLine($"到這裡是{(Environment.TickCount - start)}毫秒");

            Console.WriteLine(await questionProvider.CountAll());
            Console.WriteLine($"全部是{(Environment.TickCount - start)}毫秒");

            if (true)
            {
                Console.ReadLine();
                //var allData = questionProvider.GetAll("ECH");
                //foreach (var data in allData)
                //{
                //    Console.WriteLine($"content: {data.Content}。 bookID: {data.BookID}");
                //}
                Console.WriteLine(await questionProvider.GetInt("ECH"));

                Console.WriteLine($"{(Environment.TickCount - start)}毫秒");
                Console.WriteLine("----------------------------------");
            }

            if (true)
            {
                var start2 = Environment.TickCount;
                Console.WriteLine(await questionProvider.CountAll());
                Console.WriteLine($"{(Environment.TickCount - start2)}毫秒");
            }


            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
