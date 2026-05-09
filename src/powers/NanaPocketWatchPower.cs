using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;

namespace SlayTheNANA;

public sealed class NanaPocketWatchPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource)
    {
        if (target != base.Owner || dealer == null || dealer == base.Owner)
        {
            return;
        }

        if (!result.WasFullyBlocked)
        {
            return;
        }

        if (!dealer.IsAlive)
        {
            return;
        }

        decimal remainingBlock = target.Block;
        if (remainingBlock <= 0m)
        {
            return;
        }

        Flash();
        await CreatureCmd.Damage(choiceContext, dealer, remainingBlock, ValueProp.Unpowered, base.Owner, null);
    }
}
