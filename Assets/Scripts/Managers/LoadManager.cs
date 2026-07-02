using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LoadManager : MonoBehaviour
    {
        private enum LoadSceneType
        {
            Load = 0,
            Home = 1,
            World = 2,
            Battle = 3
        }
        [SerializeField] private LoadSceneType nextScene = LoadSceneType.Home;

        public void LoadScene()
        {
            SceneManager.LoadScene((int)nextScene);
        }
    }
}
