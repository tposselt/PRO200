using System;
using TMPro;
using Unity.Loading;
using UnityEngine;

public class TextChange : MonoBehaviour
{
    public TextMeshPro[] loadingText = new TextMeshPro[2];
    public TextMeshPro[] songText = new TextMeshPro[6];
    public TextMeshPro[] artistText = new TextMeshPro[3];
    public int screen = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (screen == 1)
        {
            moveTextMeshes(loadingText, "back");
            moveTextMeshes(songText, "forward");
            moveTextMeshes(artistText, "forward");
        }
        else if (screen == 2)
        {
            moveTextMeshes(loadingText, "forward");
            moveTextMeshes(songText, "back");
            moveTextMeshes(artistText, "forward");
        }
        else if (screen == 3)
        {
            moveTextMeshes(loadingText, "forward");
            moveTextMeshes(songText, "forward");
            moveTextMeshes(artistText, "back");
        }
        else
        {
            screen = 1;
        }
    }

    public void clickedArtist()
    {
        screen = 3;
    }
    
    private void moveTextMeshes(TextMeshPro[] textArray, string direction)
    {
        for (int i = 0; i < textArray.Length; i++)
        {
            float zPos = textArray[i].transform.position.z;
            if (direction.Equals("back") && zPos > 205)
            {
                textArray[i].transform.Translate(0, 0, (-4) * Time.deltaTime, Space.World);
            } else if (direction.Equals("forward") && zPos <= 209)
            {
                textArray[i].transform.Translate(0, 0, 4 * Time.deltaTime, Space.World);
            }
        }
    }
}
