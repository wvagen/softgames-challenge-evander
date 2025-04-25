using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Softgames.AceOfShadows
{
    public class AceOfShadows_Card : MonoBehaviour
    {
        [SerializeField]
        private Image myContentImg;
        [SerializeField]
        private Image myCardBG;
        [SerializeField]
        private TextMeshProUGUI myContentTxt;

        public void SetMyCardProps(AceOfShadows_CardScriptable newCardProps)
        {
            myContentImg.sprite = newCardProps.cardSprite;
            myContentTxt.text = newCardProps.cardName;
            myCardBG.sprite = newCardProps.cardBG;
        }

    }
}
