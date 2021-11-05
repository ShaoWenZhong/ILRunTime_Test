using System;
using System.Threading;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace CloudContentDelivery
{
    [Serializable]
    public class Uas : EditorWindow
    {
        // private IEnumerator coroutine;
        public string BucketName;
        public string BadgeName;

        public int SelectedBucketIndex = 0;
        public int SelectedEntryIndex = 0;
        public int SelectedReleaseIndex = 0;
        public int SelectedBadgeIndex = 0;

        public string SelectedBucketName = "";
        public string SelectedEntryName = "";
        public int SelectedReleaseNumber = 0;
        public string SelectedBadgeName = "";

        public string SelectedBucketUuid = "";
        public string SelectedEntryUuid = "";
        public string SelectedReleaseId = "";

        public int CurrentBucketPage = 0;
        public bool BucketPreviousButton = false;
        public bool BucketNextButton = false;

        public int CurrentEntryPage = 0;
        public bool EntryPreviousButton = false;
        public bool EntryNextButton = false;

        public int CurrentReleasePage = 0;
        public bool ReleasePreviousButton = false;
        public bool ReleaseNextButton = false;

        public int CurrentBadgePage = 0;
        public bool BadgePreviousButton = false;
        public bool BadgeNextButton = false;

        public string LinkedRelease;

        public bool showBucketArea = true;
        public bool showReleaseArea = true;
        public bool showBadgeArea = true;

        public string showBucketAreaText = "";
        public string showReleaseAreaText = "";
        public string showBadgeAreaText = "";

        public Vector2 scrollPosition;

        [SerializeField]
        public Bucket[] Buckets = null;
        [SerializeField]
        public Entry[] Entries = null;
        [SerializeField]
        public Release[] Releases = null;
        [SerializeField]
        public Badge[] Badges = null;

        [MenuItem("Window/Cloud Content Delivery")]
        public static void uas()
        {
            Uas window = EditorWindow.GetWindow<Uas>(false, "Cloud Content Delivery");
            window.Show();
        }

        void OnGUI()
        {
            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            if (AddressableAssetSettingsDefaultObject.Settings == null) {
                GUILayout.BeginHorizontal();
                GUIStyle s = new GUIStyle(EditorStyles.label);
                s.normal.textColor = Color.red;
                GUILayout.Label("Please init Addressables package first. (Window -> Asset Management -> Addressables -> Groups)", s);
                GUILayout.EndHorizontal();
                return;
            }

            CheckInitInfo();

            int buttonWidth = 300;

            int iButtonSpace = (int)(position.width / 2 - buttonWidth / 2);

            string profileId = AddressableAssetSettingsDefaultObject.Settings.activeProfileId;

            if (showBucketArea)
            {
                showBucketAreaText = "Bucket";
            }
            else
            {
                showBucketAreaText = string.Format("Bucket ( current bucket : {0})", SelectedBucketName);
            }
            
            showBucketArea = EditorGUILayout.Foldout(showBucketArea, showBucketAreaText);

            if (showBucketArea)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                this.BucketName = EditorGUILayout.TextField("Bucket Name", this.BucketName);
                Parameters.bucketName = this.BucketName;
                
                //GUILayout.Space(iButtonSpace);
                if (GUILayout.Button("Create New Bucket", GUILayout.Width(200)))
                {
                    BucketController.CreateBucket();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                GUILayout.Space(iButtonSpace + buttonWidth / 4);
                if (GUILayout.Button("List Buckets", GUILayout.Width(300)))
                {
                    if (Util.checkCosKey())
                    {
                        BucketController.LoadBuckets();
                        SelectedBucketIndex = 0;
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);

                if (Parameters.dictBuckets != null)
                {
                    this.Buckets = new Bucket[Parameters.dictBuckets.Count];
                    Parameters.dictBuckets.Values.CopyTo(this.Buckets, 0);
                }

                string[] buckets = new String[0];

                if (this.Buckets != null)
                {
                    buckets = new String[this.Buckets.Length];
                    for (int i = 0; i < this.Buckets.Length; i++)
                    {
                        buckets[i] = this.Buckets[i].name;
                    }
                }

                SelectedBucketIndex = EditorGUILayout.Popup("Bucket", SelectedBucketIndex, buckets);
                
                if (this.Buckets != null && this.Buckets.Length > 0)
                {
                    SelectedBucketName = this.Buckets[SelectedBucketIndex].name;
                    SelectedBucketUuid = this.Buckets[SelectedBucketIndex].id;
                    Parameters.bucketUuid = SelectedBucketUuid;
                }

                if (Parameters.bucketPreviousButton != false) {
                    BucketPreviousButton = Parameters.bucketPreviousButton;
                }

                if (Parameters.bucketNextButton != false)
                {
                    BucketNextButton = Parameters.bucketNextButton;
                }

                if (Parameters.currentBucketPage != 0)
                {
                    CurrentBucketPage = Parameters.currentBucketPage;
                }

                GUI.enabled = BucketPreviousButton;
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    BucketController.LoadBuckets(CurrentBucketPage - 1);
                    SelectedBucketIndex = 0;
                }

                GUI.enabled = BucketNextButton;
                GUILayout.Label(CurrentBucketPage.ToString(), GUILayout.Width(15));
                if (GUILayout.Button("+", GUILayout.Width(30)))
                {
                    BucketController.LoadBuckets(CurrentBucketPage + 1);
                    SelectedBucketIndex = 0;
                }
                GUILayout.EndHorizontal();

                GUI.enabled = true;
            }

            showReleaseArea = EditorGUILayout.Foldout(showReleaseArea, showReleaseAreaText);

            if (showReleaseArea)
            {
                showReleaseAreaText = "Release";
            }
            else
            {
                showReleaseAreaText = string.Format("Release ( current release : {0})", SelectedReleaseNumber);
            }

            if (showReleaseArea)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                string remotebuildPath = AddressableAssetSettingsDefaultObject.Settings.profileSettings.GetValueByName(profileId, "RemoteBuildPath");
                remotebuildPath = remotebuildPath.Replace("[BuildTarget]", UnityEditor.EditorUserBuildSettings.activeBuildTarget.ToString());
                EditorGUILayout.LabelField("Sync Path", remotebuildPath);
                if (GUILayout.Button("Sync Entries", GUILayout.Width(200)))
                {
                    // we should set path to "remotebuildpath"
                    string selectedBuildPath = AddressableAssetSettingsDefaultObject.Settings.RemoteCatalogBuildPath.GetName(AddressableAssetSettingsDefaultObject.Settings);
                    string selectedLoadPath = AddressableAssetSettingsDefaultObject.Settings.RemoteCatalogLoadPath.GetName(AddressableAssetSettingsDefaultObject.Settings);

                    if (String.IsNullOrEmpty(selectedBuildPath)) {
                        var buildpath = ((BundledAssetGroupSchema)AddressableAssetSettingsDefaultObject.Settings.DefaultGroup.GetSchema(typeof(BundledAssetGroupSchema))).BuildPath;
                        selectedBuildPath = buildpath.GetName(AddressableAssetSettingsDefaultObject.Settings);
                    }

                    if (String.IsNullOrEmpty(selectedLoadPath)) {
                        var loadpath = ((BundledAssetGroupSchema)AddressableAssetSettingsDefaultObject.Settings.DefaultGroup.GetSchema(typeof(BundledAssetGroupSchema))).LoadPath;
                        selectedLoadPath = loadpath.GetName(AddressableAssetSettingsDefaultObject.Settings);
                    }
                    if (string.Equals(selectedBuildPath, "RemoteBuildPath") && string.Equals(selectedLoadPath, "RemoteLoadPath") && Util.checkCosKey())
                    {
                        if (Parameters.syncFinished)
                        {
                            UploadWindow.uploadWindow();
                            Thread thread = new Thread(new ParameterizedThreadStart(EntryController.SyncEntries));
                            thread.Start(remotebuildPath);
                        }
                        else
                        {
                            UploadWindow.uploadWindow();
                        }
                    }
                    else
                    {
                        if (!string.Equals(selectedBuildPath, "RemoteBuildPath"))
                            EditorUtility.DisplayDialog("Build Path ERROR!", "Please set Build Path to RemoteBuildPath on AddressableAssetSetting. Current Selection is :" + selectedBuildPath, "OK");
                        else
                            EditorUtility.DisplayDialog("Load Path ERROR!", "Please set Load Path to RemoteLoadPath on AddressableAssetSetting.", "OK");
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                GUILayout.Space(iButtonSpace + buttonWidth / 4);
                if (GUILayout.Button("List Entries", GUILayout.Width(300)))
                {
                    Entries = null;
                    Parameters.dictEntries = null;
                    EntryController.LoadEntries();
                    SelectedEntryIndex = 0;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                if (Parameters.dictEntries != null) {
                    this.Entries = new Entry[Parameters.dictEntries.Count];
                    Parameters.dictEntries.Values.CopyTo(this.Entries, 0);
                }
                string[] entries = new String[0];
                if (this.Entries != null) {
                    entries = new String[this.Entries.Length];
                    int i = 0;
                    foreach (Entry en in this.Entries) {
                        entries[i] = en.path;
                        i++;
                    }
                }

                SelectedEntryIndex = EditorGUILayout.Popup("Entry", SelectedEntryIndex, entries);
                
                if (this.Entries != null && this.Entries.Length > 0)
                {
                    Parameters.entryUuid = this.Entries[SelectedEntryIndex].entryid;
                }

                if (Parameters.entryPreviousButton != false)
                {
                    EntryPreviousButton = Parameters.entryPreviousButton;
                }

                if (Parameters.entryNextButton != false)
                {
                    EntryNextButton = Parameters.entryNextButton;
                }

                if (Parameters.currentEntryPage != 0)
                {
                    CurrentEntryPage = Parameters.currentEntryPage;
                }

                GUI.enabled = EntryPreviousButton;
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    EntryController.LoadEntries(CurrentEntryPage - 1);
                    SelectedEntryIndex = 0;
                }

                GUI.enabled = EntryNextButton;
                GUILayout.Label(CurrentEntryPage.ToString(), GUILayout.Width(15));
                if (GUILayout.Button("+", GUILayout.Width(30)))
                {
                    EntryController.LoadEntries(CurrentEntryPage + 1);
                    SelectedEntryIndex = 0;
                }
                GUI.enabled = true;

                if (GUILayout.Button(new GUIContent("Info", "Show detailed entry information"), GUILayout.Width(50)))
                {
                    EntryController.ViewEntry(Entries[SelectedEntryIndex]);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                GUILayout.Space(iButtonSpace + buttonWidth / 4);
                if (GUILayout.Button("Create Release", GUILayout.Width(300)))
                {
                    ReleaseController.CreateRelease();
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                GUILayout.Space(iButtonSpace + buttonWidth / 4);
                if (GUILayout.Button("List Releases", GUILayout.Width(300)))
                {
                    Parameters.dictReleases = null;
                    this.Releases = null;
                    ReleaseController.ListReleases();
                    SelectedReleaseIndex = 0;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                if (Parameters.dictReleases != null)
                {
                    this.Releases = new Release[Parameters.dictReleases.Count];
                    Parameters.dictReleases.Values.CopyTo(this.Releases, 0);
                }
                string[] releases = new string[0];
                if (this.Releases != null && this.Releases.Length > 0)
                {
                    releases = new string[this.Releases.Length];
                    for (int i = 0; i < this.Releases.Length; i++)
                    {
                        releases[i] = this.Releases[i].release_num.ToString();
                    }
                }
                SelectedReleaseIndex = EditorGUILayout.Popup("Release", SelectedReleaseIndex, releases);
                if (releases.Length > 0) {
                    Parameters.releaseUuid = releases[SelectedReleaseIndex];
                }

                if (this.Releases != null && this.Releases.Length > 0)
                {
                    SelectedReleaseId = this.Releases[SelectedReleaseIndex].releaseid;
                    SelectedReleaseNumber = this.Releases[SelectedReleaseIndex].release_num;
                }

                if (Parameters.releasePreviousButton != false)
                {
                    ReleasePreviousButton = Parameters.releasePreviousButton;
                }

                if (Parameters.releaseNextButton != false)
                {
                    ReleaseNextButton = Parameters.releaseNextButton;
                }

                if (Parameters.currentReleasePage != 0)
                {
                    CurrentReleasePage = Parameters.currentReleasePage;
                }

                GUI.enabled = ReleasePreviousButton;
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    ReleaseController.ListReleases(CurrentReleasePage - 1);
                    SelectedReleaseIndex = 0;
                }

                GUI.enabled = ReleaseNextButton;
                GUILayout.Label(CurrentReleasePage.ToString(), GUILayout.Width(15));
                if (GUILayout.Button("+", GUILayout.Width(30)))
                {
                    ReleaseController.ListReleases(CurrentReleasePage + 1);
                    SelectedReleaseIndex = 0;
                }
                GUI.enabled = true;


                if (GUILayout.Button(new GUIContent("Info", "Show detailed release information"), GUILayout.Width(50)))
                {
                    ReleaseController.ViewRelease(Releases[SelectedReleaseIndex]);
                }
                GUILayout.EndHorizontal();
            }


            showBadgeArea = EditorGUILayout.Foldout(showBadgeArea, showBadgeAreaText);

            if (showBadgeArea)
            {
                showBadgeAreaText = "Badge";
            }
            else
            {
                showBadgeAreaText = string.Format("Badge ( current badge : {0})", SelectedBadgeName);
            }

            if (showBadgeArea)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                this.BadgeName = EditorGUILayout.TextField("Badge Name", this.BadgeName);
                // Parameters.bucketName = this.BucketName;

                if (GUILayout.Button(new GUIContent("Create New Badge", "Create a new badge linked to current selected release."), GUILayout.Width(136)))
                {
                    if (!string.IsNullOrEmpty(this.BadgeName))
                    {
                        if (this.Releases != null && this.Releases.Length > 0)
                        {
                            BadgeController.UpdateBadge(this.BadgeName, SelectedReleaseId);
                        }
                        else
                        {
                            EditorUtility.DisplayDialog("Create Badge Error", "Please select a release first, a badge must be linked to a release.", "OK");
                        }
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                if (Parameters.dictBadges != null)
                {
                    this.Badges = new Badge[Parameters.dictBadges.Count];
                    Parameters.dictBadges.Values.CopyTo(this.Badges, 0);
                }
                string[] badges = new String[0];

                if (this.Badges != null)
                {
                    badges = new String[this.Badges.Length];
                    for (int i = 0; i < this.Badges.Length; i++)
                    {
                        badges[i] = this.Badges[i].name;
                    }
                }

                SelectedBadgeIndex = EditorGUILayout.Popup("Badge", SelectedBadgeIndex, badges);

                if (badges.Length > 0)
                {
                    SelectedBadgeName = badges[SelectedBadgeIndex];
                }

                if (Parameters.badgePreviousButton != false)
                {
                    BadgePreviousButton = Parameters.badgePreviousButton;
                }

                if (Parameters.badgeNextButton != false)
                {
                    BadgeNextButton = Parameters.badgeNextButton;
                }

                if (Parameters.currentBadgePage != 0)
                {
                    CurrentBadgePage = Parameters.currentBadgePage;
                }

                GUI.enabled = BadgePreviousButton;
                if (GUILayout.Button("-", GUILayout.Width(30)))
                {
                    BadgeController.ListBadges(CurrentReleasePage - 1);
                    SelectedBadgeIndex = 0;
                }

                GUI.enabled = BadgeNextButton;
                GUILayout.Label(CurrentBadgePage.ToString(), GUILayout.Width(15));
                if (GUILayout.Button("+", GUILayout.Width(30)))
                {
                    BadgeController.ListBadges(CurrentReleasePage + 1);
                    SelectedBadgeIndex = 0;
                }
                GUI.enabled = true;

                if (GUILayout.Button(new GUIContent("Load", "Load all badges by page."), GUILayout.Width(50)))
                {
                    Parameters.dictBadges = null;
                    this.Badges = null;
                    BadgeController.ListBadges();
                    SelectedBadgeIndex = 0;
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);

                if (this.Badges != null && this.Badges.Length > 0)
                {
                    this.LinkedRelease = string.Format("{0} ({1})", this.Badges[SelectedBadgeIndex].releasenum, this.Badges[SelectedBadgeIndex].releaseid);
                }

                EditorGUILayout.LabelField("Linked Release", this.LinkedRelease);

                // if (GUILayout.Button("Update", GUILayout.Width(82), GUI.tooltip))
                if (GUILayout.Button(new GUIContent("Update", "Link this badge to current selected release."), GUILayout.Width(82)))
                {
                    if (this.Releases != null && this.Releases.Length > 0 && this.Badges != null && this.Badges.Length > 0 )
                    {
                        if (SelectedBadgeName.Equals("latest"))
                        {
                            EditorUtility.DisplayDialog("Update Badge Error", "Can not change release for badge latest.", "OK");
                        }
                        else
                        {
                            BadgeController.UpdateBadge(SelectedBadgeName, SelectedReleaseId);
                        }
                    }
                    else
                    {
                        EditorUtility.DisplayDialog("Update Badge Error", "Please select a badge and release first.", "OK");
                    }
                }

                GUILayout.EndHorizontal();
            
                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                GUILayout.Space(150);
                if (GUILayout.Button("Set Remote Load Url"))
                {
                    if (string.IsNullOrEmpty(Parameters.bucketUuid))
                    {
                        return;
                    }

                    string host = "";

                    if (Parameters.useContentServer)
                    {
                        host = Parameters.proxyHost;
                    }
                    else
                    {
                        host = Parameters.apiHost;
                    }

                    if (Parameters.useLatest || string.IsNullOrEmpty(Parameters.releaseUuid))
                    {
                        Parameters.remoteLoadUrl = host + "client_api/v1/buckets/" + Parameters.bucketUuid + "/release_by_badge/latest/entry_by_path/content/?path=";

                    }
                    else if (Parameters.useReleaseId)
                    {
                        Parameters.remoteLoadUrl = host + "client_api/v1/buckets/" + Parameters.bucketUuid + "/releases/" + Parameters.releaseUuid + "/entry_by_path/content/?path=";
                    }
                    else
                    {
                        Parameters.remoteLoadUrl = host + "client_api/v1/buckets/" + Parameters.bucketUuid + "/release_by_badge/" + this.Badges[SelectedBadgeIndex].name + "/entry_by_path/content/?path=";
                    }

                    AddressableAssetSettingsDefaultObject.Settings.profileSettings.SetValue(profileId, "RemoteLoadPath", Parameters.remoteLoadUrl);
                    EditorUtility.DisplayDialog("Remote Load Path Set", "URL: " + Parameters.remoteLoadUrl + " set to RemoteLoadPath field", "OK");
                }
                Parameters.useLatest = GUILayout.Toggle(Parameters.useLatest, new GUIContent("latest", "Always loading latest badge. You can not use release when select latest."), GUILayout.Width(50), GUILayout.Height(20));
                Parameters.useContentServer = GUILayout.Toggle(Parameters.useContentServer, "content server", GUILayout.Width(105), GUILayout.Height(20));

                GUI.enabled = !Parameters.useLatest;
                Parameters.useReleaseId = GUILayout.Toggle(Parameters.useReleaseId, "release", GUILayout.Height(20));
                GUI.enabled = true;

                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Space(Parameters.indentSize);
                EditorGUILayout.LabelField("Remote Load Url", Parameters.remoteLoadUrl);
                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            //GUILayout.EndScrollView();

        }

        void OnEnable()
        {

        }

        void CheckInitInfo()
        {
            if (string.IsNullOrEmpty(Parameters.cosKey))
            {
                GUILayout.BeginHorizontal();
                GUIStyle s = new GUIStyle(EditorStyles.label);
                s.normal.textColor = Color.red;
                GUILayout.Label("Plesse set cos key before using. (Edit -> Project Settings... -> Cloud Content Delivery)", s);
                GUILayout.EndHorizontal();
            }
            else if (string.IsNullOrEmpty(Parameters.projectGuid))
            {
                if (!string.IsNullOrEmpty(CosKey.getProjectGuid()))
                {
                    Parameters.projectGuid = CosKey.getProjectGuid();
                }
                else
                {
                    GUILayout.BeginHorizontal();
                    GUIStyle s = new GUIStyle(EditorStyles.label);
                    s.normal.textColor = Color.red;
                    GUILayout.Label("Waiting to initialize project info ...", s);
                    GUILayout.EndHorizontal();

                    string projectGuid = Util.getProjectGuid();
                    CosKey.SaveProjectGuid(projectGuid);
                    Parameters.projectGuid = projectGuid;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(CosKey.getProjectGuid()))
                {
                    CosKey.SaveProjectGuid(Parameters.projectGuid);
                }
            }
        }
    }

    [Serializable]
    public class RootObject
    {
        public Bucket[] Buckets;
    }

    [Serializable]
    public class UploadUrl
    {
        public string uploadUrl;
    }

    [Serializable]
    public class RootEntries
    {
        public Entry[] Entries;
    }

    [Serializable]
    public class RootReleases
    {
        public Release[] Releases;
    }

    [Serializable]
    public class RootBadges
    {
        public Badge[] Badges;
    }
}
