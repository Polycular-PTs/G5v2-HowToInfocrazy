using UnityEngine;

public class StufeVorlage
{
    public int timeST;
    public int attentionPerSecondST;
    public int minusWhenTimeIsUpST;
    public int oppositionWhenTimeIsUpST;

    public void InitiateStufeValues(int timeST, int attentionPerSecondST, int minusWhenTimeIsUpST, int oppositionWhenTimeIsUpST)
    {
        this.timeST = timeST;
        this.attentionPerSecondST = attentionPerSecondST;
        this.minusWhenTimeIsUpST = minusWhenTimeIsUpST;
        this.oppositionWhenTimeIsUpST = oppositionWhenTimeIsUpST;
    }
}
