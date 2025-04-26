using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Softgames.Common
{
    public class Common_Scene_Loader : MonoBehaviour
    {
        [SerializeField] private Common_UI common_UI;

        public void LoadScene(string path)
        {
            common_UI.Loading(true);
            StartCoroutine(Load_Scene(path));
        }

        private IEnumerator Load_Scene(string sceneName_Path)
        {
            yield return new WaitForSeconds(.6f);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName_Path);
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {

                    asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
            common_UI.Loading(false);
        }
    }
}