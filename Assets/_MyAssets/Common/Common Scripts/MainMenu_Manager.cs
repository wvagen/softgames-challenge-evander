using UnityEngine;

namespace Softgames.Common
{
    public class MainMenu_Manager : MonoBehaviour
    {
        private Common_Scene_Loader sceneLoader;

        private void Start()
        {
            sceneLoader = FindFirstObjectByType<Common_Scene_Loader>();
        }

        public void LoadAceOfShadowsProject()
        {
            sceneLoader.LoadScene(Constants.SCENE_ACE_OF_SHADOWS);
        }
        public void LoadMagicOfWordsProject()
        {
            sceneLoader.LoadScene(Constants.SCENE_MAGIC_WORDS);
        }
        public void LoadPhoenixProject()
        {
            sceneLoader.LoadScene(Constants.SCENE_PHOENIX_FLAME);
        }

        public void HomeBtn()
        {
            sceneLoader.LoadScene(Constants.SCENE_HOME);
        }
    }
}
