using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{

    public int VidaInicial = 1;
    //[HideInInspector]
    public int Vida;
    public float Velocidade = 5;

    void Awake()
    {
        Vida = VidaInicial;
    }
}

