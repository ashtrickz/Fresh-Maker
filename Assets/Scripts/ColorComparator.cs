using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorComparator : MonoBehaviour
{
    public int level = 1;
    public int accuracyPercent;

    private float threshold = 0.1f;
    
    private ClientMovement client;
    
    [SerializeField] private Color[] lvlColor;

    public void CompareColors(Color mixedColor)
    {
        if(level <= lvlColor.Length)
            if(ColorsAreClose(lvlColor[level-1], mixedColor))
                    ChangeLevel();
    }
    
    private bool ColorsAreClose(Color levelColor, Color mixedColor)
    {
        Debug.Log($"Asked Color: {levelColor.r}, {levelColor.g}, {levelColor.b}\n" +
                  $"\t\t Your Color: {mixedColor.r}, {mixedColor.g}, {mixedColor.b}" );
        
        var rDist = Mathf.Abs(levelColor.r - mixedColor.r);
        var gDist = Mathf.Abs(levelColor.g - mixedColor.g);
        var bDist = Mathf.Abs(levelColor.b - mixedColor.b);

        accuracyPercent = Mathf.Abs((int) ((100 - ((rDist + gDist + bDist) * 100)) + .5f));
        FindObjectOfType<RoundPopUp>().ShowAccuracy(accuracyPercent);
        Debug.Log($"R Dist: {rDist}, G Dist: {gDist}, B Dist: {bDist}, together: {rDist + gDist + bDist}");
        Debug.Log($"Your accuracy is {accuracyPercent}%");
        if(rDist + gDist + bDist > threshold)
            return false;
        return true;
    }

    private void ChangeLevel()
    {
        if (level < lvlColor.Length)
        {
            level++;
        }
        client = FindObjectOfType<ClientMovement>();
        client.CustomerServed();
    }
    
}
