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


        static async Task Main(string[] args)
        {
            //MySqlPart();

            await MongoPart();


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
            var uat = "mongodb+srv://question-bank-admin:2liAQgWgmKCRhhzP@questionbank.qhfpn.mongodb.net/{0}?retryWrites=true&w=majority&MinConnectionPoolSize=30&MaxConnectionPoolSize=100";
            var release = "mongodb+srv://question-bank-admin:PV975tjHcXgzHpUE@clusters.3w0iy.mongodb.net/{0}?retryWrites=true&w=majority";
            MongoClient mongoClient = new MongoClient(uat);
            var database = mongoClient.GetDatabase("QuestionBank");

            //test3
            var testSubject = "ECH";
            //var subjectList = new List<string>() { "EPE", "JNA", "JPE", "HCN", "ECN", "EEN", "JCO", "HEW", "JCN", "JEN" };
            var subjectList = new List<string>() { testSubject };

            MongoDataStream<Question> mongoDataStream = new MongoDataStream<Question>(database, Subjects); //單例
            QuestionProvider questionProvider = new QuestionProvider(Subjects, mongoDataStream);

            //activate
            mongoDataStream.Activate();


            var knowledge = "E-CH-233";
            var source = "NS016";


            var here = Environment.TickCount;
            await questionProvider.IsComplete();

            Console.WriteLine($"全完成需要 {Environment.TickCount - here}毫秒");

            //測試資料正確性

            while (true)
            {
                var subject1 = Console.ReadLine();
                if (string.IsNullOrEmpty(subject1))
                {
                    continue;
                }
                Console.WriteLine($"{subject1} 有 {await questionProvider.QuestionCollections[subject1].GetQuestionAmount()}");
            }




            while (true)
            {
                Console.ReadLine();
                var x = Environment.TickCount;



                //var bookIds = new List<string>() { "110N-ECHB00", "110N-ECHB01", "110N-ECHB02", "110N-ECHB03", "110N-ECHB04", "110N-ECHB05", "110N-ECHB06" };
                //var knowledges = new List<string>() { "ECH-14", "ECH-15", "ECH-16", "ECH-17", "ECH-18", "ECH-19", "ECH-20", "ECH-21", "ECH-24", "ECH-23", "ECH-22", "ECH-26", "ECH-25", "ECH-27", "ECH-28" };

                var bookIds = new List<string>() {
  "110N-JMAB01",
  "110N-JMAB02",
  "110N-JMAB03",
  "110N-JMAB04",
  "110N-JMAB05",
  "110N-JMAB06" };
                var knowledges = new List<string>() {
                    
  "JMA-1-1-4",
  "JMA-1-1-5",
  "JMA-1-1-2",
  "JMA-1-1-1",
  "JMA-1-1-3",
  "JMA-1-2-2",
  "JMA-1-2-3",
  "JMA-1-2-1",
  "JMA-1-1-6",
  "JMA-1-1-7",
  "JMA-1-2-6",
  "JMA-1-2-7",
  "JMA-1-2-4",
  "JMA-1-2-5",
  "JMA-1-3-1",
  "JMA-1-3-2",
  "JMA-1-4-1",
  "JMA-1-4-3",
  "JMA-1-4-2",
  "JMA-1-5-9",
  "JMA-1-5-3",
  "JMA-1-5-2",
  "JMA-1-5-6",
  "JMA-1-5-8",
  "JMA-1-5-1",
  "JMA-1-5-5",
  "JMA-1-5-10",
  "JMA-1-5-4",
  "JMA-1-5-7",
  "JMA-1-5-12",
  "JMA-1-5-13",
  "JMA-1-5-14",
  "JMA-1-5-11",
  "JMA-1-6-1",
  "JMA-1-6-2",
  "JMA-1-6-4",
  "JMA-1-6-6",
  "JMA-1-6-3",
  "JMA-1-6-5",
  "JMA-1-3-3",
  "JMA-2-1-1",
  "JMA-2-1-3",
  "JMA-2-1-2",
  "JMA-2-1-5",
  "JMA-2-1-4",
  "JMA-2-1-6",
  "JMA-2-1-9",
  "JMA-2-1-8",
  "JMA-2-1-7",
  "JMA-2-1-10",
  "JMA-5-4-1",
  "JMA-5-1-1",
  "JMA-5-1-2",
  "JMA-5-1-4",
  "JMA-5-1-3",
  "JMA-6-1-1",
  "JMA-5-1-5",
  "JMA-2-2-1",
  "JMA-2-2-3",
  "JMA-2-2-4",
  "JMA-2-2-2",
  "JMA-2-2-5",
  "JMA-2-3-3",
  "JMA-2-3-1",
  "JMA-2-3-5",
  "JMA-2-3-2",
  "JMA-2-3-4",
  "JMA-2-3-6",
  "JMA-3-1-4",
  "JMA-3-1-1",
  "JMA-3-1-3",
  "JMA-3-1-2",
  "JMA-3-2-1",
  "JMA-3-2-3",
  "JMA-3-3-1",
  "JMA-3-2-2",
  "JMA-1-7-1",
  "JMA-1-7-3",
  "JMA-1-7-2",
  "JMA-1-7-4",
  "JMA-1-7-5",
  "JMA-7-1-1",
  "JMA-7-2-3",
  "JMA-7-2-2",
  "JMA-7-2-1",
  "JMA-7-2-5",
  "JMA-7-2-4",
  "JMA-7-2-6",
  "JMA-7-2-8",
  "JMA-7-2-7",
  "JMA-8-1-6",
  "JMA-8-1-1",
  "JMA-8-1-3",
  "JMA-8-1-4",
  "JMA-8-1-2",
  "JMA-8-1-5",
  "JMA-8-2-3",
  "JMA-8-2-2",
  "JMA-8-2-5",
  "JMA-8-2-4",
  "JMA-8-2-1",
  "JMA-1-8-4",
  "JMA-1-8-2",
  "JMA-1-8-1",
  "JMA-1-8-3",
  "JMA-9-2-1",
  "JMA-9-1-1",
  "JMA-9-2-3",
  "JMA-9-2-2",
  "JMA-1-9-1",
  "JMA-1-9-3",
  "JMA-1-9-4",
  "JMA-1-9-6",
  "JMA-1-9-2",
  "JMA-1-9-5",
  "JMA-1-9-9",
  "JMA-1-9-7",
  "JMA-1-9-11",
  "JMA-1-9-12",
  "JMA-1-9-8",
  "JMA-1-9-10",
  "JMA-5-2-2",
  "JMA-5-2-4",
  "JMA-5-2-3",
  "JMA-9-3-5",
  "JMA-9-3-4",
  "JMA-9-3-2",
  "JMA-9-3-3",
  "JMA-9-3-6",
  "JMA-9-3-1",
  "JMA-9-3-8",
  "JMA-9-3-7",
  "JMA-2-4-1",
  "JMA-2-4-2",
  "JMA-2-4-4",
  "JMA-2-4-3",
  "JMA-2-4-5",
  "JMA-2-4-6",
  "JMA-2-4-7",
  "JMA-2-4-8",
  "JMA-2-4-9",
  "JMA-8-1-8",
  "JMA-8-1-9",
  "JMA-8-1-7",
  "JMA-1-10-4",
  "JMA-1-10-5",
  "JMA-1-10-6",
  "JMA-1-10-1",
  "JMA-1-10-2",
  "JMA-1-10-3",
  "JMA-1-10-8",
  "JMA-1-10-7",
  "JMA-10-1-1",
  "JMA-5-1-8",
  "JMA-10-2-1",
  "JMA-10-2-2",
  "JMA-10-1-2",
  "JMA-10-2-3",
  "JMA-5-2-6",
  "JMA-5-2-5",
  "JMA-5-1-6",
  "JMA-5-4-2",
  "JMA-5-2-7",
  "JMA-5-4-3",
  "JMA-5-1-7",
  "JMA-5-5-1",
  "JMA-5-5-5",
  "JMA-5-5-3",
  "JMA-5-5-6",
  "JMA-5-5-4",
  "JMA-5-5-2",
  "JMA-5-2-9",
  "JMA-5-2-13",
  "JMA-5-2-14",
  "JMA-5-2-11",
  "JMA-5-2-10",
  "JMA-5-2-8",
  "JMA-5-2-12",
  "JMA-5-2-17",
  "JMA-5-2-16",
  "JMA-5-2-15",
  "JMA-5-2-21",
  "JMA-5-2-18",
  "JMA-5-2-20",
  "JMA-5-2-19",
  "JMA-5-3-3",
  "JMA-5-3-2",
  "JMA-5-3-4",
  "JMA-5-3-5",
  "JMA-5-3-6",
  "JMA-5-3-13",
  "JMA-5-3-10",
  "JMA-5-3-14",
  "JMA-5-3-7",
  "JMA-5-3-12",
  "JMA-5-3-15",
  "JMA-5-3-11",
  "JMA-5-3-8",
  "JMA-5-3-9",
  "JMA-1-7-6",
  "JMA-1-7-7",
  "JMA-1-7-8",
  "JMA-5-2-22",
  "JMA-5-6-3",
  "JMA-5-6-1",
  "JMA-5-6-2",
  "JMA-5-6-5",
  "JMA-5-6-4",
  "JMA-5-6-9",
  "JMA-5-6-10",
  "JMA-5-6-11",
  "JMA-5-6-6",
  "JMA-5-6-8",
  "JMA-5-6-7",
  "JMA-5-6-12",
  "JMA-5-6-13",
  "JMA-5-6-14",
  "JMA-5-7-8",
  "JMA-5-7-6",
  "JMA-5-7-4",
  "JMA-5-7-2",
  "JMA-5-7-5",
  "JMA-5-7-1",
  "JMA-5-7-3",
  "JMA-5-7-7",
  "JMA-5-7-9",
  "JMA-5-7-11",
  "JMA-5-7-10",
  "JMA-11-1-1",
  "JMA-11-1-2",
  "JMA-5-2-26",
  "JMA-5-2-23",
  "JMA-5-2-25",
  "JMA-5-2-24",
  "JMA-10-3-1",
  "JMA-10-3-2",
  "JMA-10-3-3",
  "JMA-10-3-4",
  "JMA-10-3-5",
  "JMA-8-2-9",
  "JMA-8-2-8",
  "JMA-8-2-7",
  "JMA-8-2-6",
  "JMA-12-1-1",
  "JMA-12-1-2",
  "JMA-6-3-3",
  "JMA-6-6-1",
  "JMA-6-4-2",
  "JMA-6-7-2",
  "JMA-6-3-2",
  "JMA-6-4-3",
  "JMA-6-3-1",
  "JMA-6-4-1",
  "JMA-6-7-1"
 };



                var result = await questionProvider.QuestionCollections[testSubject].GetByKnowledges(bookIds, knowledges);

                var boo = result.Contains(new MongoDB.Bson.ObjectId("613ca539732190520fdc534a"));

                var y = Environment.TickCount;

                var ans = database.GetCollection<Question>("Question" + testSubject).Find(x => result.Contains(x._id))
                                  .ToEnumerable()
                                  .GroupBy(x => x.MetaData.QuestionType)
                                  .ToDictionary(x => x.Key, x => x.ToList());

                var z = Environment.TickCount;

                var diff = ans.Select(x => new
                {
                    key = x.Key,
                    Value = x.Value.GroupBy(y => y.MetaData.Difficulty)
                                  .ToDictionary(y => y.Key, y => y.ToList())
                }).ToList();

                Console.WriteLine($"找索引用了{z - y}毫秒");
                Console.WriteLine($"難易度用了{Environment.TickCount - z}毫秒");

                var test = Environment.TickCount;
                var a = database.GetCollection<Question>("QuestionECH").Find(x => result.Contains(x._id))
                                .ToString();
                Console.WriteLine($"tolist用了{Environment.TickCount - test}毫秒");


                Console.WriteLine($"花了{Environment.TickCount - x}毫秒");
                Console.WriteLine("----------------------------------");


                Console.WriteLine();

                var n1 = Environment.TickCount;
                
                var a2 = (await questionProvider.QuestionCollections[testSubject]
                                               .GetByKnowledges2(bookIds, knowledges)).ToList();

                Console.WriteLine($"{Environment.TickCount - n1}毫秒");
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
