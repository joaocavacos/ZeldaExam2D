using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{
    private Gems gems;
    public Text gemText;

    void Start() {
        gems = FindObjectOfType<Gems>();
    }
    
    void Update(){
        gemText.text = gems.gems.ToString();  
    }


}
