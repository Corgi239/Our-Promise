
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Dialogue_System
{
    public class ChoiceNode: BaseNode
    {
        [Input] public Connection entry;
        [Output(dynamicPortList = true)] public List<Reply> replies;
        public string speakerENG = "Yon";
        public string speakerRUS = "Йон";
        public Sprite sprite;
        
        private string GetSpeakerName(Language lang = Language.ENG)
        {
            return lang switch
            {
                Language.ENG => speakerENG,
                Language.RUS => speakerRUS,
                _ => "Unsupported Language"
            };
        }

        public override string GetDataString(Language lang)
        {
            string res = $"ChoiceNode/{GetSpeakerName(lang)}/";
            return replies.Aggregate(res, (current, reply) => current + reply.GetReply(lang) + '/');
        }

        public override Sprite GetSprite()
        {
            return sprite;
        }
    }
    
    [Serializable]
    public struct Reply
    {
        public string replyENG;
        public string replyRUS;
        public string GetReply(Language lang = Language.ENG)
        {
            return lang switch
            {
                Language.ENG => replyENG,
                Language.RUS => replyRUS,
                _ => "Unsupported Language"
            };
        }
    }
}