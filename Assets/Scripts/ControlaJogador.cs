using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ControlaJogador : MonoBehaviour, IMatavel, ICuravel
{
    // a declaração de variável aqui permite que ela seja usada para todos os metodos.
    private Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject TextoGameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    private MovimentaJogador meuMovimentoJogador;
    private AnimacaoPersonagem animacaoJogador;
    public Status statusJogador;

    

    void Start(){
        meuMovimentoJogador = GetComponent<MovimentaJogador>();
        animacaoJogador = GetComponent<AnimacaoPersonagem>();
        statusJogador = GetComponent<Status>();
    }
    
    // Update is called once per frame
    void Update(){
        // aqui declaramos a variavel eixoX e damos a ela o metodo input pra indicar que o personagem vai se mover na horizontal, quando apertamos as teclas
        float eixoX = Input.GetAxis("Horizontal");
        // aqui declaramos a variavel eixoY e damos a ela o metodo input pra indicar que o personagem vai se mover na vertical, quando apertamos as teclas
        float eixoZ = Input.GetAxis("Vertical");
        // aqui dizemos que a variavel direção, que foi declarada antes do metodo update, recebe um novo vetor e passamos pra ele, o eixoX e o eixoZ, o eixoY será sempre zero pois não vamos nos mover para cima e para baixo
        direcao = new Vector3(eixoX, 0, eixoZ);


        // condicional utilizada para trocar o valor da variavel booleana, para indicar se o personagem esta se movendo ou não, para que as animações funcionem
        // le-se se direção for diferente de zero, então pega o componente animator e coloque o valor de movimento para true, senão coloque o valor de movimento para false
        //if(direcao != Vector3.zero){
        //animatorJogador.SetBool("Movendo", true);
        //} else{
        //animatorJogador.SetBool("Movendo", false);
        //} esse codigo acima foi refatorado no script AnimacaoPersonagem

        animacaoJogador.Movimentar(direcao.magnitude);

    }
    
    // metodo FixedUpdate roda a cada 0,02 segundos, e a linha de código ali dentro faz com que o personagem se mova de acordo com a sua fisica.
    void FixedUpdate(){
        meuMovimentoJogador.Movimentar(direcao, statusJogador.Velocidade);

        meuMovimentoJogador.RotacaoJogador(MascaraChao);

        
    }

    public void TomarDano (int dano){
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizarSliderVidaJogador();
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        if (statusJogador.Vida <= 0){
            Morrer();
        }
    }

    public void Morrer()
    {
        scriptControlaInterface.GameOver();
    }

    public void CurarVida(int quantidadeDeCura){
        statusJogador.Vida += quantidadeDeCura;
        if(statusJogador.Vida > statusJogador.VidaInicial){
            statusJogador.Vida = statusJogador.VidaInicial;
        }
        scriptControlaInterface.AtualizarSliderVidaJogador();
    }
}
