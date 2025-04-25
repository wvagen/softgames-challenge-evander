using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Softgames.AceOfShadows
{
    public class AceOfShadows_Card : MonoBehaviour
    {
        [SerializeField]
        private AceOfShadows_CardScriptable myCardProps;
        [SerializeField]
        private Image myContentImg;
        [SerializeField]
        private TextMeshProUGUI myContentTxt;

        private void Start()
        {
            myContentImg.sprite = myCardProps.cardSprite;
            myContentTxt.text = myCardProps.cardName;
        }
    }
}
