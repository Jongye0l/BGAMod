using System.Collections.Generic;
using ADOFAI;
using HarmonyLib;
using UnityEngine;

namespace BGAMod {
    [HarmonyPatch]
    public class PlayPatch {
        [HarmonyPatch(typeof(scnEditor), "Play")]
        [HarmonyPostfix]
        public static void Hide() {
            foreach(scrFloor floor in scnEditor.instance.floors) floor.startScale = floor.transform.localScale = Vector3.zero;
            foreach(scrPlanet planet in scrController.instance.allPlanets) {
                for(int i = 0; i < planet.transform.childCount; i++) 
                    planet.transform.GetChild(i).gameObject.SetActive(false);
                planet.gameObject.GetComponent<PlanetRenderer>().visible = false;
            }
            scrConductor.instance.hitSoundVolume = 0;
        }
        
        [HarmonyPatch(typeof(scnGame), "ApplyEvent")]
        [HarmonyPostfix]
        public static void RemoveMultiPlanet(LevelEvent evnt, List<scrFloor> floors, int? customFloorID, ffxPlusBase __result) {
            int num1 = customFloorID ?? evnt.floor;
            scrFloor floor = floors[num1];
            if(__result is ffxMoveFloorPlus ffxMoveFloorPlus) ffxMoveFloorPlus.scaleUsed = false;
            if(evnt.eventType == LevelEventType.SetHitsound) floor.setHitsound = null;
        }
    }
}