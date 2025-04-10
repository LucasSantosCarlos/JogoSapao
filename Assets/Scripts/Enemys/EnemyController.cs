using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float posicaoInicial;
    [SerializeField]
    private float velocidade;
    [SerializeField]
    private float distancia;
    private SpriteRenderer sr;
    private bool esquerda = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicaoInicial = transform.position.x;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimentacao
        if (esquerda)
        {
            transform.Translate(Time.deltaTime * velocidade * -1, 0, 0);
        }
        else
        {
            transform.Translate(Time.deltaTime * velocidade, 0, 0);
        }
        //Direcao
        if (transform.position.x <= (posicaoInicial - distancia))
        {
            esquerda = false;
            sr.flipX = false;
        }
        if (transform.position.x >= (posicaoInicial + distancia))
        {
            esquerda = true;
            sr.flipX = true;
        }
    }
}
