using UnityEngine;

public abstract class ReceiveEffect : MonoBehaviour
{
    public abstract void ReceiveRedEffect();
    public abstract void ReceiveBlueEffect(Vector3 direction);
    public abstract void ReceiveYellowEffect();
    public abstract void ReceiveOrangeEffect();
    public abstract void ReceivePurpleEffect(Vector3 direction);
    public abstract void ReceiveGreenEffect();
}
