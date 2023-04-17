using UnityEngine;

public class Gem : Collectables
{
    Animator animator;
    int collectionHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        collectionHash = Animator.StringToHash("Collect");
    }

    public override void Collect()
    {
        base.Collect();
        animator.SetTrigger(collectionHash);
    }
}