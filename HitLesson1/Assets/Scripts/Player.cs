using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Schema;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpSpeed; // hoe hoog de sprong word;
    [SerializeField] private TMP_Text puntenText; //gewoon text shit;
    
    private Rigidbody2D player; // makkelijke manier om de player rigidbody te halen;
    private Manager manager;

    private int punten; // vrij logische

    private void Start()
    {
        player = GetComponent<Rigidbody2D>(); // haalt de rigidbody op van hey huidige gameobject
        manager = FindObjectOfType<Manager>(); // maakt een connectie met de manager
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // spreekt voor zich
        {
            player.velocity = new Vector2(0,jumpSpeed) ;  // Vector2.up == new Vector2(0 , 1) en die kracht sturen we omhoog en de zwaartekracht haalt die langzaam naar beneden;
        }
        puntenText.text = "punten:" + punten; // .text zodat ie het leest als een string en update de live score;
    }

    private void OnTriggerEnter2D(Collider2D col) // kijkt voor collision voor de palen of de ruimte ertussen
    {
        if (col.gameObject.CompareTag("Wall")) { manager.CheckHighScore(punten); } // gaat naar de manager om te kijken of de highscore word aangepast en returnd 0
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Punt")) { punten += 1; }        // dit is de ruimte ertussen dan komt er een punt bij;
    }
}
