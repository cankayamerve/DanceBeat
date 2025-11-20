using Mediapipe;
using Mediapipe.Unity;
using UnityEngine;

//humanbodybonedatayi script execution orderda once
public class CharacterMovement : MonoBehaviour
{
  [SerializeField] private HolisticLandmarkListAnnotationController holisticLandmarkController;
  private float slerpSpeed = 5f;
  void Start()
  {
    HumanBodyBoneData.Instance.initialHipPosition = HumanBodyBoneData.Instance.HIPS.position;
    HumanBodyBoneData.Instance.initialFootPosition = (HumanBodyBoneData.Instance.LEFTFOOT.position + HumanBodyBoneData.Instance.RIGHTFOOT.position) / 2f;
  }
  private void Update()
  {
    var bodyLandmarks = holisticLandmarkController?.GetCurrentPoseLandmarks();
    var leftHandLandmarks = holisticLandmarkController?.GetCurrentLeftHandLandmarks();
    var rightHandLandmarks = holisticLandmarkController?.GetCurrentRightHandLandmarks();

    if (bodyLandmarks == null || bodyLandmarks.Count < 23) return;
    if (!CalibrationManager.Instance.IsCalibrated()) return;

    //body icin gerekli landmarklar
    var noseLandmark = bodyLandmarks[0];
    var leftEarLandmark = bodyLandmarks[7];
    var rightEarLandmark = bodyLandmarks[8];
    var leftShoulderLandmark = bodyLandmarks[11];
    var rightShoulderLandmark = bodyLandmarks[12];
    var leftElbowLandmark = bodyLandmarks[13];
    var rightElbowLandmark = bodyLandmarks[14];
    var leftWristLandmark = bodyLandmarks[15];
    var rightWristLandmark = bodyLandmarks[16];


    if (LandmarkUtils.IsLandmarkValid(leftEarLandmark) && LandmarkUtils.IsLandmarkValid(rightEarLandmark))
      UpdateHeadRotation(HumanBodyBoneData.Instance.HEAD, leftEarLandmark, rightEarLandmark, noseLandmark, HumanBodyBoneData.Instance.initialLeftEar, HumanBodyBoneData.Instance.initialRightEar);

    if (LandmarkUtils.IsLandmarkValid(leftShoulderLandmark) && LandmarkUtils.IsLandmarkValid(leftElbowLandmark))
      UpdateCalibratedArmRotation(HumanBodyBoneData.Instance.LEFTARM, leftShoulderLandmark, leftElbowLandmark, HumanBodyBoneData.Instance.initialLeftShoulder,
        HumanBodyBoneData.Instance.initialLeftElbow, HumanBodyBoneData.Instance.initialLeftArmRotation);

    if (LandmarkUtils.IsLandmarkValid(leftElbowLandmark) && LandmarkUtils.IsLandmarkValid(leftWristLandmark))
      UpdateCalibratedArmRotation(HumanBodyBoneData.Instance.LEFTELBOW, leftElbowLandmark, leftWristLandmark, HumanBodyBoneData.Instance.initialLeftElbow,
          HumanBodyBoneData.Instance.initialLeftWrist, HumanBodyBoneData.Instance.initialLeftElbowRotation);

    if (LandmarkUtils.IsLandmarkValid(rightShoulderLandmark) && LandmarkUtils.IsLandmarkValid(rightElbowLandmark))
      UpdateCalibratedArmRotation(HumanBodyBoneData.Instance.RIGHTARM, rightShoulderLandmark, rightElbowLandmark, HumanBodyBoneData.Instance.initialRightShoulder,
          HumanBodyBoneData.Instance.initialRightElbow, HumanBodyBoneData.Instance.initialRightArmRotation);

    if (LandmarkUtils.IsLandmarkValid(rightElbowLandmark) && LandmarkUtils.IsLandmarkValid(rightWristLandmark))
      UpdateCalibratedArmRotation(HumanBodyBoneData.Instance.RIGHTELBOW, rightElbowLandmark, rightWristLandmark, HumanBodyBoneData.Instance.initialRightElbow,
          HumanBodyBoneData.Instance.initialRightWrist, HumanBodyBoneData.Instance.initialRightElbowRotation);

    // sol bilek
    if (leftHandLandmarks != null)
    {
      var leftWristLandmarkForHandLandmarks = leftHandLandmarks[0];

      var leftThumbFirstLandmark = leftHandLandmarks[1]; //parmaklar kokten uca dogru numarali
      var leftThumbSecondLandmark = leftHandLandmarks[2];
      var leftThumbThirdLandmark = leftHandLandmarks[3];
      var leftThumbFourthLandmark = leftHandLandmarks[4];

      var leftIndexFirstLandmark = leftHandLandmarks[5];
      var leftIndexSecondLandmark = leftHandLandmarks[6];
      var leftIndexThirdLandmark = leftHandLandmarks[7];
      var leftIndexFourthLandmark = leftHandLandmarks[8];

      var leftMiddleFirstLandmark = leftHandLandmarks[9];
      var leftMiddleSecondLandmark = leftHandLandmarks[10];
      var leftMiddleThirdLandmark = leftHandLandmarks[11];
      var leftMiddleFourthLandmark = leftHandLandmarks[12];

      var leftRingFirstLandmark = leftHandLandmarks[13];
      var leftRingSecondLandmark = leftHandLandmarks[14];
      var leftRingThirdLandmark = leftHandLandmarks[15];
      var leftRingFourthLandmark = leftHandLandmarks[16];

      var leftPinkyFirstLandmark = leftHandLandmarks[17];
      var leftPinkySecondLandmark = leftHandLandmarks[18];
      var leftPinkyThirdLandmark = leftHandLandmarks[19];
      var leftPinkyFourthLandmark = leftHandLandmarks[20];

      //bilek hareketi
      UpdateWristRotation(HumanBodyBoneData.Instance.LEFTWRIST, leftWristLandmarkForHandLandmarks, leftMiddleFirstLandmark, leftIndexFirstLandmark, leftPinkyFirstLandmark, true);
      //parmaklarin hareketi
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDTHUMB[0], leftThumbFirstLandmark, leftThumbSecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDTHUMB[1], leftThumbSecondLandmark, leftThumbThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDTHUMB[2], leftThumbThirdLandmark, leftThumbFourthLandmark);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDINDEX[0], leftIndexFirstLandmark, leftIndexSecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDINDEX[1], leftIndexSecondLandmark, leftIndexThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDINDEX[2], leftIndexThirdLandmark, leftIndexFourthLandmark);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDMIDDLE[0], leftMiddleFirstLandmark, leftMiddleSecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDMIDDLE[1], leftMiddleSecondLandmark, leftMiddleThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDMIDDLE[2], leftMiddleThirdLandmark, leftMiddleFourthLandmark);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDRING[0], leftRingFirstLandmark, leftRingSecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDRING[1], leftRingSecondLandmark, leftRingThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDRING[2], leftRingThirdLandmark, leftRingFourthLandmark);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDPINKY[0], leftPinkyFirstLandmark, leftPinkySecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDPINKY[1], leftPinkySecondLandmark, leftPinkyThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.LEFTHANDPINKY[2], leftPinkyThirdLandmark, leftPinkyFourthLandmark);

    }
    //sag bilek
    if (rightHandLandmarks != null)
    {
      var rightWristLandmarkForHandLandmarks = rightHandLandmarks[0];

      var rightThumbFirstLandmark = rightHandLandmarks[1];
      var rightThumbSecondLandmark = rightHandLandmarks[2];
      var rightThumbThirdLandmark = rightHandLandmarks[3];
      var rightThumbFourthLandmark = rightHandLandmarks[4];

      var rightIndexFirstLandmark = rightHandLandmarks[5];
      var rightIndexSecondLandmark = rightHandLandmarks[6];
      var rightIndexThirdLandmark = rightHandLandmarks[7];
      var rightIndexFourthLandmark = rightHandLandmarks[8];

      var rightMiddleFirstLandmark = rightHandLandmarks[9];
      var rightMiddleSecondLandmark = rightHandLandmarks[10];
      var rightMiddleThirdLandmark = rightHandLandmarks[11];
      var rightMiddleFourthLandmark = rightHandLandmarks[12];

      var rightRingFirstLandmark = rightHandLandmarks[13];
      var rightRingSecondLandmark = rightHandLandmarks[14];
      var rightRingThirdLandmark = rightHandLandmarks[15];
      var rightRingFourthLandmark = rightHandLandmarks[16];

      var rightPinkyFirstLandmark = rightHandLandmarks[17];
      var rightPinkySecondLandmark = rightHandLandmarks[18];
      var rightPinkyThirdLandmark = rightHandLandmarks[19];
      var rightPinkyFourthLandmark = rightHandLandmarks[20];

      UpdateWristRotation(HumanBodyBoneData.Instance.RIGHTWRIST, rightWristLandmarkForHandLandmarks, rightMiddleFirstLandmark, rightIndexFirstLandmark, rightPinkyFirstLandmark, false);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDTHUMB[0], rightThumbFirstLandmark, rightThumbSecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDTHUMB[1], rightThumbSecondLandmark, rightThumbThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDTHUMB[2], rightThumbThirdLandmark, rightThumbFourthLandmark);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDINDEX[0], rightIndexFirstLandmark, rightIndexSecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDINDEX[1], rightIndexSecondLandmark, rightIndexThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDINDEX[2], rightIndexThirdLandmark, rightIndexFourthLandmark);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDMIDDLE[0], rightMiddleFirstLandmark, rightMiddleSecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDMIDDLE[1], rightMiddleSecondLandmark, rightMiddleThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDMIDDLE[2], rightMiddleThirdLandmark, rightMiddleFourthLandmark);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDRING[0], rightRingFirstLandmark, rightRingSecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDRING[1], rightRingSecondLandmark, rightRingThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDRING[2], rightRingThirdLandmark, rightRingFourthLandmark);

      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDPINKY[0], rightPinkyFirstLandmark, rightPinkySecondLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDPINKY[1], rightPinkySecondLandmark, rightPinkyThirdLandmark);
      UpdateFingerJointRotation(HumanBodyBoneData.Instance.RIGHTHANDPINKY[2], rightPinkyThirdLandmark, rightPinkyFourthLandmark);

    }

    if (LandmarkUtils.IsLandmarkValid(leftShoulderLandmark) && LandmarkUtils.IsLandmarkValid(rightShoulderLandmark))
    {
      UpdateWaistRotation(HumanBodyBoneData.Instance.SPINE, leftShoulderLandmark, rightShoulderLandmark, HumanBodyBoneData.Instance.initialLeftShoulder, HumanBodyBoneData.Instance.initialRightShoulder);
    }

    //bacaklar icin
    var leftHipLandmark = bodyLandmarks[23];
    var rightHipLandmark = bodyLandmarks[24];
    var leftKneeLandmark = bodyLandmarks[25];
    var rightKneeLandmark = bodyLandmarks[26];
    var leftAnkleLandmark = bodyLandmarks[27];
    var rightAnkleLandmark = bodyLandmarks[28];
    var leftHeelLandmark = bodyLandmarks[29];
    var rightHeelLandmark = bodyLandmarks[30];
    var leftFootIndex = bodyLandmarks[31];
    var rightFootIndex = bodyLandmarks[32];


    if (LandmarkUtils.IsLandmarkValid(leftHipLandmark) && LandmarkUtils.IsLandmarkValid(leftKneeLandmark) && LandmarkUtils.IsLandmarkValid(leftAnkleLandmark))
    {
      UpdateCalibratedLegRotation(HumanBodyBoneData.Instance.LEFTUPPERLEG, HumanBodyBoneData.Instance.LEFTLOWERLEG, leftHipLandmark, leftKneeLandmark, leftAnkleLandmark,
      HumanBodyBoneData.Instance.initialLeftHip, HumanBodyBoneData.Instance.initialLeftLowerLeg, HumanBodyBoneData.Instance.initialLeftFoot,
      HumanBodyBoneData.Instance.initialLeftUpperLegRotation, HumanBodyBoneData.Instance.initialLeftLowerLegRotation);
    }

    if (LandmarkUtils.IsLandmarkValid(rightHipLandmark) && LandmarkUtils.IsLandmarkValid(rightKneeLandmark) && LandmarkUtils.IsLandmarkValid(rightAnkleLandmark))
    {
      UpdateCalibratedLegRotation(HumanBodyBoneData.Instance.RIGHTUPPERLEG, HumanBodyBoneData.Instance.RIGHTLOWERLEG, rightHipLandmark, rightKneeLandmark, rightAnkleLandmark,
      HumanBodyBoneData.Instance.initialRightHip, HumanBodyBoneData.Instance.initialRightLowerLeg, HumanBodyBoneData.Instance.initialRightFoot,
      HumanBodyBoneData.Instance.initialRightUpperLegRotation, HumanBodyBoneData.Instance.initialRightLowerLegRotation);
    }

    //var initialFootPosAvg = HumanBodyBoneData.Instance.initialLeftFoot;
    var initialFootPosAvg = (HumanBodyBoneData.Instance.initialLeftFoot + HumanBodyBoneData.Instance.initialRightFoot) / 2f;
    //UpdateHipMovement(HumanBodyBoneData.Instance.HIPS, leftAnkleLandmark, rightAnkleLandmark, initialFootPosAvg, HumanBodyBoneData.Instance.initialHipPosition);
    UpdateBodyRotation(HumanBodyBoneData.Instance.HIPS, HumanBodyBoneData.Instance.SPINE, leftHipLandmark, rightHipLandmark, HumanBodyBoneData.Instance.hipsRotation);
  }

  private void UpdateFingerJointRotation(Transform modelTargetJoint, NormalizedLandmark fromJointLandmark, NormalizedLandmark toJointLandmark)
  {
    if (modelTargetJoint == null) return;

    Vector3 fromJointVector = LandmarkUtils.ConvertLandmarkToVector(fromJointLandmark);
    Vector3 toJointVector = LandmarkUtils.ConvertLandmarkToVector(toJointLandmark);
    Vector3 directionVector = toJointVector - fromJointVector;

    Vector3 localDirectionVector = modelTargetJoint.parent != null ? modelTargetJoint.parent.InverseTransformDirection(directionVector) : directionVector;
    Quaternion netRotation = Quaternion.FromToRotation(Vector3.down, localDirectionVector);

    modelTargetJoint.localRotation = Quaternion.Slerp(modelTargetJoint.localRotation, netRotation, Time.deltaTime * slerpSpeed);
  }
  private void UpdateWristRotation(Transform modelWristBone, NormalizedLandmark wristJointLandmark, NormalizedLandmark indexJointLandmark, NormalizedLandmark pinkyJointLandmark, NormalizedLandmark middleJointLandmark, bool isLeft)
  {
    if (modelWristBone == null) return;

    Vector3 wristVector = LandmarkUtils.ConvertLandmarkToVector(wristJointLandmark);
    Vector3 indexVector = LandmarkUtils.ConvertLandmarkToVector(indexJointLandmark);
    Vector3 pinkyVector = LandmarkUtils.ConvertLandmarkToVector(pinkyJointLandmark);
    Vector3 middleVector = LandmarkUtils.ConvertLandmarkToVector(middleJointLandmark);

    Vector3 palmRight = indexVector - pinkyVector;
    if (isLeft) palmRight = -palmRight;

    Vector3 palmForward = middleVector - wristVector;
    Vector3 palmUp = Vector3.Cross(palmRight, palmForward);
    palmForward = Vector3.Cross(palmUp, palmRight);

    Quaternion targetRotation = Quaternion.LookRotation(palmForward, palmUp);
    Quaternion localTargetRotation = Quaternion.Inverse(modelWristBone.parent.rotation) * targetRotation;

    Quaternion handCorrectionRotation = Quaternion.Euler(-90f, 0f, 0f); // burasi avuc icinin dogru yone bakmasi icin duzenleme
    localTargetRotation *= handCorrectionRotation;
    modelWristBone.localRotation = Quaternion.Slerp(modelWristBone.localRotation, localTargetRotation, Time.deltaTime * slerpSpeed);
  }

  //void UpdateHipMovement(Transform hip, NormalizedLandmark currentModelsLeftFootPosLandmark, NormalizedLandmark currentModelsRightFootLandmark, Vector3 initialFootPos, Vector3 intitialHipPos)
  //{
  //  var currentModelsLeftFootPos = LandmarkUtils.ConvertLandmarkToVector(currentModelsLeftFootPosLandmark);
  //  var currentModelsRightFootPos = LandmarkUtils.ConvertLandmarkToVector(currentModelsRightFootLandmark);

  //  var leftFootGroundDistance = Mathf.Abs(currentModelsLeftFootPos.y - initialFootPos.y);
  //  var rightFootGroundDistance = Mathf.Abs(currentModelsRightFootPos.y - initialFootPos.y);
  //  var closestFootToGround = Mathf.Min(leftFootGroundDistance, rightFootGroundDistance);

  //  hip.position = Vector3.Lerp(hip.position, new Vector3(hip.position.x, intitialHipPos.y - closestFootToGround, hip.position.z), Time.deltaTime * slerpSpeed);
  //}
  private void UpdateBodyRotation(Transform modelHip, Transform modelSpine, NormalizedLandmark leftHipLandmark, NormalizedLandmark rightHipLandmark, Quaternion prevHipRotation)
  {
    if (modelHip == null || HumanBodyBoneData.Instance.SPINE == null) return;

    Vector3 leftShoulderVector = LandmarkUtils.ConvertLandmarkToVector(leftHipLandmark);
    Vector3 rightShoulderVector = LandmarkUtils.ConvertLandmarkToVector(rightHipLandmark);
    Vector3 shoulderDir = rightShoulderVector - leftShoulderVector;

    Vector3 forward = Vector3.Cross(Vector3.up, shoulderDir);
    Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);

    modelHip.rotation = Quaternion.Slerp(modelHip.rotation, targetRotation, Time.deltaTime * slerpSpeed);

    Quaternion rotationDifference = Quaternion.Inverse(prevHipRotation) * modelHip.rotation;
    Quaternion targetSpineRotation = Quaternion.Inverse(rotationDifference) * HumanBodyBoneData.Instance.SPINE.rotation;
    modelSpine.rotation = Quaternion.Slerp(modelSpine.rotation, targetSpineRotation, Time.deltaTime * 0.75f);
    prevHipRotation = modelHip.rotation;
  }

  //alt kisim kalibrasyonlu kisim
  private void UpdateCalibratedArmRotation(Transform modelTargetJoint, NormalizedLandmark fromJointLandmark, NormalizedLandmark toJointLandmark, Vector3 initialFromPosition, Vector3 initialToPosition, Quaternion initialJointRotation)
  {
    if (modelTargetJoint == null) return;

    Vector3 currentFromVector = LandmarkUtils.ConvertLandmarkToVector(fromJointLandmark);
    Vector3 currentToVector = LandmarkUtils.ConvertLandmarkToVector(toJointLandmark);
    Vector3 initialDirectionVector = (initialToPosition - initialFromPosition).normalized;
    Vector3 currentDirectionVector = (currentToVector - currentFromVector).normalized;

    Quaternion deltaRotation = Quaternion.FromToRotation(initialDirectionVector, currentDirectionVector);

    if (modelTargetJoint.parent != null)
    { // burda var olan donusumun once worlde gore sonra locale gore olmasi icin once parentin rotasyonunun inversi var sonra rotasyon sonra tekrar locale cevirme
      deltaRotation = Quaternion.Inverse(modelTargetJoint.parent.rotation) * deltaRotation * modelTargetJoint.parent.rotation;
    }
    Quaternion targetRotation = deltaRotation * initialJointRotation;
    modelTargetJoint.localRotation = Quaternion.Slerp(modelTargetJoint.localRotation, targetRotation, Time.deltaTime * slerpSpeed);
  }

  private void UpdateWaistRotation(Transform modelSpine, NormalizedLandmark leftShoulderLandmark, NormalizedLandmark rightShoulderLandmark, Vector3 initialLShoulder, Vector3 initialRShoulder)
  {
    if (modelSpine == null) return;

    Vector3 leftShoulderVector = LandmarkUtils.ConvertLandmarkToVector(leftShoulderLandmark);
    Vector3 rightShoulderVector = LandmarkUtils.ConvertLandmarkToVector(rightShoulderLandmark);

    Vector3 shoulderDir = rightShoulderVector - leftShoulderVector;
    Vector3 currentShoulderVectorHorizontal = new Vector3(shoulderDir.x, 0, shoulderDir.z);
    Vector3 calibratedShoulderVectorHorizontal = new Vector3(initialRShoulder.x - initialLShoulder.x, 0, initialRShoulder.z - initialLShoulder.z);

    float yAngle = Vector3.SignedAngle(calibratedShoulderVectorHorizontal, currentShoulderVectorHorizontal, Vector3.up);
    float zAngle = ((shoulderDir.y / shoulderDir.magnitude) - ((initialRShoulder.y - initialLShoulder.y) / (initialRShoulder - initialLShoulder).magnitude)) * Mathf.Rad2Deg;

    Vector3 shoulderCenter = (leftShoulderVector + rightShoulderVector) / 2f;
    Vector3 initialCenter = (initialLShoulder + initialRShoulder) / 2f;
    float xAngle = shoulderCenter.z - initialCenter.z; //one arkaya egilme

    Quaternion targetRotation = Quaternion.Euler(xAngle, yAngle, zAngle); // z eksiydi 
    modelSpine.rotation = Quaternion.Slerp(modelSpine.rotation, targetRotation, Time.deltaTime * slerpSpeed);
  }

  private void UpdateHeadRotation(Transform headTransform, NormalizedLandmark leftEar, NormalizedLandmark rightEar, NormalizedLandmark nose, Vector3 initialLeftEar, Vector3 initialRightEar)
  {
    if (headTransform == null) return;

    Vector3 leftEarVector = LandmarkUtils.ConvertLandmarkToVector(leftEar);
    Vector3 rightEarVector = LandmarkUtils.ConvertLandmarkToVector(rightEar);
    Vector3 noseVector = LandmarkUtils.ConvertLandmarkToVector(nose);

    Vector3 initialEarDir = new Vector3(initialRightEar.x - initialLeftEar.x, 0, initialRightEar.z - initialLeftEar.z);
    Vector3 currentEarDir = new Vector3(rightEarVector.x - leftEarVector.x, 0, rightEarVector.z - leftEarVector.z);
    float yawAngle = Vector3.SignedAngle(initialEarDir, currentEarDir, Vector3.up); //sag-sol

    float yAxisDifference = noseVector.y - HumanBodyBoneData.Instance.initialNosePosition.y;
    float pitchAngle = yAxisDifference * 400f;  //yukari-asagi

    float earHeightDiff = (rightEarVector.y - leftEarVector.y);
    float rollAngle = earHeightDiff * 500f; //egme

    Quaternion finalRotation = Quaternion.Euler(pitchAngle, yawAngle, -rollAngle);
    headTransform.localRotation = Quaternion.Slerp(headTransform.localRotation, finalRotation, Time.deltaTime * slerpSpeed);
  }

  private float leftKneeOffset = 0f;
  private float rightKneeOffset = 0f;
  private bool kneeCalibrated = false;
  private void UpdateCalibratedLegRotation(Transform upperLeg, Transform lowerLeg, NormalizedLandmark hipPosLandmark, NormalizedLandmark kneePosLandmark, NormalizedLandmark anklePosLandmark,
    Vector3 initialHipPos, Vector3 initialKneePos, Vector3 initialAnklePos, Quaternion initialUpperLegRotation, Quaternion initialLowerLegRotation)
  {
    if (upperLeg == null || lowerLeg == null) return;

    var hipPos = LandmarkUtils.ConvertLandmarkToVector(hipPosLandmark);
    var kneePos = LandmarkUtils.ConvertLandmarkToVector(kneePosLandmark);
    var anklePos = LandmarkUtils.ConvertLandmarkToVector(anklePosLandmark);

    Vector3 currentUpperLegVector = (kneePos - hipPos).normalized;
    Vector3 currentLowerLegVector = (anklePos - kneePos).normalized;
    Vector3 initialUpperLegVector = (initialKneePos - initialHipPos).normalized;
    Vector3 initialLowerLegVector = (initialAnklePos - initialKneePos).normalized;

    Quaternion upperLegRotation = Quaternion.FromToRotation(initialUpperLegVector, currentUpperLegVector);

    upperLeg.localRotation = Quaternion.Slerp(upperLeg.localRotation, upperLegRotation * initialUpperLegRotation, Time.deltaTime * slerpSpeed);

    float kneeAngle = Vector3.Angle(currentUpperLegVector, currentLowerLegVector);
    Vector3 cross = Vector3.Cross(currentUpperLegVector, currentLowerLegVector);
    float dir = Vector3.Dot(cross, upperLeg.right); // local eksene göre yön kontrolü
    if (dir < 0)
      kneeAngle = -kneeAngle;

    if (!kneeCalibrated)
    {
      if (upperLeg == HumanBodyBoneData.Instance.LEFTUPPERLEG)
      {
        leftKneeOffset = kneeAngle;
        rightKneeOffset = kneeAngle;

      }
      else
        rightKneeOffset = kneeAngle;

      if (leftKneeOffset != 0 && rightKneeOffset != 0)
        kneeCalibrated = true;
    }

    if (upperLeg == HumanBodyBoneData.Instance.LEFTUPPERLEG)
      kneeAngle -= leftKneeOffset;
    else
      kneeAngle -= rightKneeOffset;

    Quaternion kneeRotation = Quaternion.AngleAxis(kneeAngle, Vector3.right);
    lowerLeg.localRotation = Quaternion.Slerp(lowerLeg.localRotation, initialLowerLegRotation * kneeRotation, Time.deltaTime * slerpSpeed);
  }

}
