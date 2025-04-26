using Softgames.Common;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

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
        public string textChat;
        private List<MagicWords_ChatFragment> randomCon;

        //End of variables testing purposes

        Dictionary<string, Sprite> chatEmojies = new Dictionary<string, Sprite>(); // 0: sad , 1:intrigued , 

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
            MagicWords_ChatContent chatContent = Instantiate(conversationGO, conversationPos).GetComponent<MagicWords_ChatContent>();
            randomCon = ChatToChunks(textChat);
            chatContent.SetMe(false, charImg, randomCon);
        }

        List<MagicWords_ChatFragment> ChatToChunks(string text)
        {
            List<MagicWords_ChatFragment> chatChunks = new List<MagicWords_ChatFragment>();
            chatEmojies["affirmative"] = emojies[0];
            chatEmojies["intrigued"] = emojies[1];
            chatEmojies["neutral"] = emojies[2];
            bool isEmojiFound = false;

            foreach (KeyValuePair<string, Sprite> entry in chatEmojies)
            {
                if (text.Contains(entry.Key))
                {
                    isEmojiFound = true;
                    string[] textChunks = text.Split(new string[] { "{" + entry.Key + "}" }, StringSplitOptions.None);

                    if (text[0] == '{') // Emoji then text
                    {
                        MagicWords_ChatFragment newChunk = new MagicWords_ChatFragment();
                        newChunk.Sprite = chatEmojies[entry.Key];
                        chatChunks.Add(newChunk);

                        MagicWords_ChatFragment newChunk1 = new MagicWords_ChatFragment();
                        newChunk1.Text = textChunks[0];
                        chatChunks.Add(newChunk1);
                    }
                    else if (text[text.Length - 1] == '}') // Text then emoji
                    {
                        MagicWords_ChatFragment newChunk1 = new MagicWords_ChatFragment();
                        newChunk1.Text = textChunks[0];
                        chatChunks.Add(newChunk1);

                        MagicWords_ChatFragment newChunk = new MagicWords_ChatFragment();
                        newChunk.Sprite = chatEmojies[entry.Key];
                        chatChunks.Add(newChunk);

                    }
                    else if (textChunks.Length > 1) // Text then Emoji then Text
                    {
                        MagicWords_ChatFragment newChunk1 = new MagicWords_ChatFragment();
                        newChunk1.Text = textChunks[0];
                        chatChunks.Add(newChunk1);

                        MagicWords_ChatFragment newChunk2 = new MagicWords_ChatFragment();
                        newChunk2.Sprite = chatEmojies[entry.Key];
                        chatChunks.Add(newChunk2);

                        MagicWords_ChatFragment newChunk3 = new MagicWords_ChatFragment();
                        newChunk3.Text = textChunks[1];
                        chatChunks.Add(newChunk3);
                    }
                }

            }
            if (!isEmojiFound)
            {
                MagicWords_ChatFragment newChunk = new MagicWords_ChatFragment();
                newChunk.Text = text;
                chatChunks.Add(newChunk);
            }
            return chatChunks;

        }


        //For testing purposes
        List<MagicWords_ChatFragment> RandomConversation()
        {
            List<MagicWords_ChatFragment> randomCon = new List<MagicWords_ChatFragment>();

            for (int i = 0; i < 3; i++)
            {
                MagicWords_ChatFragment con = new MagicWords_ChatFragment();
                if (i % 2 == 0) con.Text = RandomString(UnityEngine.Random.Range(10, 20));
                else con.Sprite = emojies[UnityEngine.Random.Range(0, emojies.Count)];

                randomCon.Add(con);
            }

            return randomCon;
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[UnityEngine.Random.Range(0, s.Length)]).ToArray());
        }

        //End of testing purposes

    }
}
