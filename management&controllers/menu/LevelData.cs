using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelData
{
    [SerializeField]
    private string _name;
    public string name{ get { return _name; } set { _name = name; } }

    [SerializeField]
    private ChapterData _bindedChapter;

    public ChapterData bindedChapter { get { return _bindedChapter; } }

    [SerializeField]
    private bool _isLevelActive;
    public bool isLevelActive { get { return _isLevelActive; } set { _isLevelActive = value; } }

    [SerializeField]
    private bool _isLevelFinished;
    public bool isLevelFinished { get { return _isLevelFinished; } set { _isLevelFinished = value; } }


    [SerializeField]
    private float _successRate;
    public float successRate { get { return _successRate; } set { _successRate = value; } }


    [SerializeField]
    private int _levelNumber;
    public int levelNumber { get { return _levelNumber; } set { _levelNumber = value; } }


    [SerializeField]
    private string _sceneName;
    public string sceneName { get { return _sceneName; } }


    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);
        DataManagement.dataManagement.WriteToFile(name + ".txt", json);
    }

    public void LoadData()
    {
        string json = DataManagement.dataManagement.ReadFromFile(name + ".txt");
        JsonUtility.FromJsonOverwrite(json,this);
    }


}
