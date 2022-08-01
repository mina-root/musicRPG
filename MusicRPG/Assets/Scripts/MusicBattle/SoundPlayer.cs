using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CriWare;

public class SoundPlayer : MonoBehaviour
{
    private CriAtomEx.CueInfo[] cueInfoList;
    private CriAtomExPlayer atomExPlayer;
    private CriAtomExAcb atomExAcb;
    private CriAtomExPlayback playback;

    IEnumerator Start()
    {
        /* キューシートファイルのロード待ち */
        while (CriAtom.CueSheetsAreLoading) {
            yield return null;
        }

        /* AtomExPlayerの生成 */
        atomExPlayer = new CriAtomExPlayer(true);

        /* Cue情報の取得 */
        atomExAcb = CriAtom.GetAcb("CueSheet_0");
        cueInfoList = atomExAcb.GetCueInfoList();
    }
    private void OnDestroy()
    {
        atomExPlayer.Dispose();
    }

    void OnGUI()
    {
        /* キュー名再生ボタンの生成 */
        for (int i = 0; i < cueInfoList.Length; i++) {
            if (GUI.Button(new Rect(Screen.width - 40, (Screen.height / cueInfoList.Length) * i, 40, Screen.height / cueInfoList.Length), cueInfoList[i].name)) {
                /* 再生中の場合は停止 */
                if(atomExPlayer.GetStatus() == CriAtomExPlayer.Status.Playing) {
                    atomExPlayer.Stop();
                }
                atomExPlayer.SetCue(atomExAcb, cueInfoList[i].name);
                playback = atomExPlayer.Start();
            }
        }
    }

    public int getPlaytime(){//再生時間を返す
        return (int) playback.GetTimeSyncedWithAudio();
    }
}