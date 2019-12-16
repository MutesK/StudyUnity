using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    void Start()
    {
        base.Start();

        _GameManager.PlayerInfo = this;
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
        

        // Left, Right Move
        _Rigid.AddForce(Vector2.right * Input.GetAxisRaw("Horizontal"), ForceMode2D.Impulse);

        Vector2 DirectionalVector = _Rigid.velocity.normalized;

        if (_Rigid.velocity.x > _Property.MaxWalkSpeed)
            _Rigid.velocity = new Vector2(_Property.MaxWalkSpeed, _Rigid.velocity.y);
        if (_Rigid.velocity.x < -_Property.MaxWalkSpeed)
            _Rigid.velocity = new Vector2(-_Property.MaxWalkSpeed, _Rigid.velocity.y);

        RaycastHit2D RayHit = Physics2D.Raycast(_Rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
        Debug.DrawRay(_Rigid.position, Vector3.down, new Color(0, 1, 0));

        if (RayHit.collider != null && DirectionalVector.y < 0)
        {
            _Anim.SetBool("isJump", false);
        }

    }

    void Update()
    {
        base.Update();

        if (Input.GetButton("Horizontal"))
        {
            _Render.flipX = Input.GetAxisRaw("Horizontal") != 1;
        }

        if (Input.GetButtonUp("Horizontal"))
            _Rigid.velocity = new Vector2(_Rigid.velocity.normalized.x * _Property.DecreaseWalkSpeed, _Rigid.velocity.y);

        if (Input.GetButtonDown("Vertical") && !_Anim.GetBool("isJump"))
            Jump();

        UpdateAnimation();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            float PlayerYpos = _Rigid.velocity.y;
            float CollisionObjectYpos = collision.transform.position.y;

            if (collision.gameObject.name != "Spikes" && PlayerYpos < 0 && CollisionObjectYpos < _Rigid.transform.position.y)
            {
                OnAttack(collision.gameObject);
            }
            else
            {
                OnDamaged(collision.transform.position);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            // Point
            bool isBronzeCoin = collision.gameObject.name.Contains("Bronze");
            bool isSilverCoin = collision.gameObject.name.Contains("Silver");
            bool isGoldCoin = collision.gameObject.name.Contains("Gold");

            if (isBronzeCoin)
                _GameManager.StagePoint += 50;

            if (isSilverCoin)
                _GameManager.StagePoint += 100;

            if (isGoldCoin)
                _GameManager.StagePoint += 150;

            collision.gameObject.SetActive(false);
            SoundManager.Instance.PlaySound("Item");
         
        }
        else if(collision.gameObject.tag == "Finish")
        {
            // Next Stage
            SoundManager.Instance.PlaySound("Finish");
            _GameManager.NextStage();
        }
    }

    private void Jump()
    {
        _Rigid.AddForce(Vector2.up * _Property.JumpForce, ForceMode2D.Impulse);

        _Anim.SetBool("isJump", true);
        SoundManager.Instance.PlaySound("Jump");

    }

    private void UpdateAnimation()
    {
        if (Math.Abs(_Rigid.velocity.x) < _Property.AnimTriggerRangeWalk)
            _Anim.SetBool("isWalk", false);
        else
            _Anim.SetBool("isWalk", true);
    }

    public override void OnDamaged(Vector2 Target)
    {
        AddHealth(-1);


        ///// 무적 설정
        // 레이어 변경
        gameObject.layer = 11;

        // 투명 설정
        _Render.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int Direction = _Rigid.position.x - Target.x > 0 ? 1 : -1;
        _Rigid.AddForce(new Vector2(Direction, 1) * 8.0f, ForceMode2D.Impulse);
        _Anim.SetBool("isJump", true);

        SoundManager.Instance.PlaySound("Damaged");


        Invoke("OffDamaged", 3);
    }

    public override void OnAttack(GameObject DemagedObject)
    {

        // Point
        _GameManager.StagePoint += 50;

        // Reaction Force
        _Rigid.AddForce(Vector2.up * 8.0f, ForceMode2D.Impulse);
        _Anim.SetBool("isJump", true);

        // Die Process
        EnemyController EnemyController = DemagedObject.GetComponent<EnemyController>();

        SoundManager.Instance.PlaySound("Attack");

        EnemyController.OnDamaged(_Rigid.transform.position);
    }

    void OffDamaged()
    {
        gameObject.layer = 10;

        _Render.color = new Color(1, 1, 1, 1);
    }

    public override void OnDie()
    {
        base.OnDie();

        SoundManager.Instance.PlaySound("Die");

        _GameManager.OnPlayerDeath();
    }
}
