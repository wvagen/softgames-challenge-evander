using UnityEngine;
using UnityEngine.EventSystems;

namespace Softgames.Common
{
    public class Common_Btn : EventTrigger
    {
        Vector2 initScale;
        private void Start()
        {
            initScale = transform.localScale;
        }

        public override void OnPointerEnter(PointerEventData data)
        {
            Common_AudioManager.audioManInstance.Play_Sfx("hoverBtnSFX");
            gameObject.transform.localScale = initScale * 1.1f;
        }

        public override void OnPointerExit(PointerEventData data)
        {
            gameObject.transform.localScale = initScale;
        }


    }
}
