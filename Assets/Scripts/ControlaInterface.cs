using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlaInterface : MonoBehaviour
{
    private ControlaJogador scriptControlaJogador;
    public Slider SliderVidaJogador;
    public GameObject PainelDeGameOver;
    public Text TextoTempoDeSobrevivencia;
    public Text TextoPontuacaoMaxima;
    private float tempoPontuacaoSalva;
    private int quantidadeDeZumbiMortos;
    public Text TextoDaQuantidadeDeZumbiMortos;
    public Text TextoChefeAparece;
    public GameObject SairButton;
    public Text sobreviva;
    public float TempoParaVitoria = 330f;
    public GameObject PainelVitoria;
    public Text TextoSobrevivenciaVitoria;
    public Text TextoZumbiMortosVitoria;
    public Text TextoDeMunicao;
    public bool ModoInfinito;
    public GameObject FadeToBlackPanel;
    public GameObject ImagemFinal;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        scriptControlaJogador = GameObject.FindWithTag("Jogador").GetComponent<ControlaJogador>();
        SliderVidaJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        AtualizarSliderVidaJogador();
        tempoPontuacaoSalva = PlayerPrefs.GetFloat("PontuacaoMaxima");

        StartCoroutine(DesaparecerTexto(3f, sobreviva));
    }

    private void Update()
    {
        if (!ModoInfinito)
        {
            if (TempoParaVitoria <= Time.timeSinceLevelLoad)
            {
                StartCoroutine(FadeToBlack(ImagemFinal, true, 2));
            }

        }
 
    }

    public void AtualizarSliderVidaJogador(){
        SliderVidaJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void GameOver(){
        PainelDeGameOver.SetActive(true);
        Time.timeScale = 0;
        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        // % == modulo,em uma divisão ele pega o apenas o resto dessa divisão; 
        //(int) converte o resultado de uma operação para um numero inteiro, descartando os numeros apos a virgula e retornando apenas a parte inteira da operação
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoTempoDeSobrevivencia.text = "Você sobreviveu por: " + minutos +" min e " + segundos + " s.";

        ajustarPontuacaoMaxima(minutos, segundos);
    }

    public void Reiniciar (){
        if (!ModoInfinito)
        {
            SceneManager.LoadScene("game hotel");
        }
        else
        {
            SceneManager.LoadScene("game hotel infinito");
        }
    }

    void ajustarPontuacaoMaxima(int minutos, int segundos){
        if(Time.timeSinceLevelLoad > tempoPontuacaoSalva){
            tempoPontuacaoSalva = Time.timeSinceLevelLoad;
            TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é:{0}min e {1}s", minutos, segundos);
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoPontuacaoSalva);
        }
        if(TextoPontuacaoMaxima.text == ""){
        int min = (int)(tempoPontuacaoSalva / 60);
        int seg = (int)(tempoPontuacaoSalva % 60);
        TextoPontuacaoMaxima.text = string.Format("Seu melhor tempo é:{0}min e {1}s", min, seg);
        }
    }

    public void AtualizarAQuantidadeDeZumbiMortos(){
        quantidadeDeZumbiMortos++;
        TextoDaQuantidadeDeZumbiMortos.text = string.Format("x {0}", quantidadeDeZumbiMortos);

    }

    public void AparecerTextoChefeCriado(){
        StartCoroutine(DesaparecerTexto(2, TextoChefeAparece));
    }

    IEnumerator DesaparecerTexto(float tempoDeSumico, Text textoParaSumir){
        textoParaSumir.gameObject.SetActive(true);
        Color corTexto = textoParaSumir.color;
        corTexto.a = 1;
        textoParaSumir.color = corTexto;
        yield return new WaitForSeconds(1);
        float contador = 0;
        while (textoParaSumir.color.a > 0){
            contador += Time.deltaTime / tempoDeSumico;
            corTexto.a = Mathf.Lerp(1,0, contador);
            textoParaSumir.color = corTexto;
            if (textoParaSumir.color.a <= 0){
                textoParaSumir.gameObject.SetActive(false);
            }
            yield return null;
        }

    }
    public void Sair(){
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");
        
    }

    private void Vitoria()
    {
        
        PainelVitoria.SetActive(true);
        Time.timeScale = 0;
        int minutos = (int)(Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoSobrevivenciaVitoria.text = "Você sobreviveu por: " + minutos + " min e " + segundos + " s.";
        TextoZumbiMortosVitoria.text = string.Format("x {0}", quantidadeDeZumbiMortos);
        ajustarPontuacaoMaxima(minutos, segundos);
    }

    public void AtualizaInterfaceMunicao(int municao, int totalDeMunicao)
    {
        TextoDeMunicao.text = string.Format("{0}/{1}", municao, totalDeMunicao);
    }

    public IEnumerator FadeToBlack(GameObject objetoImagem, bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = objetoImagem.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while(objetoImagem.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                objetoImagem.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        Vitoria();
    }
}
