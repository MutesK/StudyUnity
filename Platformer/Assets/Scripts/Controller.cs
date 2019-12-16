using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public Rigidbody2D _Rigid;
    public SpriteRenderer _Render;
    public Animator _Anim;
    public Collider2D _Collider;
    public PlayerProperty _Property;
    public GameManager _GameManager;


    protected virtual void Start()
    {
        _Rigid = GetComponent<Rigidbody2D>();
        _Render = GetComponent<SpriteRenderer>();
        _Anim = GetComponent<Animator>();
        _Collider = GetComponent<Collider2D>();
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Update()
    {
    }

    public void AddHealth(int Health)
    {
        _Property.Hp += Health;

        if (_Property.Hp <= 0)
            OnDie();

        _GameManager.OnHealthChanged(_Property.Hp);
    }


    /// <summary>
    /// Custom Callback Functions
    /// </summary>
    public abstract void OnAttack(GameObject DamagedObject);

    public abstract void OnDamaged(Vector2 Target);

    public virtual void OnDie()
    {
        _Render.color = new Color(1, 1, 1, 0.4f);
        _Render.flipY = true;
        _Collider.enabled = false; // 충돌 처리 제거

        _Rigid.AddForce(Vector2.up * 5.0f, ForceMode2D.Impulse);
    }

}

