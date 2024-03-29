using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ReactionData", order = 1)]
public class ReactionData : ScriptableObject
{
    public GameObject floatingText;
    public GameObject redParticle;
    public GameObject redHitEffect;
    public GameObject blueParticle;
    public GameObject blueHitEffect;
    public GameObject yellowParticle;
    public GameObject yellowHitEffect;
    public float intensity;
}
