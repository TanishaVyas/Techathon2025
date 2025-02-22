using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem particleSystem;
    public ParticleSystem blink;

    public List<Color> colors = new List<Color>();

    public int[] arr = new int[4];
    private int randomInt;

    public ScoreManager sc;

    void Start()
    {
       arr[0]=0;
       arr[1]=1;
       arr[2]=2;
       arr[3]=3;
        colors.Add(new Color(214f / 255f, 112f / 255f, 42f / 255f)); // RGB: (227, 142, 1)
        colors.Add(new Color(85f / 255f, 89f / 255f, 200f / 255f)); // RGB: (64, 183, 161)
        colors.Add(new Color(93f / 255f, 64f / 255f, 54f / 255f)); // RGB: (134, 113, 91)
        colors.Add(new Color(91f / 255f, 205f / 255f, 172f / 255f));
        randomInt = Random.Range(0, colors.Count);
        float slsize = Random.Range(0.2f, 0.5f);
        transform.localScale = new Vector3(slsize, slsize, 1);
        sc =
            GameObject
                .FindWithTag("GameController")
                .GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<scalePortal>().DecreaseScale();
            ParticleSystem.MainModule mainModule = particleSystem.main;
            mainModule.startColor = colors[randomInt];
            particleSystem.Play();
            blink.Play();
            other.gameObject.GetComponent<colourChangePortal>().portalMusic();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<scalePortal>().IncreaseScale();
            other
                .gameObject
                .GetComponent<colourChangePortal>()
                .randomColour(colors[randomInt]);
            other
                .gameObject
                .GetComponent<poweruphooser>().powerupchange(arr[randomInt]);
            sc.incrementScore(15,transform.position);
            Destroy (gameObject);
            blink.Stop();
        }
    }
}
