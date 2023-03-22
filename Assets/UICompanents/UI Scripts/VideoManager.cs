using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    [SerializeField] private List<VideoClip> _videoClips;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private GameObject _videoPanel;
    
    private void OnEnable()
    {
        Page.OnDescription += Page_OnDescription;
        Page.OffDescription += Page_OffDescription;
    }

    private void OnDisable()
    {
        Page.OnDescription -= Page_OnDescription;
        Page.OffDescription -= Page_OffDescription;
    }

    private void Page_OnDescription()
    {
        _videoPanel.SetActive(true);
    }

    private void Page_OffDescription()
    {
        _videoPanel.SetActive(false);
    }

    public void SetVideo(int videoClipIndex)
    {
        _videoPlayer.clip = _videoClips[videoClipIndex];
        _videoPlayer.Play();
    }


}
