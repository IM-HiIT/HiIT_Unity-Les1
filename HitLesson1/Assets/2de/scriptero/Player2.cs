using TMPro;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    [SerializeField] Manager2 mane;
    [SerializeField] TMP_Text text;
    [SerializeField] private int sprong;
    [SerializeField] private  new AudioSource[] audio;

    private Rigidbody2D rd;
    private int punten = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        if (PlayerPrefs.GetInt("check") != 0) { SoundOff(); };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.timeScale != 0)
        {

            audio[0].Play();
            rd.velocity = new Vector2(0, sprong);

        }
        text.text = "Punten: " + punten;
        rd.rotation = (rd.velocity.y / 10) * 70;
    }

    private void SoundOff() {foreach (AudioSource source in audio) { source.volume = 0; }}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall")) { audio[2].Play(); mane.HitWall(punten);  }
        if (other.gameObject.CompareTag("Punt")) { punten++; audio[1].Play(); }
    }
}
