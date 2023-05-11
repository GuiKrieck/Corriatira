using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaBomba : MonoBehaviour
{
    private int dano = 3;
    private int tempoDeDestruição = 10;
    private float distanciaDaExplosao = 20;
    public AudioClip SomDaExplosao;
    public GameObject ParticulaExplosao;

    void Start()
    {
        Destroy(gameObject, tempoDeDestruição);


    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Jogador")
        {
            explodir(transform.position, distanciaDaExplosao);
            Instantiate(ParticulaExplosao, transform.position, transform.rotation);
            ControlaAudio.instancia.PlayOneShot(SomDaExplosao);
            Destroy(gameObject);
        }
    }

    private void explodir(Vector3 centro, float raio)
    {
        Collider[] hitColliders = Physics.OverlapSphere(centro, raio);
        for(int x = 0; x < hitColliders.Length; x++)
        {
            switch (hitColliders[x].tag)
            {
                case "Inimigo":
                    ControlaInimigo inimigo = hitColliders[x].GetComponent<ControlaInimigo>();
                    inimigo.TomarDano(dano);
                    break;
                case "ChefedeFase":
                    ControlaChefe chefe = hitColliders[x].GetComponent<ControlaChefe>();
                    chefe.TomarDano(dano);
                    break;
            }   

            
        }
    }
}
