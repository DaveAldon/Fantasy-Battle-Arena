using UnityEngine;
using System.Collections;

public static class Globals {
	public static string username { get; set; }
	public static string password { get; set; }
	public static string PlayFabId { get; set; }

	//From character controller
	// Input threshold in order to take effect. Arbitarily set.
	public const float INPUT_THRESHOLD = 0.5f;
	public const float FAST_FALL_THRESHOLD = 0.5f;

	public const int ENV_MASK = 0x100;

	public const string PACKAGE_NAME = "PC2D";

	public const float MINIMUM_DISTANCE_CHECK = 0.01f;

	public static int GetFrameCount(float time)
	{
		float frames = time / Time.fixedDeltaTime;
		int roundedFrames = Mathf.RoundToInt(frames);

		if (Mathf.Approximately(frames, roundedFrames))
		{
			return roundedFrames;
		}

		return Mathf.CeilToInt(frames);
	}
}
