﻿using UnityEngine;

namespace Dialogue_System
{
    public class NarrationLineNode : BaseNode
    {
        [Input] public Connection entry;
        [Output] public Connection exit;
        public string speakerENG;
        public string speakerRUS;
        public string lineENG;
        public string lineRUS;
        public Sprite sprite;

        private string GetDialogueLine(Language lang=Language.ENG)
        {
            return lang switch
            {
                Language.ENG => lineENG,
                Language.RUS => lineRUS,
                _ => "Unsupported Language"
            };
        }

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
            return $"NarrationLineNode/{GetSpeakerName(lang)}/{GetDialogueLine(lang)}";
        }
    
        public override Sprite GetSprite()
        {
            return sprite;
        }
    
    }


}