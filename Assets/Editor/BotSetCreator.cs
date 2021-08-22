using System;
using System.Collections.Generic;
using Bots_Configs.ScriptableObjectConfig;
using UnityEditor;
using UnityEngine;
using СlothesConfigs.ScriptableObjectConfig;

namespace Editor
{
    public class BotSetCreator : ScriptableWizard
    {
        public int AmountToGenerate;
        
        public TextAsset BotNames;

        public ClothesSet HatsSet;
        public ClothesSet ShirtSet;
        public ClothesSet PantsSet;

        public Sprite[] BodySprites;
        
        public Sprite DefaultBodySprite;

        public string NewSetName = "Base Bots Set";

        [MenuItem("Bots/Create Bots Profiles Set")]
        public static void GenerateBotsSet()
        {
            DisplayWizard<BotSetCreator>("Create Bots Set", "Create");
        }

        private void OnWizardUpdate()
        {
            isValid = false;

            errorString = "";

            if (AssetDatabase.IsValidFolder(SetsFolder + "/" + NewSetName))
                errorString += "Such Set Already or Set name empty!\n";
            if (BotNames == null)
                errorString += "No name set\n";


            isValid = errorString.Length == 0 &&
                      BotNames != null &&
                      !AssetDatabase.IsValidFolder(SetsFolder + "/" + NewSetName);
        }

        private void OnWizardCreate()
        {
            AssetDatabase.CreateFolder(SetsFolder, NewSetName);
            var newFolderPath = SetsFolder + "/" + NewSetName;
            Debug.Log(newFolderPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            var botProfiles = CreateInstance<BotsSet>();
            botProfiles.BotConfigs = new List<BotConfig>();
            
            var botNames = GetNicknames(BotNames);

            var rng = new System.Random();

            for (var i = 0; i < AmountToGenerate; i++)
            {
                var botNameId = rng.Next(0, botNames.Count-1);
                var newProfile = CreateInstance<BotConfig>();
                newProfile.BotId = i;
                newProfile.SetName(botNames[botNameId]);
                
                var botSprite = BodySprites.Length>0 ? BodySprites[rng.Next(0, BodySprites.Length-1)] : DefaultBodySprite;
                newProfile.SetBaseSprite(botSprite);
                
                var hat = HatsSet!=null ? HatsSet.Clothes[rng.Next(0, HatsSet.Clothes.Length-1)]:null;
                var shirt = ShirtSet!=null ? ShirtSet.Clothes[rng.Next(0, ShirtSet.Clothes.Length-1)]:null;
                var pants = PantsSet!=null ? PantsSet.Clothes[rng.Next(0, PantsSet.Clothes.Length-1)]:null;

                var clothes = new[] {hat, shirt, pants};
                newProfile.SetClothes(clothes);
                
                AssetDatabase.CreateAsset(newProfile, newFolderPath + "/" + (newProfile.BotId + 1) + "_Bot" + ".asset");
                AssetDatabase.SaveAssets();
                botProfiles.BotConfigs.Add(newProfile);
            }

            AssetDatabase.CreateAsset(botProfiles, newFolderPath + "/BotProfiles.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Close();
        }
        
        private List<string> GetNicknames(TextAsset nicknamesAsset)
        {
            var separator = new[] { " ", "\n", "\r" };
            var nicknames = nicknamesAsset.text;
            var nicknamesList = new List<string>(nicknames.Split(separator, StringSplitOptions.RemoveEmptyEntries));
            return nicknamesList;
        }

        private const string SetsFolder = "Assets/Bots Configs";
    }
}
