using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colourPlanet : MonoBehaviour
{ 
    public List<Color> colors = new List<Color>();
    
    public SpriteRenderer body;

    // Start is called before the first frame update
    void Start()
    {
        List<Color> colors = new List<Color>();
        colors.Add(new Color(214f / 255f, 112f / 255f, 42f / 255f)); // RGB: (227, 142, 1)
        colors.Add(new Color(85f / 255f, 89f / 255f, 200f / 255f)); // RGB: (64, 183, 161)
        colors.Add(new Color(93f / 255f, 64f / 255f, 54f / 255f)); // RGB: (134, 113, 91)
                colors.Add(new Color(91f / 255f, 205f / 255f, 172f / 255f));

        int randomInt = Random.Range(0, colors.Count);
        body.color=colors[randomInt];
    }

}
