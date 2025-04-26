using Softgames.Common;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace Softgames.MagicWords
{
    public class MagicWords_GameManager : MonoBehaviour
    {
       
        [SerializeField] private GameObject conversationGO;
        [SerializeField] private Transform conversationPos;

        [SerializeField] private GameObject skipBtn;
        [SerializeField] private GameObject startBtn, startOverBtn;

        //For testing purposes
        [Header("Testing purposes")]
        public Sprite charImg;
        public List<Sprite> emojies;
        private List<MagicWords_ChatFragment> randomCon;

        private Common_UI common_UI;

        //End of variables testing purposes

        Dictionary<string, Sprite> chatEmojies = new Dictionary<string, Sprite>(); 
        Dictionary<string, AvatarProps> avatars = new Dictionary<string, AvatarProps>();
        int indexConv = 0;

        public Sprite defaultCharacterImg;

        public class AvatarProps
        {
            public Sprite img { get; set; }
            public string name { get; set; }
            public bool isRight { get; set; }
        }

        MagicWords_Chat_Model chatModel;

        private void Start()
        {
            common_UI = FindFirstObjectByType<Common_UI>(); // I know its not recommended just because the deadline
        }

        public async void StartBtn()
        {
            CleanChat();
            startOverBtn.SetActive(true);
            startBtn.SetActive(false);
            common_UI.Loading(true);
            //chatModel = await MagicWords_Chat_Controller.GetChat(); // This doesnt work on WebGL
            indexConv = 0;
            StartCoroutine(ExtractChatContent());
        }

        public void SpawnConversation(int convIndex)
        {
            MagicWords_ChatContent chatContent = Instantiate(conversationGO, conversationPos).GetComponent<MagicWords_ChatContent>();
            randomCon = ChatToChunks(chatModel.dialogue[convIndex].Text);
            if (avatars.ContainsKey(chatModel.dialogue[convIndex].Name))
            {
                chatContent.SetMe(avatars[chatModel.dialogue[convIndex].Name].isRight, avatars[chatModel.dialogue[convIndex].Name].img, avatars[chatModel.dialogue[convIndex].Name].name, randomCon);
            }
            else
            {
                chatContent.SetMe(false, defaultCharacterImg, chatModel.dialogue[convIndex].Name, randomCon);
            }
        }

        IEnumerator ExtractChatContent()
        {
            //This is not very clean but because the async  made some problems with the WEBGL build
            using (UnityWebRequest webRequest = UnityWebRequest.Get(Constants.API_URL_CHAT))
            {
                yield return webRequest.SendWebRequest();

                chatModel = MagicWords_Chat_Model.FromJson(webRequest.downloadHandler.text);
            }

            for (int i = 0; i < chatModel.emojies.Count; i++)
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(chatModel.emojies[i].Url);
                yield return www.SendWebRequest();
                Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                Sprite newSprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(.5f, .5f));
                chatEmojies[chatModel.emojies[i].Name] = newSprite;
            }

            for (int i = 0; i < chatModel.avatars.Count; i++)
            {
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(chatModel.avatars[i].Url);
                yield return www.SendWebRequest();
                Texture2D myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;

                Sprite newSprite = Sprite.Create(myTexture, new Rect(0, 0, myTexture.width, myTexture.height), new Vector2(.5f, .5f));
                AvatarProps avatarProps = new AvatarProps();
                avatarProps.name = chatModel.avatars[i].Name;
                avatarProps.isRight = chatModel.avatars[i].Position.ToLower() == "right";
                avatarProps.img = newSprite;
                avatars[chatModel.avatars[i].Name] = avatarProps;
            }

            skipBtn.SetActive(true);
            common_UI.Loading(false);
            SkipBtn();
        }

        public void SkipBtn()
        {
            CleanChat();
            SpawnConversation(indexConv);
            indexConv++;
            if (indexConv >= chatModel.dialogue.Count)
            {
                skipBtn.SetActive(false);
            }
        }

        void CleanChat()
        {
            for (int i = 0; i < conversationPos.childCount; i++)
            {
                Destroy(conversationPos.transform.GetChild(i).gameObject);
            }
        }


        List<MagicWords_ChatFragment> ChatToChunks(string text)
        {
            List<MagicWords_ChatFragment> chatChunks = new List<MagicWords_ChatFragment>();
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
