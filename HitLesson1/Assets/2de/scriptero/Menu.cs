using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioMixer mix;
    [SerializeField] private Slider[] slid;
    [SerializeField] private GameObject pref;
    private bool GameOver;
    private bool flip;

    private void Start()
    {
        for (int i = 0; i < slid.Length; i++) 
        {
            if (!PlayerPrefs.HasKey(slid[i].name)) { PlayerPrefs.SetFloat(slid[i].name, 1); }
            slid[i].value = PlayerPrefs.GetFloat(slid[i].name); SetFloat(i); 
        }
    }
    public void SetFloat(int i)
    {
        mix.SetFloat(slid[i].name, Mathf.Log10(slid[i].value) * 20); // zorg dat de audio mixr volume exposed is met rechter muisknop en niet de pitch
        PlayerPrefs.SetFloat(slid[i].name, slid[i].value);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
        if (Input.GetKeyDown(KeyCode.Tab)){ menu(); }
    }
    public void menu()
    {
        if (!pref.activeInHierarchy)
        {
            if (Time.timeScale == 0) { pref.SetActive(true); GameOver = true; }
            else
            {
                Time.timeScale = 0; Cursor.visible = true; pref.SetActive(true); GameOver = false; ;
            }
        }
        else 
        {
            if (GameOver) { pref.SetActive(false); }
            else
            {
                pref.SetActive(false); Time.timeScale = 1; if (flip) { Cursor.visible = false; }
            }
        }
    }
    public void Switch()
    {
        SceneManager.LoadScene(1);
        flip = true;

    }

}
