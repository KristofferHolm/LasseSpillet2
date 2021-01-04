using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BilledeAssets", menuName = "ScriptableObjects/BilledeAssets", order = 1)]
public class BilledeGenerator : ScriptableObject
{
    public List<Sprite> H�r, Hoved, �jne, N�se, Mund, Krop;

    int imagesInCategories = 5;
    //krop,hoved,�jne,n�se,mund,h�r
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
                    listen.Insert(i, �jne[Random.Range(0, imagesInCategories)]);
                    break;
                case 3:
                    listen.Insert(i, N�se[Random.Range(0, imagesInCategories)]);
                    break;
                case 4:
                    listen.Insert(i, Mund[Random.Range(0, imagesInCategories)]);
                    break;
                case 5:
                    listen.Insert(i, H�r[Random.Range(0, imagesInCategories)]);
                    break;
                default:
                    break;
            }
        }
        return listen;
    }
    
}

