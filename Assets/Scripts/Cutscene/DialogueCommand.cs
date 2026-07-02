using Cysharp.Threading.Tasks;
using Fungus;
using UnityEngine;

namespace Cutscene
{
    [CreateAssetMenu(fileName = "New Dialogue Command", menuName = "Cutscene/Command/Dialogue Command")]
    public class DialogueCommand : CutsceneCommand
    {
        public string fungusBlock;
        public override async UniTask Execute(CutsceneContext context)
        {
            await context.Dialogue.Play(fungusBlock);
        }
    }
}