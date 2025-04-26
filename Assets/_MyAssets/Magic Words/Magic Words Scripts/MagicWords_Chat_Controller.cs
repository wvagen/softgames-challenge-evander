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
                return MagicWords_Chat_Model.FromJson(responseString);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return null;
            }

        }
    }
}
