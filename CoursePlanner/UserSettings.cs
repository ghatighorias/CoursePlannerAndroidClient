using System;
using Android.App;
using Android.Content;
using CoursePlanner.Api;

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
            var UserName = prefs.GetString("UserName", "");
            var PasswordHash = prefs.GetString("PasswordHash", "");

            return new Setting(ServerUrl, SaveLoginDetail, UserName, PasswordHash);
        }

        public static bool SaveSetting(Setting setting)
        {
            var prefs = Application.Context.GetSharedPreferences(SlotName, FileCreationMode.Private);
            var prefEditor = prefs.Edit();
            prefEditor.PutString("ServerUrl", setting.ServerUrl);
            prefEditor.PutBoolean("SaveLoginDetail", setting.SaveLoginDetail);
            return prefEditor.Commit();
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

        public string UserName
        {
            get;
            set;
        }

        private string passwordHash;
        public string PasswordHash
        {
            get { return passwordHash; }
            set { passwordHash = Utilities.GetHashed(value); }
        }

        public Setting()
        {
        }

        public Setting(string serverUrl, bool saveLoginDetail, String userName, String passwordHash)
        {
            SaveLoginDetail = saveLoginDetail;
            UserName = userName;
            PasswordHash = passwordHash;
        }
    }
}
