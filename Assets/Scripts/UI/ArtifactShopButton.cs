using UnityEngine;

public class ArtifactShopButton : MonoBehaviour
{
    public void BuyArtifact()
    {
        if (ArtifactShop.Instance == null)
            return;

        ArtifactShop.Instance.BuyCurrentArtifact();
    }
}