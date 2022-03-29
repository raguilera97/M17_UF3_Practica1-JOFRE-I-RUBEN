using UnityEngine;

public class SelectableCharacter : MonoBehaviour {

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
