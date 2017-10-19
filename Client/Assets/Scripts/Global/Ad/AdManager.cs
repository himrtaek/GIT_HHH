using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdManager : SingleTone<AdManager>
{
	public string gameId = "1375194";
	public bool enableTestMode = true;

    public IEnumerator ShowAd()
    {
		if (Advertisement.isSupported)
		{
			Advertisement.Initialize(gameId, enableTestMode);
		}

		while (!Advertisement.isInitialized || !Advertisement.IsReady())
		{
			yield return new WaitForSeconds(0.5f);
		}

		Advertisement.Show();
    }
}
