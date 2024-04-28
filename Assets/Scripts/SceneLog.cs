using TMPro;
using UnityEngine;

public class SceneLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public static SceneLog Instance;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void Log(string message)
    {
        text.text += message + "\n";
    }
}