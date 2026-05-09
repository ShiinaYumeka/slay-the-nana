using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using SlayTheNANA.src.cardtags;
using System.Collections.Generic;
using System.Linq;

namespace SlayTheNANA;

public sealed class NanaBoneSpecialPower : PowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<NanaBoneSpecialPower>())];
    

    public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost)
    {
        modifiedCost = originalCost;
        if (card.Owner.Creature != base.Owner)
        {
            return false;
        }

        if (!card.Tags.Contains(CustomCardTag.Bone))
        {
            return false;
        }

        if (originalCost <= 0m)
        {
            return false;
        }

        modifiedCost = originalCost - (decimal)base.Amount;
        if (modifiedCost < 0m)
        {
            modifiedCost = default(decimal);
        }

        return true;
    }
}
