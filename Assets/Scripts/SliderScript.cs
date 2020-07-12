using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public Slider mainSlider;
    // Start is called before the first frame update
    void Start()
    {
        mainSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    public void ValueChangeCheck()
    {
        if (DataScript.LoadDataFlag)
            return;
        Debug.Log(mainSlider.value);
        if(mainSlider.value >= 10.00)
        {
            DoFireworks();
        }

    }
    public void DoFireworks()
    {
        {

            //iDoFireworks = 1;
            SpecialEffectWood.Instance.Explosion(transform.position);
            //SpecialEffectWood.Instance.Explosion(m_HeaderText.transform.position);
            SpecialEffectsHelper.Instance.Explosion(transform.position);
            // SpecialEffectsHelper.Instance.Explosion(Text2.transform.position);
            //SpecialEffectsHelper.Instance.Explosion(m_RetryBtn.gameObject.transform.position);
            //SpecialEffectsHelper.Instance.Explosion(Text1.transform.position);
            //SpecialEffectWood.Instance.Explosion(bPos[4, 4].button.gameObject.transform.position);
            SoundEffectsHelper.Instance.MakeCheeringSound();
            //Destroy(gameObject);
            //StartCoroutine("wait");
            ///iDoFireworks = 2;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
