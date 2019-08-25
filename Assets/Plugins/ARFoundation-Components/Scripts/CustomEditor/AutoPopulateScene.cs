// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents.Editor
{

    public static class AutoPopulateScene
    {

        internal const string cubePrefabPath = "Assets/Cube.prefab";

        internal static void SetupARFoundation()
        {

            if (!GameObject.Find("AR Session Origin"))
            {

                EditorApplication.ExecuteMenuItem("GameObject/XR/AR Session Origin");

            }

            var mainCamera = GameObject.Find("Main Camera");

            if (mainCamera)
            {

                Object.DestroyImmediate(mainCamera);

            }

            if (!GameObject.Find("AR Session"))
            {

                EditorApplication.ExecuteMenuItem("GameObject/XR/AR Session");

            }

            var sessionOrigin = GameObject.Find("AR Session Origin");

            sessionOrigin.AddOrGetComponent<ARRaycastManager>();

        }

        internal static void SetupARFoundationComponents()
        {

            var camera = GameObject.Find("AR Camera");
            var cameraManager = camera.AddOrGetComponent<ARCameraManager>();
            cameraManager.lightEstimationMode = LightEstimationMode.AmbientIntensity;

            var light = GameObject.Find("Directional Light");
            var lightEstimation = light.AddOrGetComponent<ARLightEstimation>();
            lightEstimation.cameraManager = cameraManager;

        }

        internal static void SetupARFoundationComponentsDemoCube()
        {

            var cubePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(cubePrefabPath);

            if (cubePrefab)
            {
                return;
            }

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = Vector3.one * 0.1f;

            PrefabUtility.SaveAsPrefabAsset(cube, cubePrefabPath);

            Object.DestroyImmediate(cube);

        }

    }

}

#endif
