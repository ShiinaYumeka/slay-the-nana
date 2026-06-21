using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;

namespace SlayTheNANA;

[Pool(typeof(NanaDummyRelicPool))]
public sealed class NanaRelicTestPlus : CustomRelicModel
{
	public override RelicRarity Rarity => RelicRarity.Starter;

	public override string PackedIconPath => "res://images/relics/nana_relic_test.png";

	protected override string PackedIconOutlinePath => "res://images/relics/nana_relic_test.png";

	protected override string BigIconPath => "res://images/relics/nana_relic_test.png";

	public override async Task AfterDamageGiven(PlayerChoiceContext choiceContext, Creature? dealer, DamageResult result, ValueProp props, Creature target, CardModel? cardSource)
	{

		var field = cardSource?.GetType().GetField("IsNanaFcMove");
		bool isNanaFcMove = field != null && (bool)field.GetValue(cardSource);

		if ((dealer == base.Owner.Creature || dealer?.PetOwner == base.Owner) && !target.IsPlayer && isNanaFcMove != true)
		{
			(await PowerCmd.Apply<NanaFc>(choiceContext, dealer, result.UnblockedDamage, base.Owner.Creature, null))?.NanaFcGain();
		}
	}

	public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
	{
		if (player == base.Owner)
		{
			ICombatState combatState = player.Creature.CombatState;
			if (combatState.RoundNumber == 1)
			{
				Flash();
				(await PowerCmd.Apply<NanaFc>(choiceContext, base.Owner.Creature, 99, base.Owner.Creature, null))?.NanaFcGain();
			}
		}
	}
}
