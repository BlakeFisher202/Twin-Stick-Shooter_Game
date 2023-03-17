using UnityEngine;

public class SessionDisplay : MonoBehaviour
{
    public void onSuccess(int sessionIndex)
    {
        var field = GetComponent<TMPro.TextMeshProUGUI>();
        field.text = $"Session#{sessionIndex}";
    }

    public void OnFail(string error)
    {
        var field = GetComponent<TMPro.TextMeshProUGUI>();
        field.text = $"Error: Not Connected";
    }
}
