using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Softgames.Common;
using TMPro;

namespace Softgames.AceOfShadows
{
    public class AceOfShadows_GameManager : MonoBehaviour
    {

        //META Params
        [Header("META Params")]

        [SerializeField] private float cardsMargin = 0.05f;
        [SerializeField] private float cardsDrawSpeed = 1f;
        [SerializeField] private float cardsSlideSpeed = 2f;

        [SerializeField]
        private List<AceOfShadows_CardScriptable> myCards;
        [SerializeField]
        private AceOfShadows_Card cardPrefab;

        [SerializeField]
        private Transform cardsSpawnPos,targetPosition;
        [SerializeField]
        private GameObject startBtn, stopBtn;
        [SerializeField]
        private TextMeshProUGUI cardCountTxt;

        private List<AceOfShadows_Card> cardsStack;

        List<int> randomIndexes = new List<int>();
        int cardReaeachedIndex = 0;

        void Start()
        {
            SpawnCardsRandomly();
        }

        void SpawnCardsRandomly()
        {
            Vector3 accMarginVec = Vector2.zero;
            randomIndexes = new List<int>();
            cardsStack = new List<AceOfShadows_Card>();

            for (int i = 0; i < myCards.Count; i++)
            {
                randomIndexes.Add(i);
            }

            //Spawn the cards randomly
            for (int i = 0; i < myCards.Count; i++)
            {
                AceOfShadows_Card cardScript = Instantiate(cardPrefab,cardsSpawnPos).GetComponent<AceOfShadows_Card>();
                cardScript.transform.position += accMarginVec;
                int randomCardIndex = Random.Range(0, randomIndexes.Count);
                cardScript.SetMyCardProps(myCards[randomIndexes[randomCardIndex]]);
                cardsStack.Insert(0,cardScript);
                randomIndexes.RemoveAt(randomCardIndex);
                accMarginVec.x -= cardsMargin;
            }
        }

        public void SartDrawingCard()
        {
            StartCoroutine(SartDrawingCardsCouroutine());
            startBtn.SetActive(false);
            stopBtn.SetActive(true);
        }
        public void StopDrawingCards()
        {
            StopAllCoroutines();
            startBtn.SetActive(true);
            stopBtn.SetActive(false);
        }

        IEnumerator SartDrawingCardsCouroutine()
        {
            Vector3 accMarginVec = Vector2.zero;
            for (int i = cardReaeachedIndex; i < cardsStack.Count; i++)
            {
                Common_AudioManager.audioManInstance.Play_Sfx("drawCardSFX");
                cardsStack[i].gameObject.LeanMoveX(targetPosition.position.x, cardsSlideSpeed);
                cardReaeachedIndex++;
                cardsStack[i].transform.SetParent(targetPosition);
                cardsStack[i].transform.SetAsLastSibling();
                yield return new WaitForSeconds(cardsDrawSpeed);
                cardCountTxt.text = "Card Count\n" + cardReaeachedIndex.ToString();
            }
        }
    }
}