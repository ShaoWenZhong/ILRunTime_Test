using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace CloudContentDelivery
{
    public class ReleaseController
    {
        public static void CreateRelease()
        {
            if (!Util.checkCosKey())
            {
                return;
            }

            string url = string.Format("{0}api/v1/buckets/{1}/releases/", Parameters.apiHost, Parameters.bucketUuid);
            string requestBody = "{}";

            try
            {
                string responseBody = HttpUtil.getHttpResponse(url, "POST", requestBody);
                // Release release = JsonUtility.FromJson<Release>(responseBody);
                Debug.Log(string.Format("Create release : {0}", responseBody));
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("Create Release Error", e.Message, "OK");
                // Debug.LogError(string.Format("Create release error : {0}", e.Message));
            }
        }
        
        public static void ListReleases(int page = 1)
        {
            if (!Util.checkCosKey())
            {
                return;
            }

            if (page < 1)
            {
                return;
            }

            /*
            if (page > Parameters.totalReleasePages)
            {
                return;
            }
            */

            if (page == 1)
            {
                Parameters.releasePreviousButton = false;
            }
            else
            {
                Parameters.releasePreviousButton = true;
            }

            string url = string.Format("{0}api/v1/buckets/{1}/releases/?page={2}&per_page={3}", Parameters.apiHost, Parameters.bucketUuid, page, Parameters.countPerpage);

            try
            {
                HttpResponse resp = HttpUtil.getHttpResponseWithHeaders(url, "GET");
                string releasesJson = resp.responseBody;
                Debug.Log(releasesJson);
                string pagesPattern = Util.getHeader(resp.headers, "Content-Range");
                if (!string.IsNullOrEmpty(pagesPattern))
                {
                    Parameters.totalReleaseCounts = int.Parse(pagesPattern.Split('/')[1]);
                    Parameters.totalReleasePages = Parameters.totalReleaseCounts / 10 + 1;
                    if (page == Parameters.totalReleasePages)
                    {
                        Parameters.releaseNextButton = false;
                    }
                    else
                    {
                        Parameters.releaseNextButton = true;
                    }
                }

                Parameters.dictReleases = new Dictionary<string, Release>();
                Release[] releases = JsonUtility.FromJson<RootReleases>("{\"Releases\":" + releasesJson + "}").Releases;

                StringBuilder sbInfo = new StringBuilder();
                if (releases.Length > 0)
                {
                    sbInfo.AppendLine("Total " + Parameters.totalReleaseCounts + " Releases");
                    int j = releases.Length;
                    foreach (Release re in releases)
                    {
                        re.release_num = j;
                        Parameters.dictReleases.Add(re.releaseid, re);
                        j--;
                    }
                }
                else
                {
                    sbInfo.AppendLine("No Release in the bucket");
                }

                Parameters.currentReleasePage = page;
                Debug.Log(sbInfo);

            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("List Release Error", e.Message, "OK");
                // Debug.LogError(string.Format("List release error : {0}", e.Message));
            }
        }

        public static void ViewRelease(Release release)
        {
            
            EditorUtility.DisplayDialog("Release Info", release.ToMessage(), "OK");
        }
    }
}