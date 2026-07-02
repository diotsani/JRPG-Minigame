using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cutscene
{
    [CreateAssetMenu(fileName = "New Move Command", menuName = "Cutscene/Command/Move Command")]
    public class MoveCommand : CutsceneCommand
    {
        public MoveCommandActor actor;
        public MoveCommandTarget target;

        public override async UniTask Execute(CutsceneContext context)
        {
            await context.Cutscene.PlayMove(actor, target);
        }
    }
}