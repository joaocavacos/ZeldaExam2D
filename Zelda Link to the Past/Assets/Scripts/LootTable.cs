using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loot{
    public Collectibles loot;
    public int lootChance;
}

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public Collectibles LootCollectible(){

        int probability = 0;
        int currentProbability = Random.Range(0,100);

        for (int i = 0; i < loots.Length; i++)
        {
            probability += loots[i].lootChance;
            if(currentProbability <= probability){
                return loots[i].loot; //Drop loot
            }
        }

        return null; //Drop nothing
    }


}
