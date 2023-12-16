using UnityEngine;

public class Flower : MonoBehaviour
{
    protected void TagByAction(Collider2D col, string tag1, string tag2, EffectType effect)
    {
        if (col.CompareTag(tag1))
        {
            ColorEffect.Instance.InvokeEffect(this.transform.position, effect);
            Destroy(this.gameObject);
        }
        if(tag2 != null)
        {
            if (col.CompareTag(tag2))
            {
                ColorEffect.Instance.InvokeEffect(this.transform.position, effect);
                Destroy(this.gameObject);
            }
        }
    }
}
