using BaseLib.Config;
using BaseLib.Utils.Patching;
using Godot;
using Godot.Bridge;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Ancients;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Characters;
using MegaCrit.Sts2.Core.Models.Events;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Saves.Managers;
using MegaCrit.Sts2.Core.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlayTheNANA;

[ModInitializer(nameof(Initialize))]
public static class SlayTheNANAInitializer
{
	//public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new("SlayTheNANA", MegaCrit.Sts2.Core.Logging.LogType.Generic);
	public static void Initialize()
	{
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaBoneCombo));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaBoneStorm));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaLooxAtk));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaLooxDef));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaHealerSword));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaStick));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaExecute));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaTrial));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaVine));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaOrangeBone));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaBlueBone));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaThreeTrial));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaButter));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaClaw));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaGasterAssist));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaRedTrident));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaMurder));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaPurpleSmoke));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaPurpleBone));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaBoneReturn));
        //ModHelper.AddModelToPool(typeof(IroncladCardPool), typeof(NanaFinalTrial));

        //ModHelper.AddModelToPool(typeof(IroncladRelicPool), typeof(NanaRedTridentRelic));
        //ModHelper.AddModelToPool(typeof(IroncladRelicPool), typeof(NanaJudgeRelic));
        //      ModHelper.AddModelToPool(typeof(IroncladRelicPool), typeof(NanaRelicTest));
        
        var harmony = new Harmony("SlayTheNANA");
		harmony.PatchAll();
		ScriptManagerBridge.LookupScriptsInAssembly(typeof(SlayTheNANAInitializer).Assembly);


		Log.Info("SlayTheNANA - 加载成功!!!");
	}
}


//[HarmonyPatch(typeof(ModelDb), nameof(ModelDb.AllCharacters), MethodType.Getter)]
//public static class ModelDbAllCharactersPatch
//{
//	static void Postfix(ref IEnumerable<CharacterModel> __result)
//	{
//		__result = __result
//			.Append(ModelDb.Character<NanaDummy>())
//			.Distinct();
//	}
//}
