using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BilledeAssets", menuName = "ScriptableObjects/BilledeAssets", order = 1)]
public class BilledeGenerator : ScriptableObject
{
    public List<Sprite> Hår, Hoved, Øjne, Næse, Mund, Krop;

    int imagesInCategories = 5;
    //krop,hoved,øjne,næse,mund,hår
    public List<Sprite> GetRandomBillede()
    {
        var listen = new List<Sprite>();
        for (int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    listen.Insert(i, Krop[Random.Range(0, imagesInCategories)]);
                    break;
                case 1:
                    listen.Insert(i, Hoved[Random.Range(0, imagesInCategories)]);
                    break;
                case 2:
                    listen.Insert(i, Øjne[Random.Range(0, imagesInCategories)]);
                    break;
                case 3:
                    listen.Insert(i, Næse[Random.Range(0, imagesInCategories)]);
                    break;
                case 4:
                    listen.Insert(i, Mund[Random.Range(0, imagesInCategories)]);
                    break;
                case 5:
                    listen.Insert(i, Hår[Random.Range(0, imagesInCategories)]);
                    break;
                default:
                    break;
            }
        }
        return listen;
    }
    
}

