using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] SelectableObject ringSelector;

    private bool itsSelected = false;

    private void Start()
    {
        Selections.Instance.unitList.Add(this.gameObject.GetComponent<Unit>());
    }

    private void OnDestroy()
    {
        Selections.Instance.unitList.Remove(this.gameObject.GetComponent<Unit>());
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
