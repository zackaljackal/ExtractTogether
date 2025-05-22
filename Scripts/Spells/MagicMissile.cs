using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Netcode;
using UnityEngine;

public class MagicMissile : Spell
{
    private float flightSpeed = 17f;
    private float flightTime = 1f;
    public Vector3 direction;
    private int damage = 2;
    private Rigidbody2D rb;
    private float _maxCooldown = 0.75f;

    public override float MaxCooldown
    {
        get { return _maxCooldown; }
        set { _maxCooldown = value; }
    }


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var angle = Quaternion.AngleAxis(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg, transform.forward);
        if (direction.x < 0)
            GetComponent<SpriteRenderer>().flipY = true;
        transform.rotation = angle;
    }

    // Update is called once per frame
    void Update()
    {
        if (flightTime <= 0) Destroy(gameObject);
        rb.velocity = direction * flightSpeed;
        flightTime -= Time.deltaTime;
    }

    public override void Cast(Vector3 dir, Wizard caster)
    {
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
            Wizard wizard = collision.gameObject.GetComponent<Wizard>();
            wizard.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public override Sprite Sprite
    {
        get
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Firebolt SpriteSheet");
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == "Fireball0")
                {
                    return sprite;
                }
            }
            return null;
        }
    }
}
