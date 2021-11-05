using System;

namespace CloudContentDelivery
{
    [Serializable]
    public class EntryInfo
    {
        public string full_path;
        public string path;
        public string content_hash;
        public long content_size;
        public string content_type;
        public string objectKey;

        public EntryInfo(string fullPath, string path, string hash, long size, string contentType)
        {
            this.full_path = fullPath;
            this.path = path;
            this.content_hash = hash;
            this.content_size = size;
            this.content_type = contentType;
            if (!Parameters.projectGuid.Equals(""))
            {
                this.objectKey = Parameters.projectGuid + "/" + hash;
            }
        }

        public override string ToString()
        {
            return string.Format("path : {0}, full path : {1}, hash : {2},  size : {3}, objectKey : {4}",
                path, full_path, content_hash, content_size, objectKey);
        }
    }
}