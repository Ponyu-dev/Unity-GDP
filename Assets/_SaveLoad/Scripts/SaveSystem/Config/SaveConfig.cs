using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SaveSystem.Config
{
    [CreateAssetMenu(menuName = "SaveLoader/Config", fileName = "SaveConfig", order = 0)]
    public class SaveConfig : ScriptableObject
    {
        public bool IsCustomPlace() => place == Place.Custom;

        [BoxGroup("B", false), HorizontalGroup("B/H")]
        [TitleGroup("B/H/Settings")]
        public bool debug = true;
        
        [TitleGroup("B/H/Settings")]
        public Place place = Place.UnityPersistent;
        
        [TitleGroup("B/H/Settings"), ShowIf("IsCustomPlace")]
        public string customLocation;

        public string SaveFilePath(string fileName) => Path.Combine(Location, fileName);

        private string Location
        {
            get
            {
                return place switch
                {
                    Place.UnityPersistent => Application.persistentDataPath,
                    Place.StartLocation => Directory.GetCurrentDirectory(),
                    Place.Custom => customLocation,
                    _ => Application.persistentDataPath
                };
            }
        }

        [GUIColor(1, 0, 0, 1), PropertySpace]
        [TitleGroup("B/H/Settings"), Button(ButtonSizes.Medium)]
        public void ClearSaveFolder()
        {
            if (Directory.Exists(Location))
                Directory.Delete(Location, true);
        }
    }
}