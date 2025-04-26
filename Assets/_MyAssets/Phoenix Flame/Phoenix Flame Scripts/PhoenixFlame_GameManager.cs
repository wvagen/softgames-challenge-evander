using Softgames.Common;
using UnityEngine;


namespace Softgames.PhoenixFlame
{
    public class PhoenixFlame_GameManager : MonoBehaviour
    {
        [Header("META Params")]
        [SerializeField] private float lighteningSpeed = 2f;
        [SerializeField] private float initFireEmission = 2f;

        [SerializeField] private PhoenixFlame_StickLight lightStick;
        [SerializeField] private Animator lightAnimator;

        bool isFlaming = false;
        private void Start()
        {
            LightUnlightStickBtn();
        }
        public void LightUnlightStickBtn()
        {
            isFlaming = !isFlaming;
            lightStick.LightUnlightStick(initFireEmission,lighteningSpeed);
            lightAnimator.SetBool(Constants.ANIM_PARAM_FLAME, isFlaming);
            if(isFlaming) Common_AudioManager.audioManInstance.Play_Sfx("fireSFX");
            else Common_AudioManager.audioManInstance.Stop_Sfx("fireSFX");
        }
    }
}
