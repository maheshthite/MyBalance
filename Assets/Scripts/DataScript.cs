using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class WeeklyBal
{
    public string sBalType;
    public string sPlanTo;
    public int sLevel;
    public int sNotes;

    public Text m_BalTypeText;
    private TextMesh m_PlanToTextComponent;
    private TextMesh m_NotesTextComponent;


}

public class DataScript : MonoBehaviour
{
    public Button m_PrevBtn;
    public Button m_NextBtn;
    public Button m_SettingsBtn;
    WeeklyBal[] mWeeklyBal = new WeeklyBal[5];

    
    public static DateTime WorkWeek;
    public static string strWorkWeek;
    public static string strMinWorkWeek;
    public static DateTime MinWorkWeek;
    public static string strMaxWorkWeek;
    public static DateTime MaxWorkWeek;
    public Button m_PrevTopicBtn;
    public  Button m_NextTopicBtn;

    public static GameObject m_TopictypeBtn;
    public static TMP_InputField tmpInputPlanTo;
    public static TMP_InputField tmpInputNotes;
    public static Slider mainSlider;

    public static int iCurrentTopic = 0;
    public static string[] MyTopicNames = new string[5];
    public static Color[] MyColors = new Color[5];
    public static int[] FontSizes = new int[5];
    public string currentDataWeek = "";
    public static bool LoadDataFlag = false;
    private float enshootCooldown;
    float timer = 60.0f; //Amount of seconds
                         // Start is called before the first frame update
    private void Awake()
    {
        DateTime today = DateTime.Now;
        int weekDay = (int)today.DayOfWeek; // 0 - sunday , 6 is saturday
        if (InGameVars.strWorkWeek == "")
        {
            WorkWeek = today.AddDays(weekDay * -1);
            strWorkWeek = WorkWeek.ToString("MM/dd/yyyy");
        }
        else
        {// comin back from settings screen
            WorkWeek = InGameVars.WorkWeek;
            strWorkWeek = InGameVars.strWorkWeek;
        }
        //Get a reference to the text component.
        //Since we are using the base class type <TMP_Text> this component could be either a <TextMeshPro> or <TextMeshProUGUI> component.
      


    }
    void Start()
    {
        // Change the text on the text component.
       
        enshootCooldown = 5.0f;
        for (int i = 0; i < 5; i++)
        {
            MyColors[i] = Color.black;
            FontSizes[i] = 20;
            switch (i)
            {
                case 0:
                    MyTopicNames[i] = "Physical";
                    break;
                case 1:
                    MyTopicNames[i] = "Emotional";
                    break;
                case 2:
                    MyTopicNames[i] = "Intellectual";
                    break;
                case 3:
                    MyTopicNames[i] = "Social";
                    break;
                case 4:
                    MyTopicNames[i] = "Financial";
                    break;
            }
        }
        GetGameObject();
        m_PrevBtn.onClick.AddListener(TaskPrevOnClick);
        m_NextBtn.onClick.AddListener(TaskNextOnClick);
        m_SettingsBtn.onClick.AddListener(TaskSettingsOnClick);

        m_PrevTopicBtn.onClick.AddListener(TaskPrevTopicOnClick);
        m_NextTopicBtn.onClick.AddListener(TaskNextTopicOnClick);

        LoadDataFlag = false;
        LoadSettiingsData();
        LoadTopicData(strWorkWeek);
        currentDataWeek = strWorkWeek;
        LoadDataFlag = true;
    }
    static void GetGameObject()
    {
        m_TopictypeBtn = GameObject.Find("TopicType");
        Debug.Log("GetGameObject m_TopictypeBtn" );
        Text m_TextComponent = m_TopictypeBtn.GetComponentInChildren<Text>();
        Debug.Log("m_TextComponent" + m_TextComponent.text);
        GameObject go = GameObject.Find("PlanTo");
        tmpInputPlanTo = go.GetComponentInChildren<TMP_InputField>();
        tmpInputPlanTo.text = " PlanTo mahesh";
        GameObject go1 = GameObject.Find("Notes");
        tmpInputNotes = go1.GetComponentInChildren<TMP_InputField>();
        tmpInputNotes.text = " notes mahesh";
        GameObject go3 = GameObject.Find("Curr");
        mainSlider = go3.GetComponentInChildren<Slider>();
        mainSlider.value = 10;



    }
    public void LoadSettiingsData()
    {
        Debug.Log("inside LoadSettiingsData");
        strMinWorkWeek = PlayerPrefs.GetString("MinWorkWeek");
        if(strMinWorkWeek != null && strMinWorkWeek != "")
            MinWorkWeek = DateTime.Parse(strMinWorkWeek);
        strMaxWorkWeek = PlayerPrefs.GetString("MaxWorkWeek");
        if (strMaxWorkWeek != null && strMaxWorkWeek != "")
            MaxWorkWeek = DateTime.Parse(strMaxWorkWeek);
        // settings data
        Debug.Log("MinWorkWeek" + strMinWorkWeek);
        Debug.Log("MaxWorkWeek" + strMaxWorkWeek);
        for (int k = 0; k < 5; k++)
        {
            string strGoTypeName = "TopicType" + k.ToString() + "Topic";
            //Debug.Log(strGoTypeName);
            if (PlayerPrefs.GetString(strGoTypeName) != null && PlayerPrefs.GetString(strGoTypeName) != "")
            {
                Debug.Log(strGoTypeName +"PerfData:"+PlayerPrefs.GetString(strGoTypeName));

                MyTopicNames[k] = PlayerPrefs.GetString(strGoTypeName);
            }

            strGoTypeName = "TopicType" + k.ToString() + "Color";
            //Debug.Log(strGoTypeName);
            if (PlayerPrefs.GetString(strGoTypeName) != null && PlayerPrefs.GetString(strGoTypeName) != "")
            {

                string color = PlayerPrefs.GetString(strGoTypeName);
                Debug.Log(strGoTypeName + color);
                MyColors[k] = SetColor(color);
            }
            strGoTypeName = "TopicType" + k.ToString() + "FontSize";
            //Debug.Log(strGoTypeName);
            if (PlayerPrefs.GetString(strGoTypeName) != null && PlayerPrefs.GetString(strGoTypeName) != "")
            {
                string fontsize = PlayerPrefs.GetString(strGoTypeName);
                Debug.Log(strGoTypeName + fontsize);
                FontSizes[k] = Int32.Parse(fontsize);

            }

        }
    }
    void TaskSettingsOnClick()
    {
        AddInit.HideBannerAdv();
        DataScript.SaveTopicData(strWorkWeek);

        InGameVars.WorkWeek = WorkWeek;
        InGameVars.strWorkWeek = strWorkWeek;
        DataScript.EraseWeekTopic();
        Debug.Log("LoadNewScreen: ");
        SceneManager.LoadScene("settings");
    }
    void TaskPrevOnClick()
    {
        Debug.Log("You have clicked the TaskPrevOnClick!");
        DataScript.SaveTopicData(strWorkWeek);
        //HeaderTextScript.ChangeWeek(-7);
        WorkWeek = WorkWeek.AddDays(-7);
        strWorkWeek = WorkWeek.ToString("MM/dd/yyyy");
        //m_TextComponent.text = strWorkWeek;
        Debug.Log("You have clicked the TaskPrevOnClick!" + strWorkWeek);
        LoadTopicData(strWorkWeek);
    }
    void TaskNextOnClick()
    {
        DataScript.SaveTopicData(strWorkWeek);
        //UnitySingleton.Instance.ChangeWeek(7);
        WorkWeek = WorkWeek.AddDays(7);
        strWorkWeek = WorkWeek.ToString("MM/dd/yyyy");
        //m_TextComponent.text = strWorkWeek;
        Debug.Log("You have clicked the TaskNextOnClick!" + strWorkWeek);
        LoadTopicData(strWorkWeek);
    }

    void TaskPrevTopicOnClick()
    {
        Debug.Log("You have clicked the TaskPrevOnClick!");
        SaveTopicData(strWorkWeek);
        if (iCurrentTopic == 0)
            iCurrentTopic = 4;
        else
            iCurrentTopic--;
        LoadTopicData(strWorkWeek);

    }
    void TaskNextTopicOnClick()
    {
        Debug.Log("You have clicked the TaskNextOnClick!");
        SaveTopicData(strWorkWeek);
        if (iCurrentTopic == 4)
            iCurrentTopic = 0;
        else
            iCurrentTopic++;
        LoadTopicData(strWorkWeek);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentDataWeek != strWorkWeek)
        {
            LoadDataFlag = true;
           // SaveTopicData(strWorkWeek);
            currentDataWeek = strWorkWeek;
        }
        else
        {
            timer -= Time.deltaTime;
            if (HeaderTextScript.bAdvertimentFlag && timer <= 0)
            {
                timer = 240.0f; //Amount of seconds
                ////Debug.Log("AddInit.ShowInterstitialVideo");
                SaveTopicData(strWorkWeek);
                AddInit.ShowInterstitialVideo();

            }

            LoadDataFlag = false;
            if (enshootCooldown > 0)
            {
               // ////Debug.Log("enshootCooldown:" + enshootCooldown);
                enshootCooldown -= Time.deltaTime;
                return;
            }
            SaveTopicData(strWorkWeek);
            enshootCooldown = 5.0f;
        }

    }
    public static void LoadTopicData(string sWeek)
    {
        int ignoreMe = 0;
        // set topic
        Text m_TextComponent = m_TopictypeBtn.GetComponentInChildren<Text>();

        string strGoTypeWeeklyName = sWeek + "TopicType" + iCurrentTopic.ToString() + "Topic";
        if (PlayerPrefs.GetString(strGoTypeWeeklyName) != null && PlayerPrefs.GetString(strGoTypeWeeklyName) != "")
        {
            Debug.Log("LoadData" + strGoTypeWeeklyName + PlayerPrefs.GetString(strGoTypeWeeklyName));
            m_TextComponent.text = PlayerPrefs.GetString(strGoTypeWeeklyName);
        }
        else
        {
            Debug.Log("LoadData" + MyTopicNames[iCurrentTopic]);
            m_TextComponent.text = MyTopicNames[iCurrentTopic];
        }
        string strGoName = "BalType" + iCurrentTopic.ToString();
        ////Debug.Log(allgo[i].name);
        string sInput = sWeek + strGoName + "PlanTo";
        string splanto = PlayerPrefs.GetString(sInput);
        Debug.Log("Load PlanTo" + sInput + PlayerPrefs.GetString(sInput));
        if (splanto != null && splanto != "")
        {
            Debug.Log("Load Planto inside weekly" + sInput + splanto);
            tmpInputPlanTo.text = splanto;
        }
        else
        {
            Debug.Log("Load Planto blank" + sInput + splanto);
            tmpInputPlanTo.text = "";
        }

        sInput = sWeek + strGoName + "PlanTo_Color";
        Debug.Log("Load Color" + sInput + PlayerPrefs.GetString(sInput));
        string scolor = PlayerPrefs.GetString(sInput);
        if (scolor != null && scolor != "")
        {
            Debug.Log("Load Color inside weekly " + sInput + scolor);
            tmpInputPlanTo.textComponent.color = SetColor(scolor);
        }
        else
        {
            Debug.Log("Load Color inside settings color " + sInput + GetColor(MyColors[iCurrentTopic]));
            tmpInputPlanTo.textComponent.color = MyColors[iCurrentTopic];
        }
        sInput = sWeek + strGoName + "PlanTo_FontSize";
        Debug.Log("Load FontSize" + sInput + PlayerPrefs.GetString(sInput));
        string sfontsize = PlayerPrefs.GetString(sInput);
        if (sfontsize != null && sfontsize != "" && Int32.TryParse(sfontsize, out ignoreMe))
        {
            Debug.Log("Load fontSize inside weekly " + sInput + sfontsize);
            tmpInputPlanTo.textComponent.fontSize = Int32.Parse(sfontsize);
        }
        else
        {
            Debug.Log("Load fontSize inside settings fontSize " + sInput + FontSizes[iCurrentTopic]);
            tmpInputPlanTo.textComponent.fontSize = FontSizes[iCurrentTopic];
        }

        string slevel = sWeek + strGoName + "Level";
        mainSlider.value = PlayerPrefs.GetInt(slevel);

        sInput = sWeek + strGoName + "Notes";
        if (PlayerPrefs.GetString(sInput) != null)
            tmpInputNotes.text = PlayerPrefs.GetString(sInput);
        else
            tmpInputNotes.text = "";

        sInput = sWeek + strGoName + "Notes_Color";
        Debug.Log("Load Color" + sInput + PlayerPrefs.GetString(sInput));
        scolor = PlayerPrefs.GetString(sInput);
        if (scolor != null && scolor != "")
        {
            Debug.Log("Load Color inside weekly " + sInput + scolor);
            tmpInputNotes.textComponent.color = SetColor(scolor);
        }
        else
        {
            Debug.Log("Load Color inside settings color " + sInput + GetColor(MyColors[iCurrentTopic]));
            tmpInputNotes.textComponent.color = MyColors[iCurrentTopic];
        }
        sInput = sWeek + strGoName + "Notes_FontSize";
        Debug.Log("Load FontSize" + sInput + PlayerPrefs.GetString(sInput));
        sfontsize = PlayerPrefs.GetString(sInput);
        if (sfontsize != null && sfontsize != "" && Int32.TryParse(sfontsize, out ignoreMe))
        {
            Debug.Log("Load fontSize inside weekly " + sInput + sfontsize);
            tmpInputNotes.textComponent.fontSize = Int32.Parse(sfontsize);
        }
        else
        {
            Debug.Log("Load fontSize inside settings fontSize " + sInput + FontSizes[iCurrentTopic]);
            tmpInputNotes.textComponent.fontSize = FontSizes[iCurrentTopic];
        }

    }
    public static void SaveTopicData(string sWeek)
    {
        string strGoName = "BalType" + iCurrentTopic.ToString();

        string strGoTypeName = sWeek + "TopicType" + iCurrentTopic.ToString() + "Topic";
        PlayerPrefs.SetString(strGoTypeName, MyTopicNames[iCurrentTopic]);

        string sInput = sWeek + strGoName + "PlanTo";
        PlayerPrefs.SetString(sInput, tmpInputPlanTo.text);

        sInput = sWeek + strGoName + "PlanTo_Color";
        string scolor = GetColor(tmpInputPlanTo.textComponent.color);
        PlayerPrefs.SetString(sInput, scolor);

        sInput = sWeek + strGoName + "PlanTo_FontSize";
        string sfontsize = tmpInputPlanTo.textComponent.fontSize.ToString();
        PlayerPrefs.SetString(sInput, sfontsize);

        string slevel = sWeek + strGoName + "Level";
        PlayerPrefs.SetInt(slevel, (int)mainSlider.value);

        sInput = sWeek + strGoName + "Notes";
        PlayerPrefs.SetString(sInput, tmpInputNotes.text);

        sInput = sWeek + strGoName + "Notes_Color";
        scolor = GetColor(tmpInputNotes.textComponent.color);
        PlayerPrefs.SetString(sInput, scolor);

        sInput = sWeek + strGoName + "Notes_FontSize";
        sfontsize = tmpInputNotes.textComponent.fontSize.ToString();
        PlayerPrefs.SetString(sInput, sfontsize);

        DateTime currweek = DateTime.Parse(sWeek);
        if (MinWorkWeek == null || strMinWorkWeek == "" || currweek <= MinWorkWeek)
        {
            MinWorkWeek = currweek;
            strMinWorkWeek = sWeek;
            PlayerPrefs.SetString("MinWorkWeek", strMinWorkWeek);
        }
        if (MaxWorkWeek == null || currweek >= MaxWorkWeek)
        {
            MaxWorkWeek = currweek;
            strMaxWorkWeek = sWeek;
            PlayerPrefs.SetString("MaxWorkWeek", strMaxWorkWeek);
        }
    }
    public static void EraseWeekTopic()
    {
        for (int k = 0; k < 5; k++)
        {
            string strGoTypeWeeklyName = strWorkWeek + "TopicType" + k.ToString() + "Topic";
            PlayerPrefs.DeleteKey(strGoTypeWeeklyName);
            strGoTypeWeeklyName = strWorkWeek + "TopicType" + k.ToString() + "Color";
            PlayerPrefs.DeleteKey(strGoTypeWeeklyName);
            strGoTypeWeeklyName = strWorkWeek + "TopicType" + k.ToString() + "FontSize";
            PlayerPrefs.DeleteKey(strGoTypeWeeklyName);


            string strGoName = "BalType" + k.ToString();


            ////Debug.Log(allgo[i].name);
            string sInput = strWorkWeek + strGoName + "PlanTo";

            sInput = strWorkWeek + strGoName  + "PlanTo_Color";
            Debug.Log("delete Color" + sInput + PlayerPrefs.GetString(sInput));
            PlayerPrefs.DeleteKey(sInput);
            sInput = strWorkWeek + strGoName + "PlanTo_FontSize";
            Debug.Log("delete FontSize" + sInput + PlayerPrefs.GetString(sInput));
            PlayerPrefs.DeleteKey(sInput);


            ////Debug.Log(allgo[i].name);
             sInput = strWorkWeek + strGoName + "Notes";

            sInput = strWorkWeek + strGoName + "Notes_Color";
            Debug.Log("delete Color" + sInput + PlayerPrefs.GetString(sInput));
            PlayerPrefs.DeleteKey(sInput);
            sInput = strWorkWeek + strGoName + "Notes_FontSize";
            Debug.Log("delete FontSize" + sInput + PlayerPrefs.GetString(sInput));
            PlayerPrefs.DeleteKey(sInput);
        }
        
    }
        public static Color SetColor(string scolor)
    {
        switch (scolor)
         {
            case "Black":
                    return Color.black;
            case "Blue":
                return Color.blue;
            case "Red":
                return Color.red;
            case "Green":
                return Color.green;
            case "Purple":
                return Color.magenta;
            default:
                return Color.black;
        }
    }
    public static string GetColor(Color scolor)
    {
        if (scolor == Color.black)
            return "Black";
        else if (scolor == Color.blue)
            return "Blue";
        else if (scolor == Color.red)
            return "Red";
        else if (scolor == Color.green)
            return "Green";
        else if (scolor == Color.magenta)
            return "Purple";
        else
            return "Black";

    }
    
}
