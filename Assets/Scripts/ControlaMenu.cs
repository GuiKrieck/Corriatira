using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// abaixo usado pra trocar de cenas
using UnityEngine.SceneManagement;

public class ControlaMenu : MonoBehaviour
{
    public GameObject BotaoSair;

    private void Start(){
        #if UNITY_STANDALONE || UNITY_EDITOR
            BotaoSair.SetActive(true);
#endif
        Time.timeScale = 1;
    }

    IEnumerator MudarCena(string name){
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(name);
    }

    public void JogarJogo(){
        StartCoroutine(MudarCena("game hotel"));

    }

    public void SairDoJogo(){
        StartCoroutine(Sair());
    }

    public void JogarModoInfinito()
    {
        StartCoroutine(MudarCena("game hotel infinito"));
 
    }

    IEnumerator Sair(){
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
