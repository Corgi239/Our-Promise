using UnityEngine;

namespace GameData.GameDataScripts
{
    [CreateAssetMenu(fileName = "New Audio Data", menuName = "ScriptableObjects/Audio Data", order = 1)]
    public class AudioData: ScriptableObject
    {
        public Sound[] sounds;
        public Sound[] musicThemes;
    }
}