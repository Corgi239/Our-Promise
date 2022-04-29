using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameData.GameDataScripts
{
    [CreateAssetMenu(fileName = "New Gem Appearance Data", menuName = "ScriptableObjects/Gem Appearance Data", order = 1)]
    public class GemAppearanceData : ScriptableObject
    {
        [SerializeField] private GemAppearanceRecord[] appearances;
        public Dictionary<(GemType, GemSize, GemCut), Sprite> Sprite;

        public void Initialize()
        {
            Sprite = new Dictionary<(GemType, GemSize, GemCut), Sprite>();
            foreach (GemAppearanceRecord record in appearances) {
                Sprite[(record.type, record.size, record.cut)] = record.sprite;
            }
        }
    }

    [Serializable]
    public class GemAppearanceRecord
    {
        public GemType type;
        public GemCut cut;
        public GemSize size;
        public Sprite sprite;
    }
}