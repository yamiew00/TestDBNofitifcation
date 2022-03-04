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
        /// 出版社。還沒做!
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

        /// <summary>
        /// 新增知識點
        /// </summary>
        /// <param name="chapterUID"></param>
        /// <param name="knowledge"></param>
        public void AddKnowledge(string chapterUID, Knowledge knowledge)
        {
            if(Chapters.TryGetValue(chapterUID, out var chapter))
            {
                chapter.AddKnowledge(knowledge);
            }
            else
            {
                Chapters[chapterUID] = new Chapter()
                {
                    UID = chapterUID,
                    Knowledges = new List<Knowledge>()
                    {
                        knowledge
                    }
                };
            }
        }

        /// <summary>
        /// 新增除了知識點以外的所有相關訊息
        /// </summary>
        /// <param name="chapter"></param>
        public void AddNode(Chapter chapter)
        {
            if(Chapters.TryGetValue(chapter.UID, out var result))
            {
                result.Name = chapter.Name;
                result.UID = chapter.UID;
                result.ParentUID = chapter.ParentUID;
                result.Code = chapter.Code;
                return;
            }

            Chapters[chapter.UID] = new Chapter() 
            {
                Name = chapter.Name,
                UID = chapter.UID,
                ParentUID = chapter.ParentUID,
                Code = chapter.Code
            };
        }
    }
}
