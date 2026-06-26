using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListControl : MonoBehaviour
{
    [SerializeField] private TMP_InputField nomeInserido;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject conteudo;
    public List<GameObject> itens = new List<GameObject>();

    public void AddList()
    {
        if(nomeInserido.text != "")
        {
            GameObject novoItem = Instantiate(itemPrefab, conteudo.transform);

            novoItem.name = nomeInserido.text;
            itens.Add(novoItem);
            novoItem.GetComponent<IniciativaItem>().ConfigurarItem(nomeInserido.text, () =>RemoveList(novoItem.transform));
        }

        nomeInserido.text = "";
    }

    public void RemoveList(Transform objeto)
    {    
        itens.Remove(objeto.gameObject);
        Destroy(objeto.gameObject);        
    }

}