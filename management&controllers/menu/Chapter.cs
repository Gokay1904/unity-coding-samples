using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChapterData
{
    [SerializeField]
    private string name;
    public string chapterName { get { return name; } }

    [SerializeField]
    private LevelData[] _levelDatas;

    public LevelData[] levelDatas
    {
        get { return _levelDatas;}
    }

    public bool isChapterFinished;


    public void SaveChapter()
    {
        string json = JsonUtility.ToJson(this);
        DataManagement.dataManagement.WriteToFile(name + ".txt", json);
    }

    public void LoadChapter()
    {
        string json = DataManagement.dataManagement.ReadFromFile(name + ".txt");
        JsonUtility.FromJsonOverwrite(json, this);
    }

   


    


}
