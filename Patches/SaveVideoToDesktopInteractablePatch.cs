using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace DontSaveToDesktop.Patches;

[HarmonyPatch(typeof(SaveVideoToDesktopInteractable))]
internal static class SaveVideoToDesktopInteractablePatch
{
    [HarmonyPatch(nameof(SaveVideoToDesktopInteractable.Interact))]
    [HarmonyPrefix]
    internal static bool Interact(SaveVideoToDesktopInteractable __instance)
    {
        CameraRecording m_recording = (CameraRecording)typeof(SaveVideoToDesktopInteractable)
            .GetField("m_recording", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
        Debug.Log((object) "Saving video to folder...");
        string localizedString1 = LocalizationKeys.GetLocalizedString(LocalizationKeys.Keys.VideoSaved);
        string localizedString2 = LocalizationKeys.GetLocalizedString(LocalizationKeys.Keys.VideoSavedAs);
        string localizedString3 = LocalizationKeys.GetLocalizedString(LocalizationKeys.Keys.VideoFailedSave);
        string localizedString4 = LocalizationKeys.GetLocalizedString(LocalizationKeys.Keys.Ok);
        string videoFileName;
        
        string path;
        if (!RecordingsHandler.TryGetRecordingPath(m_recording.videoHandle, out path))
        {
            videoFileName = string.Empty;
            Modal.Show(localizedString3, "", new ModalOption[1]
            {
                new ModalOption(localizedString4)
            });
            return false;
        }

        string fileName = GameHandler.Instance.SettingsHandler.GetSetting<FileName>().Value
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
            .Replace("%RUN", SurfaceNetworkHandler.RoomStats.CurrentRun.ToString());
        
        videoFileName = fileName + ".webm";

        string filePath = GameHandler.Instance.SettingsHandler.GetSetting<FilePath>().Value
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
            .Replace("%RUN", SurfaceNetworkHandler.RoomStats.CurrentRun.ToString());
        
        if (!File.Exists(path))
        {
            Debug.LogError((object) ("Video: " + path + " Does Not Exist!"));
            Modal.Show(localizedString3, "", new ModalOption[1]
            {
                new ModalOption(localizedString4)
            });
            return false;
        }
                
        if (!Directory.Exists(filePath))
        {
            Modal.Show("Directory not found", "The directory \""+filePath+"\" does not exist. Would you like to create it? (Selecting No will save video to Desktop)", new ModalOption[]
            {
                new ModalOption("Yes", delegate() {
                    Directory.CreateDirectory(filePath);
                    string destFileName = Path.Combine(filePath, videoFileName);
                    if (File.Exists(destFileName))
                    {
                        Modal.Show("File already exists",
                            "The file \"" + fileName + ".webm\" already exists. Check the DontSaveToDesktop workshop description to fix this. Would you like to overwrite it?",
                            new ModalOption[]
                            {
                                new ModalOption("Yes", delegate()
                                {
                                    File.Copy(path, destFileName, true);
                                    Modal.Show(localizedString1, localizedString2 + "  " + videoFileName, new ModalOption[1]
                                    {
                                        new ModalOption(localizedString4)
                                    });
                                }),
                                new ModalOption("No", delegate() { })
                        });
                    }
                    else
                    {
                        File.Copy(path, destFileName);
                        Modal.Show(localizedString1, localizedString2 + "  " + videoFileName, new ModalOption[1]
                        {
                            new ModalOption(localizedString4)
                        });
                    }
                }),
                new ModalOption("No", delegate()
                {
                    string destFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), videoFileName);
                    if (File.Exists(destFileName))
                    {
                        Modal.Show("File already exists",
                            "The file \"" + fileName + ".webm\" already exists. Check the DontSaveToDesktop workshop description to fix this. Would you like to overwrite it?",
                            new ModalOption[]
                            {
                                new ModalOption("Yes", delegate()
                                {
                                    File.Copy(path, destFileName, true);
                                    Modal.Show(localizedString1, localizedString2 + "  " + videoFileName, new ModalOption[1]
                                    {
                                        new ModalOption(localizedString4)
                                    });
                                }),
                                new ModalOption("No", delegate() { })
                        });
                    }
                    else
                    {
                        File.Copy(path, destFileName);
                        Modal.Show(localizedString1, localizedString2 + "  " + videoFileName, new ModalOption[1]
                        {
                            new ModalOption(localizedString4)
                        });
                    }
                })
            });
            return false;
        }
        string destFileName = Path.Combine(filePath, videoFileName);
        if (File.Exists(destFileName))
        {
            Modal.Show("File already exists",
                "The file \"" + fileName + ".webm\" already exists. Check the DontSaveToDesktop workshop description to fix this. Would you like to overwrite it?",
                new ModalOption[]
                {
                    new ModalOption("Yes", delegate()
                    {
                        File.Copy(path, destFileName, true);
                        Modal.Show(localizedString1, localizedString2 + "  " + videoFileName, new ModalOption[1]
                        {
                            new ModalOption(localizedString4)
                        });
                    }),
                    new ModalOption("No", delegate() { })
            });
        }
        else
        {
            File.Copy(path, destFileName);
            Modal.Show(localizedString1, localizedString2 + "  " + videoFileName, new ModalOption[1]
            {
                new ModalOption(localizedString4)
            });
        }
        return false;
    }
}