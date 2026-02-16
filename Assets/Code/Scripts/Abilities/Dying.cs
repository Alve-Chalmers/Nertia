using UnityEngine;
using UnityEngine.SceneManagement;

public class Dying : PlayerAbilityScript
{
    protected override PlayerAbilityType State => PlayerAbilityType.DYING;

    protected override void OnEnable()
    {
        base.OnEnable();
        Die();
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
