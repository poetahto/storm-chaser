using UnityEngine;

public class CanvasGroupNavigator : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] canvasGroups;

    private int _activeGroupIndex;

    private void Start()
    {
        Debug.Assert(canvasGroups.Length > 0);
        _activeGroupIndex = 0;
        UpdateGroupInteractivity();
    }

    public void SetActiveGroup(int groupIndex)
    {
        _activeGroupIndex = groupIndex;
        UpdateGroupInteractivity();
    }

    private void UpdateGroupInteractivity()
    {
        foreach (var group in canvasGroups)
        {
            group.blocksRaycasts = false;
            group.gameObject.SetActive(false);
        }

        var activeGroup = canvasGroups[_activeGroupIndex];
        activeGroup.blocksRaycasts = true;
        activeGroup.gameObject.SetActive(true);
    }
}