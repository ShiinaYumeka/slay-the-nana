using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SlayTheNANA;

public sealed class NanaSpreadSin : CardModel
{
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaKarma>())];

    public NanaSpreadSin()
        : base(0, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.AttackAnimDelay);
        if (cardPlay.Target.HasPower<NanaKarma>())
        {
            int num = cardPlay.Target.GetPowerAmount<NanaKarma>();
            //await PowerCmd.Remove<NanaKarma>(cardPlay.Target);
            await PowerCmd.Apply<NanaKarma>(base.CombatState.HittableEnemies.Except([(cardPlay.Target)]), num, base.Owner.Creature, this);


        }
    }

    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Ethereal);
    }
}
