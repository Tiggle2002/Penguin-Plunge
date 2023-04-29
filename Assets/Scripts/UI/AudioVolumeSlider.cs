using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.Tools;
using Sirenix.OdinInspector;

public class AudioVolumeSlider : MonoBehaviour
{
    public MMSoundManager.MMSoundManagerTracks TargetTrack => targetTrack;

    [SerializeField, HideLabel, EnumToggleButtons]
    private MMSoundManager.MMSoundManagerTracks targetTrack = MMSoundManager.MMSoundManagerTracks.Music;

    private Slider slider;

    public void Awake() => slider = GetComponent<Slider>();

    public void ChangeVolume() => MMSoundManager.Instance.SetTrackVolume(targetTrack, slider.value);
}
