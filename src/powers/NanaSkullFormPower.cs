using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using SlayTheNANA.src.cardtags;
using System.Collections.Generic;
using System.Linq;

namespace SlayTheNANA;

public sealed class NanaSkullFormPower : PowerModel
{
	public override PowerType Type => PowerType.Buff;

	public override PowerStackType StackType => PowerStackType.Counter;

	protected override IEnumerable<IHoverTip> ExtraHoverTips =>
	[
		HoverTipFactory.Static(StaticHoverTip.ReplayStatic)
	];

	public override int ModifyCardPlayCount(CardModel card, Creature? target, int playCount)
	{
		if (card.Owner.Creature != base.Owner)
		{
			return playCount;
		}

		if (!card.Tags.Contains(CustomCardTag.Bone))
		{
			return playCount;
		}

		return playCount + (int)base.Amount;
	}
}
