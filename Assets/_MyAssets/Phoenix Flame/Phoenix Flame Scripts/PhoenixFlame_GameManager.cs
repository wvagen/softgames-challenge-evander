using UnityEngine;


namespace Softgames.PhoenixFlame
{
    public class PhoenixFlame_GameManager : MonoBehaviour
    {
        [Header("META Params")]
        [SerializeField] private float lighteningSpeed = 2f;
        [SerializeField] private float initFireEmission = 2f;

        [SerializeField] private PhoenixFlame_StickLight lightStick;

        public void LightUnlightStickBtn()
        {
            lightStick.LightUnlightStick(initFireEmission,lighteningSpeed);
        }
    }
}
