using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

using MegaCrit.Sts2.Core.Models.CardPools;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaStarQuiet : NanaCardModel
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(20m, ValueProp.Move),
        new DynamicVar("NanaFcCost", 48m),
        new DynamicVar("StrengthLoss", 12m)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<StrengthPower>())];

    protected override bool IsPlayable => base.Owner.Creature.GetPowerAmount<NanaFc>() >= base.DynamicVars["NanaFcCost"].BaseValue;
    protected override bool ShouldGlowGoldInternal => IsPlayable;

    public bool IsNanaFcMove = true;

    public NanaStarQuiet()
        : base(0, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PowerCmd.Apply<NanaFc>(choiceContext, base.Owner.Creature, -base.DynamicVars["NanaFcCost"].BaseValue, base.Owner.Creature, this);
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
            .TargetingAllOpponents(base.CombatState)
            .WithHitFx("vfx/vfx_attack_slash", null, null)
            .Execute(choiceContext);
        await PowerCmd.Apply<NanaStarQuietPower>(choiceContext, base.CombatState.HittableEnemies, base.DynamicVars["StrengthLoss"].BaseValue, base.Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        base.DynamicVars["StrengthLoss"].UpgradeValueBy(6m);
    }
}
