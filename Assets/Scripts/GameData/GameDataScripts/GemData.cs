using System.Collections.Generic;
using UnityEngine;

namespace GameData.GameDataScripts
{
    [CreateAssetMenu(fileName = "New Gem Data", menuName = "ScriptableObjects/Gem Data", order = 1)]
    public class GemData : ScriptableObject
    {
        [SerializeField] private GemAppearanceData appearanceData;
        [SerializeField] private GemTextualData textualData;

        public Dictionary<(GemType, GemSize, GemCut), Sprite> Sprite;
        public Dictionary<GemType, string> BodyEffect;
        public Dictionary<GemType, string> ReflexEffect;
        public Dictionary<GemType, string> MindEffect;
        public Dictionary<HashSet<GemType>, string> Virtue;
        public Dictionary<HashSet<GemType>, string> Vice;
        public Material resonanceConnectionMaterial;

        public void OnValidate()
        {
            appearanceData.Initialize();
            textualData.Initialize();
            Sprite = appearanceData.Sprite;
            BodyEffect = textualData.BodyEffect;
            ReflexEffect = textualData.ReflexEffect;
            MindEffect = textualData.MindEffect;
            Virtue = textualData.Virtue;
            Vice = textualData.Vice;
            resonanceConnectionMaterial = appearanceData.resonanceConnectionMaterial;
        }
    }
}