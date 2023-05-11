using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaJogador : MovimentoPersonagem
{
    public void RotacaoJogador(LayerMask MascaraChao)
    {
        //aqui vamos fazer o personagem apontar para o cursor do mouse
        //definimos uma vari�vel do tipo raio. e atribuimos a ela o valor da camera at� o mouse, na segunda linha fazemos o debug para ver o raio. usando o DrawRay, temos que dizer onde o raio come�a e a damos a dire��o e a cor vermelha.
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        //aqui definimos uma variavel do tipo RaycastHit, essa variavel vai pegar o raio e definir onde ele bate em alguma coisa
        RaycastHit impacto;

        //Aqui abrimos um bloco if e entramos na parte de fisica, vamos lan�ar um raio com o Raycast, e passamos nos atributos o raio, o ponto de impacto e a distancia que queremos que o raio va, 
        //al�m disso ainda adicionamos a variavel publica layerMask para que o raio ignore pontos de impacto que nao s�o o ch�o, como � um variavel publica deve ser colocada o valor na unity. 
        //no ponto de impacto devemos usar a palavra out, pois, a variavel RaycastHit ainda n�o tem nenhum valor
        //dentro do if definimos uma variavel vector3 e damos a ela a posi��o do ponto de impacto e subtraimos dela a posi��o do nosso jogador, na segunda linha bloqueamos o eixo y da posi��o para que o jogador n�o olhe pra cima ou pra baixo.
        //ap�s definimos uma variavel do tipo Quaternion e passamos pra ela atrav�s do metodo LookRotation a posi��o da mira. na quarta linha definimos a rota��o pelo rigidbody
        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = 0;
            Rotacionar(posicaoMiraJogador);
        }
    }
}
