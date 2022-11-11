using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI:MonoBehaviour
{
  
    [SerializeField]
    private string _sceneName;

    [SerializeField]
    private ChapterData _bindedChapterData;

    public string sceneName { get { return _sceneName; } }

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Button _levelButton;

    [SerializeField]
    private int _levelNumber;

    public int levelNumber { get { return _levelNumber;} 
        set { _levelNumber = value; if (levelText != null) { levelText.text = RomanNumberGenerator.ConvertRomanNumber(value); } } }

    [SerializeField]
    private float _successRate;
    public float successRate { get { return _successRate; }
        set
        {
            _successRate = value;

            if (_isLevelActive == true)
            {  
                for (int i = 0; i < Math.Round(value); i++)
                { successRateCircles[i].color = new Color32(255, 255, 255, 255); }
                for (int i = successRateCircles.Length - 1; i >= Math.Round(value); i--)
                {
                    successRateCircles[i].color = new Color32(50, 50, 50, 255);
                }
            }

            else
            {
                for (int i = 0; i < successRateCircles.Length; i++)
                {
                    successRateCircles[i].color = new Color32(50, 50, 50, 255);
                }
            }
        }
        }

    private bool _isLevelFinished;

    public bool isLevelFinished;
    
    public Image[] successRateCircles;

    [SerializeField]
    private bool _isLevelActive;
    public bool isLevelActive { get { return _isLevelActive; } set { _isLevelActive = value; if (value == true) { levelText.color = new Color32(255, 255, 255, 255); _levelButton.interactable = true; } else { levelText.color = new Color32(50, 50, 50, 255); _levelButton.interactable = false; } } }

    LevelData levelData;
  
    public void SetLevelUI(LevelData levelData)
    {
        this.levelData = levelData;
        name = levelData.name;
        levelNumber = levelData.levelNumber;
        isLevelActive = levelData.isLevelActive;
        successRate = levelData.successRate;
        _sceneName = levelData.sceneName;
    }

    void setSuccessRate(float time, float idealTime)
    {
        float x = ((successRateCircles.Length - 1) * time / idealTime) + 1;

        _successRate = x;
    }

    float getSuccessRate() 
    {
        return successRate;
    }

    public void OnClick()
    {
        DataManagement.playerData.currentLevelData = levelData;
        DataManagement.playerData.SaveData();

        SceneManager.LoadScene(_sceneName);
    }




}

