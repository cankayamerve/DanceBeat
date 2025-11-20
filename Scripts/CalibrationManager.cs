using Mediapipe;
using Mediapipe.Unity;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationManager : MonoBehaviour
{
  public static CalibrationManager Instance;
  [SerializeField] private HolisticLandmarkListAnnotationController holisticLandmarkController;

  [SerializeField] private float calibrationDuration = 3f;
  [SerializeField] private float tPoseAngleThreshold = 30f;
  [SerializeField] private float shoulderThreshold = 0.07f;

  private bool calibrated = false;
  private bool isCalibrating = false;
  private float calibrationTimer = 0f;

  private List<Vector3> calibrationNosePositions;
  private List<Vector3> calibrationLeftEarPositions;
  private List<Vector3> calibrationRightEarPositions;
  private List<Vector3> calibrationLeftShoulderPositions;
  private List<Vector3> calibrationRightShoulderPositions;
  private List<Vector3> calibrationLeftElbowPositions;
  private List<Vector3> calibrationRightElbowPositions;
  private List<Vector3> calibrationLeftWristPositions;
  private List<Vector3> calibrationRightWristPositions;
  private List<Vector3> calibrationLeftHipPositions;
  private List<Vector3> calibrationRightHipPositions;
  private List<Vector3> calibrationLeftLowerLegPositions;
  private List<Vector3> calibrationRightLowerLegPositions;
  private List<Vector3> calibrationLeftFootPositions;
  private List<Vector3> calibrationRightFootPositions;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(this);
    }
  }
  private void Start()
  {
    InitializeCalibrationLists();
  }
  private void Update()
  {
    var bodyLandmarks = holisticLandmarkController?.GetCurrentPoseLandmarks();

    if (!IsCalibrated() && !IsCalibrating())
    {
      if (IsTPose(bodyLandmarks))
      {
        StartCalibration();
      }
      else
      {
        UIManager.Instance.UpdateInstructionMessage("Lutfen Tpose yapin ve tamamlanana kadar bekleyin");
      }
    }
    if (IsCalibrating())
    {
      ProcessCalibration(bodyLandmarks);
      return;
    }
  }
  void InitializeCalibrationLists()
  {
    calibrationNosePositions = new List<Vector3>();
    calibrationLeftEarPositions = new List<Vector3>();
    calibrationRightEarPositions = new List<Vector3>();
    calibrationLeftShoulderPositions = new List<Vector3>();
    calibrationRightShoulderPositions = new List<Vector3>();
    calibrationLeftElbowPositions = new List<Vector3>();
    calibrationRightElbowPositions = new List<Vector3>();
    calibrationLeftWristPositions = new List<Vector3>();
    calibrationRightWristPositions = new List<Vector3>();
    calibrationLeftHipPositions = new List<Vector3>();
    calibrationRightHipPositions = new List<Vector3>();
    calibrationLeftLowerLegPositions = new List<Vector3>();
    calibrationRightLowerLegPositions = new List<Vector3>();
    calibrationLeftFootPositions = new List<Vector3>();
    calibrationRightFootPositions = new List<Vector3>();
  }
  public void StartCalibration()
  {
    isCalibrating = true;
    calibrationTimer = 0f;
    ClearCalibrationLists();
  }
  private void ClearCalibrationLists()
  {
    calibrationNosePositions.Clear();
    calibrationLeftEarPositions.Clear();
    calibrationRightEarPositions.Clear();
    calibrationLeftShoulderPositions.Clear();
    calibrationRightShoulderPositions.Clear();
    calibrationLeftElbowPositions.Clear();
    calibrationRightElbowPositions.Clear();
    calibrationLeftWristPositions.Clear();
    calibrationRightWristPositions.Clear();
    calibrationLeftHipPositions.Clear();
    calibrationRightHipPositions.Clear();
    calibrationLeftLowerLegPositions.Clear();
    calibrationRightLowerLegPositions.Clear();
    calibrationLeftFootPositions.Clear();
    calibrationRightFootPositions.Clear();
  }
  public void ProcessCalibration(IReadOnlyList<NormalizedLandmark> pose)
  {
    calibrationTimer += Time.deltaTime;

    if (!IsTPose(pose))
    {
      isCalibrating = false;
      calibrationTimer = 0f;
      ClearCalibrationLists();
      return;
    }

    CollectCalibrationData(pose);

    if (calibrationTimer >= calibrationDuration)
    {
      CompleteCalibration();
    }

    float progress = calibrationTimer / calibrationDuration;
    UIManager.Instance.UpdateInstructionMessage($"Kalibrasyon ilerliyor: {progress:P0}"); //percent defaultta virgulden sonra 2
  }
  public bool IsTPose(IReadOnlyList<NormalizedLandmark> pose)
  {
    if (pose==null || pose.Count < 17) return false;

    Vector3 leftShoulder = LandmarkUtils.ConvertLandmarkToVector(pose[11]);
    Vector3 rightShoulder = LandmarkUtils.ConvertLandmarkToVector(pose[12]);
    Vector3 leftWrist = LandmarkUtils.ConvertLandmarkToVector(pose[15]);
    Vector3 rightWrist = LandmarkUtils.ConvertLandmarkToVector(pose[16]);
    Vector3 leftHip = LandmarkUtils.ConvertLandmarkToVector(pose[23]);
    Vector3 rightHip = LandmarkUtils.ConvertLandmarkToVector(pose[24]);

    bool leftAligned = Mathf.Abs(leftWrist.y - leftShoulder.y) < shoulderThreshold;
    bool rightAligned = Mathf.Abs(rightWrist.y - rightShoulder.y) < shoulderThreshold;
    bool leftXaxisControl = leftWrist.x > leftShoulder.x;
    bool rightXaxisControl = rightWrist.x < rightShoulder.x;
    bool verticalControl = Mathf.Abs(((leftShoulder + rightShoulder) / 2).x- ((leftHip+rightHip)/2).x)<0.03?true:false; 

    return leftAligned && rightAligned && leftXaxisControl && rightXaxisControl && verticalControl;
  }
  private void CollectCalibrationData(IReadOnlyList<NormalizedLandmark> pose)
  {
    calibrationNosePositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[0]));
    calibrationLeftEarPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[7]));
    calibrationRightEarPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[8]));
    calibrationLeftShoulderPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[11]));
    calibrationRightShoulderPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[12]));
    calibrationLeftElbowPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[13]));
    calibrationRightElbowPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[14]));
    calibrationLeftWristPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[15]));
    calibrationRightWristPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[16]));
    calibrationLeftHipPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[23]));
    calibrationRightHipPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[24]));
    calibrationLeftLowerLegPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[25]));
    calibrationRightLowerLegPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[26]));
    calibrationLeftFootPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[27]));
    calibrationRightFootPositions.Add(LandmarkUtils.ConvertLandmarkToVector(pose[28]));

  }
  private void CompleteCalibration()
  {
    HumanBodyBoneData.Instance.initialNosePosition = CalculateAverage(calibrationNosePositions);
    HumanBodyBoneData.Instance.initialLeftEar = CalculateAverage(calibrationLeftEarPositions);
    HumanBodyBoneData.Instance.initialRightEar = CalculateAverage(calibrationRightEarPositions);
    HumanBodyBoneData.Instance.initialLeftShoulder = CalculateAverage(calibrationLeftShoulderPositions);
    HumanBodyBoneData.Instance.initialRightShoulder = CalculateAverage(calibrationRightShoulderPositions);
    HumanBodyBoneData.Instance.initialLeftElbow = CalculateAverage(calibrationLeftElbowPositions);
    HumanBodyBoneData.Instance.initialRightElbow = CalculateAverage(calibrationRightElbowPositions);
    HumanBodyBoneData.Instance.initialLeftWrist = CalculateAverage(calibrationLeftWristPositions);
    HumanBodyBoneData.Instance.initialRightWrist = CalculateAverage(calibrationRightWristPositions);
    HumanBodyBoneData.Instance.initialLeftHip = CalculateAverage(calibrationLeftHipPositions);
    HumanBodyBoneData.Instance.initialRightHip = CalculateAverage(calibrationRightHipPositions);
    HumanBodyBoneData.Instance.initialLeftLowerLeg = CalculateAverage(calibrationLeftLowerLegPositions);
    HumanBodyBoneData.Instance.initialRightLowerLeg = CalculateAverage(calibrationRightLowerLegPositions);
    HumanBodyBoneData.Instance.initialLeftFoot = CalculateAverage(calibrationLeftFootPositions);
    HumanBodyBoneData.Instance.initialRightFoot = CalculateAverage(calibrationRightFootPositions);

    isCalibrating = false;
    calibrated = true;

    ClearCalibrationLists();
  }
  private Vector3 CalculateAverage(List<Vector3> positions)
  {
    if (positions.Count == 0) return Vector3.zero;

    Vector3 sum = Vector3.zero;
    foreach (var pos in positions)
    {
      sum += pos;
    }
    return sum/positions.Count;
  }
  public bool IsCalibrated()
  {
    return calibrated;
  }

  public bool IsCalibrating()
  {
    return isCalibrating;
  }

}
