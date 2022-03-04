using System;
using System.Collections.Generic;
using System.Text;

namespace TestDBNotification.MySqlPart
{
    public class Chapter
    {
        public string Name;

        public string Code;

        public Chapter ParentNode;

        public Chapter(string code)
        {
            Code = code;
        }

        /// <summary>
        /// 只有末節點才會存知識點
        /// </summary>
        public List<Knowledge> Knowledges;

        public void AddKnowledge(Knowledge knowledge)
        {
            Knowledges.Add(knowledge);
        }
    }
}
