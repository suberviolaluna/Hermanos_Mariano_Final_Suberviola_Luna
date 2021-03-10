#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EditorTools;


[ExecuteInEditMode]
public class MoneyCounter : MonoBehaviour
{
    public int PlataTotal;    
    [Space]
    public int Pinchos;
    public int PlatFragil;
    public int PlatMovil;
    public int Sierra;
    [Space]
    public int Basicos;    
    public int Policias;
    public int Obrero;
    public int Rata;
    public int ReptilianoGolpea;
    public int ReptilianoDispara;
    [Space]
    public int Checkpoints;
    public int Pistola;
    public int Energia2;
    public int Energia10;

    Money[] allMoney;
    Enemy[] allEnemies;
    PowerUpEnergia[] allEnergias;

    // Start is called before the first frame update
    void Start()
    {
        Contar();
    }    

    // Update is called once per frame
    void Update()
    {
        if (UnityEditor.EditorApplication.isPlayingOrWillChangePlaymode)
        {
            enabled = false;
        }        
    }

    private void OnEnable()
    {
        Contar();
    }

    private void Contar()
    {                   
        if (FindObjectOfType<Enemy>())
            allEnemies = FindObjectsOfType<Enemy>();
        
        PlataTotal = 0;

        if (FindObjectOfType<spikes>())
            Pinchos = FindObjectsOfType<spikes>().Length;
        if (FindObjectOfType<PlataformaFall>())
            PlatFragil = FindObjectsOfType<PlataformaFall>().Length;
        if (FindObjectOfType<PlataformaMovil>())
            PlatMovil = FindObjectsOfType<PlataformaMovil>().Length;
        if (FindObjectOfType<sierra>())
            Sierra = FindObjectsOfType<sierra>().Length;
        
        Basicos = 0;        
        Policias = 0;
        Obrero = 0;
        Rata = 0;
        ReptilianoGolpea = 0;
        ReptilianoDispara = 0;

        if (FindObjectOfType<Checkpoint>())
            Checkpoints = FindObjectsOfType<Checkpoint>().Length;
        if (FindObjectOfType<PowerUpGun>())
            Pistola = FindObjectsOfType<PowerUpGun>().Length;
        Energia2 = 0;
        Energia10 = 0;

        if(FindObjectOfType<Money>())
        {
            allMoney = FindObjectsOfType<Money>();
            for (int i = 0; i < allMoney.Length; i++)
            {
                PlataTotal += allMoney[i].value;
            }
        }        

        if (FindObjectOfType<PowerUpEnergia>())
        {
            allEnergias = FindObjectsOfType<PowerUpEnergia>();
            for (int i = 0; i < allEnergias.Length; i++)
            {
                switch (allEnergias[i].CantidadDeEnergia)
                {
                    case (2):
                        Energia2++;
                        break;
                    case (10):
                        Energia10++;
                        break;
                    default:
                        break;
                }
            }
        }            
        
        for (int i = 0; i < allEnemies.Length; i++)
        {
            PlataTotal += allEnemies[i].MoneyValue;

            if(allEnemies[i].dropOnDeath != null && allEnemies[i].dropOnDeath.GetComponent<PowerUpEnergia>())
            {
                switch (allEnemies[i].dropOnDeath.GetComponent<PowerUpEnergia>().CantidadDeEnergia)
                {
                    case (2):
                        Energia2++;
                        break;
                    case (10):
                        Energia10++;
                        break;
                    default:
                        break;
                }
            }

            string name = allEnemies[i].gameObject.name;

            if (name.Contains("Chorro") || name.Contains("Oficinista") || name.Contains("Verdulero"))
            {
                Basicos++;
            }
            else if (name.Contains("Policia"))
            {
                Policias++;
            }
            else if (name.Contains("Obrero"))
            {
                Obrero++;
            }
            else if (name.Contains("Rat"))
            {
                Rata++;
            }
            else if (name.Contains("ReptilianoGolpea"))
            {
                ReptilianoGolpea++;
            }
            else if (name.Contains("ReptilianoDispara"))
            {
                ReptilianoDispara++;
            }
        }
    }
}
#endif