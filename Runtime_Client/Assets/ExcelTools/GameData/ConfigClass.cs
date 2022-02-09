using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System;

[Serializable]
public class ConfigClass
{
}

[Serializable]
public class ExcelATest1 : ConfigClass
{
    public string id;
    public string eat1_1;
    public string eat1_2;
    public int eat1_3;

    
}
[Serializable]
public class ExcelATest2 : ConfigClass
{

    public string id;
    public string eat2_1;
    public string eat2_2;
    public int eat2_3;

}
[Serializable]
public class ExcelBTest1 : ConfigClass
{

    public string id;
    public string ebt1_1;
    public string ebt1_2;
    public int ebt1_3;

}

[Serializable]
public class RoleData : ConfigClass
{
    public int id;
    public int career;
    public string careerName;
    public int modelId;
}

//[Serializable]
//public class Item_Effect
//{
//    public int id;
//    public float value;
//}


[Serializable]
public class ItemData : ConfigClass
{
    public int id;
    public int type;
    public int stype;
    public string name;
    public string icon;
    public int quality;
    public int lv;
    public string desc;
    public int bind;
    public int max_num;
    public int price;
    public string list;
}