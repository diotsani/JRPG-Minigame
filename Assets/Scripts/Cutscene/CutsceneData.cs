using UnityEngine;

namespace Cutscene
{
    [CreateAssetMenu(fileName = "New Cutscene Data", menuName = "Cutscene/Data")]
    public class CutsceneData : ScriptableObject
    {
        public string cutsceneId;
        public CutsceneCommand[] commands;
    }
}