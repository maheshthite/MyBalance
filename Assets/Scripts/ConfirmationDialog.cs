using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmationDialog : MonoBehaviour
{
    private System.Action storedActionOnConfirm;
    private static ConfirmationDialog instance;
    public Text dialogText;
    public Button m_OkayBtn;
    public Button m_CancelBtn;
    // Start is called before the first frame update
    void Start()
    {
        m_OkayBtn.onClick.AddListener(OnConfirmButton);
        m_CancelBtn.onClick.AddListener(OnCancelButton);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Show(string dialogMessage, System.Action actionOnConfirm)
    {
        instance.storedActionOnConfirm = actionOnConfirm;
        instance.dialogText.text = dialogMessage;
        instance.gameObject.SetActive(true);

        SettingsScript.SetRestGObjectActive(false);
    }
    void Awake()
    {

        instance = this;
        gameObject.SetActive(false);
    }
    public void OnConfirmButton()
    {
        if (storedActionOnConfirm != null)
        {
            storedActionOnConfirm();
            storedActionOnConfirm = null;
            gameObject.SetActive(false);
            SettingsScript.SetRestGObjectActive(true);
        }
    }
    public void OnCancelButton()
    {
        storedActionOnConfirm = null;
        gameObject.SetActive(false);

        SettingsScript.SetRestGObjectActive(true);
    }
}
