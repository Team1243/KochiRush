using System.Net.NetworkInformation;
using GoogleMobileAds.Api;
using UnityEngine;
using System;

public class RewardedAdManager : MonoBehaviour
{
    public static RewardedAdManager Instance = null;

    private RewardedAd rewardedAd;

    public Action onUserEarnedRewardAction; // ������ ���� ���� �� ����â�� �ݾ��� �� ����

    private readonly string rewardAdID = "ca-app-pub-~~~~~~~~~~~~~~~~~~~~"; // ������ ���� ID
    private readonly string rewardTestAdID = "ca-app-pub-5714181718235393~8534758301"; //������ �׽�Ʈ ���� ID

    private void Awake()
    {
        if (Instance != null)
        {
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (IsNetworkAvailable())
        {
            Debug.Log(IsNetworkAvailable());
        }

        onUserEarnedRewardAction += () => Debug.Log("Player successfull watching");

        LoadAd();
    }

    // ���� ���� ���� �غ� �ܰ�
    private void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd = new RewardedAd(rewardTestAdID); // ������ ���� ���̵��
        rewardedAd.LoadAd(request);

        #region Events

        // ������ ���� ���� �� ����â�� �ݾ��� �� ȣ��
        rewardedAd.OnUserEarnedReward += (sender, reward) => onUserEarnedRewardAction();

        // ���� �ε� �߿� ������ �߻����� �� ȣ��
        rewardedAd.OnAdFailedToLoad += (sender, args) => HandleAdFailedToLoad(sender, args);

        // ���� ��ü ȭ������ ǥ�õ��� ���� �� ȣ��
        rewardedAd.OnAdFailedToShow += (sender, args) => ShowAd();

        #endregion
    }

    // ���� �� �ε��Ǿ����� Ȯ���ϰ� ���� ���
    public void ShowAd()
    {
        if (rewardedAd != null && rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            LoadAd();
        }
        else
        {
            Debug.Log("Ad is not ready");
        }
    }

    private void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("ad loading is failure");

        if (IsNetworkAvailable())
        {
            Debug.Log("network is connect");
        }
        else
        {
            Debug.Log("network is not connect");
        }
    }

    // ���߿� ���� �Ŵ��� �ý��������� ���� �� �� ����
    private bool IsNetworkAvailable()
    {
        return NetworkInterface.GetIsNetworkAvailable();
    }
}
