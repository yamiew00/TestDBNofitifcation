using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestDBNotification.MySqlPart;
using TestDBNotification.MySqlPart.Models.YearBooks;
using TestDBNotification.QuestionBank;
using TestDBNotification.QuestionBank.Collections;
using TestDBNotification.QuestionBank.Models;
using System.Linq;
using TestDBNotification.MySqlPart.Models.SubjectMeta;
using MySql.Data.MySqlClient;
using TestDBNotification.MySqlPart.Models.Resources;

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


        static void Main(string[] args)
        {
            MySqlPart();


            Console.WriteLine("done");
            Console.ReadLine();
        }

        private static void MySqlPart()
        {
            var dev = "server=35.236.136.171;port=3306;user id=nani-back-end;password=exbnXQy?D#h5DzHK;database=subjectMeta";
            var release = "Server=34.80.171.95;Port=3306;User Id=api-service-release;Password=cyb6zGw02EzG5P3e;Database=subjectMeta";

            var start = Environment.TickCount;
            //context
            yearBooksContext yearBooksContext = new yearBooksContext(); //dev的
            SubjectMetaContext subjectMetaContext = new SubjectMetaContext(dev);
            resourceContext resourceContext = new resourceContext();    //dev的
            subjectMetaContext.Populate(Subjects);
            Console.WriteLine($"{Environment.TickCount - start}毫秒");

            //定義表:版本
            var versions = resourceContext.Definitions
                                          .Where(def => def.Type == "PUBLISHER")
                                          .ToDictionary(def => def.Code);
           

            //先拿所有書(BookList)
            Dictionary<string, Book> Books = yearBooksContext.BookLists
                                                             .ToDictionary(item => item.BookId,
                                                                           item => new Book()
                                                                           {
                                                                               BookId = item.BookId,
                                                                               Version = versions[item.BookId.Substring(3, item.BookId.Length - 3).Split("-")[0]].Name,
                                                                               Year = item.Year,
                                                                               Curriculum = item.Curriculum,
                                                                               EduSubject = item.EduSubject
                                                                           });

            //全章節
            var chapter109 = yearBooksContext.Chapter109s.AsEnumerable().ToList();
            var chapter110 = yearBooksContext.Chapter110s.AsEnumerable().ToList();
            var chapter111 = yearBooksContext.Chapter111s.AsEnumerable().ToList();

            //組書
            //[109]
            
            //走訪bookmeta
            var book109 = yearBooksContext.BookMeta109s.AsEnumerable();
            foreach (var bookMeta in book109)
            {
                var subject = bookMeta.EduSubject;
                var metaUID = bookMeta.MetaUid;
                var bookId = bookMeta.BookId;

                //科目
                var subjectMeta = subjectMetaContext.GetByUID(subject, metaUID);

                //章節
                var chapter = chapter109.FirstOrDefault(c => c.Uid == bookMeta.ParentUid);

                //合併
                var newBook = Books[bookId];
                newBook.AddKnowledge(chapter.Uid, new Knowledge()
                {
                    Code = subjectMeta.Code,
                    Name = subjectMeta.Name
                });
            }

            //走訪chapter
            var start1 = Environment.TickCount;
            foreach (var chapter in chapter109)
            {
                var bookID = chapter.BookId;
                var book = Books[bookID];

                //加節點
                book.AddNode(new Chapter()
                {
                    UID = chapter.Uid,
                    Name = chapter.Name,
                    Code = chapter.Code,
                    ParentUID = chapter.ParentUid
                });
            }

            //建構樹狀

            Console.WriteLine($"建樹狀: {Environment.TickCount - start1}毫秒");

            var s = "stop";
            //[110]
            //[111]


            //test
            var start2 = Environment.TickCount;


            //

            var jpcBook = Books.Values
                               .Where(book => book.EduSubject == "JPC")
                               .GroupBy(book => book.Version)
                               .ToDictionary(group => group.Key, group => group.GroupBy(book => book.Curriculum)
                                                                               .ToDictionary(subGroup => subGroup.Key, subGroup => subGroup.GroupBy(book => book.Year)
                                                                                                                                           .ToDictionary(tinyGroup => tinyGroup.Key, tinyGroup => tinyGroup.ToList())));

            var jpcBooks = Books.Values.Where(book => book.EduSubject == "JPC")
                                       .GroupBy(book => book.Curriculum)
                                       .ToDictionary(group => group.Key, group => group.GroupBy(book => book.Year)
                                                                                       .ToDictionary(group => group.Key,
                                                                                                     group => group.ToList()));


            Console.WriteLine($"{Environment.TickCount - start2}毫秒");
            var s2 = "stop";


            var list = subjectMetaContext.GetBySubject("HMA");

            var bookMeta109 = yearBooksContext.BookMeta109s.AsEnumerable();

        }

        public static async Task MongoPart()
        {
            var start = Environment.TickCount;

            //基本設定
            var dev = "mongodb+srv://question-bank-admin:mk+nfFx*wmBgR4G#@ruledata.tmqy1.mongodb.net/{0}?retryWrites=true&w=majority";
            var release = "mongodb+srv://question-bank-admin:PV975tjHcXgzHpUE@clusters.3w0iy.mongodb.net/{0}?retryWrites=true&w=majority";
            MongoClient mongoClient = new MongoClient(release);
            var database = mongoClient.GetDatabase("QuestionBank");


            //test3
            var ech = new List<string>() { "ECH" };
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
        }
    }
}
