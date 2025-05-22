using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Spell
{
    private float delayTime = 1.5f;      // The duration of the delay before the suction effect activates
    private float suctionRadius = 2.3f;  // The radius within which wizards are affected by the suction
    private float suctionTime = 1.5f;
    private int damage = 2;
    private bool isExploding = false;

    private float elapsedTime = 0.0f;

    private float _maxCooldown = 4f;
    private Wizard _caster;

    public override float MaxCooldown
    {
        get { return _maxCooldown; }
        set { _maxCooldown = value; }
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > delayTime)
        {
            if (elapsedTime > (delayTime + suctionTime))
            {
                isExploding = true;
                Cast(transform.position, _caster);
                Destroy(gameObject);
            }
            else
            {
                Cast(transform.position, _caster);
            }
        }
    }

    public override void Cast(Vector3 spot, Wizard caster)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, suctionRadius);
        foreach (Collider2D collider in colliders)
        {
            Wizard wizard = collider.GetComponent<Wizard>();
            if (wizard != null)
            {
                Vector3 pull = (transform.position - wizard.transform.position).normalized * 5;
                if (isExploding)
                    wizard.TakeDamage(damage);
                collider.GetComponent<Rigidbody2D>().AddForce(pull);
            }
        }
    }

    public override Sprite Sprite
    {
        get
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("TestSprites");
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == "Ether")
                    return sprite;
            }
            return null;
        }
    }
}
