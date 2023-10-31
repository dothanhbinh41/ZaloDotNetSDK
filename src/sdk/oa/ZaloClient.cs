using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using ZaloDotNetSDK.entities;
using ZaloDotNetSDK.Utils;
using ZaloDotNetSDK.entities.oa;
using ZaloDotNetSDK.entities.shop;

namespace ZaloDotNetSDK
{
    public class ZaloClient : ZaloBaseClient
    {
        string access_token = "";

        public ZaloClient(string access_token)
        {
            this.Access_token = access_token;
        }

        public string Access_token { get => access_token; set => access_token = value; }

        public JObject ExcuteRequest(string method, string endPoint,  Dictionary<string, dynamic> param){
            if (param == null) {
                param = new Dictionary<string, dynamic>();
            }

            Dictionary<string, string> headers = APIConfig.createDefaultHeader();
            if (headers.ContainsKey("access_token")){
                headers.Remove("access_token");
            }
            headers.Add("access_token", this.Access_token);


            string response;

            if ("GET".Equals(method.ToUpper()))
            {
                response = SendHttpGetRequest(endPoint, param, headers);
            }
            else
            {
                if (param.ContainsKey("file"))
                {
                    response = SendHttpUploadRequest(endPoint, param, headers);
                }
                else if (param.ContainsKey("body"))
                {
                    response = SendHttpPostRequestWithBody(endPoint, param, param["body"], headers);
                }
                else 
                {
                    response = SendHttpPostRequest(endPoint, param, headers);
                }
            }
            
            JObject result = null;
            try
            {
                result = JObject.Parse(response);
            }
            catch (Exception e)
            {
                throw new APIException("Response is not json: " + response);
            }
            return result;
        }

        //==========================Article=====================================
        public JObject CreateArticle(Article article)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            param.Add("body", JsonUtils.ParseArticle2Json(article).ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/article/create", param);

            return result;
        }

        public JObject CreateVideoArticle(VideoArticle videoArticle)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            param.Add("body", JsonUtils.ParseVideoArticle2Json(videoArticle).ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/article/create", param);

            return result;
        }

        public JObject UpdateArticle(Article article, string id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject articleJson = JsonUtils.ParseArticle2Json(article);
            articleJson.Add("id", id);
            param.Add("body", articleJson.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/article/update", param);

            return result;
        }

        public JObject UpdateVideoArticle(VideoArticle videoArticle, string id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject articleJson = JsonUtils.ParseVideoArticle2Json(videoArticle);
            articleJson.Add("id", id);
            param.Add("body", articleJson.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/article/update", param);

            return result;
        }

        public JObject DeleteArticle(string id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject articleJson = new JObject();
            articleJson.Add("id", id);
            param.Add("body", articleJson.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/article/remove", param);

            return result;
        }

        public JObject GetsliceArticle(int offset, int limit, string type)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("offset", offset.ToString());
            param.Add("limit", limit.ToString());
            param.Add("type", type);

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/article/getslice", param);

            return result;
        }

        public JObject GetdetailArticle(string id)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("id", id);

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/article/getdetail", param);

            return result;
        }

        public JObject VerifyArticle(string token)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject articleJson = new JObject();
            articleJson.Add("token", token);
            param.Add("body", articleJson.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/article/verify", param);

            return result;
        }

        public JObject PrepareUploadVideoForArticle(ZaloFile zaloFile)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("file", zaloFile);

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/article/upload_video/preparevideo", param);

            return result;
        }

        public JObject VerifyUploadVideoForArticle(string token)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("token", token);

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/article/upload_video/verify", param);

            return result;
        }
        //==========================Article=====================================

        //==========================Message=====================================
        public JObject SendTextMessageToUserId(string user_id, string content)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendImageMessageToUserIdByUrl(string user_id, string content, string image_url)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new {
                media_type= "image",
                url = image_url
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendImageMessageToUserIdByAttachmentId(string user_id, string content, string image_attachment_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "image",
                attachment_id = image_attachment_id
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendGifMessageToUserIdByAttachmentId(string user_id, string content, string gif_attachment_id, int gif_width = 120, int gif_height = 120)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "gif",
                attachment_id = gif_attachment_id,
                width = gif_width,
                height = gif_height
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendListElementMessagetoUserId(string user_id, string content, List<Element> elements)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            List<JObject> elementsJson = JsonUtils.ParseListElement2Json(elements);

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "list",
                            elements = elementsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendListButtonMessagetoUserId(string user_id, string content, List<Button> buttons)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            List<JObject> buttonsJson = JsonUtils.ParseListButton2Json(buttons);

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            buttons = buttonsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendListButtonAndElementMessagetoUserId(string user_id, string content, List<Element> elements, List<Button> buttons)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            List<JObject> buttonsJson = JsonUtils.ParseListButton2Json(buttons);
            List<JObject> elementsJson = JsonUtils.ParseListElement2Json(elements);
            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "list",
                            elements = elementsJson,
                            buttons = buttonsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendRequestUserProfileToUserId(string user_id, string element_title, string element_subtitle, string url_image)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementsJson = new List<JObject>();
            elementsJson.Add(JObject.FromObject(new
            {
                title = element_title,
                subtitle = element_subtitle,
                image_url = url_image
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = "",
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "request_user_info",
                            elements = elementsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendFileToUserId(string user_id, string file_attachment_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    attachment = new
                    {
                        type = "file",
                        payload = new
                        {
                            token = file_attachment_id
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendTextMessageToMessageId(string message_id, string content)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    message_id
                },
                message = new
                {
                    text = content
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendImageMessageToMessageIdByUrl(string message_id, string content, string image_url)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "image",
                url = image_url
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    message_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject SendImageMessageToMessageIdByAttachmentId(string message_id, string content, string image_attachment_id)
        { 
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "image",
                attachment_id = image_attachment_id
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    message_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            return ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param); 
        }

        public JObject SendListButtonMessageToMessageId(string message_id, string content, List<Button> buttons)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            List<JObject> buttonsJson = JsonUtils.ParseListButton2Json(buttons);

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    message_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            buttons = buttonsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject GetAllTagOfOfficialAccount()
        {
            JObject result = new JObject();

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/oa/tag/gettagsofoa", null);

            return result;
        }

        public JObject TagFollower(string user_id, string tag_name)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                user_id,
                tag_name
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/tag/tagfollower", param);

            return result;
        }

        public JObject RemoveTagFromFollower(string user_id, string tag_name)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                user_id,
                tag_name
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/tag/rmfollowerfromtag", param);

            return result;
        }

        public JObject DeleteTag(string tag_name)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                tag_name
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/tag/rmtag", param);

            return result;
        }

        public JObject UploadImageForOfficialAccountAPI(ZaloFile zaloFile)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("file", zaloFile);

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/upload/image", param);

            return result;
        }

        public JObject UploadGifForOfficialAccountAPI(ZaloFile zaloFile)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("file", zaloFile);

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/upload/gif", param);

            return result;
        }

        public JObject UploadFileForOfficialAccountAPI(ZaloFile zaloFile)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("file", zaloFile);

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/upload/file", param);

            return result;
        }

        public JObject GetProfileOfFollower(string user_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject dataJson = JObject.FromObject(new
            {
                user_id
            });

            param.Add("data", dataJson.ToString());

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/oa/getprofile", param);

            return result;
        }

        public JObject GetProfileOfOfficialAccount()
        {
            JObject result = new JObject();

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/oa/getoa", null);

            return result;
        }

        public JObject GetListFollower(int offset, int count)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject dataJson = JObject.FromObject(new
            {
                offset,
                count
            });

            param.Add("data", dataJson.ToString());

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/oa/getfollowers", param);

            return result;
        }

        public JObject GetListRecentChat(int offset, int count)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject dataJson = JObject.FromObject(new
            {
                offset,
                count
            });

            param.Add("data", dataJson.ToString());

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/oa/listrecentchat", param);

            return result;
        }

        public JObject GetListConversationWithUser(long user_id, int offset, int count)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject dataJson = JObject.FromObject(new
            {
                user_id,
                offset,
                count
            });

            param.Add("data", dataJson.ToString());

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/oa/conversation", param);

            return result;
        }

        public JObject RegisterIP(string ip, string name)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                ip,
                name
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/registerip", param);

            return result;
        }

        public JObject removeIP(string ip, string name)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                ip,
                name
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/removeip", param);

            return result;
        }

        public JObject UpdateFollowerInfo(string user_id, string name, string phone, string address, int city_id, int district_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                user_id,
                name,
                phone,
                address,
                city_id,
                district_id

            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/updatefollowerinfo", param);

            return result;
        }

        public JObject BroadcastArticle(string attachment_id, TargetBroadcastGender gender, List<TargetBroadcastAges> ages=null, List<TargetBroadcastLocations> locations = null, List<TargetBroadcastCities> cities = null, List<TargetBroadcastPlatforms> platforms = null, List<TargetBroadcastTelcos> telcos = null)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            List<JObject> elementsJson = new List<JObject>();
            elementsJson.Add(JObject.FromObject(new
            {
                media_type = "article",
                attachment_id
            }));

            JObject target = new JObject();
            if (ages != null)
            {
                string agesStr = "";
                foreach (TargetBroadcastAges targetBroadcastAges in ages)
                {
                    agesStr += targetBroadcastAges.getValue() + ",";
                }
                agesStr = agesStr.Substring(0, agesStr.Length - 1);
                target.Add("ages", agesStr);
            }

            if (gender != null)
            {
                target.Add("gender", gender.getValue());
            }

            if (locations != null)
            {
                string locationsStr = "";
                foreach (TargetBroadcastLocations targetBroadcastLocations in locations)
                {
                    locationsStr += targetBroadcastLocations.getValue() + ",";
                }
                locationsStr = locationsStr.Substring(0, locationsStr.Length - 1);
                target.Add("locations", locationsStr);
            }

            if (cities != null)
            {
                string citiesStr = "";
                foreach (TargetBroadcastCities targetBroadcastCities in cities)
                {
                    citiesStr += targetBroadcastCities.getValue() + ",";
                }
                citiesStr = citiesStr.Substring(0, citiesStr.Length - 1);
                target.Add("cities", citiesStr);
            }

            if (platforms != null)
            {
                string platformsStr = "";
                foreach (TargetBroadcastPlatforms targetBroadcastPlatforms in platforms)
                {
                    platformsStr += targetBroadcastPlatforms.getValue() + ",";
                }
                platformsStr = platformsStr.Substring(0, platformsStr.Length - 1);
                target.Add("platforms", platformsStr);
            }

            if (telcos != null)
            {
                string telcosStr = "";
                foreach (TargetBroadcastTelcos targetBroadcastTelcos in telcos)
                {
                    telcosStr += targetBroadcastTelcos.getValue() + ",";
                }
                telcosStr = telcosStr.Substring(0, telcosStr.Length - 1);
                target.Add("telcos", telcosStr);
            }

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    target
                },
                message = new
                {
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject BroadcastLinks(List<Element> elements, TargetBroadcastGender gender, List<TargetBroadcastAges> ages = null, List<TargetBroadcastLocations> locations = null, List<TargetBroadcastCities> cities = null, List<TargetBroadcastPlatforms> platforms = null, List<TargetBroadcastTelcos> telcos = null)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            List<JObject> elementsJson = JsonUtils.ParseListElement2Json(elements);

            JObject target = new JObject();
            if (ages != null)
            {
                string agesStr = "";
                foreach (TargetBroadcastAges targetBroadcastAges in ages)
                {
                    agesStr += targetBroadcastAges.getValue() + ",";
                }
                agesStr = agesStr.Substring(0, agesStr.Length - 1);
                target.Add("ages", agesStr);
            }

            if (gender != null)
            {
                target.Add("gender", gender.getValue());
            }

            if (locations != null)
            {
                string locationsStr = "";
                foreach (TargetBroadcastLocations targetBroadcastLocations in locations)
                {
                    locationsStr += targetBroadcastLocations.getValue() + ",";
                }
                locationsStr = locationsStr.Substring(0, locationsStr.Length - 1);
                target.Add("locations", locationsStr);
            }

            if (cities != null)
            {
                string citiesStr = "";
                foreach (TargetBroadcastCities targetBroadcastCities in cities)
                {
                    citiesStr += targetBroadcastCities.getValue() + ",";
                }
                citiesStr = citiesStr.Substring(0, citiesStr.Length - 1);
                target.Add("cities", citiesStr);
            }

            if (platforms != null)
            {
                string platformsStr = "";
                foreach (TargetBroadcastPlatforms targetBroadcastPlatforms in platforms)
                {
                    platformsStr += targetBroadcastPlatforms.getValue() + ",";
                }
                platformsStr = platformsStr.Substring(0, platformsStr.Length - 1);
                target.Add("platforms", platformsStr);
            }

            if (telcos != null)
            {
                string telcosStr = "";
                foreach (TargetBroadcastTelcos targetBroadcastTelcos in telcos)
                {
                    telcosStr += targetBroadcastTelcos.getValue() + ",";
                }
                telcosStr = telcosStr.Substring(0, telcosStr.Length - 1);
                target.Add("telcos", telcosStr);
            }

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    target
                },
                message = new
                {
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "list",
                            elements= elementsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }
        //==========================Message=====================================

        //==========================Shop=====================================
        public JObject CreateProduct(Product product)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            param.Add("body", JsonUtils.ParseProduct2Json(product).ToString().Replace("\\","").Replace("\"[", "[").Replace("]\"", "]"));

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/product/create", param);

            return result;
        }

        public JObject UpdateProduct(Product product)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            param.Add("body", JsonUtils.ParseProduct2Json(product).ToString().Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]"));

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/product/update", param);

            return result;
        }

        public JObject RemoveProduct(string id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            JObject body = JObject.FromObject(new
            {
                id
            });
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/product/remove", param);

            return result;
        }

        public JObject GetdetailProduct(string id)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("id", id);

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/product/getproduct", param);

            return result;
        }

        public JObject SearchProductByProductCode(int offset, int limit, string code)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("offset", offset.ToString());
            param.Add("limit", limit.ToString());
            param.Add("code", code);

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/product/search", param);

            return result;
        }

        public JObject GetSliceProduct(int offset, int limit)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("offset", offset.ToString());
            param.Add("limit", limit.ToString());

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/product/getproductofoa", param);

            return result;
        }

        public JObject GetSliceOrder(int offset, int limit, int status)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("offset", offset.ToString());
            param.Add("limit", limit.ToString());
            param.Add("status", status.ToString());

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/order/getorderofoa", param);

            return result;
        }

        public JObject GetdetailOrder(string id)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("id", id);

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/order/getorder", param);

            return result;
        }

        public JObject UpdateStatusOrder(string id, int status, string cancel_reason = null, string edit_reason = null)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            JObject body;

            if (edit_reason != null)
            {
                body = JObject.FromObject(new
                {
                    id,
                    status,
                    edit_reason
                });
            } else if(cancel_reason != null)
            {
                body = JObject.FromObject(new
                {
                    id,
                    status,
                    cancel_reason
                });
            } else
            {
                body = JObject.FromObject(new
                {
                    id,
                    status
                });
            }
            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/order/update", param);

            return result;
        }

        public JObject CreateOrder(Order order)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            param.Add("body", JsonUtils.ParseOrder2Json(order).ToString().Replace("\\", "").Replace("\"[", "[").Replace("]\"", "]"));

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/order/create", param);

            return result;
        }

        public JObject GetSliceCategory(int offset, int limit)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("offset", offset.ToString());
            param.Add("limit", limit.ToString());

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/category/getcategoryofoa", param);

            return result;
        }

        public JObject CreateCategory(string name, string photo, string description, ShopStatus status)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                name,
                photo,
                description,
                status = status.Value
            });

            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/category/create", param);

            return result;
        }

        public JObject UpdateCategory(string id, string name, string photo, string description, ShopStatus status)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                id,
                name,
                photo,
                description,
                status = status.Value
            });

            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/category/update", param);

            return result;
        }

        public JObject UploadImageForShopAPI(ZaloFile zaloFile, string uploadType)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("file", zaloFile);
            param.Add("upload_type", uploadType);

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/upload/photo", param);

            return result;
        }

        public JObject CreateAttributeType(string name)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                name
            });

            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/product/createattributetype", param);

            return result;
        }

        public JObject GetAttributeType()
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/product/getsliceattributetype", param);

            return result;
        }

        public JObject CreateAttribute(string name, string attibuteType)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                name,
                type = attibuteType
            });

            param.Add("body", body.ToString());

            result = ExcuteRequest("POST", "https://openapi.zalo.me/v2.0/store/product/createattribute", param);

            return result;
        }

        public JObject GetSliceAttribute(int offset, int limit)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("offset", offset);
            param.Add("limit", limit);

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/product/getsliceattribute", param);

            return result;
        }

        public JObject GetAttribute(string id)
        {
            JObject result = new JObject();

            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            param.Add("id", id);

            result = ExcuteRequest("GET", "https://openapi.zalo.me/v2.0/store/product/getattribute", param);

            return result;
        }

        //==========================Shop=====================================

        //==========================Official Account API V3=====================================

        public JObject sendTextMessageToUserIdV3(string user_id, string content)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/cs", param);

            return result;
        }

        public JObject sendImageMessageToUserIdByUrlV3(string user_id, string content, string image_url)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "image",
                url = image_url
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/cs", param);

            return result;
        }

        public JObject sendImageMessageToUserIdByAttachmentIdV3(string user_id, string content, string image_attachment_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "image",
                attachment_id = image_attachment_id
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/cs", param);

            return result;
        }

        public JObject sendGifMessageToUserIdByAttachmentIdV3(string user_id, string content, string gif_attachment_id, int gif_width = 120, int gif_height = 120)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "gif",
                attachment_id = gif_attachment_id,
                width = gif_width,
                height = gif_height
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/cs", param);

            return result;
        }

        public JObject sendRequestUserProfileToUserIdV3(string user_id, string element_title, string element_subtitle, string url_image)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementsJson = new List<JObject>();
            elementsJson.Add(JObject.FromObject(new
            {
                title = element_title,
                subtitle = element_subtitle,
                image_url = url_image
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = "",
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "request_user_info",
                            elements = elementsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/cs", param);

            return result;
        }

        public JObject sendFileToUserIdV3(string user_id, string file_attachment_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    attachment = new
                    {
                        type = "file",
                        payload = new
                        {
                            token = file_attachment_id
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/cs", param);

            return result;
        }

        public JObject sendQuoteMessageToUserIdV3(string user_id, string content, string quote_message_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    text = content,
                    quote_message_id
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/cs", param);

            return result;
        }

        public JObject sendStickerMessageToUserIdV3(string user_id, string attachment_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "sticker",
                attachment_id
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/cs", param);

            return result;
        }

        public JObject sendTextMessageToAnonyous(string anonymous_id, string conversation_id, string content)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    anonymous_id,
                    conversation_id
                },
                message = new
                {
                    text = content
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject sendImageMessageToAnonyousByUrl(string anonymous_id, string conversation_id, string content, string image_url)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "image",
                url = image_url
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    anonymous_id,
                    conversation_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject sendImageMessageToAnonyousByAttachmentId(string anonymous_id, string conversation_id, string content, string image_attachment_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "image",
                attachment_id = image_attachment_id
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    anonymous_id,
                    conversation_id
                },
                message = new
                {
                    text = content,
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject sendFileToAnonyous(string anonymous_id, string conversation_id, string file_attachment_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    anonymous_id,
                    conversation_id
                },
                message = new
                {
                    attachment = new
                    {
                        type = "file",
                        payload = new
                        {
                            token = file_attachment_id
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject sendStickerMessageAnonyous(string anonymous_id, string conversation_id, string attachment_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            List<JObject> elementJson = new List<JObject>();
            elementJson.Add(JObject.FromObject(new
            {
                media_type = "sticker",
                attachment_id
            }));

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    anonymous_id,
                    conversation_id
                },
                message = new
                {
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "media",
                            elements = elementJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject sendReactionToMessage(string user_id, string react_icon, string react_message_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    react_icon,
                    react_message_id
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/message", param);

            return result;
        }

        public JObject getQuotaCSMessageFree()
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/quota/message", param);

            return result;
        }

        public JObject getQuotaCSMessageIn48h(string message_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                message_id
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/quota/message", param);

            return result;
        }

        public JObject getQuotaPromotionMessage(string user_id)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();

            JObject body = JObject.FromObject(new
            {
                user_id,
                type = "promotion"
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v2.0/oa/quota/message", param);

            return result;
        }

        public JObject sendTransactionMessagetoUserId(string user_id, string language, List<ElementV3> elements, List<ButtonV3> buttons, TransactionTemplateType template_type)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            List<JObject> buttonsJson = JsonUtils.parseListButtonV3ToJson(buttons);
            List<JObject> elementsJson = JsonUtils.parseListElementV3ToJson(elements);
            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = template_type.getValue(),
                            language,
                            elements = elementsJson,
                            buttons = buttonsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/transaction", param);

            return result;
        }

        public JObject sendPromotionMessagetoUserId(string user_id, string language, List<ElementV3> elements, List<ButtonV3> buttons)
        {
            JObject result = new JObject();
            Dictionary<string, dynamic> param = new Dictionary<string, dynamic>();
            List<JObject> buttonsJson = JsonUtils.parseListButtonV3ToJson(buttons);
            List<JObject> elementsJson = JsonUtils.parseListElementV3ToJson(elements);
            JObject body = JObject.FromObject(new
            {
                recipient = new
                {
                    user_id
                },
                message = new
                {
                    attachment = new
                    {
                        type = "template",
                        payload = new
                        {
                            template_type = "promotion",
                            language,
                            elements = elementsJson,
                            buttons = buttonsJson
                        }
                    }
                }
            });
            param.Add("body", body.ToString());

            result = excuteRequest("POST", "https://openapi.zalo.me/v3.0/oa/message/promotion", param);

            return result;
        }

        //==========================Official Account API V3=====================================
    }
}
