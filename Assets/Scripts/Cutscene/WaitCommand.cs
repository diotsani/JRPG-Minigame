using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cutscene
{
    [CreateAssetMenu(fileName = "New Wait Command", menuName = "Cutscene/Command/Wait Command")]
    public class WaitCommand : CutsceneCommand
    {
        public WaitCommandType type;
        public float seconds;
        public bool ready;
        public override async UniTask Execute(CutsceneContext context)
        {
            ready = false;
            switch (type)
            {
                case WaitCommandType.Time:
                    await UniTask.Delay(TimeSpan.FromSeconds(seconds));
                    break;
                case WaitCommandType.Bool:
                    await UniTask.WaitUntil(() => ready);
                    break;
                default:
                    await UniTask.Delay(TimeSpan.FromSeconds(seconds));
                    break;
            }
        }
    }
}