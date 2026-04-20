using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaTearTheRoom: CardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8m, ValueProp.Move), new DynamicVar("DexterityLoss", 1m)];
    protected override IEnumerable<IHoverTip> ExtraHoverTips => (HoverTipFactory.FromCardWithCardHoverTips<NanaTearTheRoom2>());

    public NanaTearTheRoom()
        : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
            .Targeting(cardPlay.Target)
            .WithHitFx("vfx/vfx_flying_slash", null, null)
            .Execute(choiceContext);
        await PowerCmd.Apply<DexterityPower>(cardPlay.Target, -base.DynamicVars["DexterityLoss"].BaseValue, base.Owner.Creature, this);


        CardModel card = base.CombatState.CreateCard<NanaTearTheRoom2>(base.Owner);
        if (base.IsUpgraded)
        {
             CardCmd.Upgrade(card);
        }
        await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, addedByPlayer: true);

    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["DexterityLoss"].UpgradeValueBy(1m);
    }
}
