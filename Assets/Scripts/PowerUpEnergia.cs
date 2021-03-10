using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpEnergia : MonoBehaviour
{
    public int CantidadDeEnergia;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.energy += CantidadDeEnergia;
            Destroy(gameObject);
        }
    }
}
