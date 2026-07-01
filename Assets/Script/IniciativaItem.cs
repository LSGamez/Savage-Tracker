using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IniciativaItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoIniciativa;
    [SerializeField] private Button botaoIniciativa;
    public Cartas cartaAtual;

    public void ConfigurarItem(string texto, System.Action acao)
    {
        if (textoIniciativa != null) textoIniciativa.text = texto;

        if (botaoIniciativa != null)
        {
            botaoIniciativa.onClick.RemoveAllListeners();
            botaoIniciativa.onClick.AddListener(() => acao?.Invoke());
        }
    }

    public void ConfigurarItemIniciativa(string texto, System.Action acao, Cartas cartaRecebida)
    {
        if (textoIniciativa != null) textoIniciativa.text = texto;

        if (botaoIniciativa != null)
        {
            botaoIniciativa.onClick.RemoveAllListeners();
            botaoIniciativa.onClick.AddListener(() => acao?.Invoke());
            botaoIniciativa.GetComponentInChildren<TextMeshProUGUI>().text = cartaRecebida._numero;
            botaoIniciativa.GetComponent<Image>().sprite = cartaRecebida.spriteNaipe;
        }

        cartaAtual = cartaRecebida;

    }

    public void MudarCartaIniciativa()
    {
        botaoIniciativa.GetComponentInChildren<TextMeshProUGUI>().text = cartaAtual._numero;
        botaoIniciativa.GetComponent<Image>().sprite = cartaAtual.spriteNaipe;
    }
}
