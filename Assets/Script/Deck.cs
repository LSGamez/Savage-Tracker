using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public static Deck Instancia;
    private List<string> cartas = new List<string>{ "Co","A", "K", "Q", "J", "10", "9", "8", "7", "6", "5", "4", "3", "2" };
    private string[] naipe = { "E", "C", "O", "P" };
    [SerializeField] private Sprite[] imagensNaipes;
    public readonly List<Cartas> baralho = new List<Cartas>();

    void Awake()
    {
        if(Instancia != null && Instancia != this)
        {
            Destroy(this);
        } else
        {
            Instancia = this;
        }
    }
    void Start()
    {
        for (int n = 0; n < naipe.Length; n++)
        {
            for (int c = 1; c < cartas.Count; c++)
            {
                Cartas carta = new Cartas(cartas[c],naipe[n],imagensNaipes[n]);
                baralho.Add(carta);
            }
        }
        for (int i = 0; i < 2; i++)
        {
            AdicionarCoringa();
        }

    }

    void AdicionarCoringa()
    {
        Cartas carta = new Cartas("Co","",imagensNaipes[imagensNaipes.Length-1]);
        baralho.Add(carta);
    }

}

[Serializable]
public class Cartas
{
    public string _numero { get; set; }
    public string _naipe { get; set; }
    public Sprite spriteNaipe {get;set;}

    public Cartas(string numero, string naipe,Sprite sprite)
    {
        _numero = numero;
        _naipe = naipe;
        spriteNaipe = sprite;
    }
}
