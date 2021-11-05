using System.Collections.Generic;
using UnityEngine;

namespace CloudContentDelivery
{
    public class Parameters
    {
        //constant parameters
        // public const string apiHost = "http://127.0.0.1:22080/";
        public const string apiHost = "https://assetstreaming.unity.cn/";
        public const string proxyHost = "https://assetstream-content.unity.cn/";
        public const string contentType = "application/json";
        public const string k_CloudContentDeliverySettingsPathPrefix = "Assets/CloudContentDeliveryData/";
        public const string k_CloudContentDeliverySettingsPath = "Assets/CloudContentDeliveryData/CloudContentDeliverySettings.asset";
        public const string k_UploadPartStatusPathPrefix = "Assets/CloudContentDeliveryData/";
        public const string k_UploadPartStatusFile = "Assets/CloudContentDeliveryData/unfinishedUploads.json";

        //static parameters
        public static List<string> ignoreFiles = new List<string>()
        {
            ".DS_Store"
        };

        public const int indentSize = 20;
        public const int commonHeight = 20;
        
        public static string cosKey = CosKey.getCosKey();
        public static string oldCosKey = "";
        public static string projectGuid = CosKey.getProjectGuid();

        public static string bucketName = "";
        public static string bucketUuid = "";
        public static Dictionary<string, Bucket> dictBuckets = null;
        public static Dictionary<string, Entry> dictEntries = null;
        public static Dictionary<string, Release> dictReleases = null;
        public static Dictionary<string, Badge> dictBadges = null;
        public static string entryUuid = "";
        public static string bucketArgs = "";
        public static string releaseUuid = "latest";
        public static string remoteLoadUrl = "";
        public static bool useLatest = true;
        public static bool useContentServer = false;
        public static bool useReleaseId = false;

        public static int countPerpage = 10;

        // public static string[] buckets;
        public static int currentBucketPage = 0;
        public static int totalBucketCounts = 0;
        public static int totalBucketPages = 1;
        public static bool bucketPreviousButton = false;
        public static bool bucketNextButton = false;

        public static int currentEntryPage = 0;
        public static int totalEntryCounts = 0;
        public static int totalEntryPages = 1;
        public static bool entryPreviousButton = false;
        public static bool entryNextButton = false;

        public static int currentReleasePage = 0;
        public static int totalReleaseCounts = 0;
        public static int totalReleasePages = 1;
        public static bool releasePreviousButton = false;
        public static bool releaseNextButton = false;

        public static int currentBadgePage = 0;
        public static int totalBadgeCounts = 0;
        public static int totalBadgePages = 1;
        public static bool badgePreviousButton = false;
        public static bool badgeNextButton = false;

        public static UploadPartsStatusList unfinishedUploadList = null;
        public static Dictionary<string, int> unfinishedIndexObjetkeyMapping = null;

        public static bool syncFinished = true;
        public static long totalUploadSize = 0;
        public static long alreadyUploadSize = 0;
        public static int totalUploadFiles = 0;
        public static int alreadyUploadFiles = 0;

        public static long totalUploadSize4Current = 0;
        public static long alreadyUploadSize4Current = 0;
        public static long alreadyUploadPartsSize4Current = 0;

        public static int createdFiles = 0;
        public static int updatedFiles = 0;
        public static int failedFiles = 0;

        public const string CosAppId = "1301029430";
        public const string CosRegion = "ap-shanghai";
        public const string CosBucket = "asset-streaming-1301029430";
        public const int maxRetries = 3;
    }
}