using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace CloudContentDelivery
{
    public class BucketController
    {
        public static void LoadBuckets(int page = 1)
        {
            if (page < 1)
            {
                return;
            }

            /*
            if (page > Parameters.totalBucketPages)
            {
                return;
            }
            */

            if (page == 1)
            {
                Parameters.bucketPreviousButton = false;
            }
            else
            {
                Parameters.bucketPreviousButton = true;
            }

            string url = string.Format("{0}api/v1/buckets/?page={1}&per_page={2}", Parameters.apiHost, page, Parameters.countPerpage);

            try
            {
                HttpResponse resp = HttpUtil.getHttpResponseWithHeaders(url, "GET");
                string bucketsJson = resp.responseBody;
                string pagesPattern = Util.getHeader(resp.headers, "Content-Range"); // 1-10/14
                if (!string.IsNullOrEmpty(pagesPattern))
                {
                    Parameters.totalBucketCounts = int.Parse(pagesPattern.Split('/')[1]);
                    Parameters.totalBucketPages = Parameters.totalBucketCounts / 10 + 1;

                    if (page == Parameters.totalBucketPages)
                    {
                        Parameters.bucketNextButton = false;
                    }
                    else
                    {
                        Parameters.bucketNextButton = true;
                    }
                }

                StringBuilder sbInfo = new StringBuilder();
                Bucket[] buckets = JsonUtility.FromJson<RootObject>("{\"Buckets\":" + bucketsJson + "}").Buckets;
                Parameters.dictBuckets = new Dictionary<string, Bucket>();

                if (buckets.Length > 0)
                {
                    sbInfo.AppendLine("Total " + Parameters.totalBucketCounts + " Buckets");
                    foreach (Bucket bu in buckets)
                    {
                        //sbInfo.AppendLine(en.ToString());
                        Parameters.dictBuckets.Add(bu.name, bu);
                    }
                }
                else
                {
                    sbInfo.AppendLine("No Bucket for this project");
                }

                Parameters.currentBucketPage = page;
                Debug.Log(sbInfo);

            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("Load Bucket Error", e.Message, "OK");
                // Debug.LogError(string.Format("Load buckets error : {0}", e.Message));
            }
        }

        public static void CreateBucket()
        {
            if (!Util.checkCosKey() || string.IsNullOrEmpty(Parameters.bucketName))
            {
                return;
            }

            string url = string.Format("{0}api/v1/buckets/", Parameters.apiHost);
            string requestBody = "{\"name\": \"" + Parameters.bucketName + "\"}";

            try
            {
                string responseBody = HttpUtil.getHttpResponse(url, "POST", requestBody);
                Debug.Log(string.Format("Create bucket : {0}", responseBody));

            }
            catch (Exception e)
            {
                EditorUtility.DisplayDialog("Create Bucket Error", e.Message, "OK");
                // Debug.LogError(string.Format("Create bucket error : {0}", e.Message));
            }
        }
    }
}