using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaCamera : MonoBehaviour
{
    public GameObject Jogador;
    Vector3 distCompensar;
    // Start is called before the first frame update
    // aqui calculamos a distancia que a camera deve ficar do jogador
    void Start()
    {
        distCompensar = transform.position - Jogador.transform.position;
    }

    // Update is called once per frame
    // aqui colocamos que a posição da camera deve ser a posição do jogador mais a distancia a compensar que calculamos no metodo Start
    void Update()
    {
        transform.position = Jogador.transform.position + distCompensar;
    }
}
