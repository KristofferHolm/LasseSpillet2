using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InsertImage : MonoBehaviour
{
    public List<Image> ImagesInPicture;
    public BilledeGenerator BilledeGenerator;
    [Button]
    void GenerateImage()
    {
        var Images = BilledeGenerator.GetRandomBillede();
        int i = 0;
        foreach (var img in ImagesInPicture)
        {
            img.sprite = Images[i];
            i++;
        }
    }
    public void InsertPicture(List<Sprite> list)
    {
        int i = 0;
        foreach (var img in ImagesInPicture)
        {
            img.sprite = list[i];
            i++;
        }
    }


}
