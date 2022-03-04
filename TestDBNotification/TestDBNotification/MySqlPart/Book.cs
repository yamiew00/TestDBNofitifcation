using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TestDBNotification.MySqlPart
{
    public class Book
    {
        public int Year;

        public string Curriculum;

        public string EduSubject;

        public string BookId;

        /// <summary>
        /// 出版社。等下要做
        /// </summary>
        public string Version;

        /// <summary>
        /// 樹狀章節
        /// </summary>
        public List<Chapter> _ChapterTree = new List<Chapter>();

        /// <summary>
        /// 章節(攤平資料)
        /// </summary>
        public Dictionary<string, Chapter> Chapters = new Dictionary<string, Chapter>();

        public void AddKnowledge(string chapterCode, Knowledge knowledge)
        {
            if(Chapters.TryGetValue(chapterCode, out var chapter))
            {
                chapter.AddKnowledge(knowledge);
            }
            else
            {
                Chapters[chapterCode] = new Chapter(chapterCode)
                {
                    Knowledges = new List<Knowledge>()
                    {
                        knowledge
                    }
                };
            }
        }
    }
}
