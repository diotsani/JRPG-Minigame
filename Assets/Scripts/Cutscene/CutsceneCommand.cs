using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cutscene
{
    public abstract class CutsceneCommand : ScriptableObject
    {
        public abstract UniTask Execute(CutsceneContext context);
    }
}