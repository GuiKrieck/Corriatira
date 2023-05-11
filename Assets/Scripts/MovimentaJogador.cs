using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentaJogador : MovimentoPersonagem
{
    public void RotacaoJogador(LayerMask MascaraChao)
    {
        //aqui vamos fazer o personagem apontar para o cursor do mouse
        //definimos uma variável do tipo raio. e atribuimos a ela o valor da camera até o mouse, na segunda linha fazemos o debug para ver o raio. usando o DrawRay, temos que dizer onde o raio começa e a damos a direção e a cor vermelha.
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        //aqui definimos uma variavel do tipo RaycastHit, essa variavel vai pegar o raio e definir onde ele bate em alguma coisa
        RaycastHit impacto;

        //Aqui abrimos um bloco if e entramos na parte de fisica, vamos lançar um raio com o Raycast, e passamos nos atributos o raio, o ponto de impacto e a distancia que queremos que o raio va, 
        //além disso ainda adicionamos a variavel publica layerMask para que o raio ignore pontos de impacto que nao são o chão, como é um variavel publica deve ser colocada o valor na unity. 
        //no ponto de impacto devemos usar a palavra out, pois, a variavel RaycastHit ainda não tem nenhum valor
        //dentro do if definimos uma variavel vector3 e damos a ela a posição do ponto de impacto e subtraimos dela a posição do nosso jogador, na segunda linha bloqueamos o eixo y da posição para que o jogador não olhe pra cima ou pra baixo.
        //após definimos uma variavel do tipo Quaternion e passamos pra ela através do metodo LookRotation a posição da mira. na quarta linha definimos a rotação pelo rigidbody
        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;
            posicaoMiraJogador.y = 0;
            Rotacionar(posicaoMiraJogador);
        }
    }
}
