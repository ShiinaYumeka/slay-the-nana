using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Timeline.Epochs;
using MegaCrit.Sts2.Core.Unlocks;
using SlayTheNANA;
using System.Collections.Generic;
using System.Linq;

namespace MegaCrit.Sts2.Core.Models.CardPools;

public sealed class NanaDummyCardPool : CardPoolModel
{
	public override string Title => "nana_dummy";

	public override string EnergyColorName => "nana_dummy";

	public override string CardFrameMaterialPath => "card_frame_nana_dummy";

	public override Color DeckEntryCardColor => new Color("E6D000");

	public override Color EnergyOutlineColor => new Color("804020");

	public override bool IsColorless => false;

	protected override CardModel[] GenerateAllCards()
	{
		return new CardModel[87]
        {
            ModelDb.Card<NanaStrikeDummy>(),
            ModelDb.Card<NanaDefendDummy>(),
            ModelDb.Card<NanaBoneCombo>(),
			ModelDb.Card<NanaBoneStorm>(),
			ModelDb.Card<NanaLooxAtk>(),
			ModelDb.Card<NanaLooxDef>(),
			ModelDb.Card<NanaHealerSword>(),
			ModelDb.Card<NanaStick>(),
			ModelDb.Card<NanaExecute>(),
			ModelDb.Card<NanaBladeSlash>(),
			ModelDb.Card<NanaTrial>(),
            ModelDb.Card<NanaVerdict>(),
			ModelDb.Card<NanaFairTrial>(),
			ModelDb.Card<NanaVine>(),
			ModelDb.Card<NanaOrangeBone>(),
			ModelDb.Card<NanaBlueBone>(),
			ModelDb.Card<NanaBoneSpecial>(),
			ModelDb.Card<NanaFiveBoneEqual>(),
			ModelDb.Card<NanaWhiteBone>(),
			ModelDb.Card<NanaSkullForm>(),
			ModelDb.Card<NanaThreeTrial>(),
			ModelDb.Card<NanaButter>(),
			ModelDb.Card<NanaClaw>(),
			ModelDb.Card<NanaGasterAssist>(),
			ModelDb.Card<NanaRedTrident>(),
			ModelDb.Card<NanaMurder>(),
			ModelDb.Card<NanaMercyWorld>(),
			ModelDb.Card<NanaPurpleSmoke>(),
			ModelDb.Card<NanaPurpleBone>(),
			ModelDb.Card<NanaBoneReturn>(),
			ModelDb.Card<NanaFinalTrial>(),
			ModelDb.Card<NanaJudicator>(),
            ModelDb.Card<NanaBloodPool>(),
            ModelDb.Card<NanaInsanity>(),
            ModelDb.Card<NanaShiftTheBlame>(),
            ModelDb.Card<NanaBleeding>(),
            ModelDb.Card<NanaKnifeWithBlood>(),
            ModelDb.Card<NanaRealKnife>(),
            ModelDb.Card<NanaSpreadSin>(),
            ModelDb.Card<NanaDoubleSin>(),
            ModelDb.Card<NanaCruel>(),
            ModelDb.Card<NanaBoneSpike>(),
            ModelDb.Card<NanaFatalBoneSpike>(),
            ModelDb.Card<NanaPf666>(),
            ModelDb.Card<NanaStringSound>(),
            ModelDb.Card<NanaTearTheRoom>(),
            ModelDb.Card<NanaTearTheRoom2>(),
            ModelDb.Card<NanaGravity>(),
            ModelDb.Card<NanaWhirlwind>(),
            ModelDb.Card<NanaRedBone>(),
            ModelDb.Card<NanaSleep>(),
            ModelDb.Card<NanaWaterDispenser>(),
            ModelDb.Card<NanaTrolly>(),
            ModelDb.Card<NanaMotionShield>(),
            ModelDb.Card<NanaDoor>(),
            ModelDb.Card<NanaRun>(),
            ModelDb.Card<NanaXiyan>(),
            ModelDb.Card<NanaSlip>(),
            ModelDb.Card<NanaGb>(),
            ModelDb.Card<NanaHeadWind>(),
            ModelDb.Card<NanaTailWind>(),
            ModelDb.Card<NanaRecover>(),
            ModelDb.Card<NanaRecoverDark>(),
            ModelDb.Card<NanaHakurou>(),
            ModelDb.Card<NanaRoukan>(),
            ModelDb.Card<NanaReality>(),
            ModelDb.Card<NanaVoidVirus>(),
            ModelDb.Card<NanaForteFc>(),
            ModelDb.Card<NanaTaj>(),
            ModelDb.Card<NanaTurret>(),
            ModelDb.Card<NanaShadeSoul>(),
            ModelDb.Card<NanaElemental>(),
            ModelDb.Card<NanaStarQuiet>(),
            ModelDb.Card<NanaSacrificeRam>(),
            ModelDb.Card<NanaBoneWall>(),
            ModelDb.Card<NanaFireShield>(),
            ModelDb.Card<NanaIceShield>(),
            ModelDb.Card<NanaChocolate>(),
            ModelDb.Card<NanaTricolorBone>(),
            ModelDb.Card<NanaFlandre>(),
            ModelDb.Card<NanaPocketWatch>(),
            ModelDb.Card<NanaMurderer>(),
            ModelDb.Card<NanaFloweyTurret>(),
            ModelDb.Card<NanaWhiteCard>(),
            ModelDb.Card<NanaBin>(),
            ModelDb.Card<NanaBlockRiposte>(),
            ModelDb.Card<NanaGrassBody>()
        };
	}
}


public class TestCardPool : CustomCardPoolModel
{
    // 卡池的ID。必须唯一防撞车。
    public override string Title => "test";

    // 描述中使用的能量图标。大小为24x24。
    public override string? TextEnergyIconPath => "res://test/images/energy_test.png";
    // tooltip和卡牌左上角的能量图标。大小为74x74。
    public override string? BigEnergyIconPath => "res://test/images/energy_test_big.png";

    // 卡池的主题色。
    public override Color DeckEntryCardColor => new(0.5f, 0.5f, 1f);

    // 卡池是否是无色。例如事件、状态等卡池就是无色的。
    public override bool IsColorless => false;
}