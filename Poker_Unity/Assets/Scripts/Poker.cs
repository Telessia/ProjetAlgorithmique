﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poker : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject playerPrefab;
    public List<GameObject> deck;
    private List<GameObject> joueurs;
    public int nbJoueurs = 5;
    public List<GameObject> flop = new List<GameObject>(5);
    // Start is called before the first frame update
    void Start()
    {
        test();
        GameObject c1 = GameObject.Find("Carte_1");
        GameObject c2 = GameObject.Find("Carte_2");
       /* this.deck[0].transform.position = c1.transform.position;
        this.deck[1].transform.position = c2.transform.position;
        this.deck[0].GetComponent<Carte>().isFaceUp = true;
        this.deck[1].GetComponent<Carte>().isFaceUp = true;*/
        generationJoueurs();
        distribution();
        c1.GetComponent<SpriteRenderer>().enabled = false;
        c2.GetComponent<SpriteRenderer>().enabled = false;
        this.joueurs[0].GetComponent<Joueur>().sonTour = true;
        this.joueurs[0].GetComponent<Joueur>().main[0].transform.position = c1.transform.position;
        this.joueurs[0].GetComponent<Joueur>().main[1].transform.position = c2.transform.position;
        c1 = GameObject.Find("Carte_3");
        c2 = GameObject.Find("Carte_4");
        c1.GetComponent<SpriteRenderer>().enabled = false;
        c2.GetComponent<SpriteRenderer>().enabled = false;
        this.joueurs[1].GetComponent<Joueur>().sonTour = true;
        this.joueurs[1].GetComponent<Joueur>().main[0].transform.position = c1.transform.position;
        this.joueurs[1].GetComponent<Joueur>().main[1].transform.position = c2.transform.position;
        this.joueurs[1].GetComponent<Joueur>().main[0].transform.rotation = c1.transform.rotation;
        this.joueurs[1].GetComponent<Joueur>().main[1].transform.rotation = c2.transform.rotation;
        c1 = GameObject.Find("Carte_5");
        c2 = GameObject.Find("Carte_6");
        c1.GetComponent<SpriteRenderer>().enabled = false;
        c2.GetComponent<SpriteRenderer>().enabled = false;
        this.joueurs[2].GetComponent<Joueur>().sonTour = true;
        this.joueurs[2].GetComponent<Joueur>().main[0].transform.position = c1.transform.position;
        this.joueurs[2].GetComponent<Joueur>().main[1].transform.position = c2.transform.position;
        this.joueurs[2].GetComponent<Joueur>().main[0].transform.rotation = c1.transform.rotation;
        this.joueurs[2].GetComponent<Joueur>().main[1].transform.rotation = c2.transform.rotation;
        c1 = GameObject.Find("Carte_7");
        c2 = GameObject.Find("Carte_8");
        c1.GetComponent<SpriteRenderer>().enabled = false;
        c2.GetComponent<SpriteRenderer>().enabled = false;
        this.joueurs[3].GetComponent<Joueur>().sonTour = true;
        this.joueurs[3].GetComponent<Joueur>().main[0].transform.position = c1.transform.position;
        this.joueurs[3].GetComponent<Joueur>().main[1].transform.position = c2.transform.position;
        this.joueurs[3].GetComponent<Joueur>().main[0].transform.rotation = c1.transform.rotation;
        this.joueurs[3].GetComponent<Joueur>().main[1].transform.rotation = c2.transform.rotation;
        c1 = GameObject.Find("Carte_9");
        c2 = GameObject.Find("Carte_10");
        c1.GetComponent<SpriteRenderer>().enabled = false;
        c2.GetComponent<SpriteRenderer>().enabled = false;
        this.joueurs[4].GetComponent<Joueur>().sonTour = true;
        this.joueurs[4].GetComponent<Joueur>().main[0].transform.position = c1.transform.position;
        this.joueurs[4].GetComponent<Joueur>().main[1].transform.position = c2.transform.position;
        this.joueurs[4].GetComponent<Joueur>().main[0].transform.rotation = c1.transform.rotation;
        this.joueurs[4].GetComponent<Joueur>().main[1].transform.rotation = c2.transform.rotation;
        flopper(); flopper(); flopper(); flopper(); flopper();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test()
    {
        generationPaquet();
    }

    public static List<string> generatedDeck()
    {
        List<string> newDeck = new List<string>();
        Couleur couleur = 0;
        Valeur valeur = 0;
        for(int i = 0; i <= 3; i++)
        {
            for(int j = 0; j <= 12; j++)
            {
                newDeck.Add((valeur + j) + " de " + (couleur + i));
            }
        }
        return newDeck;
    }
    public void shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
    public void generationPaquet()
    {
        List<string> deck = generatedDeck();
        shuffle(deck);
        float t = 0;
        GameObject posDeck = GameObject.Find("Deck");
        foreach(string s in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(posDeck.transform.position.x+t, posDeck.transform.position.y+t, posDeck.transform.position.z+t), Quaternion.identity);
            newCard.name = s;
            newCard.GetComponent<Carte>().isFaceUp = false;
            this.deck.Add(newCard);
            t += 0.005f;
            
        }
    }
    public void generationJoueurs()
    {
        this.joueurs = new List<GameObject>(this.nbJoueurs);
        for(int i = 1; i <= nbJoueurs; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefab);
            newPlayer.name = "Joueur_" + i;
            this.joueurs.Add(newPlayer);
        }
    }
    public void distribution()
    {
        System.Random random = new System.Random();
        int rdm;
        GameObject player;
        for(int i = 1; i <= this.nbJoueurs; i++)
        {
            player = GameObject.Find("Joueur_" + i);
            player.GetComponent<Joueur>().main = new List<GameObject>(2);
            for (int j = 1; j <= 2; j++)
            {
                rdm = random.Next(this.deck.Count);
                player.GetComponent<Joueur>().main.Add(this.deck[rdm]);
                this.deck.RemoveAt(rdm);
            }
        }
    }
    public void flopper()
    {
        System.Random random = new System.Random();
        int rdm = random.Next(this.deck.Count);
        GameObject carte = this.deck[rdm];
        this.deck.Remove(carte);
        this.flop.Add(carte);
        int i = this.flop.IndexOf(carte);
        GameObject pos = GameObject.Find("Tirage_" + (i + 1));
        carte.transform.position = pos.transform.position;
        carte.GetComponent<Carte>().isFaceUp = true;
    }
}
