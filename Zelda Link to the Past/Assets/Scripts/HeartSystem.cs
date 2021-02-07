using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HeartSystem
{
    public event EventHandler OnDamageTaken;
    public event EventHandler OnHeal;
    public event EventHandler OnDead;
    private List<Heart> heartList;

    public const int MAX_FRAGMENT_AMMOUNT = 2;

    public HeartSystem(int heartAmmount){

        heartList = new List<Heart>();

        for (int i = 0; i < heartAmmount; i++)
        {
            Heart heart = new Heart(2);
            heartList.Add(heart);
        }
    }

    public List<Heart> GetHeartList(){
        return heartList;
    }

    //Damage taken & decreasing hearts
    public void TakeDamage(int damageAmmount){

        //Cycle for all hearts
        for (int i = heartList.Count - 1; i >= 0; i--)
        {
            Heart heart = heartList[i];
            //Check if the heart can take all the damage
            if(damageAmmount > heart.GetFragmentAmmount()){
                //If it can't, damage the heart and go to the next one
                damageAmmount -= heart.GetFragmentAmmount();
                heart.TakeDamage(heart.GetFragmentAmmount());
            }
            else{
                //Can take all damage, then break cycle
                heart.TakeDamage(damageAmmount);
                break;
            }
        }

        if(OnDamageTaken != null){
            OnDamageTaken(this, EventArgs.Empty);
        }

        if(OnDead != null){
            OnDead(this, EventArgs.Empty);
        }
    }

    public void Heal(int healAmmount){
        for (int i = 0; i < heartList.Count; i++)
        {
            Heart heart = heartList[i];
            int missingFragments = MAX_FRAGMENT_AMMOUNT - heart.GetFragmentAmmount();

            if(healAmmount > missingFragments){
                healAmmount -= missingFragments;
                heart.Heal(missingFragments);
            }
            else{
                heart.Heal(healAmmount);
                break;
            }
        }

        if(OnHeal != null){
            OnHeal(this, EventArgs.Empty);
        }
    }

    public bool isDead(){
        return heartList[0].GetFragmentAmmount() == 0;
    }

    //Single heart
    public class Heart{

        private int fragments;

        public Heart(int fragments){
            this.fragments = fragments;
        }

        public int GetFragmentAmmount(){
            return fragments;
        }

        public void SetFragments(int fragments){
            this.fragments = fragments;
        }

        public void TakeDamage(int damageAmmount){
            //Check if damage is enough to kill or just deal damage
            if(damageAmmount >= fragments){
                fragments = 0;
            }
            else{
                fragments -= damageAmmount;
            }
        }

        public void Heal(int healAmmount){
            //Check if HP is maxed out or not
            if (fragments + healAmmount > MAX_FRAGMENT_AMMOUNT)
            {
                fragments = MAX_FRAGMENT_AMMOUNT;
            }else{
                fragments += healAmmount;
            }
        }

    }
}
