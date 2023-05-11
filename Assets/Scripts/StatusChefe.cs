using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusChefe : MonoBehaviour
{

    public int VidaInicial = 1;
    //[HideInInspector]
    public int Vida;
    public float Velocidade = 5;
    private GeradorChefe gerador;

    void Awake()
    {
        gerador = GameObject.FindObjectOfType<GeradorChefe>();
        Vida = VidaInicial + gerador.AumentoDeVidaDoChefe;
    }
}

