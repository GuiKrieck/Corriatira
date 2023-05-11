using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    private Rigidbody meuRigidbody;

    void Awake()
    {
        meuRigidbody = GetComponent<Rigidbody>();
    }

    public void Movimentar(Vector3 direcao, float velocidade)
    {
        meuRigidbody.MovePosition(meuRigidbody.position + (direcao.normalized * velocidade * Time.deltaTime));
        //na linha acima usamos o normalized para que a unity "normalize" a variavel dire��o para que o inimigo n�o use a distancia da diferen�a como velocidade.
    }

    public void Rotacionar(Vector3 direcao)
    {
        //Abaixo vamos fazer o inimigo olhar para o nosso personagem, fazemos isso dentro do if, pois o inimigo s� precisa olhar pro personagem caso ele esteja se movimentando
        //Quaternion s�o variaveis usadas para calcular rota��o, usamos o LookRotation para que o inimigo "olhe" para onde esta se movendo
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        meuRigidbody.MoveRotation(novaRotacao);

    }

    public void Morrer(){
        meuRigidbody.constraints = RigidbodyConstraints.None;
        meuRigidbody.velocity = Vector3.zero;
        meuRigidbody.isKinematic = false;
        GetComponent<Collider>().enabled = false;
    }
}
