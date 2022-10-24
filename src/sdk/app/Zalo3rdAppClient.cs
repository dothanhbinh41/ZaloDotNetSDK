using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ZaloDotNetSDK;

namespace ZaloCSharpSDK
{
    public class Zalo3rdAppClient : ZaloBaseClient
    {
        private ZaloAppInfo _appInfo;

        public Zalo3rdAppClient(ZaloAppInfo _appInfo)
        {
            this._appInfo = _appInfo;
        }
        private static string LOGIN_ENPOINT = "https://oauth.zaloapp.com/v3/auth?app_id={0}&redirect_uri={1}";
        public string LoginUrl => string.Format(LOGIN_ENPOINT, _appInfo.AppId, _appInfo.CallbackUrl);

        private static string ACCESSTOKEN_ENPOINT = "https://oauth.zaloapp.com/v3/access_token";
        public JObject GetAccessToken(string oauthCode)
        {
            string response = "";
            try
            {
                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
                param.Add("app_id", _appInfo.AppId.ToString());
                param.Add("app_secret", _appInfo.SecretKey);
                param.Add("code", oauthCode);
                response = SendHttpGetRequest(ACCESSTOKEN_ENPOINT, param, APIConfig.DEFAULT_HEADER);
                return JObject.Parse(response);
            }
            catch (Exception e)
            {
                throw new APIException(response);
            }
        }

        private static string GET_PROFILE_ENPOINT = "https://graph.zalo.me/" + APIConfig.DEFAULT_3RDAPP_API_VERSION + "/me";
        public JObject GetProfile(string accessToken, string fields)
        {
            string response = "";
            try
            {
                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
                param.Add("access_token", accessToken);
                param.Add("fields", fields);
                response = SendHttpGetRequest(GET_PROFILE_ENPOINT, param, APIConfig.DEFAULT_HEADER);
                return JObject.Parse(response);
            }
            catch (Exception e)
            {
                throw new APIException(response);
            }
        }

        private static string GET_FRIENDS_ENPOINT = "https://graph.zalo.me/" + APIConfig.DEFAULT_3RDAPP_API_VERSION + "/me/friends";
        public JObject GetFriends(string accessToken, int offset, int limit, string fields)
        {
            string response = "";
            try
            {
                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
                param.Add("access_token", accessToken);
                param.Add("offset", offset.ToString());
                param.Add("limit", limit.ToString());
                param.Add("fields", fields);
                response = SendHttpGetRequest(GET_FRIENDS_ENPOINT, param, APIConfig.DEFAULT_HEADER);
                return JObject.Parse(response);
            }
            catch (Exception e)
            {
                throw new APIException(response);
            }
        }

        private static string GET_INVITABLE_FRIENDS_ENPOINT = "https://graph.zalo.me/" + APIConfig.DEFAULT_3RDAPP_API_VERSION + "/me/invitable_friends";
        public JObject GetInvitableFriends(string accessToken, int offset, int limit, string fields)
        {
            string response = "";
            try
            {
                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
                param.Add("access_token", accessToken);
                param.Add("offset", offset.ToString());
                param.Add("limit", limit.ToString());
                param.Add("fields", fields);
                response = SendHttpGetRequest(GET_INVITABLE_FRIENDS_ENPOINT, param, APIConfig.DEFAULT_HEADER);
                return JObject.Parse(response);
            }
            catch (Exception e)
            {
                throw new APIException(response);
            }
        }

        private static string POST_FEED_ENPOINT = "https://graph.zalo.me/" + APIConfig.DEFAULT_3RDAPP_API_VERSION + "/me/feed";
        public JObject PostFeed(string accessToken, string message, string link)
        {
            string response = "";
            try
            {
                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
                param.Add("access_token", accessToken);
                param.Add("message", message);
                param.Add("link", link);
                response = SendHttpPostRequest(POST_FEED_ENPOINT, param, APIConfig.DEFAULT_HEADER);
                return JObject.Parse(response);
            }
            catch (Exception e)
            {
                throw new APIException(response);
            }
        }

        private static string SEND_APP_REQUEST_ENPOINT = "https://graph.zalo.me/" + APIConfig.DEFAULT_3RDAPP_API_VERSION + "/apprequests";
        public JObject SendAppRequest(string accessToken, List<long> toUserIds, string message)
        {
            string response = "";
            try
            {
                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
                param.Add("access_token", accessToken);
                param.Add("to", string.Join(",", toUserIds));
                param.Add("message", message);
                response = SendHttpPostRequest(SEND_APP_REQUEST_ENPOINT, param, APIConfig.DEFAULT_HEADER);
                return JObject.Parse(response);
            }
            catch (Exception e)
            {
                throw new APIException(response);
            }
        }

        private static string SEND_MESSAGE_ENDPOINT = $"https://graph.zalo.me/{APIConfig.DEFAULT_3RDAPP_API_VERSION}/me/message";
        public JObject SendMessage(string accessToken, long userId, string message, string link)
        {
            string response = "";
            try
            {
                Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
                param.Add("access_token", accessToken);
                param.Add("to", userId.ToString());
                param.Add("message", message);
                param.Add("link", link);
                response = SendHttpPostRequest(SEND_MESSAGE_ENDPOINT, param, APIConfig.DEFAULT_HEADER);
                return JObject.Parse(response);
            }
            catch (Exception e)
            {
                throw new APIException(response);
            }
        }
    }
}