using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Badges;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Godot.HttpRequest;
using static SlayTheNANA.NanaRelicTestPlus;

namespace SlayTheNANA;

public sealed class NanaRelicTestPlus : RelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
    {

        var field = cardSource?.GetType().GetField("IsNanaFcMove");
        bool isNanaFcMove = field != null && (bool)field.GetValue(cardSource);

        if ((dealer == base.Owner.Creature || dealer?.PetOwner == base.Owner) && !target.IsPlayer && isNanaFcMove != true)
        {
            (await PowerCmd.Apply<NanaFc>(dealer, result.UnblockedDamage, base.Owner.Creature, null))?.NanaFcGain();
        }
    }

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player == base.Owner)
        {
            CombatState combatState = player.Creature.CombatState;
            if (combatState.RoundNumber == 1)
            {
                Flash();
                (await PowerCmd.Apply<NanaFc>(base.Owner.Creature, 99, base.Owner.Creature, null))?.NanaFcGain();
            }
        }
    }
}
