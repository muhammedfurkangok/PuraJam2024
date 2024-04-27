using TMPro;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI _promptText;
   public void UpdateUI(string promtMessage)
   {
      _promptText.text = promtMessage;
   }
}
