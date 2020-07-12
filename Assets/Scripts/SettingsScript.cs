using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    public Button m_SaveBtn;
    public Button m_CancelBtn;
    public Button m_EraseAllBtn;
    public static GameObject restdialog;
    // Start is called before the first frame update
    void Start()
    {
        LoadTopics();
        m_CancelBtn.onClick.AddListener(TaskCancelOnClick);
        m_SaveBtn.onClick.AddListener(TaskSaveOnClick);
        m_EraseAllBtn.onClick.AddListener(TaskEraseAllOnClick);
        GetGameObject();
    }
    static void GetGameObject()
    {
        restdialog = GameObject.Find("RestDialog");
    }
    public static void SetRestGObjectActive(bool ActiveInactiveFlag)
    {
        restdialog.SetActive(ActiveInactiveFlag);
    }
    void TaskEraseAllOnClick()
    {
        DeleteAllData();

    }
    void TaskCancelOnClick()
    {
        Debug.Log("LoadNewScreen: ");
        SceneManager.LoadScene("scene2x");
    }
    void TaskSaveOnClick()
    {
        SaveTopics();
        Debug.Log("LoadNewScreen: ");
        SceneManager.LoadScene("scene2x");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public  void DeleteAllData()
    {
        ConfirmationDialog.Show("Are you sure you want to", ClearAllDataCallback);
       
    }
    public  void ClearAllDataCallback()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("LoadNewScreen: ");
        SceneManager.LoadScene("scene2x");
    }

    public static void SaveTopics()
    {
        for (int k = 0; k < 5; k++)
        {

            string strGoName = "TopicType" + k.ToString();
            // Debug.Log("Text:" + allText[i].name);
            string strGoWeekTypeName = InGameVars.strWorkWeek + "TopicType" + k.ToString();


            //Debug.Log(strGoName);
            GameObject typ1 = GameObject.Find(strGoName);

            TMP_InputField[] allgo = typ1.GetComponentsInChildren<TMP_InputField>(true);
            for (int i = 0; i < allgo.Length; i++)
            {
                //   Debug.Log(allgo[i].name);
                string sInput = strGoName + "Topic";
                PlayerPrefs.SetString(sInput, allgo[i].text);
                sInput = strGoWeekTypeName + "Topic";
                PlayerPrefs.SetString(strGoWeekTypeName, allgo[i].text);
            }

            Dropdown[] alldropdowns = typ1.GetComponentsInChildren<Dropdown>(true);
            for (int i = 0; i < alldropdowns.Length; i++)
            {
                Debug.Log(alldropdowns[i].name);
                if (alldropdowns[i].name == "Color")
                {
                    Debug.Log(alldropdowns[i].options[alldropdowns[i].value].text);
                    string sInput = strGoName + alldropdowns[i].name ;
                    PlayerPrefs.SetString(sInput, alldropdowns[i].options[alldropdowns[i].value].text);
                    sInput = strGoWeekTypeName + alldropdowns[i].name;
                    PlayerPrefs.SetString(sInput, alldropdowns[i].options[alldropdowns[i].value].text);
                }
                if (alldropdowns[i].name == "FontSize")
                {
                    Debug.Log(alldropdowns[i].options[alldropdowns[i].value].text);
                    string sInput = strGoName + alldropdowns[i].name;
                    PlayerPrefs.SetString(sInput, alldropdowns[i].options[alldropdowns[i].value].text);
                    sInput = strGoWeekTypeName + alldropdowns[i].name;
                    PlayerPrefs.SetString(strGoWeekTypeName, alldropdowns[i].options[alldropdowns[i].value].text);
                }
            }

        }

    }
    public static void LoadTopics()
    {
        for (int k = 0; k < 5; k++)
        {

            string strGoName = "TopicType" + k.ToString();
            //Debug.Log(strGoName);
            GameObject typ1 = GameObject.Find(strGoName);

            TMP_InputField[] allgo = typ1.GetComponentsInChildren<TMP_InputField>(true);
            for (int i = 0; i < allgo.Length; i++)
            {
                //Debug.Log(allgo[i].name);
                string sInput =  strGoName + "Topic";
                Debug.Log(sInput + "PerfData:" + PlayerPrefs.GetString(sInput));
                allgo[i].text = PlayerPrefs.GetString(sInput);

            }
            Dropdown[] alldropdowns = typ1.GetComponentsInChildren<Dropdown>(true);
            for (int i = 0; i < alldropdowns.Length; i++)
            {
                Debug.Log(alldropdowns[i].name);
                if (alldropdowns[i].name == "Color")
                {
                    Debug.Log(alldropdowns[i].options[alldropdowns[i].value].text);
                    string sInput = strGoName + alldropdowns[i].name;
                    string svalue = PlayerPrefs.GetString(sInput);
                    alldropdowns[i].value = alldropdowns[i].options.FindIndex(option => option.text == svalue);
                }
                if (alldropdowns[i].name == "FontSize")
                {
                    Debug.Log(alldropdowns[i].options[alldropdowns[i].value].text);
                    string sInput = strGoName + alldropdowns[i].name;
                    string svalue = PlayerPrefs.GetString(sInput);
                    if (svalue != null && svalue != "")
                        alldropdowns[i].value = alldropdowns[i].options.FindIndex(option => option.text == svalue);
                    else
                        alldropdowns[i].value = alldropdowns[i].options.FindIndex(option => option.text == "20");

                }
            }

        }


    }
}
