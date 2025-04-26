using UnityEngine;
using TMPro;

namespace Softgames.Common
{
    public class Common_UI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI fpsTxt;

        [SerializeField]
        private float refreshTime = 2f; //Refresh to display the FPS for every how many seconds

        [SerializeField]
        private Animator myAnim;

        float actualTime = 0;
        float fps;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        // Update is called once per frame
        void Update()
        {
            DisplayFPS();
        }

        void DisplayFPS()
        {
            actualTime += Time.deltaTime;

            if (actualTime > refreshTime)
            {
                actualTime = 0;
                fps = 1.0f / Time.deltaTime;

                if (fps < 30) {
                    fpsTxt.text = "FPS: <color=red> " +  ((int)fps).ToString() + " </color>";
                }
                else if (fps < 40)
                {
                    fpsTxt.text = "FPS: <color=orange> " + ((int)fps).ToString() + " </color>";
                }
                else
                {
                    fpsTxt.text = "FPS: <color=green> " + ((int)fps).ToString() + " </color>";
                }

            }
        }

        public void Loading(bool isLoading)
        {
            myAnim.SetBool(Constants.ANIM_PARAM_LOADING, isLoading);

        }
    }
}
