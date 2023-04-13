using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;


/// <summary>
/// Unity Warpper for KAT Native SDK
/// </summary>
public class KATNativeSDK
{
    public static Vector3 QuaternionToVector3(Quaternion quaternion)
    {
        double qw = quaternion.W;
        double qx = quaternion.X;
        double qy = quaternion.Y;
        double qz = quaternion.Z;

        double qw2 = qw * qw;
        double qx2 = qx * qx;
        double qy2 = qy * qy;
        double qz2 = qz * qz;

        double test = qx * qy + qz * qw;

        double x, y, z;

        if (test > 0.499)
        {
            y = 2.0 * Math.Atan2(qx, qw);
            x = Math.PI / 2.0;
            z = 0.0;
        }
        else if (test < -0.499)
        {
            y = -2.0 * Math.Atan2(qx, qw);
            x = -Math.PI / 2.0;
            z = 0.0;
        }
        else
        {
            double sqx = qx * qx;
            double sqy = qy * qy;
            double sqz = qz * qz;

            x = Math.Atan2(2.0 * qy * qw - 2.0 * qx * qz, 1.0 - 2.0 * sqy - 2.0 * qz2);
            y = Math.Atan2(2.0 * qx * qw - 2.0 * qy * qz, 1.0 - 2.0 * sqx - 2.0 * qz2);
            z = Math.Asin(2.0 * test);
        }
        return new Vector3((float)(y), (float)(x), (float)(z));
    }
    public static Vector3 Calibrated_value(Vector3 value)
    {
        double minValueX = -0.051393598318099976;
        double maxValueX = 0.09015262126922607;
        double minValueY = -3.1399073600769043;
        double maxValueY = 3.140648365020752;
        double minValueZ = -0.10589293390512466;
        double maxValueZ = 0.06071655452251434;

        double rangeX = maxValueX - minValueX;
        double rangeY = maxValueY - minValueY;
        double rangeZ = maxValueZ - minValueZ;

        double x, y, z;

        x = ((value.X - minValueX) % rangeX + rangeX) % rangeX + minValueX;
        y = ((value.Y - minValueY) % rangeY + rangeY) % rangeY + minValueY;
        z = ((value.Z - minValueZ) % rangeZ + rangeZ) % rangeZ + minValueZ;

        return new Vector3((float)(x), (float)(y), (float)(z));
    }
    /// <summary>
    /// Description of KAT Devices
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct DeviceDescription
	{
        //Device Name
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string device;

		//Device Serial Number
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string serialNumber;

		//Device PID
		public int pid;

		//Device VID
		public int vid;

		//Device Type
		//0. Err 1. Tread Mill 2. Tracker 
		public int deviceType;
	};

	/// <summary>
	/// Device Status Data
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct DeviceData
	{
        //Is Calibration Button Pressed?
        [MarshalAs(UnmanagedType.I1)]
		public bool btnPressed;
		[MarshalAs(UnmanagedType.I1)]
		//Is Battery Charging?
		public bool isBatteryCharging;
		//Battery Used
		public float batteryLevel;
		[MarshalAs(UnmanagedType.I1)]
		public byte firmwareVersion;
	};

	/// <summary>
	/// TreadMill Device Data
	/// </summary>
	[StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
	public struct TreadMillData
	{
		//Device Name
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		public string deviceName;
		[MarshalAs(UnmanagedType.I1)]
		//Is Device Connected
		public bool connected;
		//Last Update Time
		public double lastUpdateTimePoint;

		//Body Rotation(Quaternion), for treadmill it will cause GL
		public Quaternion bodyRotationRaw;

		//Target Move Speed With Direction
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public Vector3[] moveSpeed;

		//Sensor Device Datas
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public DeviceData[] deviceDatas;

		//Extra Data of TreadMill
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public byte[] extraData;
	};

	//Get Device Count
	[DllImport("KATNativeSDK.dll")]
    public static extern int DeviceCount();

	//Get Device Description
	[DllImport("KATNativeSDK.dll")]
    public static extern DeviceDescription GetDevicesDesc(uint index);

	//Get Calibration Time
	[DllImport("KATNativeSDK.dll")]
	public static extern double GetLastCalibratedTimeEscaped();


	//Get Treadmill Data, main Func
	[DllImport("KATNativeSDK.dll")]
	public static extern TreadMillData GetWalkStatus(string sn = "");

	//KAT Extensions, Only for WalkCoord2 and later device
	public class KATExtension
	{
		//KAT Extensions, amplitude: 0(close) - 1.0(max)

		[DllImport("KATNativeSDK.dll")]
		public static extern void VibrateConst(float amplitude);

		[DllImport("KATNativeSDK.dll")]
		public static extern void LEDConst(float amplitude);

		//Vibrate in duration
		[DllImport("KATNativeSDK.dll")]
		public static extern void VibrateInSeconds(float amplitude, float duration);

		//Vibrate once, simulate a "Click" like function
		[DllImport("KATNativeSDK.dll")]
		public static extern void VibrateOnce(float amplitude);

		//Vibrate with a frequency in duration
		[DllImport("KATNativeSDK.dll")]
		public static extern void VibrateFor(float duration, float frequency, float amplitude);

		//Lighting LED in Seconds
		[DllImport("KATNativeSDK.dll")]
		public static extern void LEDInSeconds(float amplitude, float duration);

		//Lighting once
		[DllImport("KATNativeSDK.dll")]
		public static extern void LEDOnce(float amplitude);

		//Vibrate with a frequency in duration
		[DllImport("KATNativeSDK.dll")]
		public static extern void LEDFor(float duration, float frequency, float amplitude);

	}
}
