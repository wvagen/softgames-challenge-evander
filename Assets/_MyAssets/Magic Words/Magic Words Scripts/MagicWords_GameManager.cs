using Softgames.Common;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Softgames.MagicWords
{
    public class MagicWords_GameManager : MonoBehaviour
    {
        [SerializeField] private Common_UI common_UI;
        [SerializeField] private GameObject conversationGO;
        [SerializeField] private Transform conversationPos;

        //For testing purposes
        [Header("Testing purposes")]
        public Sprite charImg;
        public List<Sprite> emojies;
        private List<MagicWords_ChatFragment> randomCon;

        bool canStartOver = false;
        MagicWords_Chat_Model chatModel;
        public async void StartBtn()
        {
            canStartOver = !canStartOver;
            //common_UI.Loading(canStartOver);
            //chatModel = await MagicWords_Chat_Controller.GetChat();
            //Debug.Log(chatModel.Dialogues[0].Name);
            SpawnConversation();
        }

        public void SpawnConversation()
        {
            MagicWords_ChatContent chatContent = Instantiate(conversationGO,conversationPos).GetComponent<MagicWords_ChatContent>();
            randomCon = RandomConversation();
            chatContent.SetMe(false, charImg, randomCon);
        }
        
        //For testing purposes
        List<MagicWords_ChatFragment> RandomConversation()
        {
            List<MagicWords_ChatFragment> randomCon = new List<MagicWords_ChatFragment>();

            for (int i = 0; i < 3; i++)
            {
                MagicWords_ChatFragment con = new MagicWords_ChatFragment();
                if (i % 2 == 0) con.Text = RandomString(Random.Range(10, 20));
                else con.Sprite = emojies[Random.Range(0, emojies.Count)];

                randomCon.Add(con);
            }

            return randomCon;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Range(0,s.Length)]).ToArray());
        }

        //End of testing purposes

    }
}
