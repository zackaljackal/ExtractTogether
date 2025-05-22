using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    float explosionTime = 0.3f;
    private int damage = 4;

    void Awake()
    {
        var scale = gameObject.transform.localScale;
        scale.x *= 0.5f;
        scale.y *= 0.5f;
        gameObject.transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        if (explosionTime <= 0) Destroy(gameObject);
        var scale = gameObject.transform.localScale;
        scale.x += Time.deltaTime * 1.1f;
        scale.y += Time.deltaTime * 1.2f;
        gameObject.transform.localScale = scale;
        explosionTime -= Time.deltaTime;
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Wizard")
        {
            Wizard wizard = collision.gameObject.GetComponent<Wizard>();
            wizard.TakeDamage(damage);
        }
    }
}
