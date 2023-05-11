using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorChefe : MonoBehaviour
{
    private float tempoParaAProximaGeracao = 0;
    public float TempoEntreGeracoes = 60;
    public GameObject ChefePrefab;
    private ControlaInterface scriptControlaInterface;
    public Transform[] PosicoesPossiveisDeGeração;
    private Transform jogador;
    public int AumentoDeVidaDoChefe = 2;


    private void Start(){
        tempoParaAProximaGeracao = TempoEntreGeracoes;
        scriptControlaInterface = GameObject.FindObjectOfType(typeof(ControlaInterface)) as ControlaInterface;
        jogador = GameObject.FindWithTag("Jogador").transform;
    }

    private void Update(){ 
        if (Time.timeSinceLevelLoad > tempoParaAProximaGeracao){
            Vector3 posicaoDeCriacao = CalcularPosicaoMaisDistanteDoJogador();
            Instantiate(ChefePrefab, posicaoDeCriacao, Quaternion.identity);
            scriptControlaInterface.AparecerTextoChefeCriado();
            tempoParaAProximaGeracao = Time.timeSinceLevelLoad + TempoEntreGeracoes;
            AumentoDeVidaDoChefe++;
        } 
    }

    Vector3 CalcularPosicaoMaisDistanteDoJogador(){
        Vector3 posicaoDeMaiorDistancia = Vector3.zero;
        float maiorDistancia = 0;

        foreach(Transform posicao in PosicoesPossiveisDeGeração){
            float distanciaEntreOJogador = Vector3.Distance(posicao.position, jogador.position);
            if (distanciaEntreOJogador > maiorDistancia){
                maiorDistancia = distanciaEntreOJogador;
                posicaoDeMaiorDistancia = posicao.position;

            }
        }
        return posicaoDeMaiorDistancia;
    }
}
