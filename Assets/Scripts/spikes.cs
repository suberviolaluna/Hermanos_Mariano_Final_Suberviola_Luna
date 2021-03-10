using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{
    public int damage;

    Player player;
    bool playerInside = false;

    IEnumerator damageTimer;    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float dir = Mathf.FloorToInt(Mathf.Clamp(collision.transform.position.x - gameObject.transform.position.x, -1, 1));

        if (collision.gameObject.tag == "Player")
        {
            playerInside = true;
            //canDamage = true;
            player = collision.GetComponent<Player>();
            player.TakeDamage(damage, dir, gameObject.name);

            damageTimer = DamageTimer();
            StartCoroutine(damageTimer);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().culpable = gameObject.name;
            collision.GetComponent<Enemy>().TakeDamage(100, dir * 2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInside = false;
            damageTimer = DamageTimer();
            StopCoroutine(damageTimer);
        }
    }

    IEnumerator DamageTimer()
    {
        yield return new WaitForSeconds(1.8f);
        if (playerInside)
        {            
            float dir = Mathf.FloorToInt(Mathf.Clamp(player.transform.position.x - gameObject.transform.position.x, -1, 1));
            player.TakeDamage(damage, dir, gameObject.name);
            damageTimer = DamageTimer();
            StartCoroutine(damageTimer);
        }
        else
        {
            StopCoroutine(damageTimer);
        }
    }

}
