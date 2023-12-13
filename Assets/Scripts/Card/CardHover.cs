using UnityEngine;

public class CardHover : MonoBehaviour
{
    private float scaleUptargetScale = 1.2f;
    private float scaleDowntargetScale = 1f;
    public void ScaleUp()
    {
        this.transform.localScale = new Vector3(scaleUptargetScale, scaleUptargetScale, this.transform.localScale.z);
    }

    public void ScaleDown()
    {
        this.transform.localScale = new Vector3(scaleDowntargetScale, scaleDowntargetScale, this.transform.localScale.z);
    }
}
