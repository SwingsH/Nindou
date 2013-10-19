using UnityEngine;
using System.Collections;

/// <summary>
/// 偵測網路連線狀態用
/// </summary>
public enum ConnectType
{
    HTTP,
    Socket
}
public enum TestStatus
{
    Testing,
    Done,
    None
}
public static class NetworkConnectTester
{
    private static int PROBING_PUBLIC_IP_TESTTIME = 10;

    // server 網路連線模式
    private static ConnectionTesterStatus SERVER_CONNECTION_STATUS = ConnectionTesterStatus.PublicIPIsConnectable;
    
    // 快速(較有效率) 偵測網路連線相關
    private static string _connTestStatusMessage = "Testing network connection capabilities.";
    private static string _connTestMessage = "Test in progress";
    private static string _shouldEnableNatMessage = "";
    //private static bool _doneConnTesting = false;
    private static TestStatus _connTestStatus = TestStatus.None;
    private static bool _conntestProbingPublicIP = false;
    private static float connTestTimer;
    private static bool _connTestUseNat = false; // Indicates if the useNat parameter be enabled when starting a server
    private static ConnectionTesterStatus _connectionTestResult = ConnectionTesterStatus.Undetermined; // client 網路連線模式
    private static int _port = 0;
    private static float _startTestTime = 0.0f; // 開始測試時間
    private static ConnectType _connectType;
    private static bool _serverConnectCapability; // 是否有連線到 server 的能力 ( server 是否開啟中不納入考慮 )

    /// <summary>
    /// 是否有連線到 server 的能力 ( server 是否開啟中不納入考慮 )
    /// </summary>
    public static bool ServerConnectCapability
    {
        get
        {
            //return false;
            return _serverConnectCapability;
        }
    }

    /// <summary>
    /// 測試是否完畢
    /// </summary>
    public static TestStatus Status
    {
        get
        {
            return _connTestStatus;
        }
    }

    /// <summary>
    /// 開始測試
    /// </summary>
    public static void StartTest(ConnectType type, int port)
    {
        _connectionTestResult = ConnectionTesterStatus.Undetermined;
        _connTestStatus = TestStatus.Testing;
        _port = port;
        _connectType = type;
        _startTestTime = Time.time;
        CommonFunction.DebugMsg(" NetworkConnect StartTest : " + _startTestTime.ToString());
	}

    /// <summary>
    /// 重試測試
    /// </summary>
    public static void RetryTest()
    {
        _connTestStatus = TestStatus.Testing;
        StartTest(_connectType, _port);

        CommonFunction.DebugMsg(" NetworkConnect StartTest : " + Time.time.ToString());
    }

    /// <summary>
    /// 開始測試
    /// </summary>
    public static void EndTest()
    {
        CommonFunction.DebugMsg("Current Status: " + _connTestStatusMessage);
        CommonFunction.DebugMsg("Test result : " + _connTestMessage);
        if (_shouldEnableNatMessage != string.Empty)
            CommonFunction.DebugMsg(_shouldEnableNatMessage);
        CommonFunction.DebugMsg(" NetworkConnect EndTest : " + Time.time.ToString());

        _connTestStatusMessage = string.Empty;
        _connTestMessage = string.Empty;
        _shouldEnableNatMessage = string.Empty;

        _connTestStatus = TestStatus.None;
    }

    /// <summary>
    /// 測試中需要呼叫的 method
    /// </summary>
    public static void Update()
    {
        if (Time.time - _startTestTime < 0.1f)
            return;
        switch (_connTestStatus)
        {
            case TestStatus.None:
            case TestStatus.Done:
                return;
            case TestStatus.Testing:
                TestConnection();
                break;
        }
    }

    /// <summary>
    /// 結算是否可以連線到 server
    /// </summary>
    public static void DetermineCanConnectTo()
    {
        _serverConnectCapability = CanConnectTo(_connectionTestResult, SERVER_CONNECTION_STATUS);
        CommonFunction.DebugMsg("Server Connect Capability : " + _serverConnectCapability.ToString());
    }

    /// <summary>
    /// 持續測試連線狀態
    /// </summary>
    private static void TestConnection()
    {
		// Start/Poll the connection test, report the results in a label and 
		// react to the results accordingly
		_connectionTestResult = Network.TestConnection();
		switch (_connectionTestResult) {
			case ConnectionTesterStatus.Error: 
				_connTestMessage = "Problem determining NAT capabilities";
				_connTestStatus = TestStatus.Done;
                DetermineCanConnectTo();
				break;
				
			case ConnectionTesterStatus.Undetermined: 
				_connTestMessage = "Undetermined NAT capabilities";
				_connTestStatus = TestStatus.Testing;
				break;
							
			case ConnectionTesterStatus.PublicIPIsConnectable:
				_connTestMessage = "Directly connectable public IP address.";
				_connTestUseNat = false;
                _connTestStatus = TestStatus.Done;
                DetermineCanConnectTo();
				break;
				
			// This case is a bit special as we now need to check if we can 
			// circumvent the blocking by using NAT punchthrough
			case ConnectionTesterStatus.PublicIPPortBlocked:
				_connTestMessage = "Non-connectable public IP address (port " +
                    _port.ToString() + " blocked), running a server is impossible.";
				_connTestUseNat = false;
				// If no NAT punchthrough test has been performed on this public 
				// IP, force a test
				if (!_conntestProbingPublicIP) {
					_connectionTestResult = Network.TestConnectionNAT();
					_conntestProbingPublicIP = true;
					_connTestStatusMessage = "Testing if blocked public IP can be circumvented";
                    connTestTimer = Time.time + PROBING_PUBLIC_IP_TESTTIME;
				}
				// NAT punchthrough test was performed but we still get blocked
				else if (Time.time > connTestTimer) {
					_conntestProbingPublicIP = false; 		// reset
					_connTestUseNat = true;
                    _connTestStatus = TestStatus.Done;
                    DetermineCanConnectTo();
				}
				break;

            // server 專用的 status, server 才需要 listen port
			case ConnectionTesterStatus.PublicIPNoServerStarted:
				_connTestMessage = "Public IP address but server not initialized, "+
					"it must be started to check server accessibility. Restart "+
					"connection test when ready.";

                // HTTP 型式, client 本就無 listen port, 偵測已結束
                if (_connectType == ConnectType.HTTP)
                {
                    _connTestStatus = TestStatus.Done;
                    DetermineCanConnectTo();
                }
				break;
							
			case ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted:
				_connTestMessage = "Limited NAT punchthrough capabilities. Cannot "+
					"connect to all types of NAT servers. Running a server "+
					"is ill advised as not everyone can connect.";
				_connTestUseNat = true;
                _connTestStatus = TestStatus.Done;
                DetermineCanConnectTo();
				break;
				
			case ConnectionTesterStatus.LimitedNATPunchthroughSymmetric:
				_connTestMessage = "Limited NAT punchthrough capabilities. Cannot "+
					"connect to all types of NAT servers. Running a server "+
					"is ill advised as not everyone can connect.";
				_connTestUseNat = true;
                _connTestStatus = TestStatus.Done;
                DetermineCanConnectTo();
				break;
			
			case ConnectionTesterStatus.NATpunchthroughAddressRestrictedCone:
			case ConnectionTesterStatus.NATpunchthroughFullCone:
				_connTestMessage = "NAT punchthrough capable. Can connect to all "+
					"servers and receive connections from all clients. Enabling "+
					"NAT punchthrough functionality.";
				_connTestUseNat = true;
                _connTestStatus = TestStatus.Done;
                DetermineCanConnectTo();
				break;

			default: 
				_connTestMessage = "Error in test routine, got " + _connectionTestResult;
                break;
		}
		if (_connTestStatus == TestStatus.Done) {
			if (_connTestUseNat)
				_shouldEnableNatMessage = "When starting a server the NAT "+
					"punchthrough feature should be enabled (useNat parameter)";
			else
				_shouldEnableNatMessage = "NAT punchthrough not needed";
			_connTestStatusMessage = "Done testing";
		}
	}

    /// <summary>
    /// 依照 S 和 C 的網路狀況, 測試是否可以相互連線
    /// </summary>
    private static bool CanConnectTo(ConnectionTesterStatus serverType, ConnectionTesterStatus clientType)
	{
		if (serverType == ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted &&
			clientType == ConnectionTesterStatus.LimitedNATPunchthroughSymmetric)
			return false;
		else if (serverType == ConnectionTesterStatus.LimitedNATPunchthroughSymmetric &&
			clientType == ConnectionTesterStatus.LimitedNATPunchthroughPortRestricted)
			return false;
		else if (serverType == ConnectionTesterStatus.LimitedNATPunchthroughSymmetric &&
			clientType == ConnectionTesterStatus.LimitedNATPunchthroughSymmetric)
			return false;
		return true;
	}
}