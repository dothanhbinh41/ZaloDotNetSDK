using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaloDotNetSDK
{
    public class ZaloEndpoint
    {
        //  OA API ENDPOINT
        public static string GET_TAGS_ENDPOINT = GetEndpoint("tag/gettagsofoa");
        public static string TAG_FOLLOWER_ENDPOINT = GetEndpoint("tag/tagfollower");
        public static string REMOVE_TAG_ENDPOINT = GetEndpoint("tag/rmtag");
        public static string REMOVE_FOLLOWER_FROM_TAG_ENDPOINT = GetEndpoint("tag/rmfollowerfromtag");
        public static string SEND_TEXT_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/text");
        public static string SEND_IMAGE_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/image");
        public static string SEND_LINKS_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/links");
        public static string SEND_ACTION_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/actionlist");
        public static string SEND_STICKER_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/sticker");
        public static string SEND_GIF_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/gif");
        public static string GET_PROFILE_EDNPOINT = GetEndpoint("getprofile");
        public static string UPLOAD_IMAGE_ENDPOINT = GetEndpoint("upload/image");
        public static string UPLOAD_GIF_ENDPOINT = GetEndpoint("upload/gif");

        public static string GET_MESSAGE_STATUS_ENDPOINT = GetEndpoint("getmessagestatus");
        public static string REPLY_TEXT_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/reply/text");
        public static string REPLY_IMAGE_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/reply/image");
        public static string REPLY_LINKS_MESSAGE_ENDPOINT = GetEndpoint("sendmessage/reply/links");
        public static string CREATE_QRCODE_ENDPOINT = GetEndpoint("qrcode");

        // STORE API ENDPOINT
        public static string UPDATE_VARIATION_ENDPOINT = GetEndpoint("store/product/updatevariation");
        public static string ADD_VARIATION_ENDPOINT = GetEndpoint("store/product/addvariation");
        public static string GET_ATTR_ENDPOINT = GetEndpoint("store/product/mgetattr");
        public static string GET_ATTR_OF_OA_ENDPOINT = GetEndpoint("store/product/getattrofoa");
        public static string UPDATE_ATTR_ENDPOINT = GetEndpoint("store/product/updateattr");
        public static string CREATE_ATTR_ENDPOINT = GetEndpoint("store/product/createattr");
        public static string GET_ATTR_TYPE_OF_OA_ENDPOINT = GetEndpoint("store/product/getattrtypeofoa");
        public static string CREATE_PRODUCT_ENDPOINT = GetEndpoint("store/product/create");
        public static string UPDATE_PRODUCT_ENDPOINT = GetEndpoint("store/product/update");
        public static string REMOVE_PRODUCT_ENDPOINT = GetEndpoint("store/product/remove");
        public static string GET_PRODUCT_ENDPOINT = GetEndpoint("store/product/getproduct");
        public static string GET_PRODUCT_OF_OA_ENDPOINT = GetEndpoint("store/product/getproductofoa");
        public static string UPLOAD_PRODUCT_PHOTO_ENDPOINT = GetEndpoint("store/upload/productphoto");
        public static string CREATE_CATEGORY_ENDPOINT = GetEndpoint("store/category/create");
        public static string UPDATE_CATEGORY_ENDPOINT = GetEndpoint("store/category/update");
        public static string GET_CATEGORY_OF_OA_ENDPOINT = GetEndpoint("store/category/getcategoryofoa");
        public static string UPLOAD_CATEGORY_PHOTO = GetEndpoint("store/upload/categoryphoto");
        public static string UPDATE_ORDER_ENDPOINT = GetEndpoint("store/order/update");
        public static string GET_ORDER_OF_OA_ENDPOINT = GetEndpoint("store/order/getorderofoa");
        public static string GET_ORDER_ENDPOINT = GetEndpoint("store/order/getorder");
        public static string UPDATE_SHOP_ENDPOINT = GetEndpoint("store/updateshop");

        // ARTICLE API ENDPOINT
        public static string GET_SLICE_VIDEO_MEDIA_ENDPOINT = GetEndpoint("media/video/getslice");
        public static string UPDATE_VIDEO_MEDIA_ENDPOINT = GetEndpoint("media/video/update");
        public static string CREATE_VIDEO_MEDIA_ENDPOINT = GetEndpoint("media/video/create");
        public static string CREATE_MEDIA_ENDPOINT = GetEndpoint("media/create");
        public static string VERIFY_MEDIA_ENDPOINT = GetEndpoint("media/verify");
        public static string UPDATE_MEDIA_ENDPOINT = GetEndpoint("media/update");
        public static string REMOVE_MEDIA_ENDPOINT = GetEndpoint("media/remove");
        public static string GET_SLICE_MEDIA_ENDPOINT = GetEndpoint("media/getslice");
        public static string BROADCAST_MEDIA_ENDPOINT = GetEndpoint("broadcast/medias");
        public static string GET_UPLOAD_LINK_ENDPOINT = GetEndpoint("media/upload/video");
        public static string GET_MEDIA_VIDEO_ID_ENDPOINT = GetEndpoint("media/getvideoid");
        public static string GET_MEDIA_VIDEO_STATUS_ENDPOINT = GetEndpoint("media/getvideostatus");


        private static string GetEndpoint(string url)
        {
            return string.Format("{0}/{1}/{2}", APIConfig.DEFAULT_OA_API_BASE, APIConfig.DEFAULT_OA_API_VERSION, url);
        }
    }
}
