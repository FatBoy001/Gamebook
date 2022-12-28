using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private static float ATTACK_COOLDOWN = 3f;
    private float attackCooldown = ATTACK_COOLDOWN;
    public GameObject Fireball;

    public Transform bossTransform;

    public void FixedUpdate()
    {
        if (!GameManager.instance.player.alive) return;
        if (!GameManager.instance.boss.alive) return;

        if (Mathf.FloorToInt(attackCooldown) > 0)
        {
            attackCooldown -= Time.fixedDeltaTime;
        }
        else
        {
            attackCooldown = ATTACK_COOLDOWN;
            Instantiate(Fireball, bossTransform.position, bossTransform.rotation);
        }
    }
}
