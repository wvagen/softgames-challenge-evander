using System.Net.Http;
using System.Threading.Tasks;
using System;
using UnityEngine;

namespace Softgames.MagicWords
{
    public class MagicWords_Chat_Controller : MonoBehaviour
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<MagicWords_Chat_Model> GetChat()
        {
            try
            {
                var responseString = await client.GetStringAsync(Constants.API_URL_CHAT);
                Debug.Log(responseString);
                MagicWords_Chat_Model newChatModel = new MagicWords_Chat_Model();
                newChatModel = newChatModel.FromJson(responseString);

                return newChatModel;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }

        }
    }
}
