using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
void Update(){
    //a linha abaixo faz o billboard olhar PARA a camera.
    //transform.LookAt( transform.position - Camera.main.transform.forward);
    //a linha abaixo faz o billboard olhara PARA A MESMA direção da camera.
    transform.LookAt( transform.position + Camera.main.transform.forward);
}
}
