using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public List<Image> playerLivesImages;
    void Start()
    {

    }

    void Update()
    {

    }

    public void RemoveFarthestHeart()
    {
        Image farthestHeart = playerLivesImages[0];
        float highestX = -1;
        for (int i = 0; i < playerLivesImages.Count; i++)
        {
            if (playerLivesImages[i].transform.position.x > highestX) farthestHeart = playerLivesImages[i];
        }

        Destroy(farthestHeart.gameObject);
    }
}
