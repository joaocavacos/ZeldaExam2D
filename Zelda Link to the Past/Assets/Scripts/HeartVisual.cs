using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartVisual : MonoBehaviour
{

    [SerializeField] private Sprite fullheartSprite;
    [SerializeField] private Sprite halfheartSprite;
    [SerializeField] private Sprite emptyheartSprite;

    private List<HeartImage> heartImageList;

    private HeartSystem heartSystem;


    void Awake() {
        heartImageList = new List<HeartImage>();
    }

    void Start() {
        HeartSystem heartSystem = new HeartSystem(3);
        SetHeartSystem(heartSystem);
        
    }

    public void SetHeartSystem(HeartSystem heartSystem){
        this.heartSystem = heartSystem;

        List<HeartSystem.Heart> heartList = heartSystem.GetHeartList();

        Vector2 heartAnchorPos = new Vector2(0,0);

        for (int i = 0; i < heartList.Count; i++)
        {
            HeartSystem.Heart heart = heartList[i];
            CreateHeartImage(heartAnchorPos).SetHeartFragments(heart.GetFragmentAmmount());
            heartAnchorPos += new Vector2(60,0);
        }
        heartSystem.OnDamageTaken += HeartSystemOnDamageTaken;
        heartSystem.OnHeal += HeartSystemOnHeal;
        heartSystem.OnDead += HeartSystemOnDead;
    }

    private void HeartSystemOnDamageTaken(object sender, System.EventArgs e){

        //Player was damaged
        RefreshAllHearts();
    }

    private void HeartSystemOnHeal(object sender, System.EventArgs e){
        //Player healed
        RefreshAllHearts();
    }

    private void HeartSystemOnDead(object sender, System.EventArgs e){
        //Player dead
        //Do stuff, game over
    }

    private void RefreshAllHearts(){
        List<HeartSystem.Heart> heartList = heartSystem.GetHeartList();

        for (int i = 0; i < heartImageList.Count; i++)
        {
            HeartImage heartImage = heartImageList[i];
            HeartSystem.Heart heart = heartList[i];
            heartImage.SetHeartFragments(heart.GetFragmentAmmount());
        }
    }

    private HeartImage CreateHeartImage(Vector2 anchorPos){
        //Create Game Object
        GameObject heartGO = new GameObject("Heart", typeof(Image));

        //Set child of transform
        heartGO.transform.parent = transform;
        heartGO.transform.localPosition = Vector3.zero;
        heartGO.transform.localScale = Vector3.one;

        //Locate and size of the heart
        heartGO.GetComponent<RectTransform>().anchoredPosition = anchorPos;
        heartGO.GetComponent<RectTransform>().sizeDelta = new Vector2(40,40);

        //Set heart sprite
        Image heartImageUI = heartGO.GetComponent<Image>();
        heartImageUI.sprite = fullheartSprite;

        HeartImage heartImage = new HeartImage(this, heartImageUI);
        heartImageList.Add(heartImage);

        return heartImage;
    }

    //Single heart
    public class HeartImage{

        private Image heartImage;
        private HeartVisual heartVisual;

        public HeartImage(HeartVisual heartVisual, Image heartImage){
            this.heartVisual = heartVisual;
            this.heartImage = heartImage;
        }

        //Setting heart fragments
        public void SetHeartFragments(int fragments){
            switch(fragments){
                case 0: heartImage.sprite = heartVisual.emptyheartSprite; break;
                case 1: heartImage.sprite = heartVisual.halfheartSprite; break;
                case 2: heartImage.sprite = heartVisual.fullheartSprite; break;
            }
        }

    }
}
