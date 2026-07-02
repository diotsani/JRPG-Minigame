using Unity.Cinemachine;
using UnityEngine;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera currentCamera;
        private CinemachineCamera _lastCamera;
        [SerializeField] private CinemachineCamera[] cameras;

        public void Switch(int index)
        {
            if (currentCamera != null)
            {
                currentCamera.Priority = 0;
                _lastCamera = currentCamera;
            }

            currentCamera = cameras[index];
            currentCamera.Priority = 100;
            
            currentCamera.gameObject.SetActive(true);
            if (_lastCamera != null)
            {
                _lastCamera.gameObject.SetActive(false);
            }
        }
    }
}