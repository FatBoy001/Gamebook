using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Collidable  
{
    private Damage damage;
    private Vector2 firingDirection;

    public Vector2 playerPosition;
    public GameObject destroyEffect;


    public new Rigidbody2D rigidbody;
    public RaycastHit2D hit { get; set; }
    public BoxCollider2D boxCollider2D { get; set; }
    
    
    public override void Start()
    {
        base.Start();
        playerPosition = GameManager.instance.player.transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        firingDirection = (playerPosition - (Vector2)transform.position).normalized;
        rigidbody.AddForce(firingDirection * 1f, ForceMode2D.Impulse);
    }
    public override void Update()
    {
        base.Update();
        if (GameManager.instance.boss.alive==false|| GameManager.instance.player.alive == false)
            Destroy(gameObject);
    }

    public override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Collision")
        {
            GameObject effect = Instantiate(destroyEffect, transform.position, transform.rotation);
            SoundManager.instance.Audio.PlayOneShot(SoundManager.instance.explosion);

            Destroy(effect, 0.7f);         
            Destroy(this.gameObject);
        }
        if (coll.name == "Player")
        {
            damage = new Damage() { health = 1, origin = transform.position, pushForce = 5};
            coll.SendMessage("getDamage", damage);
            
            GameObject effect = Instantiate(destroyEffect, transform.position, transform.rotation);
            SoundManager.instance.Audio.PlayOneShot(SoundManager.instance.explosion);
            Destroy(effect, 0.7f);
            Destroy(this.gameObject);
        }
    }
}
