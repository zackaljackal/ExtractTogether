using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FireBall : Spell
{
    private float delay = 1f;
    [SerializeField] Explosion explosion;
    private float _maxCooldown = 2.5f;
    private Wizard _caster;

    public override float MaxCooldown
    {
        get { return _maxCooldown; }
        set { _maxCooldown = value; }
    }


    private void Awake()
    {
        var spriteColor = GetComponent<SpriteRenderer>().color;
        spriteColor = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (delay <= 0) Cast(transform.position, _caster);
        var transparency = gameObject.GetComponent<SpriteRenderer>().color;
        transparency = new Color(transparency.r, transparency.g, transparency.b, transparency.a + Time.deltaTime * 1/delay);
        gameObject.GetComponent<SpriteRenderer>().color = transparency;
        delay -= Time.deltaTime;
    }

    public override void Cast(Vector3 spot, Wizard caster)
    {
        var boom = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public override Sprite Sprite
    {
        get
        {
            Sprite[] sprites = Resources.LoadAll<Sprite>("Explosion SpriteSheet");
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == "Explosion SpriteSheet_1")
                    return sprite;
            }
            return null;
        }
    }
}
