using UnityEngine;

public class SelectableObject : MonoBehaviour {

    public SpriteRenderer selectImage;

    private void Awake() {
        selectImage.enabled = false;
    }

    public void TurnOffSelector()
    {
        selectImage.enabled = false;
    }

    
    public void TurnOnSelector()
    {
        selectImage.enabled = true;
    }

}
