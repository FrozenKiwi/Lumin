// Copyright (c) XRTK. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using XRTK.Attributes;
using XRTK.Definitions.Platforms;
using XRTK.Definitions.SpatialAwarenessSystem;
using XRTK.Interfaces.SpatialAwarenessSystem;
using XRTK.Lumin.Native;
using XRTK.Lumin.Profiles;
using XRTK.Providers.SpatialObservers;
using XRTK.Services;
using XRTK.Utilities;

namespace XRTK.Lumin.Providers.SpatialAwareness.SpatialObservers
{
    [RuntimePlatform(typeof(LuminPlatform))]
    [System.Runtime.InteropServices.Guid("A1E7BFED-F290-43E3-84B6-01C740CC9614")]
    public class LuminSpatialMeshObserver : BaseMixedRealitySpatialMeshObserver
    {
        /// <inheritdoc />
        public LuminSpatialMeshObserver(string name, uint priority, LuminSpatialMeshObserverProfile profile, IMixedRealitySpatialAwarenessSystem parentService)
            : base(name, priority, profile, parentService)
        {
            meshingSettings = MlMeshing2.MLMeshingSettings.Default;

            if (MeshRecalculateNormals)
            {
                meshingSettings.flags |= MlMeshing2.MeshingFlags.ComputeNormals;
            }
        }

        private float lastUpdated = 0;
        private MlApi.MLHandle meshingHandle;
        private MlMeshing2.MLMeshingSettings meshingSettings;

        #region IMixedRealityService implementation

        /// <inheritdoc />
        public override void Initialize()
        {
            base.Initialize();

            if (!Application.isPlaying || Application.isEditor) { return; }

            if (!meshingHandle.IsValid)
            {
                if (!MlMeshing2.MLMeshingInitSettings(ref meshingSettings).IsOk)
                {
                    Debug.LogError($"Failed to initialize meshing settings!");
                }

                if (!MlMeshing2.MLMeshingCreateClient(ref meshingHandle, meshingSettings).IsOk)
                {
                    Debug.LogError($"failed to create meshing client!");
                }
            }
        }

        /// <inheritdoc />
        public override void Update()
        {
            base.Update();

            // Only update the observer if it is running.
            if (!IsRunning ||
                !Application.isPlaying)
            {
                return;
            }

            // and If enough time has passed since the previous observer update
            if (!(Time.time - lastUpdated >= UpdateInterval)) { return; }

            // Update the observer location if it is not stationary
            if (!IsStationaryObserver)
            {
                if (MixedRealityToolkit.CameraSystem != null)
                {
                    ObserverOrigin = MixedRealityToolkit.CameraSystem.MainCameraRig.CameraTransform.localPosition;
                    ObserverOrientation = MixedRealityToolkit.CameraSystem.MainCameraRig.CameraTransform.localRotation;
                }
                else
                {
                    var cameraTransform = CameraCache.Main.transform;
                    ObserverOrigin = cameraTransform.position;
                    ObserverOrientation = cameraTransform.rotation;
                }
            }

            lastUpdated = Time.time;
        }

        /// <inheritdoc />
        public override void Destroy()
        {
            if (!Application.isPlaying) { return; }

            if (meshingHandle.IsValid)
            {
                if (!MlMeshing2.MLMeshingDestroyClient(ref meshingHandle).IsOk)
                {
                    Debug.LogError($"Failed to destroy meshing client!");
                }
            }
        }

        #endregion IMixedRealityService implementation

        #region IMixedRealitySpatialMeshObserver implementation

        /// <inheritdoc/>
        public override void StartObserving()
        {
            if (IsRunning)
            {
                return;
            }

            base.StartObserving();

            // We want the first update immediately.
            lastUpdated = 0;
        }

        /// <inheritdoc />
        public override void StopObserving()
        {
            if (!IsRunning)
            {
                return;
            }

            base.StopObserving();
        }

        #endregion IMixedRealitySpatialMeshObserver implementation

    }
}
