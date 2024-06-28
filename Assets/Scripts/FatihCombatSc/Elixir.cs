using CagataySc;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Elixir : BaseAbility
{
    public Button elixirButton;
    public NewPlayer newPlayer;
    public int elixirRefillTurn;
    public int waitTurnCounter = 0;

    public int elixirAnimDur;

    private void Start()
    {
        elixirAnimDur = 2;
    }

    public override void Cast(Entity currentEntity, Entity targetEntity)
    {
        currentEntity.PlayAnimation(AnimatorHashes.IsUsingElixir);
        DOVirtual.DelayedCall(elixirAnimDur, () =>
        {
            currentEntity.HealEntity(20);
            currentEntity.PlayAnimation(AnimatorHashes.IsIdle);
            base.Cast(currentEntity, targetEntity);

        });


    }



}
