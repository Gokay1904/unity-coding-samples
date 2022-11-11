using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    private string name = "playerdata";
    [SerializeField]
    private int _currentLevelIndex; //Mevcut level
    public int currentLevelIndex{ get; set; }
    [SerializeField]
    private LevelData _currentLevelData;
    public LevelData currentLevelData{ get; set; }

    [SerializeField]
    private ChapterData _currentChapterData; 
    public ChapterData currentChapterData { get; set; }

    [SerializeField]
    private int _currency;
    public int currency { get; set; }

    public void LoadData()
    {
        string json = DataManagement.dataManagement.ReadFromFile(name + ".txt");
        JsonUtility.FromJsonOverwrite(json, this);
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        DataManagement.dataManagement.WriteToFile(name + ".txt", json);
    }




}
