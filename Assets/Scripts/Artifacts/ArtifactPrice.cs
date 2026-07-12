using UnityEngine;

[System.Serializable]
public class ArtifactPrice
{
    public int Wheat;
    public int Wood;
    public int Iron;

    public ArtifactPrice(int wheat, int wood, int iron)
    {
        Wheat = wheat;
        Wood = wood;
        Iron = iron;
    }

    public void Multiply(float multiplier)
    {
        Wheat = Mathf.RoundToInt(Wheat * multiplier);
        Wood = Mathf.RoundToInt(Wood * multiplier);
        Iron = Mathf.RoundToInt(Iron * multiplier);
    }
}