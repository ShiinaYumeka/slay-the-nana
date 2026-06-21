using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Combat.History.Entries;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using SlayTheNANA.src.cardtags;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Entities.Creatures;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyCardPool))]
public sealed class NanaBoneReturn: NanaCardModel
{
	protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CustomCardTag.Bone };
	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5m, ValueProp.Move),new RepeatVar(2)];

	public NanaBoneReturn()
		: base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
	{
	}

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).WithHitCount(base.DynamicVars.Repeat.IntValue).FromCard(this)
			.Targeting(cardPlay.Target)
			.WithHitFx("vfx/vfx_attack_blunt", null, null)
			.Execute(choiceContext);
	}
	public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
	{
		if (player == base.Owner && CombatManager.Instance.History.CardPlaysFinished.Any((CardPlayFinishedEntry e) => e.HappenedLastPlayerTurn(base.Owner) && e.CardPlay.Card == this))
		{
			CardPile? pile = base.Pile;
			if (pile == null || pile.Type != PileType.Hand)
			{
				await CardPileCmd.Add(this, PileType.Hand);
			}
		}
	}
	

	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(2m);
	}
}
