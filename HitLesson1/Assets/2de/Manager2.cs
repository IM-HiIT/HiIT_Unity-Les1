using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager2 : MonoBehaviour
{
    [SerializeField] private Walls2 wals;
    private float Timer;
    private float _Timer;
    [SerializeField] private int count;
    private int aantal;
    [SerializeField] private string[] names;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject panel;

    private void Start()
    {
        Timer = wals.GetTimer() / count;
    }
    void Update()
    { 
        _Timer -= Time.deltaTime;
        if (count != aantal && _Timer <= 0) { Place(); }
        if(Input.GetKeyDown(KeyCode.Escape)) { ResetHighScores(); }
        if (Input.GetKeyDown(KeyCode.RightAlt) && panel.activeInHierarchy) { ResetScene(); }
    }
    private void Place()
    {
        int randomNumber = UnityEngine.Random.Range(-3, 4);
        aantal++;
        Walls2 wall = Instantiate(wals, new Vector2(10, randomNumber), Quaternion.identity);
        wall.transform.SetParent(this.transform, true);
        _Timer = Timer;
    }
    public void HitWall(int punten)
    {
        Time.timeScale = 0;
        SortArray();
        if (punten > PlayerPrefs.GetInt(names[0])) // dit allemaal goed kunnen uitleggen. is nog een puntje vooral de playerprefs principe
        {
            PlayerPrefs.SetInt(names[0], punten);
            for (int i = 1; i< names.Length; i++) // en hier starten op 1 en dan rekenen met - 1 zodat je nooit buiten je array komt
            {
                if (PlayerPrefs.GetInt(names[i - 1]) > PlayerPrefs.GetInt(names[i]))
                {
                    (names[i - 1], names[i]) = (names[i], names[i - 1]);
                }
                else { break; } // de break is overkill maar wel een goed stukje logica
            }
        } 
        PrintScores();
        panel.SetActive(true);
    }
    private void SortArray()
    {
        int[] temp = new int[names.Length];
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = PlayerPrefs.GetInt(names[i]);
        }
        Array.Sort(temp, names);
    }
    private void ResetHighScores()
    {
        for (int i = 0;i < names.Length;i++)
        {
            PlayerPrefs.SetInt(names[i], 0);
        }
    }
    public void ResetScene()
    {
        SceneManager.LoadScene(sceneBuildIndex:1);
        Time.timeScale = 1;
    }
    private void PrintScores()
    {
        string lit = "GameOver\nHighscores:";
        for (int i = names.Length - 1 ; i > -1; i--)
        {
            lit += "\n" + (names.Length - i) + ": " + PlayerPrefs.GetInt(names[i]);
        }
        text.text = lit;
    }
}