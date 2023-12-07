using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    [SerializeField] private Item item; // de palen
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject map;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private float timer;
    [SerializeField] private string[] positions; // lijstje met namen om de highscores bij te houden

    private float _timer;
    private int counter; // regeld de hoeveelheid palen

    private void Update()
    {
        Tick();
        if (Input.GetKeyDown(KeyCode.Escape)) { ResetHighScores(); }
    }
    private void Tick() //startup functie om alle palen netjes op plaats te zetten
    {
        if (counter == 3) { return; }

        _timer -= Time.deltaTime;
        if (_timer < 0 ) // als de counter vol is hoeven er geen extra palen gemaakt te worden
        {
            int r = Random.Range(-3, 4);
            Item _item =Instantiate(item, new Vector2(11, r), Quaternion.identity);// Quaternion.identity is gewoon om te zeggen dat het object niet gedraaid is
            _item.transform.parent = map.transform;
            _timer = timer;
            counter++;
        }
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1.0f;
    }

    #region HighScores
    private void GetHighScores() // deze roep je in het begin op om alle scores te ordenen om het leven later makkelijker te maken
    {
        int[] lit = new int[positions.Length];
        for (int i = 0; i < positions.Length; i++)
        {
            lit[i] = PlayerPrefs.GetInt(positions[i]);
        }
        Array.Sort(lit, positions);
    }
    private void ResetHighScores() // zet alle scores op 0. en uiteindelijk alle nammen op null of iets
    {
        for (int i = 0; i < positions.Length; i++)
        {
            PlayerPrefs.SetInt(positions[i], 0);
        }
    }
    public void CheckHighScore(int punten) // kijkt of de meegegeven score hoger is dan de laagste
    {
        Time.timeScale = 0; 
        GetHighScores();
        if (PlayerPrefs.GetInt(positions[0]) < punten) // de 1ste in de lijst is altijd de laagste
        {
            PlayerPrefs.SetInt(positions[0], punten);
            for (int i = 1; i < positions.Length; i++) // en hier kijken we hoe ver de score omhoog moet in de rang orde
            {
                if (PlayerPrefs.GetInt(positions[i - 1]) > PlayerPrefs.GetInt(positions[i]))
                {
                    (positions[i - 1], positions[i]) = (positions[i], positions[i - 1]);
                }
                else {break;}
            }
        }
        PrintScores();  
        gameOver.SetActive(true);
    }

    private void PrintScores() // hier printen we de scores uiteindelijk ook voor UI maar nu is ie vooral voor debuggen
    {
        string lit = "GAME OVER\nHIGHSCORES:\n";
        for (int i = positions.Length; i > 0; i--) { lit += "Score:"+ PlayerPrefs.GetInt(positions[i - 1]) + "\n"; }
        gameOverText.text = lit;
    }
    #endregion
}
