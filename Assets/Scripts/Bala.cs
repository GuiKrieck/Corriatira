using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float Velocidade = 20;
    private Rigidbody rigidbodyBala;
    public AudioClip SomDeMorte;

    private void Start(){
        rigidbodyBala = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       rigidbodyBala.MovePosition(rigidbodyBala.position + (transform.forward * Velocidade * Time.deltaTime)); 
    }

    // marcamos a opção istrigger na unity e geramos esse novo void para dizer o que vai acontecer caso a bala se choque com alguma coisa 
    // esse novo void nos diz quando o objeto, no caso nossa bala, entra em contato com alguma coisa, e se caso ele entrar em contato com algum objeto, ele guarda esse objeto na variavel do tipo Collider que declaramos entre os parenteses
    // no if estamos perguntado se o objeto que a bala colidiu foi um objeto com a tag "Inimigo"(tags são definidas na unity), caso true ele deve ser destruido, fora do if estamos destruindo a bala para que ela não atravesse nenhum objeto com colisão
    void OnTriggerEnter(Collider objetoDeColisao){
        Quaternion rotacaoOpostaABala = Quaternion.LookRotation(-transform.forward);
        switch(objetoDeColisao.tag ){
            case "Inimigo":
                ControlaInimigo inimigo = objetoDeColisao.GetComponent<ControlaInimigo>();
                inimigo.TomarDano(1);
                inimigo.ParticulaSangue(transform.position,rotacaoOpostaABala);
            break;
            case "ChefedeFase":
                ControlaChefe chefe = objetoDeColisao.GetComponent<ControlaChefe>();
                chefe.TomarDano(1);
                chefe.ParticulaSangue(transform.position, rotacaoOpostaABala);
            break;
            
        }
        Destroy(gameObject);
    }
        
}
