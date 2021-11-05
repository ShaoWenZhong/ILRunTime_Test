using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace CloudContentDelivery
{
    public class BadgeController
    {
        public static void CreateBadge(string badgeName)
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

        public static void UpdateBadge(string badgeName, string releaseid)
        {
            if (!Util.checkCosKey())
            {
                return;
            }

            string url = string.Format("{0}api/v1/buckets/{1}/badges/", Parameters.apiHost, Parameters.bucketUuid);
            string requestBody = JsonUtility.ToJson(new UpdateBadgeParams(badgeName, releaseid));

            try
            {
                string responseBody = HttpUtil.getHttpResponse(url, "PUT", requestBody);
                // Release release = JsonUtility.FromJson<Release>(responseBody);
                Debug.Log(string.Format("Update Badge : {0}", responseBody));
            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("Update Badge Error", e.Message, "OK");
                // Debug.LogError(string.Format("Create release error : {0}", e.Message));
            }
        }

        public static void ListBadges(int page = 1)
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
                Parameters.badgePreviousButton = false;
            }
            else
            {
                Parameters.badgePreviousButton = true;
            }

            string url = string.Format("{0}api/v1/buckets/{1}/badges/?page={2}&per_page={3}", Parameters.apiHost, Parameters.bucketUuid, page, Parameters.countPerpage);

            try
            {
                HttpResponse resp = HttpUtil.getHttpResponseWithHeaders(url, "GET");
                string badgesJson = resp.responseBody;
                string pagesPattern = Util.getHeader(resp.headers, "Content-Range");
                if (!string.IsNullOrEmpty(pagesPattern))
                {
                    Parameters.totalBadgeCounts = int.Parse(pagesPattern.Split('/')[1]);
                    Parameters.totalBadgePages = Parameters.totalBadgeCounts / 10 + 1;
                    if (page == Parameters.totalBadgePages)
                    {
                        Parameters.badgeNextButton = false;
                    }
                    else
                    {
                        Parameters.badgeNextButton = true;
                    }
                }

                Parameters.dictBadges = new Dictionary<string, Badge>();
                Badge[] badges = JsonUtility.FromJson<RootBadges>("{\"Badges\":" + badgesJson + "}").Badges;

                StringBuilder sbInfo = new StringBuilder();
                if (badges.Length > 0)
                {
                    sbInfo.AppendLine("Total " + Parameters.totalBadgeCounts + " Badges");
                    // int j = badges.Length;
                    foreach (Badge ba in badges)
                    {
                        Parameters.dictBadges.Add(ba.name, ba);
                        // j--;
                    }
                }
                else
                {
                    sbInfo.AppendLine("No Badge in the bucket");
                }

                Parameters.currentBadgePage = page;
                Debug.Log(sbInfo);

            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("List Badge Error", e.Message, "OK");
                // Debug.LogError(string.Format("List release error : {0}", e.Message));
            }
        }

        public static void ViewRelease(Release release)
        {
            
            EditorUtility.DisplayDialog("Release Info", release.ToMessage(), "OK");
        }
    }
}