using UnityEngine;

namespace Dialogue_System
{
    public class NarrationLineNode : BaseNode
    {
        [Input] public int entry;
        [Output] public int exit;
        public string speakerName;
        public Sprite speakerSprite;
        public string dialogueLineENG;
        public string dialogueLineRUS;

        private string GetDialogueLine(Language lang=Language.ENG)
        {
            return lang switch
            {
                Language.ENG => dialogueLineENG,
                Language.RUS => dialogueLineRUS,
                _ => "Unsupported Language"
            };
        }

        public override string GetDataString()
        {
            return $"NarrationLineNode/{speakerName}/{GetDialogueLine()}";
        }
    
        public override Sprite GetSprite()
        {
            return speakerSprite;
        }
    
    }

    public enum Language {ENG, RUS}
}