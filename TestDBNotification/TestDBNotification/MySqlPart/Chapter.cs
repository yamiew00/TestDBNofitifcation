using System;
using System.Collections.Generic;
using System.Text;

namespace TestDBNotification.MySqlPart
{
    public class Chapter
    {
        public string UID;

        public string Name;

        public string Code;

        public string ParentUID;

        public Chapter ParentNode;

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
