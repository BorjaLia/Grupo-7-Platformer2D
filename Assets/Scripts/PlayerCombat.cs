using UnityEngine;
public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public void Attack()
    {
        animator.SetBool("IsAttacking", true);
    }

    public void StopAttacking()
    {
        animator.SetBool("IsAttacking", false);
    }
}
