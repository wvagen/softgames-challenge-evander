using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Softgames.MagicWords
{
    [Serializable]
    public class MagicWords_Chat_Model
    {
        public List<Dialogue> dialogue { get; set; }
        public List<Avatars> avatars { get; set; }
        public List<Emojies> emojies { get; set; }

        public static MagicWords_Chat_Model FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MagicWords_Chat_Model>(json);
        }

    }

    public class Dialogue
    {
        public string Name { get; set; }
        public string Text { get; set; }

    }

    public class Emojies
    {
        public string Name { get; set; }
        public string Url { get; set; }

    }

    public class Avatars
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Position { get; set; }

    }
}
