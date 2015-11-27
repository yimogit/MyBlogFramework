using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Config
{
    public static class QueryEnum
    {
        /// <summary>
        /// 上传类型
        /// </summary>
        public enum UploadType
        {
            /// <summary>
            /// 普通图片
            /// </summary>
            WebImg = 1,
            /// <summary>
            /// 网站logo
            /// </summary>
            WebLogo = 2,
            /// <summary>
            /// Banner图片
            /// </summary>
            WebBanner=3,
            /// <summary>
            /// 新闻图片
            /// </summary>
            WebNews=4,
            /// <summary>
            /// 产品图片
            /// </summary>
            WebPros=5
        }
        public enum VerifCodeType
        {
            /// <summary>
            /// 无验证码
            /// </summary>
            NoCode = 1,
            /// <summary>
            /// 普通验证码
            /// </summary>
            DigitalCode = 2,
            /// <summary>
            /// 极限验证码
            /// </summary>
            LimitCode = 3
        }
        public enum CurrentPage
        { 
            Index=1,
            About=2,
            News=3,
            Pros=4,
            Cases=5,
            Contact=6
        }
        public enum UrlType
        { 
            /// <summary>
            /// 友情链接
            /// </summary>
            FriendLink=1,
            /// <summary>
            /// 我的收藏
            /// </summary>
            Collection=2
        }
        public static UrlType GetUrlTypeByID(int? id)
        {
            switch (id)
            {
                case 1:
                    return UrlType.FriendLink;
                case 2:
                    return UrlType.Collection;
                default:
                    return UrlType.FriendLink;
            }
        }


    }
}