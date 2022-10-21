using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DSNConnection : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI NameText;

    [SerializeField]
    GameObject UploadLink;

    [SerializeField]
    GameObject DownloadLink;

    RectTransform UploadLinkRT;
    RectTransform DownloadLinkRT;

    public enum DishID {
        DSS14,
        DSS24,
        DSS25,
        DSS26,
        DSS34,
        DSS35,
        DSS36,
        DSS43,
        DSS53,
        DSS54,
        DSS55,
        DSS56,
        DSS63,
        DSS64,
    };

    public DishID dishID = DishID.DSS14;

    void Start()
    {
        UploadLinkRT = UploadLink.GetComponent<RectTransform>();
        DownloadLinkRT = DownloadLink.GetComponent<RectTransform>();
    }

    public void SetSpeedWidths(float newUploadWidth, float newDownloadWidth)
    {
        if (UploadLinkRT == null || DownloadLinkRT == null)
            return;
        UploadLinkRT.sizeDelta = new Vector2(newUploadWidth, UploadLinkRT.sizeDelta.y);
        DownloadLinkRT.sizeDelta = new Vector2(newDownloadWidth, DownloadLinkRT.sizeDelta.y);
    }
}
