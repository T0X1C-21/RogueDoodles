using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "UserInterfaceData", menuName = "GameData/UserInterfaceData")]
public class UserInterfaceData : ScriptableObject {

    [System.Serializable]
    public struct UpgradeMenuInfo {
        
        [Space(10)]
        [Header("----- WEAPON UPGRADES ------")]
        public string inkSplashText;
        public Image inkSplashImage;
        public string crayonMissileText;
        public Image crayonMissileImage;
        public string notebookTearText;
        public Image notebookTearImage;

        [Space(10)]
        [Header("----- PASSIVE UPGRADES ------")]
        public string attackSpeedPlusPlusText;
        public Image attackSpeedPlusPlusImage;
        public string projectileCountPlusPlusText;
        public Image projectilCountPlusPlusImage;
        public string piercingPlusPlusText;
        public Image piercingPlusPlusImage;
        public string sizePlusPlusText;
        public Image sizePlusPlusImage;
        public string attackDamagePlusPlusText;
        public Image attackDamagePlusPlusImage;
        public string rerollPlusPlusText;
        public Image rerollPlusPlusImage;
        public string luckPlusPlusText;
        public Image luckPlusPlusImage;

    }


    public UpgradeMenuInfo upgradeMenuInfo;

}
