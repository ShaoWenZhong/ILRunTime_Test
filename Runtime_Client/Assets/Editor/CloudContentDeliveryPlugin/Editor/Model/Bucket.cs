using System;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace CloudContentDelivery
{
    [Serializable]
    public class Bucket
    {
        public string description;
        public string id;
        public string name;
        public string projectguid;
    }
}