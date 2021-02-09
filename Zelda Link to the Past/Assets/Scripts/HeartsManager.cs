using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsManager : MonoBehaviour
{

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    public FloatValue heartsContainer;
    public FloatValue playerCurrentHealth;

    void Start()
    {
        CreateHearts();
    }

    public void CreateHearts(){
        for (int i = 0; i < heartsContainer.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts(){
        float health = playerCurrentHealth.runtimeValue / 2; //divided by 2 because of half hearts

        for (int i = 0; i < heartsContainer.initialValue; i++)
        {
            if(i <= health - 1){
                //Full heart
                hearts[i].sprite = fullHeart;
            }
            else if(i >= health)
            {
                //Empty heart
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                //Half heart
                hearts[i].sprite = halfHeart;
            }
        }


    }

}
