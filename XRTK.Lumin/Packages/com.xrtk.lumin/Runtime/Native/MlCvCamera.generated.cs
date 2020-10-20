//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace XRTK.Lumin.Native
{
    using System.Runtime.InteropServices;

    internal static class MlCvCamera
    {
        /// <summary>
        /// Default distortion vector size
        /// </summary>
        public const int MLCVCameraIntrinsics_MaxDistortionCoefficients = unchecked((int)5);

        /// <summary>
        /// Camera id enum
        /// @apilevel 5
        /// </summary>
        public enum MLCVCameraID : int
        {
            /// <summary>
            /// Default camera id
            /// </summary>
            MLCVCameraID_ColorCamera = unchecked((int)0),

            /// <summary>
            /// Camera id
            /// </summary>
            MLCVCameraID_Ensure32Bits = unchecked((int)0x7FFFFFFF),
        }

        /// <summary>
        /// Camera intrinsic parameter
        /// @apilevel 5
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MLCVCameraIntrinsicCalibrationParameters
        {
            /// <summary>
            /// Structure version
            /// </summary>
            public uint version;

            /// <summary>
            /// Camera width
            /// </summary>
            public uint width;

            /// <summary>
            /// Camera height
            /// </summary>
            public uint height;

            /// <summary>
            /// Camera focal length
            /// </summary>
            public MlTypes.MLVec2f focal_length;

            /// <summary>
            /// Camera principal point
            /// </summary>
            public MlTypes.MLVec2f principal_point;

            /// <summary>
            /// Field of view
            /// </summary>
            public float fov;

            /// <summary>
            /// Distortion vector
            /// The distortion co-efficients are in the following order:
            /// [k1, k2, p1, p2, k3]
            /// </summary>
            public fixed double distortion[5];
        }

        /// <summary>
        /// Create Camera Tracker
        /// MLResult_Ok On success
        /// MLResult_PrivilegeDenied Necessary privilege is missing
        /// MLResult_UnspecifiedFailure Unable to create tracker
        /// </summary>
        /// <param name="out_handle">Tracker handle</param>
        /// <remarks>
        /// @apilevel 5
        /// @priv ComputerVision
        /// </remarks>
        [DllImport("ml_perception_client", CallingConvention = CallingConvention.Cdecl)]
        public static extern MlApi.MLResult MLCVCameraTrackingCreate(ref MlApi.MLHandle out_handle);

        /// <summary>
        /// Get camera intrinsic parameters
        /// MLResult_Ok On success
        /// MLResult_PrivilegeDenied Necessary privilege is missing
        /// MLResult_UnspecifiedFailure Unable to retrieve intrinsic parameter
        /// </summary>
        /// <param name="camera_handle">MLHandle previously created with MLCVCameraTrackingCreate</param>
        /// <param name="id">Camera id</param>
        /// <param name="out_intrinsics">Camera intrinsic parameters</param>
        /// <remarks>
        /// @apilevel 5
        /// @priv ComputerVision
        /// </remarks>
        [DllImport("ml_perception_client", CallingConvention = CallingConvention.Cdecl)]
        public static extern MlApi.MLResult MLCVCameraGetIntrinsicCalibrationParameters(MlApi.MLHandle camera_handle, MlCvCamera.MLCVCameraID id, ref MlCvCamera.MLCVCameraIntrinsicCalibrationParameters out_intrinsics);

        /// <summary>
        /// Get the camera pose in the world coordiante system
        /// MLResult_Ok Obtained transform successfully
        /// MLResult_PrivilegeDenied Necessary privilege is missing
        /// MLResult_InvalidParam id parameter was not valid or out_transform parameter
        /// was not valid (null)
        /// MLResult_UnspecifiedFailure Failed to obtain transform due to internal error
        /// </summary>
        /// <param name="camera_handle">MLHandle previously created with MLCVCameraTrackingCreate</param>
        /// <param name="head_handle">MLHandle previously created with MLHeadTrackingCreate</param>
        /// <param name="id">Camera id</param>
        /// <param name="camera_timestamp_ns">Time in nanoseconds to request the pose</param>
        /// <param name="out_transform">Transfom from camera to world origin</param>
        /// <remarks>
        /// Use the timestamp provided from the on_video_buffer_available callback from
        /// ml_camerah The camera tracker only caches a limited set of poses Retrieve
        /// the camera pose as soon as the timestamp is available
        /// @apilevel 5
        /// @priv ComputerVision
        /// </remarks>
        [DllImport("ml_perception_client", CallingConvention = CallingConvention.Cdecl)]
        public static extern MlApi.MLResult MLCVCameraGetFramePose(MlApi.MLHandle camera_handle, MlApi.MLHandle head_handle, MlCvCamera.MLCVCameraID id, ulong camera_timestamp_ns, ref MlTypes.MLTransform out_transform);

        /// <summary>
        /// Destroy Tracker after usage
        /// MLResult_Ok On success
        /// MLResult_PrivilegeDenied Necessary privilege is missing
        /// MLResult_UnspecifiedFailure Unable to create tracker
        /// </summary>
        /// <param name="camera_handle">MLHandle previously created with MLCVCameraTrackingCreate</param>
        /// <remarks>
        /// @apilevel 5
        /// @priv ComputerVision
        /// </remarks>
        [DllImport("ml_perception_client", CallingConvention = CallingConvention.Cdecl)]
        public static extern MlApi.MLResult MLCVCameraTrackingDestroy(MlApi.MLHandle camera_handle);
    }
}