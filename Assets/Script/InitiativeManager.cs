using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitiativeManager : MonoBehaviour
{
    private readonly List<string> cartas = new List<string> { "Co", "A", "K", "Q", "J", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
    private readonly List<string> naipe = new List<string> { "E", "C", "O", "P" };

    [SerializeField] private ListControl listControl;
    [SerializeField] private IniciativaItem iniciativaPrefab;
    [SerializeField] private Transform conteudo;
    [SerializeField] private GameObject telaIniciativa;
    [SerializeField] private GameObject telaReroll;
    private List<GameObject> listaIniciativa;
    private List<IniciativaItem> iniciativa = new List<IniciativaItem>();
    List<Cartas> deckAtual = new List<Cartas>();

    private bool SaiuCoringa = false;

    public IniciativaItem itemSelecionadoReroll = null;

    void Start()
    {
        telaIniciativa.SetActive(false);
        telaReroll.SetActive(false);
        ResetarEEmbaralhar();
    }

    public void PrimeiraIniciativa()
    {
        listaIniciativa = listControl.itens;
        telaIniciativa.SetActive(true);
        RodarTodasIniciativas();
    }

    public void RodarTodasIniciativas()
    {
        LimparRodadaAnterior();
        foreach (GameObject item in listaIniciativa)
        {
            IniciativaItem novoItem = Instantiate(iniciativaPrefab, conteudo);
            string textoOriginal = item.GetComponentInChildren<TextMeshProUGUI>()?.text ?? "";

            Cartas carta = Sorteio();

            novoItem.ConfigurarItemIniciativa(textoOriginal, () =>
            {
                AbrirFecharRerrolar(novoItem);
            }, carta);

            iniciativa.Add(novoItem);
        }

        Organizador();
        AtualizarOrdem();
    }

    public void Fechar()
    {
        itemSelecionadoReroll = null;
        telaReroll.SetActive(!telaReroll.activeSelf);
    }
    public void AbrirFecharRerrolar(IniciativaItem itemClicado = null)
    {
        if (telaReroll.activeSelf) itemSelecionadoReroll = null;
        else itemSelecionadoReroll = itemClicado;

        telaReroll.SetActive(!telaReroll.activeSelf);
    }
    public void VantagemComplicacao(bool vantagem)
    {
        IniciativaItem item = new IniciativaItem();
        item.ConfigurarItemIniciativa("", () => { }, Sorteio());

        print("Item Novo " + item.cartaAtual._numero + item.cartaAtual._naipe);
        print("Item Antigo " + itemSelecionadoReroll.cartaAtual._numero + itemSelecionadoReroll.cartaAtual._naipe);

        print(" Nova " + DataCarta(item) + " Antiga " + DataCarta(itemSelecionadoReroll));
        print(DataCarta(item) > DataCarta(itemSelecionadoReroll));

        if (vantagem)
        {
            if (DataCarta(item) > DataCarta(itemSelecionadoReroll))
                itemSelecionadoReroll.cartaAtual = item.cartaAtual;
        }
        else
        {
            if (DataCarta(item) < DataCarta(itemSelecionadoReroll))
                itemSelecionadoReroll.cartaAtual = item.cartaAtual;
        }

        Organizador();
        AtualizarOrdem();
        AbrirFecharRerrolar();
    }

    public Cartas Sorteio()
    {
        if (deckAtual == null || deckAtual.Count == 0)
        {
            ResetarEEmbaralhar();
            if (deckAtual.Count == 0) return null;

        }

        int index = Random.Range(0, deckAtual.Count);

        Cartas cartaSorteada = deckAtual[index];
        deckAtual.RemoveAt(index);

        if (cartaSorteada._numero == "Co") SaiuCoringa = true;

        return cartaSorteada;

    }

    public void Organizador()
    {
        int N = iniciativa.Count;

        for (int j = N - 1; j > 0; j--)
        {
            for (int i = 0; i < j; i++)
            {
                if (DataCarta(iniciativa[i]) > DataCarta(iniciativa[i + 1]))
                {
                    TrocaValores(i, i + 1);
                }
                else if (DataCarta(iniciativa[i]) == DataCarta(iniciativa[i + 1]) && DataCarta(iniciativa[i]) != 0)
                {
                    if (NaipeCarta(iniciativa[i]) > NaipeCarta(iniciativa[i + 1]))
                    {
                        TrocaValores(i, i + 1);
                    }
                }
            }
        }
    }

    private void TrocaValores(int m, int n)
    {
        IniciativaItem temp = iniciativa[m];
        iniciativa[m] = iniciativa[n];
        iniciativa[n] = temp;

    }

    private int DataCarta(IniciativaItem item)
    {
        if (item == null || item.cartaAtual == null) return int.MaxValue;

        int index = cartas.IndexOf(item.cartaAtual._numero);

        return index != -1 ? index : int.MaxValue;
    }

    private int NaipeCarta(IniciativaItem item)
    {
        if (item == null || item.cartaAtual == null) return int.MaxValue;

        int index = naipe.IndexOf(item.cartaAtual._naipe);
        return index != -1 ? index : int.MaxValue;
    }

    private void AtualizarOrdem()
    {
        for (int i = 0; i < iniciativa.Count; i++)
        {
            iniciativa[i].transform.SetSiblingIndex(i);
        }
    }

    private void LimparRodadaAnterior()
    {
        foreach (var item in iniciativa)
        {
            if (item != null) Destroy(item.gameObject);
        }
        iniciativa.Clear();

        if (SaiuCoringa)
        {
            ResetarEEmbaralhar();
            SaiuCoringa = false;
        }
    }

    private void ResetarEEmbaralhar()
    {
        deckAtual.Clear();
        deckAtual = new List<Cartas>(Deck.Instancia.baralho);
    }

    public void VoltarParaMenu()
    {
        telaIniciativa.SetActive(false);
        ResetarEEmbaralhar();
    }

}