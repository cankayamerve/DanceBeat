using UnityEngine;

public class HumanBodyBoneData:MonoBehaviour
{
  public static HumanBodyBoneData Instance;
  [SerializeField] private Animator animator;

  //initial position degerleri calibrasyon sonrasinda toplanan değerlerinin averagina gore
  public Vector3 initialNosePosition { get; set; }
  public Vector3 initialHipPosition { get; set; }
  public Vector3 initialFootPosition { get; set; }
  public Vector3 initialLeftShoulder { get; set; }
  public Vector3 initialRightShoulder { get; set; }
  public Vector3 initialLeftElbow { get; set; }
  public Vector3 initialRightElbow { get; set; }
  public Vector3 initialLeftWrist { get; set; }
  public Vector3 initialRightWrist { get; set; }
  public Vector3 initialLeftHip { get; set; }
  public Vector3 initialRightHip { get; set; }
  public Vector3 initialLeftEar { get; set; }
  public Vector3 initialRightEar { get; set; }
  public Vector3 initialLeftLowerLeg { get; set; }
  public Vector3 initialRightLowerLeg { get; set; }
  public Vector3 initialLeftFoot { get; set; }
  public Vector3 initialRightFoot { get; set; }

  //modelin vucut jointlerinin transformlari
  public Transform HIPS { get; set; }
  public Transform LEFTSHOULDER { get; set; }
  public Transform RIGHTSHOULDER { get; set; }
  public Transform LEFTARM { get; set; }
  public Transform RIGHTARM { get; set; }
  public Transform LEFTELBOW { get; set; }
  public Transform RIGHTELBOW { get; set; }
  public Transform LEFTWRIST { get; set; }
  public Transform RIGHTWRIST { get; set; }
  public Transform LEFTHIP { get; set; }
  public Transform RIGHTHIP { get; set; }
  public Transform HEAD { get; set; }
  public Transform UPPERCHEST { get; set; }
  public Transform CHEST { get; set; }
  public Transform SPINE { get; set; }
  public Transform LEFTUPPERLEG { get; set; }
  public Transform LEFTLOWERLEG { get; set; }
  public Transform LEFTFOOT { get; set; }
  public Transform RIGHTUPPERLEG { get; set; }
  public Transform RIGHTLOWERLEG { get; set; }
  public Transform RIGHTFOOT { get; set; }
  public Transform[] LEFTHANDTHUMB { get; set; } //proximaldan distala dogru indexler articak
  public Transform[] RIGHTHANDTHUMB { get; set; }
  public Transform[] LEFTHANDINDEX { get; set; }
  public Transform[] RIGHTHANDINDEX { get; set; }
  public Transform[] LEFTHANDMIDDLE { get; set; }
  public Transform[] RIGHTHANDMIDDLE { get; set; }
  public Transform[] LEFTHANDRING { get; set; }
  public Transform[] RIGHTHANDRING { get; set; }
  public Transform[] LEFTHANDPINKY { get; set; }
  public Transform[] RIGHTHANDPINKY { get; set; }
  public Quaternion hipsRotation { get; set; } 


  public Quaternion initialHipRotation { get; set; }
  //public Quaternion initialLeftShoulderRotation { get; set; }
  //public Quaternion initialRightShoulderRotation { get; set; }
  public Quaternion initialLeftArmRotation { get; set; }
  public Quaternion initialRightArmRotation { get; set; }
  public Quaternion initialLeftElbowRotation { get; set; }
  public Quaternion initialRightElbowRotation { get; set; }
  //public Quaternion initialLeftWristRotation { get; set; }
  //public Quaternion initialRightWristRotation { get; set; }
  public Quaternion initialLeftUpperLegRotation { get; set; }
  public Quaternion initialRightUpperLegRotation { get; set; }
  public Quaternion initialLeftLowerLegRotation { get; set; }
  public Quaternion initialRightLowerLegRotation { get; set; }
  //public Quaternion initialLeftFootRotation { get; set; }
  //public Quaternion initialRightFootRotation { get; set; }
  //public Quaternion initialHeadRotation { get; set; }
  void Awake()
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

  void Start()
  {
    InitializeBodyPartTransforms();
    InitializeBodyPartRotations();
    InitializeFingerTransforms();
  }

  void InitializeBodyPartTransforms()
  {
    HIPS = animator.GetBoneTransform(HumanBodyBones.Hips);
    LEFTSHOULDER = animator.GetBoneTransform(HumanBodyBones.LeftShoulder);
    RIGHTSHOULDER = animator.GetBoneTransform(HumanBodyBones.RightShoulder);
    LEFTARM = animator.GetBoneTransform(HumanBodyBones.LeftUpperArm);
    RIGHTARM = animator.GetBoneTransform(HumanBodyBones.RightUpperArm);
    LEFTELBOW = animator.GetBoneTransform(HumanBodyBones.LeftLowerArm);
    RIGHTELBOW = animator.GetBoneTransform(HumanBodyBones.RightLowerArm);
    LEFTWRIST = animator.GetBoneTransform(HumanBodyBones.LeftHand);
    RIGHTWRIST = animator.GetBoneTransform(HumanBodyBones.RightHand);
    HEAD = animator.GetBoneTransform(HumanBodyBones.Head);
    UPPERCHEST = animator.GetBoneTransform(HumanBodyBones.UpperChest);
    CHEST = animator.GetBoneTransform(HumanBodyBones.Chest);
    SPINE = animator.GetBoneTransform(HumanBodyBones.Spine);
    LEFTUPPERLEG = animator.GetBoneTransform(HumanBodyBones.LeftUpperLeg);
    LEFTLOWERLEG = animator.GetBoneTransform(HumanBodyBones.LeftLowerLeg);
    LEFTFOOT = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
    RIGHTUPPERLEG = animator.GetBoneTransform(HumanBodyBones.RightUpperLeg);
    RIGHTLOWERLEG = animator.GetBoneTransform(HumanBodyBones.RightLowerLeg);
    RIGHTFOOT = animator.GetBoneTransform(HumanBodyBones.RightFoot);

  }
  void InitializeBodyPartRotations()
  {
    //initialModelsLeftFootRotation = LEFTFOOT.rotation;
    //initialModelsRightFootRotation = RIGHTFOOT.rotation;
    hipsRotation = HIPS.rotation;

    initialHipRotation = HIPS.localRotation;
    //initialLeftShoulderRotation = LEFTSHOULDER.localRotation;
    //initialRightShoulderRotation = RIGHTSHOULDER.localRotation;
    initialLeftArmRotation = LEFTARM.localRotation;
    initialRightArmRotation = RIGHTARM.localRotation;
    initialLeftElbowRotation = LEFTELBOW.localRotation;
    initialRightElbowRotation = RIGHTELBOW.localRotation;
    //initialLeftWristRotation = LEFTWRIST.localRotation;
    //initialRightWristRotation = RIGHTWRIST.localRotation;
    initialLeftUpperLegRotation = LEFTUPPERLEG.localRotation;
    initialRightUpperLegRotation = RIGHTUPPERLEG.localRotation;
    initialLeftLowerLegRotation = LEFTLOWERLEG.localRotation;
    initialRightLowerLegRotation = RIGHTLOWERLEG.localRotation;
    //initialLeftFootRotation = LEFTFOOT.localRotation;
    //initialRightFootRotation = RIGHTFOOT.localRotation;
    //initialHeadRotation = HEAD.localRotation;
  }
  void InitializeFingerTransforms()
  {
    //sol el
    LEFTHANDTHUMB = new Transform[3];
    LEFTHANDTHUMB[0] = animator.GetBoneTransform(HumanBodyBones.LeftThumbProximal);
    LEFTHANDTHUMB[1] = animator.GetBoneTransform(HumanBodyBones.LeftThumbIntermediate);
    LEFTHANDTHUMB[2] = animator.GetBoneTransform(HumanBodyBones.LeftThumbDistal);

    LEFTHANDINDEX = new Transform[3];
    LEFTHANDINDEX[0] = animator.GetBoneTransform(HumanBodyBones.LeftIndexProximal);
    LEFTHANDINDEX[1] = animator.GetBoneTransform(HumanBodyBones.LeftIndexIntermediate);
    LEFTHANDINDEX[2] = animator.GetBoneTransform(HumanBodyBones.LeftIndexDistal);

    LEFTHANDMIDDLE = new Transform[3];
    LEFTHANDMIDDLE[0] = animator.GetBoneTransform(HumanBodyBones.LeftMiddleProximal);
    LEFTHANDMIDDLE[1] = animator.GetBoneTransform(HumanBodyBones.LeftMiddleIntermediate);
    LEFTHANDMIDDLE[2] = animator.GetBoneTransform(HumanBodyBones.LeftMiddleDistal);

    LEFTHANDRING = new Transform[3];
    LEFTHANDRING[0] = animator.GetBoneTransform(HumanBodyBones.LeftRingProximal);
    LEFTHANDRING[1] = animator.GetBoneTransform(HumanBodyBones.LeftRingIntermediate);
    LEFTHANDRING[2] = animator.GetBoneTransform(HumanBodyBones.LeftRingDistal);

    LEFTHANDPINKY = new Transform[3];
    LEFTHANDPINKY[0] = animator.GetBoneTransform(HumanBodyBones.LeftLittleProximal);
    LEFTHANDPINKY[1] = animator.GetBoneTransform(HumanBodyBones.LeftLittleIntermediate);
    LEFTHANDPINKY[2] = animator.GetBoneTransform(HumanBodyBones.LeftLittleDistal);

    //sag el
    RIGHTHANDTHUMB = new Transform[3];
    RIGHTHANDTHUMB[0] = animator.GetBoneTransform(HumanBodyBones.RightThumbProximal);
    RIGHTHANDTHUMB[1] = animator.GetBoneTransform(HumanBodyBones.RightThumbIntermediate);
    RIGHTHANDTHUMB[2] = animator.GetBoneTransform(HumanBodyBones.RightThumbDistal);

    RIGHTHANDINDEX = new Transform[3];
    RIGHTHANDINDEX[0] = animator.GetBoneTransform(HumanBodyBones.RightIndexProximal);
    RIGHTHANDINDEX[1] = animator.GetBoneTransform(HumanBodyBones.RightIndexIntermediate);
    RIGHTHANDINDEX[2] = animator.GetBoneTransform(HumanBodyBones.RightIndexDistal);

    RIGHTHANDMIDDLE = new Transform[3];
    RIGHTHANDMIDDLE[0] = animator.GetBoneTransform(HumanBodyBones.RightMiddleProximal);
    RIGHTHANDMIDDLE[1] = animator.GetBoneTransform(HumanBodyBones.RightMiddleIntermediate);
    RIGHTHANDMIDDLE[2] = animator.GetBoneTransform(HumanBodyBones.RightMiddleDistal);

    RIGHTHANDRING = new Transform[3];
    RIGHTHANDRING[0] = animator.GetBoneTransform(HumanBodyBones.RightRingProximal);
    RIGHTHANDRING[1] = animator.GetBoneTransform(HumanBodyBones.RightRingIntermediate);
    RIGHTHANDRING[2] = animator.GetBoneTransform(HumanBodyBones.RightRingDistal);

    RIGHTHANDPINKY = new Transform[3];
    RIGHTHANDPINKY[0] = animator.GetBoneTransform(HumanBodyBones.RightLittleProximal);
    RIGHTHANDPINKY[1] = animator.GetBoneTransform(HumanBodyBones.RightLittleIntermediate);
    RIGHTHANDPINKY[2] = animator.GetBoneTransform(HumanBodyBones.RightLittleDistal);
  }
}
