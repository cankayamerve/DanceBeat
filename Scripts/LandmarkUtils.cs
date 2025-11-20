using Mediapipe;
using UnityEngine;

public static class LandmarkUtils
{
  public static Vector3 ConvertLandmarkToVector(NormalizedLandmark landmark)
  {
    return new Vector3(landmark.X, landmark.Y, landmark.Z);
  }
  public static bool IsLandmarkValid(NormalizedLandmark landmark)
  {
    return landmark != null && landmark.Visibility > 0.8f;
  }

}
