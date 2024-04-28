using Mirror;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers
{
   public class PlayerUIController : NetworkBehaviour
   {
      [SerializeField] private TextMeshProUGUI _promptText;

      public override void OnStartClient()
      {
         base.OnStartClient();

         if (!NetworkServer.activeHost)
         {
            _promptText.text = "";
            enabled = false;
         }
      }

      public void UpdateUI(string promtMessage)
      {
         _promptText.text = promtMessage;
      }
   }
}
