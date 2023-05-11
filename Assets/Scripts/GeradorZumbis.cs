using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorZumbis : MonoBehaviour
{
    public GameObject Zumbi;
    private float contadorTempo = 0;
    public float TempoGerarZumbi = 1;
    public LayerMask LayerZumbi;
    private float distanciaDeGeracao = 3;
    private float distanciaDoJogadorParaGeracao = 20;
    private GameObject jogador;
    private int quantidadeMaximadeZumbisVivos = 2;
    private int quantidadeDeZumbisVivos;
    private float tempoProximoAumentoDeDificuldade = 60;
    private float contadorDeAumentarADificuldade;
    private StatusZumbi statusZumbi;
    public int VidaZumbiNoAumentoDaDificuldade = 0;

    void Start()
    {
        jogador = GameObject.FindWithTag("Jogador");
        
        for(int i = 0; i < quantidadeMaximadeZumbisVivos; i++){
            StartCoroutine(GerarNovoZumbi());
        }
        contadorDeAumentarADificuldade = tempoProximoAumentoDeDificuldade;

        this.statusZumbi = GameObject.FindObjectOfType<StatusZumbi>();
    }
    
    
    void Update()
    {   
        bool possoGerarZumbisPelaDistancia = Vector3.Distance(transform.position, jogador.transform.position) > distanciaDoJogadorParaGeracao;

        if(possoGerarZumbisPelaDistancia == true && quantidadeDeZumbisVivos < quantidadeMaximadeZumbisVivos)
        {
            contadorTempo += Time.deltaTime;

            if (contadorTempo >= TempoGerarZumbi)
            {
                StartCoroutine(GerarNovoZumbi());
                contadorTempo = 0;
            }
        }

        if (Time.timeSinceLevelLoad > contadorDeAumentarADificuldade){
            quantidadeMaximadeZumbisVivos++;
            contadorDeAumentarADificuldade = Time.timeSinceLevelLoad + tempoProximoAumentoDeDificuldade;
            VidaZumbiNoAumentoDaDificuldade++;

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanciaDeGeracao);
    }

    IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);

        while(colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);
            yield return null;
        }
        ControlaInimigo zumbi = Instantiate(Zumbi, posicaoDeCriacao, transform.rotation).GetComponent<ControlaInimigo>();
        zumbi.meuGerador = this;
        quantidadeDeZumbisVivos++;
    }

    Vector3 AleatorizarPosicao()
    {
        
        Vector3 posicao = Random.insideUnitSphere * distanciaDeGeracao;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
    }

    public void DiminuirQuantidadeDeZumbisVivos(){
        quantidadeDeZumbisVivos--;
    }
}
