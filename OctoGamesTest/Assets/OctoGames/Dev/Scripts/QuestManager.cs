using Naninovel;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _questName;
    [SerializeField] private TextMeshProUGUI _questDescription;
    [SerializeField] private GameObject questIcon;
    
    public void SetQuest()
    {
        var varManager = Engine.GetService<ICustomVariableManager>();
        var questName = varManager.GetVariableValue("questName");
        var questDesc = varManager.GetVariableValue("questDesc");
        _questName.text = questName;
        _questDescription.text = questDesc;
        if (questName == "finish") questIcon.SetActive(false);
    }
}
