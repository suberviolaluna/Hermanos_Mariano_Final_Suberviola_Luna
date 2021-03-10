using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapaBotonObligatorio : MonoBehaviour
{
    //GameManager gm;
    public GameObject Deuda;    
    
    //void Start()
    //{
    //    gm = GameObject.FindObjectOfType<GameManager>();
    //}

    public void Unlock ()
    {
        int zona = transform.parent.GetComponent<MapZoneButton>().zone;

        if(GameManager.StarsDictionary["Z" + zona + "N1"] && GameManager.StarsDictionary["Z" + zona + "N2"] && GameManager.StarsDictionary["Z" + zona + "N3"])
        {
            gameObject.GetComponent<Button>().interactable = true;
            GameManager.paidDeudas += 1;
            
            GameManager.instance.SaveData();
            Deuda.SetActive(false);
        }
        else
        {
            Deuda.GetComponent<Button>().interactable = false;
        }
    }
}
