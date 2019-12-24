using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TypeEffecter Effecter;
    public Image _Portrait;
    public GameObject _ScanObject;

    public GameObject _TalkPanel;
    public DialogTalkManager _DialogManager;
    public Animator _Anim;
    public bool _isAction;

    public int TalkIndex;
    void Start()
    {
        _isAction = false;
        _TalkPanel.SetActive(_isAction);

        _Anim = _TalkPanel.GetComponent<Animator>();
    }

    public void Action(GameObject TalkObject)
    {
        _isAction = !_isAction;
        _ScanObject = TalkObject;

        switch (_isAction)
        {
            case true:
                _TalkPanel.SetActive(_isAction);
                Effecter.ClearMessage();
                _Anim.SetBool("isShow", _isAction);
                Talk();
                break;
            case false:
                _Anim.SetBool("isShow", _isAction);
                HideDialogUI();
                break;
        }
    }

    void Talk()
    {
        ExamableObject exambleObject = _ScanObject.GetComponent<ExamableObject>();
        bool isNPC = exambleObject.isNPC;
        int id = exambleObject.id;

        if (isNPC)
        {
            _Portrait.sprite = exambleObject._Portrait[_DialogManager.GetProtraitIndex(id, TalkIndex)];

            _Portrait.color = new Color(1, 1, 1, 1);
        }
        else
        {
            _Portrait.color = new Color(1, 1, 1, 0);
        }

        ShowDialogUI(_DialogManager.GetTalk(id, TalkIndex++));
    }

    void ShowDialogUI(string Message)
    {
        Effecter.SetMessage(Message);
    }

    void HideDialogUI() // Must Invoke Function
    {
        _TalkPanel.SetActive(_isAction);

        Effecter.CancleMessage();
    }
}
