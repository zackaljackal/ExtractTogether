using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Boomerang : Spell
{
    private float flightSpeed = 17f;
    private float flightTime = 2f;
    public Vector3 direction;
    private bool turnedAround = false;
    private int damage = 2;
    private Rigidbody2D rb;
    private float _maxCooldown = 0.75f;
    private Wizard _caster;

    public override float MaxCooldown
    {
        get { return _maxCooldown; }
        set { _maxCooldown = value; }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var angle = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, transform.forward);
        if (direction.x < 0)
            GetComponent<SpriteRenderer>().flipY = true;
        transform.rotation = angle;
    }


    void Update()
    {
        if (flightTime <= 0 || (flightTime <= 1.8f && Vector3.Distance(transform.position, _caster.transform.position) < 0.1f)) Destroy(gameObject);
        if (turnedAround)
            rb.velocity = (_caster.transform.position - transform.position).normalized * flightSpeed;
        else
            rb.velocity = direction * flightSpeed;
        if (flightTime <= 1f) turnedAround = true;
        flightTime -= Time.deltaTime;
    }

    public override void Cast(Vector3 dir, Wizard caster)
    {
        _caster = caster;
        direction = dir;
        var angle = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, transform.forward);
        if (direction.x < 0)
            GetComponent<SpriteRenderer>().flipY = true;
        transform.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wizard")
        {
            Debug.Log("Hit yourself");
            Wizard wizard = collision.gameObject.GetComponent<Wizard>();
            wizard.TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            turnedAround = true;
        }
    }

    public override Sprite Sprite
    {
        get
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("TestSprites");
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == "Boomerang")
                    return sprite;
            }
            return null;
        }
    }
}
