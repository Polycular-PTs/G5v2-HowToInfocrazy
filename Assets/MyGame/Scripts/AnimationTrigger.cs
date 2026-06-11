using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTrigger : MonoBehaviour
{
    private Animator anim;
    public string stateName;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play(stateName);
    }

}
