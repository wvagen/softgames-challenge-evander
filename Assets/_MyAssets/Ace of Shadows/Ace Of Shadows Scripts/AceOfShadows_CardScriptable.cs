using UnityEngine;

namespace Softgames.AceOfShadows
{
    [CreateAssetMenu(fileName = "New Ace Of Shadows Card", menuName = "Ace of Shadows Card")]
    public class AceOfShadows_CardScriptable : ScriptableObject
    {
        public string cardName;
        public Sprite cardSprite;
        public Sprite cardBG;
    }
}
