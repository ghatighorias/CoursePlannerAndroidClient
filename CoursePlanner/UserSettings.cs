using System;
using Android.App;
using Android.Content;

namespace CoursePlanner
{
    public static class UserSettings
    {
        public static String SlotName
        {
            get;
            set;
        }

        public static Setting LoadSetting()
        {
            var prefs = Application.Context.GetSharedPreferences(SlotName, FileCreationMode.Private);

            var UserName = prefs.GetString("UserName", String.Empty);
            var ServerUrl = prefs.GetString("ServerUrl", String.Empty);
            var PasswordHash = prefs.GetString("PasswordHash", String.Empty);
            var SaveLoginDetail = prefs.GetBoolean("SaveLoginDetail", false);

            return new Setting(ServerUrl, SaveLoginDetail, UserName, PasswordHash);
        }

        public static bool SaveSetting(Setting setting)
        {
            var prefs = Application.Context.GetSharedPreferences(SlotName, FileCreationMode.Private);
            var prefEditor = prefs.Edit();

            prefEditor.PutString("UserName", setting.UserName);
            prefEditor.PutString("ServerUrl", setting.ServerUrl);
            prefEditor.PutString("PasswordHash", setting.PasswordHash);
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

        public string PasswordHash
        {
            get;
            set;
        }

        public Setting()
        {
        }

        public Setting(string serverUrl, bool saveLoginDetail, String userName, String passwordHash)
        {
            ServerUrl = serverUrl;
            SaveLoginDetail = saveLoginDetail;
            UserName = userName;
            PasswordHash = passwordHash;
        }
    }
}
