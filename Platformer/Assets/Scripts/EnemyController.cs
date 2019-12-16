using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EMoveState
{
    Right = -1,
    Stop,
    Left,
};

public class EnemyController : Controller
{
    int _State;


    void Start()
    {
        base.Start();

        _State = -1;
        _Anim.SetInteger("ActionState", _State);

        Invoke("GetNextMoveState", 5);
    }

    void FixedUpdate()
    {
        base.FixedUpdate();

        _Rigid.velocity = new Vector2(_State * 1.5f, _Rigid.velocity.y);

        Vector2 front = new Vector2(_Rigid.position.x + _State * 0.2f, _Rigid.position.y);
        Debug.DrawRay(front, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(front, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(rayHit.collider == null)
        {
            _State *= -1;
            _Anim.SetInteger("ActionState", _State);

            CancelInvoke("GetNextMoveState");

            Invoke("GetNextMoveState", 10);
        }
    }

    void Update()
    {
        base.Update();

        // Sprite Render Direction
        Vector2 DirectionalVector = _Rigid.velocity.normalized;
        _Render.flipX = DirectionalVector.x >= 0;
    }

    public void GetNextMoveState()
    {
        _State = Random.Range((int)EMoveState.Right, (int)EMoveState.Left + 1);
        _Anim.SetInteger("ActionState", _State);

        Invoke("GetNextMoveState", 10);
    }

    public override void OnAttack(GameObject DamagedObject)
    {
        throw new System.NotImplementedException();
    }

    public override void OnDamaged(Vector2 Target)
    {
        OnDie();
    }

   void DeActive()
    {
        gameObject.SetActive(false);
    }

    public override void OnDie()
    {
        base.OnDie();

        CancelInvoke("GetNextMoveState");

        _State = (int)EMoveState.Stop;
        _Anim.SetInteger("ActionState", _State);

        Invoke("DeActive", 2);
    }
}
