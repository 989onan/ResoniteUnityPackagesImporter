﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityPackageImporter.FrooxEngineRepresentation.GameObjectTypes;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using static FrooxEngine.SlotGizmo;

namespace UnityPackageImporter.FrooxEngineRepresentation
{

    class UnityNodeTypeResolver : INodeTypeResolver
    {
        private const string UnityTagPrefix = "tag:unity3d.com,2011:";
        public ulong anchor = 0; //this is referenced externally after every node parse, in the UnityStructureImporter class
        public bool Resolve(NodeEvent nodeEvent, ref Type currentType)
        {
            if (nodeEvent.Tag != null && nodeEvent.Tag.Value.StartsWith(UnityTagPrefix))
            {
                int unityObjectId;

                if (!int.TryParse(nodeEvent.Tag.Value.Replace(UnityTagPrefix, ""), out unityObjectId))
                {
                    UnityPackageImporter.Msg("This is a test, the anchor is: \"" + anchor.ToString() + "\" returning 2: false");
                    return false;
                }
                anchor = ulong.Parse(nodeEvent.Anchor.Value);
                UnityPackageImporter.Msg("This is a test, the anchor is: \"" + anchor.ToString() + "\" returning: true");
                currentType = typeof(UnityEngineObjectWrapper);
                return true;


            }
            else
            {
                UnityPackageImporter.Msg("This is a test, the anchor is: \"" + anchor.ToString() + "\" returning: false");
                return false;
            }
            
        }
    }

    class UnityObjectMapping
    {
        public static Dictionary<int, string> IdToTypeName = new Dictionary<int, string>()
        {
            { 1,  "GameObject" },
            { 2,  "Component" },
            { 3,  "LevelGameManager" },
            { 4,  "Transform" },
            { 5,  "TimeManager" },
            { 6,  "GlobalGameManager" },
            { 8,  "Behaviour" },
            { 9,  "GameManager" },
            { 11,  "AudioManager" },
            { 12,  "ParticleAnimator" },
            { 13,  "InputManager" },
            { 15,  "EllipsoidParticleEmitter" },
            { 17,  "Pipeline" },
            { 18,  "EditorExtension" },
            { 19,  "Physics2DSettings" },
            { 20,  "Camera" },
            { 21,  "Material" },
            { 23,  "MeshRenderer" },
            { 25,  "Renderer" },
            { 26,  "ParticleRenderer" },
            { 27,  "Texture" },
            { 28,  "Texture2D" },
            { 29,  "SceneSettings" },
            { 30,  "GraphicsSettings" },
            { 33,  "MeshFilter" },
            { 41,  "OcclusionPortal" },
            { 43,  "Mesh" },
            { 45,  "Skybox" },
            { 47,  "QualitySettings" },
            { 48,  "Shader" },
            { 49,  "TextAsset" },
            { 50,  "Rigidbody2D" },
            { 51,  "Physics2DManager" },
            { 53,  "Collider2D" },
            { 54,  "Rigidbody" },
            { 55,  "PhysicsManager" },
            { 56,  "Collider" },
            { 57,  "Joint" },
            { 58,  "CircleCollider2D" },
            { 59,  "HingeJoint" },
            { 60,  "PolygonCollider2D" },
            { 61,  "BoxCollider2D" },
            { 62,  "PhysicsMaterial2D" },
            { 64,  "MeshCollider" },
            { 65,  "BoxCollider" },
            { 66,  "SpriteCollider2D" },
            { 68,  "EdgeCollider2D" },
            { 72,  "ComputeShader" },
            { 74,  "AnimationClip" },
            { 75,  "ConstantForce" },
            { 76,  "WorldParticleCollider" },
            { 78,  "TagManager" },
            { 81,  "AudioListener" },
            { 82,  "AudioSource" },
            { 83,  "AudioClip" },
            { 84,  "RenderTexture" },
            { 87,  "MeshParticleEmitter" },
            { 88,  "ParticleEmitter" },
            { 89,  "Cubemap" },
            { 90,  "Avatar" },
            { 91,  "AnimatorController" },
            { 92,  "GUILayer" },
            { 93,  "RuntimeAnimatorController" },
            { 94,  "ScriptMapper" },
            { 95,  "Animator" },
            { 96,  "TrailRenderer" },
            { 98,  "DelayedCallManager" },
            { 102,  "TextMesh" },
            { 104,  "RenderSettings" },
            { 108,  "Light" },
            { 109,  "CGProgram" },
            { 110,  "BaseAnimationTrack" },
            { 111,  "Animation" },
            { 114,  "MonoBehaviour" },
            { 115,  "MonoScript" },
            { 116,  "MonoManager" },
            { 117,  "Texture3D" },
            { 118,  "NewAnimationTrack" },
            { 119,  "Projector" },
            { 120,  "LineRenderer" },
            { 121,  "Flare" },
            { 122,  "Halo" },
            { 123,  "LensFlare" },
            { 124,  "FlareLayer" },
            { 125,  "HaloLayer" },
            { 126,  "NavMeshAreas" },
            { 127,  "HaloManager" },
            { 128,  "Font" },
            { 129,  "PlayerSettings" },
            { 130,  "NamedObject" },
            { 131,  "GUITexture" },
            { 132,  "GUIText" },
            { 133,  "GUIElement" },
            { 134,  "PhysicMaterial" },
            { 135,  "SphereCollider" },
            { 136,  "CapsuleCollider" },
            { 137,  "SkinnedMeshRenderer" },
            { 138,  "FixedJoint" },
            { 140,  "RaycastCollider" },
            { 141,  "BuildSettings" },
            { 142,  "AssetBundle" },
            { 143,  "CharacterController" },
            { 144,  "CharacterJoint" },
            { 145,  "SpringJoint" },
            { 146,  "WheelCollider" },
            { 147,  "ResourceManager" },
            { 148,  "NetworkView" },
            { 149,  "NetworkManager" },
            { 150,  "PreloadData" },
            { 152,  "MovieTexture" },
            { 153,  "ConfigurableJoint" },
            { 154,  "TerrainCollider" },
            { 155,  "MasterServerInterface" },
            { 156,  "TerrainData" },
            { 157,  "LightmapSettings" },
            { 158,  "WebCamTexture" },
            { 159,  "EditorSettings" },
            { 160,  "InteractiveCloth" },
            { 161,  "ClothRenderer" },
            { 162,  "EditorUserSettings" },
            { 163,  "SkinnedCloth" },
            { 164,  "AudioReverbFilter" },
            { 165,  "AudioHighPassFilter" },
            { 166,  "AudioChorusFilter" },
            { 167,  "AudioReverbZone" },
            { 168,  "AudioEchoFilter" },
            { 169,  "AudioLowPassFilter" },
            { 170,  "AudioDistortionFilter" },
            { 171,  "SparseTexture" },
            { 180,  "AudioBehaviour" },
            { 181,  "AudioFilter" },
            { 182,  "WindZone" },
            { 183,  "Cloth" },
            { 184,  "SubstanceArchive" },
            { 185,  "ProceduralMaterial" },
            { 186,  "ProceduralTexture" },
            { 191,  "OffMeshLink" },
            { 192,  "OcclusionArea" },
            { 193,  "Tree" },
            { 194,  "NavMeshObsolete" },
            { 195,  "NavMeshAgent" },
            { 196,  "NavMeshSettings" },
            { 197,  "LightProbesLegacy" },
            { 198,  "ParticleSystem" },
            { 199,  "ParticleSystemRenderer" },
            { 200,  "ShaderVariantCollection" },
            { 205,  "LODGroup" },
            { 206,  "BlendTree" },
            { 207,  "Motion" },
            { 208,  "NavMeshObstacle" },
            { 210,  "TerrainInstance" },
            { 212,  "SpriteRenderer" },
            { 213,  "Sprite" },
            { 214,  "CachedSpriteAtlas" },
            { 215,  "ReflectionProbe" },
            { 216,  "ReflectionProbes" },
            { 220,  "LightProbeGroup" },
            { 221,  "AnimatorOverrideController" },
            { 222,  "CanvasRenderer" },
            { 223,  "Canvas" },
            { 224,  "RectTransform" },
            { 225,  "CanvasGroup" },
            { 226,  "BillboardAsset" },
            { 227,  "BillboardRenderer" },
            { 228,  "SpeedTreeWindAsset" },
            { 229,  "AnchoredJoint2D" },
            { 230,  "Joint2D" },
            { 231,  "SpringJoint2D" },
            { 232,  "DistanceJoint2D" },
            { 233,  "HingeJoint2D" },
            { 234,  "SliderJoint2D" },
            { 235,  "WheelJoint2D" },
            { 238,  "NavMeshData" },
            { 240,  "AudioMixer" },
            { 241,  "AudioMixerController" },
            { 243,  "AudioMixerGroupController" },
            { 244,  "AudioMixerEffectController" },
            { 245,  "AudioMixerSnapshotController" },
            { 246,  "PhysicsUpdateBehaviour2D" },
            { 247,  "ConstantForce2D" },
            { 248,  "Effector2D" },
            { 249,  "AreaEffector2D" },
            { 250,  "PointEffector2D" },
            { 251,  "PlatformEffector2D" },
            { 252,  "SurfaceEffector2D" },
            { 258,  "LightProbes" },
            { 271,  "SampleClip" },
            { 272,  "AudioMixerSnapshot" },
            { 273,  "AudioMixerGroup" },
            { 290,  "AssetBundleManifest" },
            { 1001,  "Prefab" },
            { 1002,  "EditorExtensionImpl" },
            { 1003,  "AssetImporter" },
            { 1004,  "AssetDatabase" },
            { 1005,  "Mesh3DSImporter" },
            { 1006,  "TextureImporter" },
            { 1007,  "ShaderImporter" },
            { 1008,  "ComputeShaderImporter" },
            { 1011,  "AvatarMask" },
            { 1020,  "AudioImporter" },
            { 1026,  "HierarchyState" },
            { 1027,  "GUIDSerializer" },
            { 1028,  "AssetMetaData" },
            { 1029,  "DefaultAsset" },
            { 1030,  "DefaultImporter" },
            { 1031,  "TextScriptImporter" },
            { 1032,  "SceneAsset" },
            { 1034,  "NativeFormatImporter" },
            { 1035,  "MonoImporter" },
            { 1037,  "AssetServerCache" },
            { 1038,  "LibraryAssetImporter" },
            { 1040,  "ModelImporter" },
            { 1041,  "FBXImporter" },
            { 1042,  "TrueTypeFontImporter" },
            { 1044,  "MovieImporter" },
            { 1045,  "EditorBuildSettings" },
            { 1046,  "DDSImporter" },
            { 1048,  "InspectorExpandedState" },
            { 1049,  "AnnotationManager" },
            { 1050,  "PluginImporter" },
            { 1051,  "EditorUserBuildSettings" },
            { 1052,  "PVRImporter" },
            { 1053,  "ASTCImporter" },
            { 1054,  "KTXImporter" },
            { 1101,  "AnimatorStateTransition" },
            { 1102,  "AnimatorState" },
            { 1105,  "HumanTemplate" },
            { 1107,  "AnimatorStateMachine" },
            { 1108,  "PreviewAssetType" },
            { 1109,  "AnimatorTransition" },
            { 1110,  "SpeedTreeImporter" },
            { 1111,  "AnimatorTransitionBase" },
            { 1112,  "SubstanceImporter" },
            { 1113,  "LightmapParameters" },
            { 1120,  "LightmapSnapshot"}
        };
    }
}
