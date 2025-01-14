using Steamworks;

namespace DontSaveToDesktop;

internal static class Replacements
{
  internal static CameraRecording m_recording = new();
  internal static string apply(string text) {
    return text
            .Replace("%HANDLE", m_recording.videoHandle.ToString())
            .Replace("%USER", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile))
            .Replace("%YY", DateTime.Now.Year.ToString("yy"))
            .Replace("%CCYY", DateTime.Now.Year.ToString())
            .Replace("%MM", DateTime.Now.Month.ToString("00"))
            .Replace("%DD", DateTime.Now.Day.ToString("00"))
            .Replace("%hh", DateTime.Now.Hour.ToString("00"))
            .Replace("%mm", DateTime.Now.Minute.ToString("00"))
            .Replace("%ss", DateTime.Now.Second.ToString("00"))
            .Replace("%DAY", SurfaceNetworkHandler.RoomStats.CurrentDay.ToString())
            .Replace("%QDAY", SurfaceNetworkHandler.RoomStats.CurrentQuotaDay.ToString())
            .Replace("%RUN", SurfaceNetworkHandler.RoomStats.CurrentRun.ToString())
            .Replace("%localPLAYER", SteamFriends.GetPersonaName())
            .Replace("%localID", SteamUser.GetSteamID().ToString());
            //TODO: Add ContentPOVs info (owner's user ID) to CameraRecording for use in replacements if possible.
  }
}
