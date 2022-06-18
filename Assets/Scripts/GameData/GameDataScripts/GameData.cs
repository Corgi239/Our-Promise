using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameData.GameDataScripts
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "ScriptableObjects/Game Data", order = 1)]
    public class GameData : ScriptableObject
    {
        [FormerlySerializedAs("appearanceData")] [SerializeField] private GemAppearanceData gemAppearanceData;
        [FormerlySerializedAs("textualData")] [SerializeField] private GemTextualData gemTextualData;
        public float ResonanceLineWidth;
        [SerializeField] private AudioData audioData;

        
        public Dictionary<(GemType, GemSize, GemCut), Sprite> Sprite;
        public Dictionary<GemType, string> BodyEffect;
        public Dictionary<GemType, string> ReflexEffect;
        public Dictionary<GemType, string> MindEffect;
        public Dictionary<HashSet<GemType>, string> Virtue;
        public Dictionary<HashSet<GemType>, string> Vice;
        public Material resonanceConnectionMaterial;
        public Sound[] Sounds;
        public Sound[] MusicThemes;

        public void OnValidate()
        {
            gemAppearanceData.Initialize();
            gemTextualData.Initialize();
            Sprite = gemAppearanceData.Sprite;
            BodyEffect = gemTextualData.BodyEffect;
            ReflexEffect = gemTextualData.ReflexEffect;
            MindEffect = gemTextualData.MindEffect;
            Virtue = gemTextualData.Virtue;
            Vice = gemTextualData.Vice;
            resonanceConnectionMaterial = gemAppearanceData.resonanceConnectionMaterial;
            Sounds = audioData.sounds;
            MusicThemes = audioData.musicThemes;
        }
    }
}