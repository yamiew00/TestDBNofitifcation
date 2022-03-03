using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDBNotification.QuestionBank;
using TestDBNotification.QuestionBank.Collections;
using TestDBNotification.QuestionBank.Models;

namespace TestDBNotification
{
    public class Program
    {

        public static readonly string[] Subjects = new string[]
        {
            "ECH",
            "ECN",
            "EEN",
            "ELI",
            "EMA",
            "ENA",
            "EPE",
            "ESO",
            "HBI",
            "HCE",
            "HCN",
            "HCS",
            "HEA",
            "HEW",
            "HGE",
            "HHI",
            "HMA",
            "HPC",
            "HPH",
            "JBI",
            "JCN",
            "JCO",
            "JCT",
            "JEA",
            "JEN",
            "JGE",
            "JHI",
            "JMA",
            "JNA",
            "JPC",
            "JPE",
            "JPY",
            "JSO",
            "JTC"
        };


        static async Task Main(string[] args)
        {
            var start = Environment.TickCount;

            //基本設定
            var dev = "mongodb+srv://question-bank-admin:mk+nfFx*wmBgR4G#@ruledata.tmqy1.mongodb.net/{0}?retryWrites=true&w=majority";
            var release = "mongodb+srv://question-bank-admin:PV975tjHcXgzHpUE@clusters.3w0iy.mongodb.net/{0}?retryWrites=true&w=majority";
            MongoClient mongoClient = new MongoClient(release);
            var database = mongoClient.GetDatabase("QuestionBank");


            //test3
            var jen = new List<string>() { "JEN" };
            MongoDataStream<Question> mongoDataStream = new MongoDataStream<Question>(database, Subjects); //單例
            QuestionProvider questionProvider = new QuestionProvider(Subjects, mongoDataStream);

            //mongoDataStream.Activate();

            //Console.WriteLine(await questionProvider.CountAll());

            var knowledge = "E-CH-233";
            var source = "NS016";

            //Console.WriteLine($"{await questionProvider["ECH"].GetIndexContent(knowledge)}");
            Console.WriteLine($"{await questionProvider["ECH"].GetSource(source)}");
            Console.WriteLine($"到這裡是{(Environment.TickCount - start)}毫秒");

            Console.WriteLine(await questionProvider.CountAll());
            Console.WriteLine($"全部是{(Environment.TickCount - start)}毫秒");

            while (true)
            {
                Console.ReadLine();

                var x = Environment.TickCount;
                //Console.WriteLine($"EMAB09 = {await questionProvider["EMA"].GetByBook("110N-EMAB09")}");
                Console.WriteLine($"花了{Environment.TickCount - x}毫秒");
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
