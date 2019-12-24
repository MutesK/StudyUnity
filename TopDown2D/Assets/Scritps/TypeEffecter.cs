using UnityEngine;
using UnityEngine.UI;


public class TypeEffecter : MonoBehaviour
{
    string Message; // 메세지

    public int Cps;  // Character Per Speed
    public GameObject EndCursor;

    int index;
    Text TextUI;

    void Awake()
    {
        TextUI = GetComponent<Text>();
    }
    public void SetMessage(string message)
    {
        Message = message;

        EffectStart();
    }

    public void ClearMessage()
    {
        TextUI.text = "";
        EndCursor.SetActive(false);
    }

    public void CancleMessage()
    {
        ClearMessage();

        CancelInvoke("Effecting");
        EffectEnd();
    }

    void EffectStart()
    {
        EndCursor.SetActive(false);
        TextUI.text = "";
        index = 0;

        Invoke("Effecting", 0.5f);
    }

    void Effecting()
    {
        TextUI.text += Message[index];
        index++;

        if (index >= Message.Length)
        {
            EffectEnd();
        }
        else
        {
            Invoke("Effecting", 0.1f);
        }
    }

    void EffectEnd()
    {
        EndCursor.SetActive(true);
    }


}
