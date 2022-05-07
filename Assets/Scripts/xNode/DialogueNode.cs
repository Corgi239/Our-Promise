using UnityEngine;

namespace xNode
{
    public class DialogueNode : BaseNode
    {
        [Input] public int entry;
        [Output] public int exit;
        public string speakerName;
        public Sprite speakerSprite;
        public string dialogueLineENG;
        public string dialogueLineRUS;

        private string GetDialogueLine(Language lang=Language.ENG)
        {
            if (lang == Language.ENG)
                return dialogueLineENG;
            if (lang == Language.RUS)
                return dialogueLineRUS;
            else
                return "language not supported";
        }

        public override string GetString()
        {
            return $"DialogueNode/{speakerName}/{GetDialogueLine()}";
        }
    
        public override Sprite GetSprite()
        {
            return speakerSprite;
        }
    
    }

    public enum Language {ENG, RUS}
}