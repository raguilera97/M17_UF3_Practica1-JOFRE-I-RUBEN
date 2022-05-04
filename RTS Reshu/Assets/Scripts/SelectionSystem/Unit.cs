using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] SelectableObject ringSelector;

    private bool itsSelected = false;
    public bool civilOcupat = false;

    private void Start()
    {
        if (!this.gameObject.name.Contains("Enemy"))
        {
            Selections.Instance.unitList.Add(this.gameObject.GetComponent<Unit>());
        }
    }

    private void OnDestroy()
    {
        if (!this.gameObject.name.Contains("Enemy"))
        {
           Selections.Instance.unitList.Remove(this.gameObject.GetComponent<Unit>());
        }
       
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
