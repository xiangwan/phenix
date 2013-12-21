using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using NLog;

namespace Phenix.Infrastructure.Data
{
    public static class PagerHtmlHelper
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static MvcHtmlString Pager(this HtmlHelper helper, IPagedList pagedList,
            string pageIndexUrlParameterName = "page", int displayCount = 10)
        {
            if (pagedList.TotalItemCount <= pagedList.PageSize)
            {
                return MvcHtmlString.Empty;
            }

            string rawUrl = helper.ViewContext.RequestContext.HttpContext.Request.RawUrl;
            //rawUrl = rawUrl.RegexReplace(@"(\?|&)page\=\d{1,6}");
            rawUrl = Regex.Replace(rawUrl, @"(\?|&)" + pageIndexUrlParameterName + @"\=\d{1,6}", "");
            var pageCount = (int) Math.Ceiling(pagedList.TotalItemCount/(double) pagedList.PageSize);
            return
                MvcHtmlString.Create(CreateLinkUrl(rawUrl, pageCount, pagedList.CurrentPageIndex, pagedList.PageSize,
                    pageIndexUrlParameterName));
        }

        public static string CreateLinkUrl(string baseUrl, int pageCount, int nowPage, int pageSize,
            string pageIndexUrlParameterName = "page")
        {
            var sb = new StringBuilder();


            int startPage = 1;
            int endPage = 10;
            startPage = (nowPage + 5) > pageCount ? pageCount - 9 : nowPage - 4;
            endPage = nowPage < 5 ? 10 : nowPage + 5;
            if (startPage < 1)
            {
                startPage = 1;
            }
            if (pageCount < endPage)
            {
                endPage = pageCount;
            }


            baseUrl += baseUrl.IndexOf("?") == -1 ? "?" : "&";

            if (nowPage > 1)
            {
                sb.AppendFormat("<a href=\"{0}{2}={1}\" >上一页</a>", baseUrl, (nowPage - 1),
                    pageIndexUrlParameterName);
            }
            else
            {
                sb.Append("<a href='javascript:void(0);' class='nolink' >上一页</a>");
            }
            for (int i = startPage; i <= endPage; i++)
            {
                if (i == nowPage)
                {
                    sb.AppendFormat("<a href=\"javascript:void(0);\"  class='current' >{0}</a>", nowPage);
                }
                else
                {
                    sb.AppendFormat("<a href=\"{0}{2}={1}\" >{1}</a>", baseUrl, i, pageIndexUrlParameterName);
                }
            }
            if (nowPage < pageCount)
            {
                sb.AppendFormat("<a href=\"{0}{2}={1}\" >下一页</a>", baseUrl, (nowPage + 1),
                    pageIndexUrlParameterName);
            }
            else
            {
                sb.Append("<a href=\"javascript:void(0);\"  class='nolink' >下一页</a>");
            }
            return sb.ToString();
        }
    }
}