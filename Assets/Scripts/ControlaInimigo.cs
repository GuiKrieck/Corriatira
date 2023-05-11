using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaInimigo : MonoBehaviour, IMatavel
{
    public GameObject Jogador;
    private MovimentoPersonagem movimentoInimigo;
    private AnimacaoPersonagem animacaoInimigo;
    private StatusZumbi statusInimigo;
    public AudioClip SomDeMorte;
    private Vector3 posicaoAleatoria;
    private Vector3 direcao;
    private float contadorVagar;
    private float tempoEntrePosicoesAleatorias = 4;
    private float porcentagemGerarKitMedico = 0.1f;
    private float porcentagemBomba = 0.15f;
    private float porcentagemMunicao = 0.30f;
    private bool dropouItem = false;
    public GameObject KitMedicoPrefab;
    public GameObject BombaPrefab;
    public GameObject MunicaoPreFab;
    private ControlaInterface scriptControlaInterface;
    [HideInInspector]
    public GeradorZumbis meuGerador;
    public GameObject ParticulaSangueZumbi;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Jogador");
        AleatorizarZumbi();
        animacaoInimigo = GetComponent<AnimacaoPersonagem>();
        movimentoInimigo = GetComponent<MovimentoPersonagem>();
        statusInimigo = GetComponent<StatusZumbi>();
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
    }

    void FixedUpdate(){
        // o metodo Distance, calcule a distancia que um objeto esta do outro, na linha abaixo o metodo Distance vai retornar um numero que sera a distancia que o zumbi(transform.position) esta do jogador (Jogador.transform.position)
        float distancia = Vector3.Distance(transform.position, Jogador.transform.position);

        
        animacaoInimigo.Movimentar(direcao.magnitude);

       if(distancia > 50)
        {
            Vagar();
        }
       else if(distancia > 2.5 ){
            moverParaOJogador();
            animacaoInimigo.Atacar(false);
        } else {
            direcao = Jogador.transform.position - transform.position;
            animacaoInimigo.Atacar(true);
        }
    }
    
    void Vagar()
    {
        contadorVagar -= Time.deltaTime;
        if(contadorVagar <= 0)
        {
            posicaoAleatoria = AleatorizarPosicao();
            contadorVagar += tempoEntrePosicoesAleatorias + Random.Range(-1f, 1f);
            
        }

        bool ficouPertoOSuficiente = Vector3.Distance(transform.position, posicaoAleatoria) <= 0.05;
        if (ficouPertoOSuficiente == false)
        {
            direcao = posicaoAleatoria - transform.position;
            movimentoInimigo.Movimentar(direcao, statusInimigo.Velocidade);
            movimentoInimigo.Rotacionar(direcao);
        }


    }

    Vector3 AleatorizarPosicao()
    {
        int distanciaParaVagar = 10;
        Vector3 posicao = Random.insideUnitSphere * distanciaParaVagar;
        posicao += transform.position;
        posicao.y = transform.position.y;
        return posicao;
    }
    void moverParaOJogador()
    {
        movimentoInimigo.Movimentar(direcao, statusInimigo.Velocidade);
        direcao = Jogador.transform.position - transform.position;
        movimentoInimigo.Rotacionar(direcao);
    }
    // aqui definimos um novo evento(na animação de ataque do zumbi), chamamos ele atraves do void e agora vamos dizer através do codigo o que acontece quando o evento é chamado
    //na primeira linha definimos que o jogo ira ser pausado
    void AtacaJogador(){
        
        int dano = 20;
        Jogador.GetComponent<ControlaJogador>().TomarDano(dano);
    }

    void AleatorizarZumbi()
    {
        int geraTipoZumbi = Random.Range(1, transform.childCount);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);

    }

    public void TomarDano(int dano)
    {
        statusInimigo.Vida -= dano;
        if(statusInimigo.Vida <= 0)
        {
            Morrer();
        }
    }

    public void ParticulaSangue(Vector3 posicao, Quaternion rotacao){
        Instantiate(ParticulaSangueZumbi, posicao, rotacao);
    }

    public void Morrer()
    {
        //Destroy(gameObject, 2);
        animacaoInimigo.Morrer();
        //movimentoInimigo.Morrer();
        Morreu();
        this.enabled = false;
        ControlaAudio.instancia.PlayOneShot(SomDeMorte);
        VerificaGeracaodeMunicao(porcentagemMunicao);
        VerificarGeracaoDaBomba(porcentagemBomba);
        VerificarGeracaoKitMedico(porcentagemGerarKitMedico);
        scriptControlaInterface.AtualizarAQuantidadeDeZumbiMortos();
        meuGerador.DiminuirQuantidadeDeZumbisVivos();
        dropouItem = false;
    }

    private void VerificaGeracaodeMunicao(float porcentagemDeGeracao)
    {
        if(Random.value <= porcentagemDeGeracao)
        {
            Instantiate(MunicaoPreFab, transform.position, Quaternion.identity);
            dropouItem = true;
        }
    }


    private void VerificarGeracaoDaBomba(float porcentagemGeração)
    {
        if(Random.value <= porcentagemGeração & dropouItem == false)
        {
            Instantiate(BombaPrefab, transform.position, Quaternion.identity);
            dropouItem = true;
        }
    }

    void VerificarGeracaoKitMedico (float porcentagemGeracao){
        if (Random.value <= porcentagemGeracao & dropouItem == false){
            Instantiate(KitMedicoPrefab, transform.position, Quaternion.identity);
        }
    }

    void Morreu(){
        movimentoInimigo.Morrer();
        Destroy(gameObject, 2);
    }
    
}


    

