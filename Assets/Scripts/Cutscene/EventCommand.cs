using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Cutscene
{
    [CreateAssetMenu(fileName = "New Event Command", menuName = "Cutscene/Command/Event Command")]
    public class EventCommand : CutsceneCommand
    {
        public EventCommandHandler handler;
        public override async UniTask Execute(CutsceneContext context)
        {
            await context.Cutscene.PlayEvent(handler);
        }
    }
}