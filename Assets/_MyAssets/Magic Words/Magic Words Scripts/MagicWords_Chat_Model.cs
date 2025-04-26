using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace Softgames.MagicWords
{
    [Serializable]
    public class MagicWords_Chat_Model
    {
        public List<Dialogue> Dialogues { get; set; } = new List<Dialogue>();
        public List<Avatars> Avatars { get; set; }
        public List<Emojies> Emojies { get; set; }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public MagicWords_Chat_Model FromJson(string json)
        {
            Dialogues = JsonUtility.FromJson<List<Dialogue>>(json);
            return JsonUtility.FromJson<MagicWords_Chat_Model>(json);
        }

    }

    [Serializable]
    public class Dialogue
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static Dialogue FromJson(string json)
        {
            return JsonUtility.FromJson<Dialogue>(json);
        }

        public override string ToString()
        {
            return $"Name: {Name ?? "null"}, Text={Text ?? "null"}";
        }

    }

    [Serializable]
    public class Emojies
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public Emojies FromJson(string json)
        {
            return JsonUtility.FromJson<Emojies>(json);
        }

        public override string ToString()
        {
            return $"Name: {Name ?? "null"}, URL={Url ?? "null"}";
        }

    }

    [Serializable]
    public class Avatars
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public string Position { get; set; }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static Avatars FromJson(string json)
        {
            return JsonUtility.FromJson<Avatars>(json);
        }

        public override string ToString()
        {
            return $"Name: {Name ?? "null"}, URL={Url ?? "null"} , Position={Position ?? "null"}";
        }

    }
}
