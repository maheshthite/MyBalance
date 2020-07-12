using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class HeaderTextScript : MonoBehaviour
{

    public Text m_HeaderText;
    private TextMesh m_TextComponent;



    public Button m_BlackBtn;
    public Button m_RedBtn;
    public Button m_BlueBtn;
    public Button m_GreenBtn;
    public Button m_PurpleBtn;
    public static TMP_InputField selectd_TMP_InputField;
    //  public static Color CurrentSelectedColor = Color.black;
    //   public static int CurrentSelectedFontSize = 14;

    public static bool bAdvertimentFlag = true;
    public static bool bAdvertimentFlagWebGL = false;
    public static bool bFullScreenFlagWebGL = false;
    public static bool bRewardsEnableFlag = true;
    public static int iAdvertisePlaying = 0;

    public static bool bStopSaveDataFlag = false;
    public static int bAdvertimentCount = 2;

   

    // Start is called before the first frame update
    void Start()
    {
        m_TextComponent = GetComponent<TextMesh>();
        m_TextComponent.text = DataScript.strWorkWeek;

        // Change the text on the text component.

        m_BlackBtn.onClick.AddListener(BlackOnClick);
        m_RedBtn.onClick.AddListener(RedOnClick);
        m_BlueBtn.onClick.AddListener(BlueOnClick);
        m_GreenBtn.onClick.AddListener(GreenOnClick);
        m_PurpleBtn.onClick.AddListener(PurpleOnClick);


        AddInit.ShowBannerAdv();
    }
    
    void BlackOnClick()
    {
        selectd_TMP_InputField.textComponent.color = Color.black;
        //CurrentSelectedColor = Color.black;
    }
    void RedOnClick()
    {
        selectd_TMP_InputField.textComponent.color = Color.red;
        // CurrentSelectedColor = Color.red;
    }
    void BlueOnClick()
    {
        selectd_TMP_InputField.textComponent.color = Color.blue;
        //  CurrentSelectedColor = Color.blue;
    }
    void GreenOnClick()
    {
        selectd_TMP_InputField.textComponent.color = Color.green;
        //  CurrentSelectedColor = Color.green;
    }
    void PurpleOnClick()
    {
        selectd_TMP_InputField.textComponent.color = Color.magenta;
        //   CurrentSelectedColor = Color.magenta;
    }
    // Update is called once per frame
    void Update()
    {
        m_TextComponent.text = DataScript.strWorkWeek;

    }
    public void DoFireworks()
    {
        //{

        //    //iDoFireworks = 1;
        //    SpecialEffectWood.Instance.Explosion(m_HeaderText.transform.position);
        //    //SpecialEffectWood.Instance.Explosion(m_HeaderText.transform.position);
        //    SpecialEffectsHelper.Instance.Explosion(m_HeaderText.transform.position);
        //   // SpecialEffectsHelper.Instance.Explosion(Text2.transform.position);
        //    //SpecialEffectsHelper.Instance.Explosion(m_RetryBtn.gameObject.transform.position);
        //    //SpecialEffectsHelper.Instance.Explosion(Text1.transform.position);
        //    //SpecialEffectWood.Instance.Explosion(bPos[4, 4].button.gameObject.transform.position);
        //    SoundEffectsHelper.Instance.MakeCheeringSound();
        //    //Destroy(gameObject);
        //    //StartCoroutine("wait");
        //    ///iDoFireworks = 2;

        //}
    }
    void PlayAd()
    {
      //  if (bAdvertimentFlag && iAdvertisePlaying == 0)
        {
     //       iAdvertisePlaying = 1;
     //       AddInit.ShowInterstitialVideo();

        }

    }
}
