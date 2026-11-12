using System; using System.IO; using System.Text; using System.Globalization; using System.Collections.Generic; using UnityEngine;
// DeepSeek (蓝色大肥鱼) runtime for Chill with You: sets bone localPositions to the model's proportions every LateUpdate.
public static class WhaleFingerProbe {
 struct E{public string n;public Vector3 p;public E(string a,Vector3 b){n=a;p=b;}}
 static E[] entries={
  new E("Character_Head",new Vector3(-0.0867461f,0.0090387f,-0.0000000f)),
  new E("Character_LeftArm",new Vector3(-0.0887892f,0.0163812f,-0.0626286f)),
  new E("Character_LeftFoot",new Vector3(-0.4282050f,0.0000002f,0.0000000f)),
  new E("Character_LeftForeArm",new Vector3(-0.2509113f,-0.0060930f,-0.0213687f)),
  new E("Character_LeftHand",new Vector3(-0.2304508f,0.0129039f,-0.0118089f)),
  new E("Character_LeftHandIndex1",new Vector3(-0.0648678f,0.0105511f,0.0263767f)),
  new E("Character_LeftHandIndex2",new Vector3(-0.0288586f,-0.0011412f,-0.0004204f)),
  new E("Character_LeftHandIndex3",new Vector3(-0.0180446f,0.0012193f,-0.0007123f)),
  new E("Character_LeftHandMiddle1",new Vector3(-0.0746258f,0.0112702f,0.0107775f)),
  new E("Character_LeftHandMiddle2",new Vector3(-0.0326292f,-0.0012083f,-0.0005241f)),
  new E("Character_LeftHandMiddle3",new Vector3(-0.0212128f,-0.0001049f,-0.0006425f)),
  new E("Character_LeftHandPinky1",new Vector3(-0.0556403f,0.0080407f,-0.0194300f)),
  new E("Character_LeftHandPinky2",new Vector3(-0.0247705f,-0.0002146f,-0.0003490f)),
  new E("Character_LeftHandPinky3",new Vector3(-0.0200560f,0.0007262f,-0.0005979f)),
  new E("Character_LeftHandRing1",new Vector3(-0.0568184f,0.0139645f,-0.0035803f)),
  new E("Character_LeftHandRing2",new Vector3(-0.0376994f,-0.0006948f,-0.0005951f)),
  new E("Character_LeftHandRing3",new Vector3(-0.0267379f,0.0006627f,-0.0008154f)),
  new E("Character_LeftHandThumb1",new Vector3(-0.0179043f,-0.0016521f,0.0241114f)),
  new E("Character_LeftHandThumb2",new Vector3(-0.0316930f,-0.0008355f,0.0004779f)),
  new E("Character_LeftHandThumb3",new Vector3(-0.0252209f,-0.0003611f,0.0030284f)),
  new E("Character_LeftLeg",new Vector3(-0.4246192f,0.0000000f,0.0000000f)),
  new E("Character_LeftShoulder",new Vector3(-0.1362376f,0.0623483f,0.0148771f)),
  new E("Character_LeftUpLeg",new Vector3(0.0759082f,0.0081945f,0.0871196f)),
  new E("Character_Neck",new Vector3(-0.1564939f,-0.0044713f,-0.0000000f)),
  new E("Character_RightArm",new Vector3(0.0887890f,-0.0163811f,0.0626288f)),
  new E("Character_RightFoot",new Vector3(-0.4282051f,0.0000000f,0.0000000f)),
  new E("Character_RightForeArm",new Vector3(-0.2508901f,-0.0067988f,0.0214046f)),
  new E("Character_RightHand",new Vector3(-0.2304365f,0.0128917f,0.0120966f)),
  new E("Character_RightHandIndex1",new Vector3(-0.0648653f,0.0105401f,-0.0263863f)),
  new E("Character_RightHandIndex2",new Vector3(-0.0288641f,-0.0011408f,0.0004227f)),
  new E("Character_RightHandIndex3",new Vector3(-0.0180409f,0.0012181f,0.0007169f)),
  new E("Character_RightHandMiddle1",new Vector3(-0.0746251f,0.0112647f,-0.0107883f)),
  new E("Character_RightHandMiddle2",new Vector3(-0.0326349f,-0.0012080f,0.0005274f)),
  new E("Character_RightHandMiddle3",new Vector3(-0.0212094f,-0.0001046f,0.0006483f)),
  new E("Character_RightHandPinky1",new Vector3(-0.0556422f,0.0080466f,0.0194220f)),
  new E("Character_RightHandPinky2",new Vector3(-0.0247705f,-0.0002128f,0.0003505f)),
  new E("Character_RightHandPinky3",new Vector3(-0.0200558f,0.0007265f,0.0006036f)),
  new E("Character_RightHandRing1",new Vector3(-0.0568180f,0.0139651f,0.0035817f)),
  new E("Character_RightHandRing2",new Vector3(-0.0376994f,-0.0006977f,0.0005993f)),
  new E("Character_RightHandRing3",new Vector3(-0.0267378f,0.0006617f,0.0008225f)),
  new E("Character_RightHandThumb1",new Vector3(-0.0179022f,-0.0016611f,-0.0241124f)),
  new E("Character_RightHandThumb2",new Vector3(-0.0316929f,-0.0008362f,-0.0004810f)),
  new E("Character_RightHandThumb3",new Vector3(-0.0252206f,-0.0003651f,-0.0030301f)),
  new E("Character_RightLeg",new Vector3(-0.4246193f,0.0000000f,0.0000000f)),
  new E("Character_RightShoulder",new Vector3(-0.1362376f,0.0623483f,-0.0148771f)),
  new E("Character_RightUpLeg",new Vector3(0.0759082f,0.0081945f,-0.0871196f)),
  new E("Character_Spine",new Vector3(-0.0553100f,0.0024292f,-0.0000000f)),
  new E("Character_Spine1",new Vector3(-0.0614305f,-0.0019553f,0.0000000f)),
  new E("Character_Spine2",new Vector3(-0.0790482f,-0.0025160f,0.0000000f))
 };
 static Animator lpActor; static List<KeyValuePair<Transform,Vector3>> targets=new List<KeyValuePair<Transform,Vector3>>(); static float nextScan; static bool lpFailed;
 static void Scan(Animator a){targets.Clear();var map=new Dictionary<string,Vector3>();foreach(var e in entries)map[e.n]=e.p;
  foreach(var tr in a.GetComponentsInChildren<Transform>(true)){Vector3 p;if(tr.parent!=null&&tr.parent.name==tr.name)continue;if(map.TryGetValue(tr.name,out p)){targets.Add(new KeyValuePair<Transform,Vector3>(tr,p));if(!origLP.ContainsKey(tr))origLP[tr]=tr.localPosition;}}}
 static SkinnedMeshRenderer srcFace,dstBody; static int[] map; static float nextFind;
 static void CopyFace(Animator a){try{if(srcFace==null||dstBody==null||!dstBody.gameObject.activeInHierarchy||Time.unscaledTime>=nextFind){nextFind=Time.unscaledTime+2f;srcFace=null;dstBody=null;
   foreach(var r in a.GetComponentsInChildren<SkinnedMeshRenderer>(true)){if(!r.gameObject.activeInHierarchy||r.sharedMesh==null)continue;if(r.name=="Face")srcFace=r;else if((r.name=="Body"||r.name=="Body1")&&r.sharedMesh.blendShapeCount>0)dstBody=r;}
   map=null;if(dstBody!=null)Outline(dstBody);if(srcFace!=null&&dstBody!=null){var fm=srcFace.sharedMesh;var bm=dstBody.sharedMesh;map=new int[fm.blendShapeCount];for(int i=0;i<map.Length;i++)map[i]=bm.GetBlendShapeIndex(fm.GetBlendShapeName(i));}}
  if(map==null)return;for(int i=0;i<map.Length;i++)if(map[i]>=0)dstBody.SetBlendShapeWeight(map[i],srcFace.GetBlendShapeWeight(i));
  {var bm=dstBody.sharedMesh;int ib=bm.GetBlendShapeIndex("blendShape1.Eye_blink"),il=bm.GetBlendShapeIndex("blendShape1.Eye_blink_L"),ir=bm.GetBlendShapeIndex("blendShape1.Eye_blink_R");   // BLINKCAP
   if(ib>=0&&il>=0&&ir>=0){float b=dstBody.GetBlendShapeWeight(ib),l=dstBody.GetBlendShapeWeight(il),r=dstBody.GetBlendShapeWeight(ir);float cl=Mathf.Clamp(b+l,0,100),cr=Mathf.Clamp(b+r,0,100),m=Mathf.Min(cl,cr);   // shared closure via Eye_blink (keeps the lash arc), one-eye extra via L/R
    dstBody.SetBlendShapeWeight(ib,m);dstBody.SetBlendShapeWeight(il,cl-m);dstBody.SetBlendShapeWeight(ir,cr-m);
    int hl=bm.GetBlendShapeIndex("blendShape1.WhaleEyeHide_L"),hr=bm.GetBlendShapeIndex("blendShape1.WhaleEyeHide_R");if(hl>=0)dstBody.SetBlendShapeWeight(hl,Hide(cl));if(hr>=0)dstBody.SetBlendShapeWeight(hr,Hide(cr));}}
  LookCap(dstBody);   // LOOKCAP: the game drives Eye_look_* above 100 and stacks two directions; the iris then slid under the eye white
  Drape(dstBody);
  if(OVR!=null){var bm=dstBody.sharedMesh;for(int i=0;i<bm.blendShapeCount;i++)dstBody.SetBlendShapeWeight(i,0);foreach(var kv in OVR){int k=bm.GetBlendShapeIndex("blendShape1."+kv.Key);if(k>=0)dstBody.SetBlendShapeWeight(k,kv.Value);}}}catch(Exception){}}
 static float Hide(float c){float t=Mathf.Clamp01((c-55f)/40f);return t*t*(3f-2f*t)*100f;}   // EYEHIDE: only near full closure
 static int[] lkI; static Mesh lkM;
 static void LookCap(SkinnedMeshRenderer r){var bm=r.sharedMesh;if(bm!=lkM){lkM=bm;lkI=new int[4];var nm=new[]{"left","right","up","down"};for(int k=0;k<4;k++)lkI[k]=bm.GetBlendShapeIndex("blendShape1.Eye_look_"+nm[k]);}
  if(lkI[0]<0||lkI[1]<0||lkI[2]<0||lkI[3]<0)return;float h=r.GetBlendShapeWeight(lkI[0])-r.GetBlendShapeWeight(lkI[1]),v=r.GetBlendShapeWeight(lkI[2])-r.GetBlendShapeWeight(lkI[3]);
  h*=0.6f;v*=0.6f;float m=Mathf.Sqrt(h*h+v*v);if(m>60f){h*=60f/m;v*=60f/m;}
  r.SetBlendShapeWeight(lkI[0],Mathf.Max(h,0));r.SetBlendShapeWeight(lkI[1],Mathf.Max(-h,0));r.SetBlendShapeWeight(lkI[2],Mathf.Max(v,0));r.SetBlendShapeWeight(lkI[3],Mathf.Max(-v,0));}
 static Material outlineMat;
 static void Outline(SkinnedMeshRenderer r){try{var mesh=r.sharedMesh;var ms=r.sharedMaterials;if(mesh==null||mesh.subMeshCount!=3||ms.Length!=2||ms[1]==null)return;
   if(outlineMat==null){outlineMat=new Material(ms[1]);outlineMat.name="WhaleOutline";outlineMat.SetFloat("_Cull",1f);
    foreach(var pn in new[]{"_UseRim","_UseBacklight","_UseMatCap","_UseMatCap2nd","_UseEmission","_UseEmission2nd","_UseGlitter","_UseReflection","_UseShadow"})if(outlineMat.HasProperty(pn))outlineMat.SetFloat(pn,0f);}
   r.sharedMaterials=new Material[]{ms[0],ms[1],outlineMat};}catch(Exception){}}
 static int dL=-2,dR=-2; static Mesh dMesh;
 static void Drape(SkinnedMeshRenderer r){if(lpActor==null)return;var bm=r.sharedMesh;if(bm!=dMesh){dMesh=bm;dL=bm.GetBlendShapeIndex("blendShape1.WhaleSitDrape_L");dR=bm.GetBlendShapeIndex("blendShape1.WhaleSitDrape_R");}
  if(dL<0||dR<0)return;var down=-lpActor.transform.up;
  r.SetBlendShapeWeight(dL,Pitch(HumanBodyBones.LeftUpperLeg,HumanBodyBones.LeftLowerLeg,down));r.SetBlendShapeWeight(dR,Pitch(HumanBodyBones.RightUpperLeg,HumanBodyBones.RightLowerLeg,down));}
 static float Pitch(HumanBodyBones a,HumanBodyBones b,Vector3 down){var ta=lpActor.GetBoneTransform(a);var tb=lpActor.GetBoneTransform(b);if(ta==null||tb==null)return 0f;
  float ang=Vector3.Angle(tb.position-ta.position,down);return Mathf.Clamp01((ang-25f)/45f)*100f;}
 static Dictionary<Transform,Vector3> origLP=new Dictionary<Transform,Vector3>();
 static readonly HumanBodyBones[] ARM={HumanBodyBones.LeftUpperArm,HumanBodyBones.LeftLowerArm,HumanBodyBones.LeftHand,HumanBodyBones.RightUpperArm,HumanBodyBones.RightLowerArm,HumanBodyBones.RightHand};
 static Transform[] armT=new Transform[6]; static Vector3[] tgtH=new Vector3[2],tgtE=new Vector3[2]; static Quaternion[] tgtR=new Quaternion[2];
 static void SolveArm(Transform ua,Transform fa,Transform hd,Vector3 T,Vector3 PE,Quaternion HR){   // aim retarget: upper arm toward the original elbow, forearm toward the original wrist, original hand orientation
  Vector3 A=ua.position,E=fa.position;if((E-A).sqrMagnitude<1e-8f||(PE-A).sqrMagnitude<1e-8f)return;
  ua.rotation=Quaternion.FromToRotation(E-A,PE-A)*ua.rotation;Vector3 E3=fa.position,H3=hd.position;if((H3-E3).sqrMagnitude<1e-8f||(T-E3).sqrMagnitude<1e-8f)return;
  fa.rotation=Quaternion.FromToRotation(H3-E3,T-E3)*fa.rotation;hd.rotation=HR;}
 public static Dictionary<string,float> OVR;
 static void OnCam(Camera c){if(lpActor!=null&&c!=null&&c.name=="MainCamera")CopyFace(lpActor);}
 static void OnRP(UnityEngine.Rendering.ScriptableRenderContext x,Camera c){Voice();OnCam(c);}
 static bool camHooked;
 static int applyFrame=-1;
 static void Apply(Animator a){if(Time.frameCount==applyFrame)return;applyFrame=Time.frameCount;ApplyCore(a);Hair(a);Voice();}
 // STANDALONE ENTRY: the installer lists this DLL in ScriptingAssemblies.json and Init in RuntimeInitializeOnLoads.json, so no game code is modified.
 // Init inserts Step into the player loop right after all LateUpdates (before skinned meshes are updated); Step finds the HeroineService and runs on its animator.
 public static void Init(){try{var loop=UnityEngine.LowLevel.PlayerLoop.GetCurrentPlayerLoop();var subs=loop.subSystemList;
   for(int i=0;i<subs.Length;i++){if(subs[i].type!=typeof(UnityEngine.PlayerLoop.PreLateUpdate))continue;var l=new List<UnityEngine.LowLevel.PlayerLoopSystem>(subs[i].subSystemList);
    int k=l.FindIndex(x=>x.type==typeof(UnityEngine.PlayerLoop.PreLateUpdate.ScriptRunBehaviourLateUpdate));
    l.Insert(k<0?l.Count:k+1,new UnityEngine.LowLevel.PlayerLoopSystem{type=typeof(WhaleFingerProbe),updateDelegate=Step});subs[i].subSystemList=l.ToArray();}
   loop.subSystemList=subs;UnityEngine.LowLevel.PlayerLoop.SetPlayerLoop(loop);}catch(Exception){}}
 static Type hsType; static System.Reflection.FieldInfo hsAnim; static Behaviour hs; static float hsNext; static bool stepFailed;
 static void Step(){if(stepFailed)return;try{
   if(hsType==null){hsType=Type.GetType("Bulbul.HeroineService, Assembly-CSharp");if(hsType==null){stepFailed=true;return;}hsAnim=hsType.GetField("_animator",System.Reflection.BindingFlags.Instance|System.Reflection.BindingFlags.NonPublic|System.Reflection.BindingFlags.Public);}
   if(hs==null&&Time.unscaledTime>=hsNext){hsNext=Time.unscaledTime+1f;hs=UnityEngine.Object.FindObjectOfType(hsType) as Behaviour;}
   if(hs==null||!hs.isActiveAndEnabled||hsAnim==null)return;var a=hsAnim.GetValue(hs) as Animator;if(a!=null&&a.isHuman)Apply(a);}catch(Exception){stepFailed=true;}}
 static void ApplyCore(Animator a){if(!camHooked){camHooked=true;try{Camera.onPreCull+=OnCam;UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering+=OnRP;}catch(Exception){}}if(lpFailed)return;try{if(a!=lpActor||Time.unscaledTime>=nextScan){lpActor=a;nextScan=Time.unscaledTime+2f;Scan(a);}
  for(int i=0;i<targets.Count;i++){var tr=targets[i].Key;Vector3 o;if(tr!=null&&origLP.TryGetValue(tr,out o))tr.localPosition=o;}
  bool ik=true;for(int k=0;k<6;k++){armT[k]=a.GetBoneTransform(ARM[k]);if(armT[k]==null)ik=false;}
  if(ik)for(int s=0;s<2;s++){tgtH[s]=armT[s*3+2].position;tgtE[s]=armT[s*3+1].position;tgtR[s]=armT[s*3+2].rotation;}
  for(int i=0;i<targets.Count;i++){var tr=targets[i].Key;if(tr!=null)tr.localPosition=targets[i].Value;}
  if(ik&&!noIK)for(int s=0;s<2;s++)SolveArm(armT[s*3],armT[s*3+1],armT[s*3+2],tgtH[s],tgtE[s],tgtR[s]);}catch(Exception){lpFailed=true;}}
 public static bool noIK;
 // HAIR: the mesh has extra bone slots 56.. (one per hair chain bone of the source rig) that the scene binds to the Head. Here each gets its own transform under the Head,
 // driven by a small verlet chain: follows the head with lag, hangs toward gravity, sways a little, limited to a cone around the rigid pose.
 const int HK=43; static readonly int[] hPar={-1,0,1,2,-1,4,5,6,-1,8,9,10,-1,12,13,14,15,15,17,12,19,20,-1,22,23,24,25,-1,27,-1,29,-1,31,32,-1,34,35,36,37,38,-1,40,41}; static readonly Vector3[] hPiv={new Vector3(-0.000000f,0.024291f,-0.147036f),new Vector3(0.000000f,-0.091719f,-0.194299f),new Vector3(0.000000f,-0.198420f,-0.240130f),new Vector3(0.000000f,-0.301540f,-0.287394f),new Vector3(-0.000000f,0.024291f,-0.147036f),new Vector3(-0.071037f,-0.076843f,-0.226250f),new Vector3(-0.061520f,-0.255292f,-0.270311f),new Vector3(-0.060681f,-0.364273f,-0.345436f),new Vector3(-0.000000f,0.024291f,-0.147036f),new Vector3(0.016878f,-0.244843f,-0.174089f),new Vector3(-0.021498f,-0.350939f,-0.204139f),new Vector3(-0.034818f,-0.447887f,-0.209939f),new Vector3(-0.063203f,0.097214f,-0.118985f),new Vector3(-0.101004f,-0.007049f,-0.153427f),new Vector3(-0.122331f,-0.170708f,-0.167308f),new Vector3(-0.194159f,-0.245854f,-0.175313f),new Vector3(-0.280361f,-0.326147f,-0.183651f),new Vector3(-0.280361f,-0.326147f,-0.183651f),new Vector3(-0.399878f,-0.387088f,-0.183651f),new Vector3(-0.101004f,-0.007049f,-0.153427f),new Vector3(-0.185809f,-0.088403f,-0.153427f),new Vector3(-0.286392f,-0.178631f,-0.153427f),new Vector3(0.067485f,0.097214f,-0.034400f),new Vector3(0.139469f,0.003601f,-0.079758f),new Vector3(0.248011f,-0.113773f,-0.122616f),new Vector3(0.302307f,-0.164809f,-0.104683f),new Vector3(0.322225f,-0.251093f,-0.070758f),new Vector3(-0.084846f,-0.000369f,0.009228f),new Vector3(-0.077385f,-0.038910f,0.044454f),new Vector3(0.084846f,-0.000369f,0.009228f),new Vector3(0.077385f,-0.038910f,0.044454f),new Vector3(0.114310f,0.040446f,-0.160833f),new Vector3(0.174498f,-0.078290f,-0.219526f),new Vector3(0.224597f,-0.180401f,-0.215547f),new Vector3(0.078189f,0.045761f,-0.162934f),new Vector3(0.097391f,-0.037097f,-0.239708f),new Vector3(0.128767f,-0.137991f,-0.309942f),new Vector3(0.054422f,-0.224135f,-0.352154f),new Vector3(-0.009369f,-0.331249f,-0.355808f),new Vector3(0.061406f,-0.436507f,-0.305453f),new Vector3(0.009528f,0.268970f,-0.041474f),new Vector3(0.066608f,0.294124f,-0.062012f),new Vector3(0.111594f,0.267519f,-0.035105f)},hTail={new Vector3(0.000000f,-0.091719f,-0.194299f),new Vector3(0.000000f,-0.198420f,-0.240130f),new Vector3(0.000000f,-0.301540f,-0.287394f),new Vector3(0.000000f,-0.419097f,-0.324517f),new Vector3(-0.071037f,-0.076843f,-0.226250f),new Vector3(-0.061520f,-0.255292f,-0.270311f),new Vector3(-0.060681f,-0.364273f,-0.345436f),new Vector3(-0.039481f,-0.489427f,-0.314218f),new Vector3(0.016878f,-0.244843f,-0.174089f),new Vector3(-0.021498f,-0.350939f,-0.204139f),new Vector3(-0.034818f,-0.447887f,-0.209939f),new Vector3(-0.107720f,-0.538239f,-0.228388f),new Vector3(-0.101004f,-0.007049f,-0.153427f),new Vector3(-0.122331f,-0.170708f,-0.167308f),new Vector3(-0.194159f,-0.245854f,-0.175313f),new Vector3(-0.280361f,-0.326147f,-0.183651f),new Vector3(-0.467085f,-0.322465f,-0.188668f),new Vector3(-0.399878f,-0.387088f,-0.183651f),new Vector3(-0.358461f,-0.473471f,-0.183651f),new Vector3(-0.185809f,-0.088403f,-0.153427f),new Vector3(-0.286392f,-0.178631f,-0.153427f),new Vector3(-0.288364f,-0.289568f,-0.153427f),new Vector3(0.139469f,0.003601f,-0.079758f),new Vector3(0.248011f,-0.113773f,-0.122616f),new Vector3(0.302307f,-0.164809f,-0.104683f),new Vector3(0.322225f,-0.251093f,-0.070758f),new Vector3(0.291271f,-0.324066f,-0.066068f),new Vector3(-0.077385f,-0.038910f,0.044454f),new Vector3(-0.072421f,-0.071234f,0.078850f),new Vector3(0.077385f,-0.038910f,0.044454f),new Vector3(0.072421f,-0.071234f,0.078850f),new Vector3(0.174498f,-0.078290f,-0.219526f),new Vector3(0.224597f,-0.180401f,-0.215547f),new Vector3(0.219357f,-0.274463f,-0.271469f),new Vector3(0.097391f,-0.037097f,-0.239708f),new Vector3(0.128767f,-0.137991f,-0.309942f),new Vector3(0.054422f,-0.224135f,-0.352154f),new Vector3(-0.009369f,-0.331249f,-0.355808f),new Vector3(0.061406f,-0.436507f,-0.305453f),new Vector3(0.102259f,-0.483288f,-0.283205f),new Vector3(0.066608f,0.294124f,-0.062012f),new Vector3(0.111594f,0.267519f,-0.035105f),new Vector3(0.098534f,0.235594f,-0.036259f)};static readonly Vector3 hUp=new Vector3(-0.000000f,1.000000f,0.000000f); static readonly float[] hStiff={0.2342f,0.0988f,0.03f,0.03f,0.2342f,0.1176f,0.03f,0.03f,0.2342f,0.03f,0.03f,0.03f,0.25f,0.2055f,0.032f,0.03f,0.03f,0.03f,0.03f,0.2055f,0.1029f,0.0304f,0.25f,0.2165f,0.0732f,0.034f,0.03f,0.2125f,0.1673f,0.2125f,0.1673f,0.244f,0.1158f,0.0302f,0.2462f,0.1696f,0.0502f,0.03f,0.03f,0.03f,0.12f,0.12f,0.12f},hMax={5.86f,21.87f,30f,30f,5.86f,19.64f,30f,30f,5.86f,30f,30f,30f,4f,9.26f,29.76f,30f,30f,30f,30f,9.26f,21.39f,29.95f,4f,7.96f,24.9f,29.53f,30f,8.43f,13.77f,8.43f,13.77f,4.71f,19.87f,29.98f,4.45f,13.5f,27.61f,30f,30f,30f,14f,14f,14f},hLoose={0.0716f,0.6874f,1f,1f,0.0716f,0.6017f,1f,1f,0.0716f,1f,1f,1f,0f,0.2023f,0.9908f,1f,1f,1f,1f,0.2023f,0.6687f,0.9982f,0f,0.1523f,0.8038f,0.9819f,1f,0.1703f,0.3759f,0.1703f,0.3759f,0.0275f,0.6102f,0.9991f,0.0172f,0.3654f,0.908f,1f,1f,1f,0f,0f,0f};
 class HS{public Transform head;public Transform[] t=new Transform[HK];public Vector3[] c=new Vector3[HK],pv=new Vector3[HK];public bool init;public Vector3 last;}
 static Dictionary<Transform,HS> hairs=new Dictionary<Transform,HS>(); static float hNext,hAcc; static bool hFailed; public static float hairGravity=0.6f,hairWind=0.003f,hairDrag=0.12f; public static float hairMaxDeg,hairAvgDeg; public static int hairCount;
 static void HairBind(Animator a){foreach(var r in a.GetComponentsInChildren<SkinnedMeshRenderer>(true)){if(r.name!="Body"&&r.name!="Body1")continue;var bs=r.bones;if(bs.Length!=56+HK||bs[5]==null||bs[56]!=bs[5])continue;
   HS h;if(!hairs.TryGetValue(bs[5],out h)){h=new HS();h.head=bs[5];for(int i=0;i<HK;i++){var g=new GameObject("WhaleHair"+i);g.layer=bs[5].gameObject.layer;var t=g.transform;t.SetParent(bs[5],false);h.t[i]=t;}hairs[bs[5]]=h;}
   for(int i=0;i<HK;i++)bs[56+i]=h.t[i];r.bones=bs;}}
 static void HairPose(HS h,int i,Transform p,Vector3 piv,Vector3 rest){var t=h.t[i];t.rotation=Quaternion.FromToRotation(rest-piv,h.c[i]-piv)*p.rotation;t.position=piv-(t.TransformPoint(hPiv[i])-t.position);}
 static void HairStep(HS h,Transform root,float tm,bool sim){float deg=0,mx=0;Quaternion lv=Quaternion.FromToRotation(h.head.TransformDirection(hUp),Vector3.up);   // undoes the head's tilt: where the rigid hair would hang if the head were upright
  for(int i=0;i<HK;i++){var p=hPar[i]<0?h.head:h.t[hPar[i]];Vector3 piv=p.TransformPoint(hPiv[i]),rest=p.TransformPoint(hTail[i]),rd=rest-piv;float len=rd.magnitude;if(len<1e-6f)continue;
   if(!h.init){h.c[i]=h.pv[i]=rest;}
   else if(sim){Vector3 tgt=piv+Vector3.Slerp(rd,(lv*h.head.TransformDirection(hTail[i]-hPiv[i])).normalized*len,hairGravity*hLoose[i]);
    Vector3 w=(root.right*(Mathf.PerlinNoise(tm*0.31f+piv.x*1.3f,piv.y*1.3f)-0.5f)+root.forward*(Mathf.PerlinNoise(piv.x*1.3f+7.1f,tm*0.27f+piv.y*1.3f)-0.5f))*(hairWind*len*hLoose[i]);
    Vector3 c=h.c[i],nx=c+(c-h.pv[i])*(1f-hairDrag)+(tgt-c)*hStiff[i]+w;h.pv[i]=c;Vector3 d=nx-piv;if(d.sqrMagnitude<1e-10f)d=rd;d=Vector3.RotateTowards(rd,d.normalized*len,hMax[i]*Mathf.Deg2Rad,0f).normalized*len;h.c[i]=piv+d;}
   HairPose(h,i,p,piv,rest);float an=Vector3.Angle(rd,h.c[i]-piv);deg+=an;if(an>mx)mx=an;}
  h.init=true;hairMaxDeg=mx;hairAvgDeg=deg/HK;}
 static void Hair(Animator a){if(hFailed)return;try{if(Time.unscaledTime>=hNext){hNext=Time.unscaledTime+2f;HairBind(a);var dead=new List<Transform>();foreach(var kv in hairs)if(kv.Key==null)dead.Add(kv.Key);foreach(var k in dead)hairs.Remove(k);}
   hairCount=hairs.Count;if(hairs.Count==0)return;hAcc+=Mathf.Min(Time.deltaTime,0.1f);int n=0;while(hAcc>=1f/60f&&n<4){hAcc-=1f/60f;n++;}if(hAcc>1f/60f)hAcc=0f;
   foreach(var h in hairs.Values){if(h.head==null||!h.head.gameObject.activeInHierarchy)continue;if(h.init&&(h.head.position-h.last).sqrMagnitude>0.09f)h.init=false;h.last=h.head.position;   // teleport / scene cut: start from rest
    if(!h.init||n==0)HairStep(h,a.transform,Time.time,false);for(int s=0;s<n;s++)HairStep(h,a.transform,Time.time,true);}}catch(Exception){hFailed=true;}}
 // VOICE: the heroine's lines are replaced by Feiyu-RVC conversions. When any AudioSource is playing a clip named like a WAV in <Data>/WhaleVoice,
 // that WAV (48 kHz PCM16, same sample count) takes over at the same sample within the frame, so the AudioPlayer's finish check never sees a gap. No folder = no change.
 static HashSet<string> vNames; static Dictionary<string,AudioClip> vCache=new Dictionary<string,AudioClip>(); static List<string> vOrder=new List<string>(); static AudioSource[] vSrc=new AudioSource[0];
 static float vNext; static int vFrame=-1; static bool vFailed; public static int voiceSwaps; public static string voiceLast="";
 static void Voice(){if(vFailed||Time.frameCount==vFrame)return;vFrame=Time.frameCount;try{
  if(vNames==null){vNames=new HashSet<string>();var d=Path.Combine(Application.dataPath,"WhaleVoice");if(Directory.Exists(d))foreach(var f in Directory.GetFiles(d,"*.wav"))vNames.Add(Path.GetFileNameWithoutExtension(f));}
  if(vNames.Count==0)return;if(Time.unscaledTime>=vNext){vNext=Time.unscaledTime+1f;vSrc=UnityEngine.Object.FindObjectsOfType<AudioSource>();}
  foreach(var s in vSrc){if(s==null||!s.isPlaying)continue;var c=s.clip;if(c==null||!vNames.Contains(c.name))continue;AudioClip w;if(vCache.TryGetValue(c.name,out w)&&w==c)continue;
   w=VClip(c.name);if(w==null){vNames.Remove(c.name);continue;}int ts=s.timeSamples;s.clip=w;s.timeSamples=Mathf.Clamp(ts,0,Mathf.Max(0,w.samples-1));s.Play();voiceSwaps++;voiceLast=c.name;}}catch(Exception){vFailed=true;}}
 static AudioClip VClip(string n){AudioClip w;if(vCache.TryGetValue(n,out w)&&w!=null)return w;
  var b=File.ReadAllBytes(Path.Combine(Path.Combine(Application.dataPath,"WhaleVoice"),n+".wav"));int p=12,ch=1,sr=48000,bits=0,off=-1,len=0;
  while(p+8<=b.Length){string id=Encoding.ASCII.GetString(b,p,4);int l=BitConverter.ToInt32(b,p+4);if(id=="fmt "){ch=BitConverter.ToInt16(b,p+10);sr=BitConverter.ToInt32(b,p+12);bits=BitConverter.ToInt16(b,p+22);}else if(id=="data"){off=p+8;len=Math.Min(l,b.Length-off);break;}p+=8+l+(l&1);}
  if(off<0||bits!=16||ch<1)return null;int ns=len/2/ch;if(ns<1)return null;var d=new float[ns*ch];for(int i=0;i<d.Length;i++)d[i]=BitConverter.ToInt16(b,off+2*i)/32768f;
  w=AudioClip.Create(n,ns,ch,sr,false);w.SetData(d,0);vCache[n]=w;vOrder.Add(n);
  while(vOrder.Count>40){var o=vOrder[0];vOrder.RemoveAt(0);AudioClip oc;if(vCache.TryGetValue(o,out oc)){vCache.Remove(o);if(oc!=null)UnityEngine.Object.Destroy(oc);}}return w;}
 public static void Tick(Animator a){if(a==null||!a.isHuman)return;Apply(a);}
}
