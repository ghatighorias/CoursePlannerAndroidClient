using System;
using Android.App;
using Android.Content;

namespace CoursePlanner
{
    public static class UserSettings
    {
        private static string slotName;
        public static string SlotName
        {
            get { return slotName; }
            set { slotName = value;}
        }

        public static Setting LoadSetting()
        {
            var prefs = Application.Context.GetSharedPreferences(SlotName, FileCreationMode.Private);
            var ServerUrl = prefs.GetString("ServerUrl", "");
            var SaveLoginDetail = prefs.GetBoolean("SaveLoginDetail", false);
            return new Setting(ServerUrl, SaveLoginDetail);
        }

        public static bool SaveSetting(Setting setting)
        {
            var prefs = Application.Context.GetSharedPreferences(SlotName, FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("ServerUrl", setting.ServerUrl);
            prefEditor.PutBoolean("SaveLoginDetail", setting.SaveLoginDetail);
            return prefEditor.Commit();
        }

        public static bool IsSettingSaved(out Setting setting)
        {
            setting = LoadSetting();
            if (setting.SaveLoginDetail == false && setting.ServerUrl == String.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class Setting
    {
        public string ServerUrl
        {
            get;
            set;
        }

        public bool SaveLoginDetail
        {
            get;
            set;
        }

        public Setting(string serverUrl, bool saveLoginDetail)
        {
            ServerUrl = ServerUrl;
            SaveLoginDetail = saveLoginDetail;
        }
    }
}
