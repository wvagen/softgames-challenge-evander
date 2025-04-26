using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;

namespace Softgames.MagicWords
{
    public class MagicWords_ChatContent : MonoBehaviour
    {
        [SerializeField] private RectTransform characterBubble;
        [SerializeField] private Image bubbleChatCharacterImg;
        [SerializeField] private TextMeshProUGUI bubbleChatCharacterTxt;
        [SerializeField] private GameObject textChatFragment;
        [SerializeField] private GameObject spriteChatFragment;
        [SerializeField] private Transform chatContainer;

        public void SetMe(bool isRight, Sprite avatarImg,string avatarName, List<MagicWords_ChatFragment> chatFragments)
        {
            bubbleChatCharacterImg.sprite = avatarImg;
            bubbleChatCharacterTxt.text = avatarName;
            Vector3 charInitPos = characterBubble.anchoredPosition3D;
            charInitPos.x = isRight ? Mathf.Abs(charInitPos.x) : charInitPos.x;
            characterBubble.anchoredPosition3D = charInitPos;


            for (int i = 0; i < chatFragments.Count; i++)
            {
                if (string.IsNullOrEmpty(chatFragments[i].Text))
                {
                    //Spawn image chunk
                    Instantiate(spriteChatFragment, chatContainer).GetComponent<Image>().sprite = chatFragments[i].Sprite;
                }
                else
                {
                    //Spawn text chunk
                    Instantiate(textChatFragment, chatContainer).GetComponent<Text>().text = chatFragments[i].Text;
                }
            }
            chatContainer.GetComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            StartCoroutine(ContainerFitFixer());
        }

        IEnumerator ContainerFitFixer()
        {
            yield return new WaitForEndOfFrame();
            chatContainer.gameObject.SetActive(false);//This is to adjust the chat container with the Unity Frame Calculation
            yield return new WaitForEndOfFrame();
            chatContainer.gameObject.SetActive(true);
        }
    }
}
