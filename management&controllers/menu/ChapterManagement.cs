using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterManagement: MonoBehaviour
{
    [SerializeField]
    private Button chapterButtonForward;
    bool isForwardActive;

    [SerializeField]
    Button chapterButtonBackward;
    bool isBackwardActive;

    [SerializeField]
    GameObject LevelPanelPrefab;

    [SerializeField]
    private Text chapterIndexText;

    [SerializeField]
    private Text chapterNameText;

    [SerializeField]
    private GameObject LevelHorizontalLine1;

    [SerializeField]
    private GameObject LevelHorizontalLine2;

    [SerializeField]
    private ChapterData[] chapterDatas;

    [SerializeField]
    private ChapterData currentChapterData;

    [SerializeField]
    private int chapterIndex;

    private void Awake()
    {
       
    }

    private void Start()
    {
        OnChapterChanged();
    }

    private void Update()
    {   
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveCurrentLevelDatasFromChapters();
        }
     
    }

    public void OnChapterChanged()
    {

        DeleteCurrentLevels();

        currentChapterData = chapterDatas[chapterIndex];

        LoadLevelDatasFromChapters();

        CheckLevelActivities();

        CheckChapterActivities();

        SetCurrentLevels();

        SetButtonColors(); 
   
        DataManagement.playerData.currentChapterData = chapterDatas[chapterIndex];

        chapterNameText.text = currentChapterData.chapterName;

        chapterIndexText.text = $"Chapter {chapterIndex + 1}";
       
    }

    
    public void ChapterButtonForward()
    {
        if (chapterIndex < chapterDatas.Length-1)
        {
            chapterIndex++;   
            SoundManagement.soundManagement.PlaySound("ClickSound");
        }
        OnChapterChanged();
    }

    public void ChapterButtonBackward()
    {
            if (chapterIndex > 0)
            {
            chapterIndex--;
            SoundManagement.soundManagement.PlaySound("ClickSound");
        }
        OnChapterChanged();
    }



    void SetButtonColors()
    {
        if (chapterIndex > 0)
        {
            chapterButtonBackward.image.color = new Color32(255, 255, 255, 255);
        }

        else
        {
            chapterButtonBackward.image.color = new Color32(75, 75, 75, 255);
        }

        if (chapterIndex < chapterDatas.Length-1)
        {
            chapterButtonForward.image.color = new Color32(255, 255, 255, 255);
        }

        else
        {
            chapterButtonForward.image.color = new Color32(75, 75, 75, 255);
        }

    }

    void SetCurrentLevels()
    {
        for (int i = 0; i < currentChapterData.levelDatas.Length; i++)
        {
            if (i <= 4)
            {
                GameObject levelButton = Instantiate(LevelPanelPrefab, LevelHorizontalLine1.transform);

                LevelData currentLevelData = currentChapterData.levelDatas[i];

                levelButton.GetComponent<LevelUI>().SetLevelUI(currentLevelData);
            }
            else
            {

                GameObject levelButton = Instantiate(LevelPanelPrefab,LevelHorizontalLine2.transform);

                LevelData currentLevelData = currentChapterData.levelDatas[i];

                levelButton.GetComponent<LevelUI>().SetLevelUI(currentLevelData);
            }

        }
    }
    void DeleteCurrentLevels()
     {
       
            int childs = LevelHorizontalLine1.transform.childCount;

            for (int i = childs - 1; i >= 0; i--)
            { 
            Destroy(LevelHorizontalLine1.transform.GetChild(i).gameObject);
            }

            int childs2 = LevelHorizontalLine2.transform.childCount;

            for (int i = childs2 - 1; i >= 0; i--)
            {
            Destroy(LevelHorizontalLine2.transform.GetChild(i).gameObject);
            }
    }
    public void SaveCurrentLevelDatasFromChapters()
    {
        foreach (ChapterData c in chapterDatas)
        {
            foreach (LevelData levelData in c.levelDatas)
            {
                levelData.SaveData();
            }
        }
    }

    public void LoadLevelDatasFromChapters()
    {
       foreach (ChapterData c in chapterDatas)
        {
            foreach (LevelData levelData in c.levelDatas)
            {
                levelData.LoadData();
            }
        }
    }


    public void CheckLevelActivities()
    {
        for (int i = 0; i < currentChapterData.levelDatas.Length; i++)
        {   
            if (i > 0)
            {              
                    if (currentChapterData.levelDatas[i - 1].isLevelFinished == true)
                   {      
                        currentChapterData.levelDatas[i].isLevelActive = true;
                    }
                    else
                    {
                        currentChapterData.levelDatas[i].isLevelActive = false;
                    }
                
            }
        }
    }
    public void CheckChapterActivities()
    {
        if (chapterIndex != 0)
        {
            if (chapterDatas[chapterIndex-1].levelDatas[currentChapterData.levelDatas.Length - 1].isLevelFinished == true)
            {
               
                currentChapterData.isChapterFinished = true;
            }
        }

        if (currentChapterData.isChapterFinished == true)
        {
            chapterDatas[chapterIndex].levelDatas[0].isLevelActive = true;
        }
    }
    

}
    



