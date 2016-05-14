using UnityEngine;

namespace Assets.Scripts.World2Mechanics {
    public class Boss2Dialog : MonoBehaviour {

        public string onFirstHit;
        public string finalFormDialog;
        public string[] deathDialogs;
        public string martainDying;
        public DialogBox dialogBox;

        private bool hasBeenHitOnce = false;
        private bool hasSaidFinalFormCrap = false;
        private int deathCount = 0;

        public void TriggerHitDialog()
        {
            if (!hasBeenHitOnce)
            {
                dialogBox.DisplayText(onFirstHit);
                hasBeenHitOnce = true;
            }
        }
        public void TriggerFinalFormDialog()
        {
            if (!hasSaidFinalFormCrap)
            {
                dialogBox.DisplayText(finalFormDialog);
                hasSaidFinalFormCrap = true;
            }
        }
        public void TriggerKillingDialog()
        {
            if( deathCount < deathDialogs.Length && deathDialogs[deathCount] != "" )
            {
                if (dialogBox.isActiveAndEnabled)
                    dialogBox.AppendText(deathDialogs[deathCount]);
                else
                    dialogBox.DisplayText(deathDialogs[deathCount]);
            }
            deathCount++;
        }
        public void TriggerDyingDialog()
        {
            dialogBox.DisplayText(martainDying);
        }
    }
}
