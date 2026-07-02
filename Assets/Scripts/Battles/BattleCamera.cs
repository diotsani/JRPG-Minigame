using Unity.Cinemachine;
using UnityEngine;

namespace Battles
{
    public class BattleCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera mainVCam;
        [SerializeField] private CinemachineCamera heroVCam;
        [SerializeField] private CinemachineCamera enemyVCam;

        public void ResetVCamera()
        {
            mainVCam.Priority = 1;
            heroVCam.Priority = 0;
            enemyVCam.Priority = 0;
        }
        
        public void SetHeroVCam(Transform target)
        {
            heroVCam.Follow = target;
            heroVCam.Priority = 100;
        }

        public void SetEnemyVCam(Transform target)
        {
            enemyVCam.Follow = target;
            enemyVCam.Priority = 100;
        }
    }
}