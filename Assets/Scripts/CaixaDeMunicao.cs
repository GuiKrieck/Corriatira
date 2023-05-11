using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaixaDeMunicao : MonoBehaviour
{
    private int quantidadeDeMunicaoAdicionar = 18;
    private float tempoDeDestruir = 10f;

    private void Start()
    {
        Destroy(gameObject, tempoDeDestruir);
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if(objetoDeColisao.tag == "Jogador")
        {
            objetoDeColisao.GetComponent<ControlaMunicao>().AddMunicao(quantidadeDeMunicaoAdicionar);
            Destroy(gameObject);
        }
    }
}
