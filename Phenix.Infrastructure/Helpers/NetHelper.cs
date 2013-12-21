//  Copyright (c) 2010 Ray Liang (http://www.dotnetage.com)
//  Dual licensed under the MIT and GPL licenses:
//  http://www.opensource.org/licenses/mit-license.php
//  http://www.gnu.org/licenses/gpl.html

using System;
using System.IO;
using System.Net;

namespace Phenix.Infrastructure.Helpers
{
    public class NetHelper
    {
        /// <summary>
        /// Download the specified url as string
        /// </summary>
        /// <param name="url">Specified the target url to download</param>
        /// <returns>Return the download result as string.</returns>
        public static string Download(Uri url)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 3000;
                request.Headers["Accept-Encoding"] = "gzip";
                request.Headers["Accept-Language"] = "en-us";
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (var response = request.GetResponse())
                {
                    var responseStream = response.GetResponseStream();
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }


    }
}