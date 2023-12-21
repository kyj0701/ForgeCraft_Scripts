using System.Collections;
using UnityEngine;

public class Melee : Expedition
{
    private StateEvent stateChanged = new StateEvent();
    private Enums.ExpeditionState curState = Enums.ExpeditionState.Idle;
    public Enums.ExpeditionState CurState 
    { 
        get 
        { 
            return curState; 
        }
        set
        {
            stateChanged.Invoke(value);
            curState = value;
        }
    }
    

    private new void Awake()
    {
        base.Awake();

        stateChanged.AddListener(PlayStateAnimation);

        StartCoroutine(FSM());
    }

    private void PlayStateAnimation(Enums.ExpeditionState state)
    {
        _prefabs.PlayAnimation(state.ToString());
    }

    IEnumerator FSM()
    {
        while (true)
        {
            yield return StartCoroutine(curState.ToString());
        }
    }

    IEnumerator Idle()
    {
        yield return null;

        yield return CoroutineHelper.WaitForSeconds(1.0f);
        if (enemy != null) CurState = Enums.ExpeditionState.Run;
    }

    IEnumerator Run()
    {
        if (enemy.EnemyHealth <= 0)
        {
            CurState = Enums.ExpeditionState.Idle;
            StopAllCoroutines();
        }

        yield return null;

        if (Mathf.Abs(transform.position.x - enemy.transform.position.x) < status[4])
        {
            rigid.velocity = Vector2.zero;
            CurState = Enums.ExpeditionState.Attack;
        }
        else
        {
            rigid.velocity = new Vector2(transform.localScale.x * 2.0f, rigid.velocity.y);
        }

        yield return null;
    }

    IEnumerator Attack()
    {
        yield return null;

        SoundManager.Instance.SfxPlay(sFx);
        float damage = status[1] - enemy.EnemyDefence <= 1 ? 1 : status[1] - enemy.EnemyDefence;
        enemy.TakeDamage(damage);
        StackedDamage += damage;
        yield return CoroutineHelper.WaitForSeconds(status[2]);

        CurState = Enums.ExpeditionState.Run;
    }
}
