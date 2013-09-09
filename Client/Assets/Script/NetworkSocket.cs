using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public delegate void ProtocalHandleFunc(int iMainKind, int iSubKind);
public delegate void ConnectFailedFunc(string ErrorMessage);

/// <summary>
/// TCP Socket 連線方式, 該 class 只能被 NetworkInterface 使用
/// todo: 
/// 1. some important statement marked
/// 2. data 未送到 buffer section
/// </summary>
public class NetworkSocket
{
	private readonly byte _HeadLen=6;   // 至少要有 6 byte  檔頭(2) + 長度(4)
	private readonly int  RecvBufferSize=8192;
	private byte[] _Buffer;
	private MemoryStream _RecvPacket=new MemoryStream();
	private MemoryStream _SendPacket=new MemoryStream();
	private readonly int  SendBufferSize=64*1024;  //需比最長協定大
	private string _IPAddress=string.Empty;
	private ushort _Port=0;		
	private Socket _socket=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);	
	private Queue<MemoryStream> _MessageQueue=new Queue<MemoryStream>();
	private bool _PolicyAuthorize=false;	
	private bool _Connecting=false;
	private byte[] _Identify;
	
	public NetworkSocket(byte[] Header){
		_socket.ReceiveBufferSize=RecvBufferSize;
		_socket.SendBufferSize=SendBufferSize;	
		_Buffer=new byte[RecvBufferSize];
		_Identify=new byte[Header.Length];		
		Array.Copy(Header, 0, _Identify, 0, Header.Length);
	}
	
	~NetworkSocket(){
		_socket.Disconnect(false);
	}
	
	public void SetConnect(string IP, ushort Port){
		_IPAddress=IP;
		_Port=Port;	
	}
	
	private bool PolicyAuthorizaton(){
		bool Result=true;
		
		#if UNITY_WEBPLAYER			
		Result=Security.PrefetchSocketPolicy(_IPAddress, _Port, GLOBALCONST.POLICY_WAIT_MILLISECOND);
		#endif
		
		return Result;
	}
	
	private void ExcuteConnect(){
		Monitor.Enter(_socket);
		Monitor.Enter(_Connecting);
		
		string Msg=string.Empty;
		
		try{
			for(int i=0; i<2; i++)
			if(!_PolicyAuthorize)
				_PolicyAuthorize=PolicyAuthorizaton();
			
			if(!_PolicyAuthorize)			
				Msg="Policy Error!";
			
			if(_PolicyAuthorize){
				IAsyncResult result = _socket.BeginConnect( _IPAddress, _Port, null, null );  
				result.AsyncWaitHandle.WaitOne( 500, true );
				_socket.EndConnect(result);
			}
		}catch(Exception e){
			Msg=e.Message;			
		}finally{
			_Connecting=false;	
			Monitor.Exit(_Connecting);
			Monitor.Exit(_socket);
		}		
	}
	
	public void Connect(){
		string Msg=string.Empty;
		try{
			if(_IPAddress==string.Empty)
				return;
			
			if(Enable()||_Connecting){
				Msg="Connect Duplicate!";
				return;
			}
			
			if(_socket.Connected)
				_socket.Disconnect(true);
			_socket=new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);				
			Thread Connthread=new Thread(new ThreadStart(ExcuteConnect));
			Connthread.Start();
			_Connecting=true;

		}catch(Exception e){
			Msg=e.Message;			
		}
		finally{
//			if(Msg!=string.Empty)
//				_ErrorMsg=Msg;
		}		
	}
	
	public void Disconnect(){
		if(_socket.Connected)
			_socket.Disconnect(true);
	}
	
	public Boolean Enable(){		
		if(!_socket.Connected)
			return false;
		
		if(!_socket.Poll(0, SelectMode.SelectRead))
		if(!_socket.Poll(0, SelectMode.SelectWrite))
			return false;
		
		byte[] ChkConn=new byte[1];
		
		if(_socket.Poll(0, SelectMode.SelectRead))
		if(_socket.Receive(ChkConn, SocketFlags.Peek)==0)
			return false;
		
		return true;
	}
	
	private void UpdateSend(){
        //if(_SendPacket.Length<=0)
        //    return;
		
        //_SendPacket.Seek(0, SeekOrigin.Begin); 
		
        ////int Len=PlayerNetworkStation.ExtractBuffer(_SendPacket, ref _Buffer, _Buffer.Length);
		
        //int Index=0;
        //int Count=0;
		
        //byte[] DebugByte=new byte[Len];
		
        //Array.Copy(_Buffer, 0, DebugByte, 0, Len);
        ////CommonFunction.XORByteArray(ref DebugByte, GLOBALCONST.IP_VERSION_NUM);		
		
        //while(Len>0){
        //    Count=_socket.Send(_Buffer, Index, (int)Len, SocketFlags.None);				
        //    Index+=Count;
        //    Len-=Count;
        //}
	}
	
	private int GetPacketLength(){
		if(_RecvPacket.Length<_HeadLen)
			return -1;
		
		//PlayerNetworkStation.ExtractBuffer(_RecvPacket, ref _Buffer, _HeadLen);
		
		int PacketLen=int.MaxValue;
		
		for(int i=0; i<_Identify.Length; i++)
		if(_Buffer[i]!=_Identify[i])
			return -1;		
		
		try{
			PacketLen=(int)BitConverter.ToUInt32(_Buffer, 2);
		}catch{
			_RecvPacket.Write(_Buffer, 0, _HeadLen);
			return -1;
		}
		
		if(_RecvPacket.Length<PacketLen){
			//PlayerNetworkStation.InsertBuffer(_RecvPacket, 0, _Buffer, _HeadLen);
			return -1;
		}
		
		return PacketLen;
	}
	
	private void UpdateReceive(){
		if(_socket.Available<=0)
			return;
		
		int Count=_socket.Receive(_Buffer);		
		
		if(Count<=0)
			return;
		
		//CommonFunction.XORByteArray(ref _Buffer, GLOBALCONST.IP_VERSION_NUM, Count);		
		
		_RecvPacket.Write(_Buffer, 0, Count);	
		
		if(_Buffer.Length<_RecvPacket.Length)
			_Buffer=new byte[_RecvPacket.Length];		
		
		int PacketLen=0;
				
		while((PacketLen=GetPacketLength())>0){			
			//PlayerNetworkStation.ExtractBuffer(_RecvPacket,  ref  _Buffer, PacketLen);
						
			MemoryStream Msg=new MemoryStream();
			Msg.Write(_Buffer, 0, PacketLen);
			Msg.Seek(0, SeekOrigin.Begin);
			_MessageQueue.Enqueue(Msg);
		}
	}
	
	public void Send(MemoryStream Stream){
		Stream.WriteTo(_SendPacket);
	}
	
	public void Update(){
		if(Enable()){
			UpdateSend();			
			UpdateReceive();
			return;
		}
	}
	
	public MemoryStream PopMessage(){
		if(_MessageQueue.Count<1)
			return null;
		
		return _MessageQueue.Dequeue();
	}
}
