using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] SelectableCharacter ringSelector;

    bool itsSelected = false;

    private void Start()
    {
        UnitSelection.Instance.unitList.Add(this.gameObject.GetComponent<Unit>());
    }

    private void OnDestroy()
    {
        UnitSelection.Instance.unitList.Remove(this.gameObject.GetComponent<Unit>());
    }

    public void Selected()
    {
        ringSelector.TurnOnSelector();
        itsSelected = true;
       
    }

    public void Unselected()
    {
        ringSelector.TurnOffSelector();
        itsSelected = false;
        
    }

    public bool ItsSelected()
    {
        return itsSelected;
    }
}
