using UnityEngine;


namespace Softgames.PhoenixFlame
{
    public class PhoenixFlame_StickLight : MonoBehaviour
    {
        [SerializeField] private ParticleSystem myFireParticule;

        bool isLighting = false;

        float initFireEmission;
        float lighteningSpeed;

        public void LightUnlightStick(float initFireEmission, float lighteningSpeed)
        {
            isLighting = !isLighting;

            this.initFireEmission = initFireEmission;
            this.lighteningSpeed = lighteningSpeed;
        }

        private void Update()
        {
            if (isLighting)
            {
                var em = myFireParticule.emission;
                em.rateOverTime = Mathf.Lerp(em.rateOverTime.constant, initFireEmission, lighteningSpeed * Time.deltaTime);
            }
            else
            {
                var em = myFireParticule.emission;
                em.rateOverTime = Mathf.Lerp(em.rateOverTime.constant, 0, lighteningSpeed * Time.deltaTime);
            }
        }
    }
}
