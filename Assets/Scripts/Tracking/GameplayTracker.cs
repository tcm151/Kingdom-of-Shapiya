using System;
using System.Collections.Generic;

using UnityEngine;

using KOS.Events;
using KOS.Towers;
using KOS.Enemies;
using KOS.Weapons;
using KOS.PowerUps;



namespace KOS.GameplayTracking
{
    public class GameplayTracker : MonoBehaviour
    {
        // string prefilledURL = "https://docs.google.com/forms/d/e/1FAIpQLSes4_q8OwqSXL2QfXo5d0Zy2fRKynGuE5Ad8YVxtl3MEg-uAg/viewform?usp=pp_url&entry.741934920=1&entry.106197262=1&entry.989228177=1&entry.1781616229=Mouse&entry.1140007643=Windows&entry.758856800=2&entry.1630605801=2&entry.1439835769=3&entry.1337333297=3&entry.88582696=4&entry.192804890=4&entry.1592702735=4&entry.571234322=4&entry.571234322=5&entry.571234322=10&entry.571234322=16&entry.571234322=17&entry.571234322=20&entry.1992495222=4&entry.120190985=false&entry.1595212111=5&entry.1224794726=5&entry.2002705658=5&entry.1220219829=5&entry.855024020=false&entry.641382749=5&entry.2045646543=5&entry.267036226=5&entry.671796067=5&entry.199549965=false&entry.1283431646=5&entry.575686986=5&entry.1714625301=5&entry.923128155=5&entry.395076526=false&entry.1969443358=5&entry.1388835950=5&entry.372146356=5&entry.546807697=5&entry.580607006=false&entry.740541289=5&entry.1867747759=5&entry.1869482550=5&entry.1240436989=5&entry.1470950620=6&entry.1180146609=6&entry.479310654=6&entry.756791521=6&entry.1141801293=6&entry.385661381=6&entry.404954347=6&entry.138192943=6&entry.139191807=6&entry.667115333=6&entry.1406439589=6&entry.594703479=6&entry.1010809033=6&entry.633949101=6&entry.556440483=6&entry.1943387555=6&entry.1872687163=6&entry.1367676721=6&entry.120844856=6&entry.981002923=6&entry.1432084578=6&entry.221574755=6&entry.1558874751=6&entry.1692756013=6&entry.1370398887=6&entry.1582800243=6&entry.544169022=6&entry.1948759202=6&entry.1301459605=6&entry.1745141659=6&entry.911698859=6&entry.2062854058=7&entry.1821957716=7&entry.371545048=7&entry.861490628=7&entry.723589970=true&entry.96551459=true&entry.205296721=true&entry.1868302386=false&entry.597320932=false&entry.573652257=8&entry.868190254=true&entry.25970505=8&entry.1653877049=false&entry.482580490=false&entry.924253734=false&entry.915959978=false&entry.1409991674=false";
        private string userStudyURL = "https://docs.google.com/forms/d/e/1FAIpQLSes4_q8OwqSXL2QfXo5d0Zy2fRKynGuE5Ad8YVxtl3MEg-uAg/formResponse";

        private string ageID = "entry.741934920";
        private string FPSEXPID = "entry.106197262";
        private string TDEXPID = "entry.989228177";
        private string inputMethodID = "entry.1781616229";
        private string OSID = "entry.1140007643";

        private string timesJumpedID = "entry.758856800";
        private string timeSpentSprintingID = "entry.1630605801";

        private string hexesAmountEarnedID = "entry.1439835769";
        private string hexesAmountSpentID = "entry.1337333297";

        private string castleDamageTakenID = "entry.88582696";
        private string wavesCompletedID = "entry.192804890";
        private string timesForcedNextWaveID = "entry.1592702735";
        private string wavesTakenDamageID = "entry.571234322";
        private string waveFailedID = "entry.1992495222";

        private string pistolPurchasedID = "entry.120190985";
        private string pistolTimeUsedID = "entry.1595212111";
        private string pistolTimesFiredID = "entry.1224794726";
        private string pistolKillsID = "entry.2002705658";
        private string pistolTimesUpgradedID = "entry.1220219829";
        private string shotgunPurchasedID = "entry.855024020";
        private string shotgunTimeUsedID = "entry.641382749";
        private string shotgunTimesFiredID = "entry.2045646543";
        private string shotgunKillsID = "entry.267036226";
        private string shotgunTimesUpgradedID = "entry.671796067";
        private string revolverPurchasedID = "entry.199549965";
        private string revolverTimeUsedID = "entry.1283431646";
        private string revolverTimesFiredID = "entry.575686986";
        private string revolverKillsID = "entry.1714625301";
        private string revolverTimesUpgradedID = "entry.923128155";
        private string riflePurchasedID = "entry.395076526";
        private string rifleTimeUsedID = "entry.1969443358";
        private string rifleTimesFiredID = "entry.1388835950";
        private string rifleKillsID = "entry.372146356";
        private string rifleTimesUpgradedID = "entry.546807697";
        private string rocketLauncherPurchasedID = "entry.580607006";
        private string rocketLauncherTimeUsedID = "entry.740541289";
        private string rocketLauncherTimesFiredID = "entry.1867747759";
        private string rocketLauncherKillsID = "entry.1869482550";
        private string rocketLauncherTimesUpgradedID = "entry.1240436989";

        private string totalTowersBuiltID = "entry.1470950620";
        private string totalTowersRefundedID = "entry.1180146609";
        private string basicIamountBuiltID = "entry.479310654";
        private string basicIamountRefundedID = "entry.756791521";
        private string basicIkillsID = "entry.1141801293";
        private string basicItimesUpgradedID = "entry.385661381";
        private string basicIIamountBuiltID = "entry.404954347";
        private string basicIIamountRefundedID = "entry.138192943";
        private string basicIIkillsID = "entry.139191807";
        private string basicIItimesUpgradedID = "entry.667115333";
        private string heavyIamountBuiltID = "entry.1406439589";
        private string heavyIamountRefundedID = "entry.594703479";
        private string heavyIkillsID = "entry.1010809033";
        private string heavyItimesUpgradedID = "entry.633949101";
        private string heavyIIamountBuiltID = "entry.556440483";
        private string heavyIIamountRefundedID = "entry.1943387555";
        private string heavyIIkillsID = "entry.1872687163";
        private string heavyIItimesUpgradedID = "entry.1367676721";
        private string mortarIamountBuiltID = "entry.120844856";
        private string mortarIamountRefundedID = "entry.981002923";
        private string mortarIamountRefundedID2 = "entry.1432084578";
        private string mortarIkillsID = "entry.221574755";
        private string mortarItimesUpgradedID = "entry.1558874751";
        private string antiAirIamountBuiltID = "entry.1692756013";
        private string antiAirIamountRefundedID = "entry.1370398887";
        private string antiAirIkillsID = "entry.1582800243";
        private string antiAirItimesUpgradedID = "entry.544169022";
        private string slowIamountBuiltID = "entry.1948759202";
        private string slowIamountRefundedID = "entry.1301459605";
        private string slowIkillsID = "entry.1745141659";
        private string slowItimesUpgradedID = "entry.911698859";

        private string healthPowerupsCollectedID = "entry.2062854058";
        private string speedPowerupsCollectedID = "entry.1821957716";
        private string damagePowerupsCollectedID = "entry.371545048";
        private string currencyPowerupsCollectedID = "entry.861490628";

        private string openedPauseMenuID = "entry.723589970";
        private string openedSettingMenuID = "entry.96551459";
        private string openedControlsScreenID = "entry.205296721";
        private string openedGameplayScreenID = "entry.1868302386";
        private string openedBugReportScreenID = "entry.597320932";
        private string bugReportsSentID = "entry.573652257";
        private string openedTowerOptionsMenuID = "entry.868190254";
        private string timesOpenedTowerOptionsMenuID = "entry.25970505";
        private string quitToMainMenuID = "entry.1653877049";

        private string changedFOVID = "entry.482580490";
        private string changedVolumeID = "entry.924253734";
        private string changedDifficultyID = "entry.915959978";
        private string changedMouseSensitivityID = "entry.1409991674";

        private Dictionary<string, string> statsDict = new Dictionary<string, string>();
        public void BindStatsToDictionary()
        {
            statsDict.Add(ageID, stats.Info.age);
            statsDict.Add(FPSEXPID, stats.Info.FPSEXP.ToString());
            statsDict.Add(TDEXPID, stats.Info.TDEXP.ToString());
            statsDict.Add(inputMethodID, stats.Info.inputMethod);
            statsDict.Add(OSID, stats.Info.OS);

            statsDict.Add(timesJumpedID, stats.Player.timesJumped.ToString());
            stats.Player.timeSpentSprinting = Mathf.RoundToInt(stats.Player.timeSpentSprinting);
            statsDict.Add(timeSpentSprintingID, stats.Player.timeSpentSprinting.ToString());

            statsDict.Add(hexesAmountEarnedID, stats.Hexes.amountEarned.ToString());
            statsDict.Add(hexesAmountSpentID, stats.Hexes.amountSpent.ToString());

            statsDict.Add(castleDamageTakenID, stats.Waves.damageTaken.ToString());
            statsDict.Add(wavesCompletedID, stats.Waves.wavesCompleted.ToString());
            statsDict.Add(timesForcedNextWaveID, stats.Waves.timesForcedNextWave.ToString());
            // foreach (var wave in stats.Waves.wavesTakenDamage) statsDict.Add(wavesTakenDamageID, wave.ToString());
            statsDict.Add(wavesTakenDamageID, stats.Waves.wavesTakenDamage.ToString());
            statsDict.Add(waveFailedID, stats.Waves.waveFailed.ToString());

            statsDict.Add(pistolPurchasedID, stats.Weapons.Pistol.purchased.ToString());
            stats.Weapons.Pistol.timeUsed = Mathf.RoundToInt(stats.Weapons.Pistol.timeUsed);
            statsDict.Add(pistolTimeUsedID, stats.Weapons.Pistol.timeUsed.ToString());
            statsDict.Add(pistolTimesFiredID, stats.Weapons.Pistol.timesFired.ToString());
            statsDict.Add(pistolKillsID, stats.Weapons.Pistol.kills.ToString());
            statsDict.Add(pistolTimesUpgradedID, stats.Weapons.Pistol.timesUpgraded.ToString());

            statsDict.Add(shotgunPurchasedID, stats.Weapons.Shotgun.purchased.ToString());
            stats.Weapons.Shotgun.timeUsed = Mathf.RoundToInt(stats.Weapons.Shotgun.timeUsed);
            statsDict.Add(shotgunTimeUsedID, stats.Weapons.Shotgun.timeUsed.ToString());
            statsDict.Add(shotgunTimesFiredID, stats.Weapons.Shotgun.timesFired.ToString());
            statsDict.Add(shotgunKillsID, stats.Weapons.Shotgun.kills.ToString());
            statsDict.Add(shotgunTimesUpgradedID, stats.Weapons.Shotgun.timesUpgraded.ToString());
            
            statsDict.Add(revolverPurchasedID, stats.Weapons.Revolver.purchased.ToString());
            stats.Weapons.Revolver.timeUsed = Mathf.RoundToInt(stats.Weapons.Revolver.timeUsed);
            statsDict.Add(revolverTimeUsedID, stats.Weapons.Revolver.timeUsed.ToString());
            statsDict.Add(revolverTimesFiredID, stats.Weapons.Revolver.timesFired.ToString());
            statsDict.Add(revolverKillsID, stats.Weapons.Revolver.kills.ToString());
            statsDict.Add(revolverTimesUpgradedID, stats.Weapons.Revolver.timesUpgraded.ToString());
            
            statsDict.Add(riflePurchasedID, stats.Weapons.Rifle.purchased.ToString());
            stats.Weapons.Rifle.timeUsed = Mathf.RoundToInt(stats.Weapons.Rifle.timeUsed);
            statsDict.Add(rifleTimeUsedID, stats.Weapons.Rifle.timeUsed.ToString());
            statsDict.Add(rifleTimesFiredID, stats.Weapons.Rifle.timesFired.ToString());
            statsDict.Add(rifleKillsID, stats.Weapons.Rifle.kills.ToString());
            statsDict.Add(rifleTimesUpgradedID, stats.Weapons.Rifle.timesUpgraded.ToString());
            
            statsDict.Add(rocketLauncherPurchasedID, stats.Weapons.RocketLauncher.purchased.ToString());
            stats.Weapons.RocketLauncher.timeUsed = Mathf.RoundToInt(stats.Weapons.RocketLauncher.timeUsed);
            statsDict.Add(rocketLauncherTimeUsedID, stats.Weapons.RocketLauncher.timeUsed.ToString());
            statsDict.Add(rocketLauncherTimesFiredID, stats.Weapons.RocketLauncher.timesFired.ToString());
            statsDict.Add(rocketLauncherKillsID, stats.Weapons.RocketLauncher.kills.ToString());
            statsDict.Add(rocketLauncherTimesUpgradedID, stats.Weapons.RocketLauncher.timesUpgraded.ToString());

            statsDict.Add(totalTowersBuiltID, stats.Towers.TotalBuilt.ToString());
            statsDict.Add(totalTowersRefundedID, stats.Towers.TotalRefunded.ToString());

            statsDict.Add(basicIamountBuiltID, stats.Towers.BasicI.amountBuilt.ToString());
            statsDict.Add(basicIamountRefundedID, stats.Towers.BasicI.amountRefunded.ToString());
            statsDict.Add(basicIkillsID, stats.Towers.BasicI.kills.ToString());
            statsDict.Add(basicItimesUpgradedID, stats.Towers.BasicI.timesUpgraded.ToString());
            
            statsDict.Add(basicIIamountBuiltID, stats.Towers.BasicII.amountBuilt.ToString());
            statsDict.Add(basicIIamountRefundedID, stats.Towers.BasicII.amountRefunded.ToString());
            statsDict.Add(basicIIkillsID, stats.Towers.BasicII.kills.ToString());
            statsDict.Add(basicIItimesUpgradedID, stats.Towers.BasicII.timesUpgraded.ToString());
            
            statsDict.Add(heavyIamountBuiltID, stats.Towers.HeavyI.amountBuilt.ToString());
            statsDict.Add(heavyIamountRefundedID, stats.Towers.HeavyI.amountRefunded.ToString());
            statsDict.Add(heavyIkillsID, stats.Towers.HeavyI.kills.ToString());
            statsDict.Add(heavyItimesUpgradedID, stats.Towers.HeavyI.timesUpgraded.ToString());
            
            statsDict.Add(heavyIIamountBuiltID, stats.Towers.HeavyII.amountBuilt.ToString());
            statsDict.Add(heavyIIamountRefundedID, stats.Towers.HeavyII.amountRefunded.ToString());
            statsDict.Add(heavyIIkillsID, stats.Towers.HeavyII.kills.ToString());
            statsDict.Add(heavyIItimesUpgradedID, stats.Towers.HeavyII.timesUpgraded.ToString());
            
            statsDict.Add(mortarIamountBuiltID, stats.Towers.MortarI.amountBuilt.ToString());
            statsDict.Add(mortarIamountRefundedID, stats.Towers.MortarI.amountRefunded.ToString());
            statsDict.Add(mortarIamountRefundedID2, stats.Towers.MortarI.amountRefunded.ToString());
            statsDict.Add(mortarIkillsID, stats.Towers.MortarI.kills.ToString());
            statsDict.Add(mortarItimesUpgradedID, stats.Towers.MortarI.timesUpgraded.ToString());
            
            statsDict.Add(antiAirIamountBuiltID, stats.Towers.AntiAirI.amountBuilt.ToString());
            statsDict.Add(antiAirIamountRefundedID, stats.Towers.AntiAirI.amountRefunded.ToString());
            statsDict.Add(antiAirIkillsID, stats.Towers.AntiAirI.kills.ToString());
            statsDict.Add(antiAirItimesUpgradedID, stats.Towers.AntiAirI.timesUpgraded.ToString());

            statsDict.Add(slowIamountBuiltID, stats.Towers.SlowI.amountBuilt.ToString());
            statsDict.Add(slowIamountRefundedID, stats.Towers.SlowI.amountRefunded.ToString());
            statsDict.Add(slowIkillsID, stats.Towers.SlowI.kills.ToString());
            statsDict.Add(slowItimesUpgradedID, stats.Towers.SlowI.timesUpgraded.ToString());

            statsDict.Add(healthPowerupsCollectedID, stats.Powerups.healthPowerupsCollected.ToString());
            statsDict.Add(speedPowerupsCollectedID, stats.Powerups.speedPowerupsCollected.ToString());
            statsDict.Add(damagePowerupsCollectedID, stats.Powerups.damagePowerupsCollected.ToString());
            statsDict.Add(currencyPowerupsCollectedID, stats.Powerups.currencyPowerupsCollected.ToString());

            statsDict.Add(openedPauseMenuID, stats.UI.openedPauseMenu.ToString());
            statsDict.Add(openedSettingMenuID, stats.UI.openedSettingsMenu.ToString());
            statsDict.Add(openedControlsScreenID, stats.UI.openedControlsScreen.ToString());
            statsDict.Add(openedGameplayScreenID, stats.UI.openedGameplayScreen.ToString());
            statsDict.Add(openedBugReportScreenID, stats.UI.openedBugReportScreen.ToString());
            statsDict.Add(bugReportsSentID, stats.UI.bugReportsSent.ToString());
            statsDict.Add(openedTowerOptionsMenuID, stats.UI.openedTowerOptionsMenu.ToString());
            statsDict.Add(timesOpenedTowerOptionsMenuID, stats.UI.timesOpenedTowerOptionsMenu.ToString());
            statsDict.Add(quitToMainMenuID, stats.UI.quitToMainMenu.ToString());

            statsDict.Add(changedFOVID, stats.Settings.changedFOV.ToString());
            statsDict.Add(changedVolumeID, stats.Settings.changedVolume.ToString());
            statsDict.Add(changedDifficultyID, stats.Settings.changedDifficulty.ToString());
            statsDict.Add(changedMouseSensitivityID, stats.Settings.changedMouseSensitivity.ToString());


            GoogleForm userStudy = GetComponent<GoogleForm>();
            WWWForm formResponse = GoogleForm.CreateForm(statsDict);
            userStudy.SubmitResponse(userStudyURL, formResponse);
        }

        private string feedbackURL = "https://docs.google.com/forms/d/e/1FAIpQLSc44u4vPRsDGQDPaBzO9WWtQSP3rogrokkWW4RcWGzktOVoog/formResponse";
        private string feedbackAgeID = "entry.1633662730";
        private string feedbackFPSEXPID = "entry.397881592";
        private string feedbackTDEXPID = "entry.1337661503";
        private string feedbackInputMethodID = "entry.480762771";
        private string feedbackOperatingSystemID = "entry.2078900389";
        private string feedbackDifficultyID = "entry.2042368048";
        private string feedbackEnemyVarietyID = "entry.581239741";
        private string feedbackWeaponVarietyID = "entry.735247868";
        private string feedbackWeaponUseabilityID = "entry.1783775868";
        private string feedbackTowerVarietyID = "entry.2121474598";
        private string feedbackFavouriteWeaponID = "entry.1081327199";
        private string feedbackFavouriteTowerID = "entry.464878908";
        private string feedbackFunnynessID = "entry.1731348157";
        private string feedbackEnjoymentID = "entry.1144898880";
        private string feedbackLongFormFeedbackID = "entry.1367211479";

        private Dictionary<string, string> feedbackDict = new Dictionary<string, string>();

        private void BindFeedbackToDictionary()
        {
            feedbackDict.Add(feedbackAgeID, feedback.Info.age);
            feedbackDict.Add(feedbackFPSEXPID, feedback.Info.FPSEXP.ToString());
            feedbackDict.Add(feedbackTDEXPID, feedback.Info.TDEXP.ToString());
            feedbackDict.Add(feedbackInputMethodID, feedback.Info.inputMethod);
            feedbackDict.Add(feedbackOperatingSystemID, feedback.Info.OS);

            feedbackDict.Add(feedbackDifficultyID, feedback.Difficulty.ToString());
            feedbackDict.Add(feedbackEnemyVarietyID, feedback.EnemyVariety.ToString());
            feedbackDict.Add(feedbackWeaponVarietyID, feedback.WeaponVariety.ToString());
            feedbackDict.Add(feedbackWeaponUseabilityID, feedback.WeaponUseability.ToString());
            feedbackDict.Add(feedbackTowerVarietyID, feedback.TowerVariety.ToString());
            feedbackDict.Add(feedbackFavouriteWeaponID, feedback.FavouriteWeapon);
            feedbackDict.Add(feedbackFavouriteTowerID, feedback.FavouriteTower);
            feedbackDict.Add(feedbackFunnynessID, feedback.Funnyness.ToString());
            feedbackDict.Add(feedbackEnjoymentID, feedback.Enjoyment.ToString());
            feedbackDict.Add(feedbackLongFormFeedbackID, feedback.LongFormFeedback);

            GoogleForm userStudy = GetComponent<GoogleForm>();
            WWWForm formResponse = GoogleForm.CreateForm(feedbackDict);
            userStudy.SubmitResponse(feedbackURL, formResponse);
        }

        [Serializable]
        public struct Feedback
        {
            public Info Info;
            public int Difficulty;
            public int EnemyVariety;
            public int WeaponVariety;
            public int WeaponUseability;
            public int TowerVariety;
            public string FavouriteWeapon;
            public string FavouriteTower;
            public int Funnyness;
            public int Enjoyment;
            public string LongFormFeedback;

            public Feedback(string age)
            {
                Info.age = age;
                Info.FPSEXP = 3;
                Info.TDEXP = 3;
                Info.inputMethod = "none";
                Info.OS = "none";

                Difficulty = 0;
                EnemyVariety = 0;
                WeaponVariety = 0;
                WeaponUseability = 0;
                TowerVariety = 0;
                FavouriteWeapon = "none";
                FavouriteTower = "none";
                Funnyness = 0;
                Enjoyment = 0;
                LongFormFeedback = "none";
            }
        }

        [Serializable]
        public struct Stats
        {
            public Info Info;
            public Player Player;
            public Hexes Hexes;
            public Waves Waves;
            public Weapons Weapons;
            public Towers Towers;
            public Enemies Enemies;
            public Powerups Powerups;
            public UI UI;
            public Settings Settings;

            public Stats(string age)
            {
                Info.age = age;
                Info.FPSEXP = 3;
                Info.TDEXP = 3;
                Info.inputMethod = "none";
                Info.OS = "none";

                Player.timesJumped = 0;
                Player.timeSpentSprinting = 0f;
                Hexes.amountEarned = 0;
                Hexes.amountSpent = 0;

                Waves.damageTaken = 0;
                Waves.wavesCompleted = 0;
                Waves.timesForcedNextWave = 0;
                Waves.wavesTakenDamageHashSet = new HashSet<int>();
                Waves.wavesTakenDamage = new int[25];
                Waves.waveFailed = 0;

                Weapons.Pistol.purchased = true;
                Weapons.Pistol.timeUsed = 0f;
                Weapons.Pistol.timesFired = 0;
                Weapons.Pistol.kills = 0;
                Weapons.Pistol.timesUpgraded = 0;
                
                Weapons.Shotgun.purchased = false;
                Weapons.Shotgun.timeUsed = 0f;
                Weapons.Shotgun.timesFired = 0;
                Weapons.Shotgun.kills = 0;
                Weapons.Shotgun.timesUpgraded = 0;
                
                Weapons.Revolver.purchased = false;
                Weapons.Revolver.timeUsed = 0f;
                Weapons.Revolver.timesFired = 0;
                Weapons.Revolver.kills = 0;
                Weapons.Revolver.timesUpgraded = 0;
                
                Weapons.Rifle.purchased = false;
                Weapons.Rifle.timeUsed = 0f;
                Weapons.Rifle.timesFired = 0;
                Weapons.Rifle.kills = 0;
                Weapons.Rifle.timesUpgraded = 0;

                Weapons.RocketLauncher.purchased = false;
                Weapons.RocketLauncher.timeUsed = 0f;
                Weapons.RocketLauncher.timesFired = 0;
                Weapons.RocketLauncher.kills = 0;
                Weapons.RocketLauncher.timesUpgraded = 0;

                Towers.TotalBuilt = 0;
                Towers.TotalRefunded = 0;

                Towers.BasicI.amountBuilt = 0;
                Towers.BasicI.amountRefunded = 0;
                Towers.BasicI.kills = 0;
                Towers.BasicI.timesUpgraded = 0;
                
                Towers.BasicII.amountBuilt = 0;
                Towers.BasicII.amountRefunded = 0;
                Towers.BasicII.kills = 0;
                Towers.BasicII.timesUpgraded = 0;
                
                Towers.HeavyI.amountBuilt = 0;
                Towers.HeavyI.amountRefunded = 0;
                Towers.HeavyI.kills = 0;
                Towers.HeavyI.timesUpgraded = 0;
                
                Towers.HeavyII.amountBuilt = 0;
                Towers.HeavyII.amountRefunded = 0;
                Towers.HeavyII.kills = 0;
                Towers.HeavyII.timesUpgraded = 0;
                
                Towers.MortarI.amountBuilt = 0;
                Towers.MortarI.amountRefunded = 0;
                Towers.MortarI.kills = 0;
                Towers.MortarI.timesUpgraded = 0;
                
                Towers.AntiAirI.amountBuilt = 0;
                Towers.AntiAirI.amountRefunded = 0;
                Towers.AntiAirI.kills = 0;
                Towers.AntiAirI.timesUpgraded = 0;
                
                Towers.SlowI.amountBuilt = 0;
                Towers.SlowI.amountRefunded = 0;
                Towers.SlowI.kills = 0;
                Towers.SlowI.timesUpgraded = 0;

                Enemies.totalKilled = 0;

                Powerups.healthPowerupsCollected = 0;
                Powerups.speedPowerupsCollected = 0;
                Powerups.damagePowerupsCollected = 0;
                Powerups.currencyPowerupsCollected = 0;

                UI.openedPauseMenu = false;
                UI.openedSettingsMenu = false;
                UI.openedControlsScreen = false;
                UI.openedGameplayScreen = false;
                UI.openedBugReportScreen = false;
                UI.openedTowerOptionsMenu = false;
                UI.timesOpenedTowerOptionsMenu = 0;
                UI.quitToMainMenu = false;
                UI.bugReportsSent = 0;

                Settings.changedFOV = false;
                Settings.changedVolume = false;
                Settings.changedMouseSensitivity = false;
                Settings.changedDifficulty = false;


            }
        }

        public void ResetStats()
        {
            Feedback temp1 = feedback;
            feedback = new Feedback("0");
            feedback.Info = temp1.Info;

            Stats temp2 = stats;
            stats = new Stats("0");
            stats.Info = temp2.Info;
        }


        public Stats stats;
        public Feedback feedback;
        private WeaponType currentWeapon;
        private bool sprinting;
        private bool sent = false;

        private void Awake()
        {
            EventManager.Active.onSaveStats += SaveStats;
            EventManager.Active.onSaveFeedback += SaveFeedback;
            EventManager.Active.onResetStats += ResetStats;

            stats = new Stats("0");
            feedback = new Feedback("0");
            stats.Waves.wavesTakenDamageHashSet = new HashSet<int>();

            EventManager.Active.onTowerBuilt += TowerBuilt;
            EventManager.Active.onTowerUpgraded += TowerUpgraded;
            EventManager.Active.onTowerDestroyed += TowerRefunded;

            EventManager.Active.onSprinting += SetSprinting;
            EventManager.Active.onJump += Jumped;

            EventManager.Active.onEnemyKilled += EnemyKilled;

            EventManager.Active.onOpenedPauseMenu += OpenedPauseMenu;
            EventManager.Active.onOpenedSettingsMenu += OpenedSettingsMenu;
            EventManager.Active.onOpenedControlsScreen += OpenedControlsScreen;
            EventManager.Active.onOpenedGameplayScreen += OpenedGameplayScreen;
            EventManager.Active.onOpenedBugReportScreen += OpenedBugReportScreen;
            EventManager.Active.onOpenTowerOptions += OpenedTowerOptionsMenu;
            EventManager.Active.onSentBugReport += SentBugReport;
            EventManager.Active.onQuitToMainMenu += QuitToMainMenu;

            EventManager.Active.onPowerUpCollected += PowerUpCollected;

            EventManager.Active.onChangedFOV += ChangedFOV;
            EventManager.Active.onChangedVolume += ChangedVolume;
            EventManager.Active.onChangedDifficulty += ChangedDifficulty;
            EventManager.Active.onMouseSensitivityChanged += ChangedMouseSensitivity;

            EventManager.Active.onWaveEnded += WaveEnded;
            EventManager.Active.onForcedNextWave += ForcedNextWave;

            EventManager.Active.onWeaponFired += WeaponFired;
            EventManager.Active.onWeaponPurchased += WeaponPurchased;
            EventManager.Active.onWeaponUpgraded += WeaponUpgraded;
            EventManager.Active.onWeaponSwitchedTo += WeaponSwitchedTo;

            EventManager.Active.onCastleDestroyed += CastleDestroyed;
            EventManager.Active.onCastleHit += CastleHit;

            EventManager.Active.onDeposited += Deposited;
            EventManager.Active.onCharged += Charged;
        }

        private void OpenedPauseMenu() => stats.UI.openedPauseMenu = true;
        private void OpenedSettingsMenu() => stats.UI.openedSettingsMenu = true;
        private void OpenedControlsScreen() => stats.UI.openedControlsScreen = true;
        private void OpenedGameplayScreen() => stats.UI.openedGameplayScreen = true;
        private void OpenedBugReportScreen() => stats.UI.openedBugReportScreen = true;

        private void OpenedTowerOptionsMenu(Tower tower)
        {
            stats.UI.openedTowerOptionsMenu = true;
            stats.UI.timesOpenedTowerOptionsMenu++;
        }

        private void SentBugReport() => stats.UI.bugReportsSent++;
        private void QuitToMainMenu() => stats.UI.quitToMainMenu = true;

        private void ChangedFOV() => stats.Settings.changedFOV = true;
        private void ChangedVolume() => stats.Settings.changedVolume = true;
        private void ChangedDifficulty() => stats.Settings.changedDifficulty = true;
        private void ChangedMouseSensitivity() => stats.Settings.changedMouseSensitivity = true;

        private void PowerUpCollected(PowerUpType powerup)
        {
            switch (powerup)
            {
                case PowerUpType.Health:
                    stats.Powerups.healthPowerupsCollected++;
                    break;
                
                case PowerUpType.Speed:
                    stats.Powerups.speedPowerupsCollected++;
                    break;
                
                case PowerUpType.Damage:
                    stats.Powerups.damagePowerupsCollected++;
                    break;
                
                case PowerUpType.Hexes:
                    stats.Powerups.currencyPowerupsCollected++;
                    break;
            }
        }

        private void Jumped() => stats.Player.timesJumped++;
        private void SetSprinting(bool truth) => sprinting = truth;

        private void Charged(int amount) => stats.Hexes.amountSpent += amount;
        private void Deposited(int amount) => stats.Hexes.amountEarned += amount;

        private void WaveEnded(int wave, float recoveryPeriod) => stats.Waves.wavesCompleted = wave;
        private void ForcedNextWave() => stats.Waves.timesForcedNextWave++;
        private void CastleDestroyed(int wave) => stats.Waves.waveFailed = wave;

        private void CastleHit(int damage, int wave)
        {
            stats.Waves.damageTaken += damage;
            stats.Waves.wavesTakenDamageHashSet.Add(wave);
        }

        private void EnemyKilled(string name)
        {
            stats.Enemies.totalKilled++;

            switch (name)
            {
                case "Pistol":
                    stats.Weapons.Pistol.kills++;
                    break;

                case "Shotgun":
                    stats.Weapons.Shotgun.kills++;
                    break;

                case "Revolver":
                    stats.Weapons.Revolver.kills++;
                    break;

                case "Rifle":
                    stats.Weapons.Rifle.kills++;
                    break;

                case "Rocket Launcher":
                    stats.Weapons.RocketLauncher.kills++;
                    break;

                case "Basic I":
                    stats.Towers.BasicI.kills++;
                    break;

                case "Basic II":
                    stats.Towers.BasicII.kills++;
                    break;

                case "Heavy I":
                    stats.Towers.HeavyI.kills++;
                    break;

                case "Heavy II":
                    stats.Towers.HeavyII.kills++;
                    break;

                case "Slow I":
                    stats.Towers.SlowI.kills++;
                    break;
                
                case "AntiAir I":
                    stats.Towers.AntiAirI.kills++;
                    break;

                case "Mortar I":
                    stats.Towers.MortarI.kills++;
                    break;

                default:
                    Debug.Log("NO CASE FOR " + name);
                    break;
            }
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.deltaTime;

            if (sprinting) stats.Player.timeSpentSprinting += deltaTime;

            switch (currentWeapon)
            {    
                case WeaponType.Pistol:
                    stats.Weapons.Pistol.timeUsed += deltaTime;
                    break;
                
                case WeaponType.Shotgun:
                    stats.Weapons.Shotgun.timeUsed += deltaTime;
                    break;
                
                case WeaponType.Revolver:
                    stats.Weapons.Revolver.timeUsed += deltaTime;
                    break;
                
                case WeaponType.Rifle:
                    stats.Weapons.Rifle.timeUsed += deltaTime;
                    break;
                
                case WeaponType.RocketLauncher:
                    stats.Weapons.RocketLauncher.timeUsed += deltaTime;
                    break;
            }
        }

        private void TowerBuilt(TowerType tower)
        {
            stats.Towers.TotalBuilt++;

            switch (tower)
            {
                case TowerType.Basic_I:
                    stats.Towers.BasicI.amountBuilt++;
                    break;

                case TowerType.Basic_II:
                    stats.Towers.BasicII.amountBuilt++;
                    break;

                case TowerType.Heavy_I:
                    stats.Towers.HeavyI.amountBuilt++;
                    break;

                case TowerType.Heavy_II:
                    stats.Towers.HeavyII.amountBuilt++;
                    break;

                case TowerType.Mortar_I:
                    stats.Towers.MortarI.amountBuilt++;
                    break;

                case TowerType.AntiAir_I:
                    stats.Towers.AntiAirI.amountBuilt++;
                    break;

                case TowerType.Slow_I:
                    stats.Towers.SlowI.amountBuilt++;
                    break;
            }
        }

        private void TowerRefunded(TowerType tower)
        {
            stats.Towers.TotalRefunded++;

            switch (tower)
            {
                case TowerType.Basic_I:
                    stats.Towers.BasicI.amountRefunded++;
                    break;

                case TowerType.Basic_II:
                    stats.Towers.BasicII.amountRefunded++;
                    break;

                case TowerType.Heavy_I:
                    stats.Towers.HeavyI.amountRefunded++;
                    break;

                case TowerType.Heavy_II:
                    stats.Towers.HeavyII.amountRefunded++;
                    break;

                case TowerType.Mortar_I:
                    stats.Towers.MortarI.amountRefunded++;
                    break;

                case TowerType.AntiAir_I:
                    stats.Towers.AntiAirI.amountRefunded++;
                    break;

                case TowerType.Slow_I:
                    stats.Towers.SlowI.amountRefunded++;
                    break;
            }
        }

        private void TowerUpgraded(TowerType tower)
        {
            switch (tower)
            {
                case TowerType.Basic_I:
                    stats.Towers.BasicI.timesUpgraded++;
                    break;

                case TowerType.Basic_II:
                    stats.Towers.BasicII.timesUpgraded++;
                    break;

                case TowerType.Heavy_I:
                    stats.Towers.HeavyI.timesUpgraded++;
                    break;

                case TowerType.Heavy_II:
                    stats.Towers.HeavyII.timesUpgraded++;
                    break;

                case TowerType.AntiAir_I:
                    stats.Towers.AntiAirI.timesUpgraded++;
                    break;

                case TowerType.Slow_I:
                    stats.Towers.SlowI.timesUpgraded++;
                    break;
            }

        }

        private void WeaponSwitchedTo(WeaponType weapon) => currentWeapon = weapon;

        private void WeaponPurchased(WeaponType weapon)
        {
            switch (weapon)
            {
                case WeaponType.Pistol:
                    stats.Weapons.Pistol.purchased = true;
                    break;
                
                case WeaponType.Shotgun:
                    stats.Weapons.Shotgun.purchased = true;
                    break;
                    
                case WeaponType.Revolver:
                    stats.Weapons.Revolver.purchased = true;
                    break;

                case WeaponType.Rifle:
                    stats.Weapons.Rifle.purchased = true;
                    break;

                case WeaponType.RocketLauncher:
                    stats.Weapons.RocketLauncher.purchased = true;
                    break;

            }
        }

        private void WeaponUpgraded(WeaponType weapon)
        {
            switch (weapon)
            {
                case WeaponType.Pistol:
                    stats.Weapons.Pistol.timesUpgraded++;
                    break;
                
                case WeaponType.Shotgun:
                    stats.Weapons.Shotgun.timesUpgraded++;
                    break;
                    
                case WeaponType.Revolver:
                    stats.Weapons.Revolver.timesUpgraded++;
                    break;

                case WeaponType.Rifle:
                    stats.Weapons.Rifle.timesUpgraded++;
                    break;

                case WeaponType.RocketLauncher:
                    stats.Weapons.RocketLauncher.timesUpgraded++;
                    break;
            }
        }

        private void WeaponFired(WeaponData weapon)
        {
            switch (weapon.weaponType)
            {
                case WeaponType.Pistol:
                    stats.Weapons.Pistol.timesFired++;
                    break;

                case WeaponType.Shotgun:
                    stats.Weapons.Shotgun.timesFired++;
                    break;
                    
                case WeaponType.Revolver:
                    stats.Weapons.Revolver.timesFired++;
                    break;
                    
                case WeaponType.Rifle:
                    stats.Weapons.Rifle.timesFired++;
                    break;
                
                case WeaponType.RocketLauncher:
                    stats.Weapons.RocketLauncher.timesFired++;
                    break;
            }

        }

        // void OnDestroy()
        // {
        //     SaveStats(); 
        // }

        public void SaveFeedback()
        {
            BindFeedbackToDictionary();
        }

        public void SaveStats()
        {
            if (sent) return;
            sent = true;

            stats.Waves.wavesTakenDamage = new int[stats.Waves.wavesTakenDamageHashSet.Count];
            stats.Waves.wavesTakenDamageHashSet.CopyTo(stats.Waves.wavesTakenDamage);

            BindStatsToDictionary();

            // string json = JsonUtility.ToJson(stats);
            // string currentTime = System.DateTime.Now.ToString();
            // currentTime = currentTime.Replace(" ", "");
            // currentTime = currentTime.Replace("-", "");
            // currentTime = currentTime.Replace(":", "");
            // currentTime = currentTime.Replace("PM", "." + UnityEngine.Random.Range(0,8192).ToString());
            // currentTime += ".json";
            // string path = Application.dataPath + "/" + currentTime;
            // Debug.Log(path);
            // File.WriteAllText(path, json);

        }
    }
}
