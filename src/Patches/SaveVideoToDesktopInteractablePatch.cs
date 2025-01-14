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
        Replacements.m_recording = m_recording;
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

        string fileName = Replacements.apply(GameHandler.Instance.SettingsHandler.GetSetting<FileName>().Value);
        
        videoFileName = fileName + ".webm";

        string filePath = Replacements.apply(GameHandler.Instance.SettingsHandler.GetSetting<FilePath>().Value);
        
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
