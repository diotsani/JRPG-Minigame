using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cutscene
{
    [CreateAssetMenu(fileName = "New Camera Command", menuName = "Cutscene/Command/Camera Command")]
    public class CameraCommand : CutsceneCommand
    {
        public CameraCommandTarget target;
        public override UniTask Execute(CutsceneContext context)
        {
            context.Camera.Switch((int)target);
            return UniTask.CompletedTask;
        }
    }
}