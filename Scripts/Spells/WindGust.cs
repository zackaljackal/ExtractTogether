using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGust : Spell
{
    private float flightSpeed = 12f;
    private float flightTime = 1f;
    private Vector3 direction;
    private int damage = 2;
    private float _maxCooldown = 0.75f;
    private Rigidbody2D rb;

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
        var scale = gameObject.transform.localScale;
        scale.x += Time.deltaTime * 1.5f;
        scale.y += Time.deltaTime * 1.5f;
        gameObject.transform.localScale = scale;
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
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override Sprite Sprite
    {
        get
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Wind Projectile");
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == "Wind Projectile_0")
                    return sprite;
            }
            return null;
        }
    }
}
