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
            for (int i = 0; i < loadingText.Length; i++)
            {
                float zPos = loadingText[i].transform.position.z;
                if (zPos > 205)
                {
                    loadingText[i].transform.Translate(0, 0, (-4) * Time.deltaTime, Space.World);
                }
            }
            for (int i = 0; i < songText.Length; i++)
            {
                float zPos = songText[i].transform.position.z;
                // Debug.Log("zPos for song text is: " + zPos);
                if (zPos <= 209)
                {
                    songText[i].transform.Translate(0, 0, 4 * Time.deltaTime, Space.World);
                }
            }
            for (int i = 0; i < artistText.Length; i++)
            {
                float zPos = artistText[i].transform.position.z;
                if (zPos <= 209)
                {
                    artistText[i].transform.Translate(0, 0, 4 * Time.deltaTime, Space.World);
                }
            }
        } else if (screen == 2)
        {
            for (int i = 0; i < loadingText.Length; i++)
            {
                float zPos = loadingText[i].transform.position.z;
                if (zPos <= 209)
                {
                    loadingText[i].transform.Translate(0, 0, 4 * Time.deltaTime, Space.World);
                }
            }
            for (int i = 0; i < songText.Length; i++)
            {
                float zPos = songText[i].transform.position.z;
                // Debug.Log("zPos for song text is: " + zPos);
                if (zPos > 205)
                {
                    songText[i].transform.Translate(0, 0, (-4) * Time.deltaTime, Space.World);
                }
            }
            for (int i = 0; i < artistText.Length; i++)
            {
                float zPos = artistText[i].transform.position.z;
                if (zPos <= 209)
                {
                    artistText[i].transform.Translate(0, 0, 4 * Time.deltaTime, Space.World);
                }
            }
        } else if (screen == 3)
        {
            for (int i = 0; i < loadingText.Length; i++)
            {
                float zPos = loadingText[i].transform.position.z;
                if (zPos <= 209)
                {
                    loadingText[i].transform.Translate(0, 0, 4 * Time.deltaTime, Space.World);
                }
            }
            for (int i = 0; i < songText.Length; i++)
            {
                float zPos = songText[i].transform.position.z;
                // Debug.Log("zPos for song text is: " + zPos);
                if (zPos <= 209)
                {
                    songText[i].transform.Translate(0, 0, 4 * Time.deltaTime, Space.World);
                }
            }
            for (int i = 0; i < artistText.Length; i++)
            {
                float zPos = artistText[i].transform.position.z;
                if (zPos > 205)
                {
                    artistText[i].transform.Translate(0, 0, (-4) * Time.deltaTime, Space.World);
                }
            }
        } else
        {
            screen = 1;
        }
    }
}
