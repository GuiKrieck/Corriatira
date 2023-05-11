using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlaMunicao : MonoBehaviour
{
    public int pente = 18;
    public int penteMaximo = 18;
    public int totalDeMunicao;
    private float intervaloDeRecarga = 0.5f;
    private int quantidadeMaximaDeMunicao = 500;
    public AudioClip SomDeReload;
    private ControlaInterface scriptControlaInterface;

    private void Start()
    {
        scriptControlaInterface = GameObject.FindObjectOfType<ControlaInterface>();
        scriptControlaInterface.AtualizaInterfaceMunicao(pente, totalDeMunicao);
    }

    public void reload()
    {

        if(totalDeMunicao > 0)
        {
            ControlaAudio.instancia.PlayOneShot(SomDeReload);
            StartCoroutine(intervaloDeReload());
        }
    }

    private IEnumerator intervaloDeReload()
    {
        yield return new WaitForSeconds(intervaloDeRecarga);
        int quantidadeARecarregar;
        quantidadeARecarregar = penteMaximo - pente;
        if(totalDeMunicao >= quantidadeARecarregar)
        {
            pente += quantidadeARecarregar;
            totalDeMunicao -= quantidadeARecarregar;
        }
        else
        {
            pente += totalDeMunicao;
            totalDeMunicao -= totalDeMunicao;
        }
       // pente += QuantidadeDeRecarga;
       // totalDeMunicao -= QuantidadeDeRecarga;
        scriptControlaInterface.AtualizaInterfaceMunicao(pente, totalDeMunicao);
    }

    public void AddMunicao(int municaoAAdicionar)
    {
        totalDeMunicao += municaoAAdicionar;
        if (totalDeMunicao > quantidadeMaximaDeMunicao)
        {
            totalDeMunicao = quantidadeMaximaDeMunicao;
        }
        scriptControlaInterface.AtualizaInterfaceMunicao(pente, totalDeMunicao);
    }
}
