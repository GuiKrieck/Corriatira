using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaArma : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoDaArma;
    public AudioClip SomDoTiro;
    public AudioClip SomDeArmaDescarregada;
    private ControlaMunicao scriptControlaMunicao;
    private ControlaInterface scriptControlaInterface;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaMunicao = GameObject.FindObjectOfType<ControlaMunicao>();
        scriptControlaInterface = GameObject.FindObjectOfType<ControlaInterface>();
    }

    // Update is called once per frame
    void Update()
    {   // esse if pergunta pro codigo se um botão foi apertado, no caso deve-se passar o nome do botão, o Fire1 é encontrado na unit no Edit/Propriedades do Projeto/InputManager
        if (scriptControlaMunicao.pente > 0 &  Input.GetButtonDown("Fire1")){
            Instantiate(Bala, CanoDaArma.transform.position, CanoDaArma.transform.rotation); //Instantiate cria objetos dentro do jogo. tem que ser passado, o objeto, a posição e a rotação
            ControlaAudio.instancia.PlayOneShot(SomDoTiro);
            scriptControlaMunicao.pente--;
            if(scriptControlaMunicao.pente == 0)
            {
                scriptControlaMunicao.reload();
            }

            scriptControlaInterface.AtualizaInterfaceMunicao(scriptControlaMunicao.pente, scriptControlaMunicao.totalDeMunicao);
        }else if(scriptControlaMunicao.pente == 0 & Input.GetButtonDown("Fire1"))
        {
            ControlaAudio.instancia.PlayOneShot(SomDeArmaDescarregada);
        }

        if(scriptControlaMunicao.pente < scriptControlaMunicao.penteMaximo & Input.GetButtonDown("Jump"))
        {
            scriptControlaMunicao.reload();
        }
    }
}
