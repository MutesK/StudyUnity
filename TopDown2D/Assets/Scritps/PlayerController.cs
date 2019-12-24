using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float Speed = 5.0f; 

    float _Horizontal;
    float _Vertical;

    
    Rigidbody2D _Rigid;
    Animator _Anim;
    bool isHorizonMove;

    Vector3 _DirectionalVector;
    GameObject _HitedObject;

    public GameManager _GameManager;

    void Awake()
    {
        _Rigid = GetComponent<Rigidbody2D>();
        _Anim = GetComponent<Animator>();
    }

    void Update()
    {
        _Horizontal = _GameManager._isAction ? 0 : Input.GetAxisRaw("Horizontal");
        _Vertical = _GameManager._isAction ? 0 : Input.GetAxisRaw("Vertical");
        
        bool hDown = _GameManager._isAction ? false : Input.GetButtonDown("Horizontal");
        bool vDown = _GameManager._isAction ? false : Input.GetButtonDown("Vertical");
        bool hUp = _GameManager._isAction ? false : Input.GetButtonUp("Horizontal");
        bool vUp = _GameManager._isAction ? false : Input.GetButtonUp("Vertical");

        if (hDown || !vUp)
        {
            isHorizonMove = true;
        }
        else if(vDown || !hUp)
        {
            isHorizonMove = false;
        }

        // Animation Update
        if (_Anim.GetInteger("XposRaw") != (int)_Horizontal)
        {
            _Anim.SetInteger("XposRaw", (int)_Horizontal);
            _Anim.SetBool("isChanged", true);
        }
        else if (_Anim.GetInteger("YposRaw") != (int)_Vertical)
        {
            _Anim.SetInteger("YposRaw", (int)_Vertical);
            _Anim.SetBool("isChanged", true);
        }
        else
        {
            _Anim.SetBool("isChanged", false);
        }


        // For Scan Examable Object
        if(vDown && _Vertical == 1)
        {
            _DirectionalVector = Vector2.up;
        }
        else if(vDown && _Vertical == -1)
        {
            _DirectionalVector = Vector2.down;
        }
        else if(hDown && _Horizontal == 1)
        {
            _DirectionalVector = Vector2.right;
        }
        else if(hDown && _Horizontal == -1)
        {
            _DirectionalVector = Vector2.left;
        }

        // Examble Process
        if(Input.GetButtonDown("Jump") && _HitedObject != null)
        {
            _GameManager.Action(_HitedObject);
        }
    }


    void FixedUpdate()
    {
        Vector2 MoveVector = isHorizonMove ? new Vector2(_Horizontal, 0) : new Vector2(0, _Vertical);
        _Rigid.velocity = new Vector2(_Horizontal, _Vertical) * Speed;


        Debug.DrawRay(_Rigid.position, _DirectionalVector * 0.7f, new Color(0, 1, 0));
        RaycastHit2D Hit = Physics2D.Raycast(_Rigid.position, _DirectionalVector, 0.7f, LayerMask.GetMask("ExamableObject"));

        if (Hit.collider != null)
        {
            _HitedObject = Hit.collider.gameObject;
        }
        else
            _HitedObject = null;
    }



}
