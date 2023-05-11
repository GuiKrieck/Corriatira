using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusZumbi : MonoBehaviour
{

    public int VidaInicial = 1;
    //[HideInInspector]
    public int Vida;
    public float Velocidade = 5;
    private GeradorZumbis gerador;

    void Awake()
    {
        gerador = GameObject.FindObjectOfType<GeradorZumbis>();      
    }

    private void Start()
    {
        Vida = VidaInicial + gerador.VidaZumbiNoAumentoDaDificuldade;
    }
}

