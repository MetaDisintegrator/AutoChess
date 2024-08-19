using Common;
using Common.Data;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public string DataPath;
    public Dictionary<int, ShapeDefine> Shapes = new Dictionary<int, ShapeDefine>();
    public Dictionary<int, Dictionary<int, SkillDefine>> Skills = new Dictionary<int, Dictionary<int, SkillDefine>>();
    public Dictionary<int , EquipmentDefine> Equipments = new Dictionary<int , EquipmentDefine>();

    bool load = false;

    public DataManager() 
    {
        DataPath = "Data/";
        Debug.LogFormat("DataManager Init");
    }

    public void Load()
    {
        if (load) 
            return;

        string json = File.ReadAllText(DataPath + "ShapeDefine.txt");
        Shapes = JsonConvert.DeserializeObject<Dictionary<int, ShapeDefine>>(json);

        json = File.ReadAllText(DataPath + "SkillDefine.txt");
        Skills = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, SkillDefine>>>(json);

        json = File.ReadAllText(DataPath + "EquipmentDefine.txt");
        Equipments = JsonConvert.DeserializeObject<Dictionary<int, EquipmentDefine>>(json);

        load = true;
        Debug.Log("DataManager Load Done");
    }
}
