using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

//*********************************
//* socket 協定資料 send & receive 緩衝區
//* 目前改成沒有加密
//*********************************
public class NetworkSocketBuffer
{
    private const int BUFFER_LENGTH = 128; //原本預設 max 8192, 先縮減, 不夠使用的話再開

    private static bool mInitialized = false;
    private float _tryConnectTime = 0.0f;

    private static byte[] _decodeBuffer = new byte[BUFFER_LENGTH];
    private static byte[] _encodeBuffer = new byte[BUFFER_LENGTH];	
	private static MemoryStream _encodeStream = new MemoryStream();

    public static readonly byte[] Identify = { 80, 93 };		//張景嵐

    /// <summary>
    /// 打包 header, 和現存的 buffer, 準備好送給 server 的資料
    /// </summary>
    public static void Packaging(byte iMainKind, byte iSubKind)
    {
        byte[] mHeader = { iMainKind, iSubKind };	
        InsertBuffer(_encodeStream, 0, mHeader);

        int Len = (int)_encodeStream.Length;

        byte[] mBuf = BitConverter.GetBytes(Len);
        //CommonFunction.XORByteArray(ref mBuf, GLOBALCONST.IP_VERSION_NUM);		
        InsertBuffer(_encodeStream, 0, mBuf);

        mBuf = new byte[Identify.Length];
        Array.Copy(Identify, 0, mBuf, 0, mBuf.Length);
        //CommonFunction.XORByteArray(ref mBuf, GLOBALCONST.IP_VERSION_NUM);		
        InsertBuffer(_encodeStream, 0, mBuf);
    }

    public static MemoryStream EncodeStream
    {
        get { return _encodeStream; }
    }

    /// <summary>
    /// 清空 send buffer
    /// </summary>
    public static void ClearSendBuffer()
    {
        _encodeStream.SetLength(0);
    }

	public static void InsertBuffer(MemoryStream Buffer, uint offset, byte[] Data){
		InsertBuffer(Buffer, offset, Data, Data.Length);	
	}
	
	public static void InsertBuffer(MemoryStream Buffer, uint offset, byte[] Data, int Length){
		byte[] Data_Front=null;
		byte[] Data_Back=null;
		
		if(offset>0){
			Data_Front=new byte[offset];			
			ExtractBuffer(Buffer, ref Data_Front, Data_Front.Length);
		}
		
		if((Buffer.Length-offset)>0){
			int Count=(int)(Buffer.Length-offset);
			Data_Back=new byte[Count];
			ExtractBuffer(Buffer, ref Data_Back, Data_Back.Length);				
		}
		
		Buffer.SetLength(0);
		
		if(Data_Front!=null)
			Buffer.Write(Data_Front, 0, Data_Front.Length);		
		if(Data!=null)
			Buffer.Write(Data, 0, Length);
		if(Data_Back!=null)
			Buffer.Write(Data_Back, 0, Data_Back.Length);		
	}

	public static int ExtractBuffer(MemoryStream Buffer, ref byte[] Data, int length){
		if(length<=0)
			return -1;
		
		Buffer.Seek(0, SeekOrigin.Begin);
		
		int ExtractLen=Buffer.Read(Data, 0, length);
		
		int RemainCount=(int)Buffer.Length-ExtractLen;
		
		if(RemainCount==0){
			Buffer.Seek(0, SeekOrigin.Begin); 		
			Buffer.SetLength(0);			
		}
		else{			
			byte[] RemainBytes=new byte[RemainCount];

			Array.Copy(Buffer.GetBuffer(), ExtractLen, RemainBytes, 0, RemainBytes.Length); 
			
			Buffer.Seek(0, SeekOrigin.Begin); 		
			Buffer.SetLength(0);			
			Buffer.Write(RemainBytes, 0, RemainBytes.Length);
		}
		
		return ExtractLen;
	}	

    public static void Decode_ToBytes(MemoryStream Message, ref byte[] buf)
    {
		ExtractBuffer(Message, ref buf, buf.Length);
    }

	public static bool Decode_ToBoolean(MemoryStream Message)
    {
		if(Message.Length<sizeof(bool))
			return false;
		
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(bool));
		return _decodeBuffer[0]>0;
    }

	public static sbyte Decode_ToSByte(MemoryStream Message)
    {
		if(Message.Length<sizeof(byte))
			return sbyte.MinValue;
				
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(byte));
		return (sbyte)(_decodeBuffer[0] - sbyte.MaxValue);
    }

    public static byte Decode_ToByte(MemoryStream Message)
    {
		if(Message.Length<sizeof(byte))
			return 0;
				
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(byte));
		return _decodeBuffer[0];
    }

    public static ushort Decode_ToUShort(MemoryStream Message)
    {
		if(Message.Length<sizeof(ushort))
			return 0;
				
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(ushort));
		return BitConverter.ToUInt16(_decodeBuffer, 0);
    }

    public static uint Decode_ToUInt(MemoryStream Message)
    {
		if(Message.Length<sizeof(uint))
			return 0;
		
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(uint));
		return BitConverter.ToUInt32(_decodeBuffer, 0);
    }
	
	public static short Decode_ToShort(MemoryStream Message)
    {
		if(Message.Length<sizeof(short))
			return 0;
		
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(short));
		return (short)BitConverter.ToInt16(_decodeBuffer, 0);
    }
	
    public static int Decode_ToInt(MemoryStream Message)
    {
		if(Message.Length<sizeof(int))
			return 0;
				
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(int));
		return BitConverter.ToInt32(_decodeBuffer, 0);
    }

    public static ulong Decode_ToULong(MemoryStream Message)
    {
		if(Message.Length<sizeof(ulong))
			return 0;
				
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(ulong));
		return BitConverter.ToUInt64(_decodeBuffer, 0);
    }

	public static long Decode_ToLong(MemoryStream Message)
    {
		if(Message.Length<sizeof(long))
			return 0;
				
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(long));
		return BitConverter.ToInt64(_decodeBuffer, 0);
    }
	
	public static float Decode_ToFloat(MemoryStream Message)
	{
		if(Message.Length<sizeof(float))
			return 0;
				
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(float));
		return BitConverter.ToSingle(_decodeBuffer, 0);
	}

	public static double Decode_ToDouble(MemoryStream Message)
	{
		if(Message.Length<sizeof(double))
			return 0;
				
		ExtractBuffer(Message, ref _decodeBuffer, sizeof(double));
		return BitConverter.ToDouble(_decodeBuffer, 0);
	}

    public static string Decode_ToStr(MemoryStream Message)
    {
		if(Message.Length<sizeof(UInt32))
			return "";
		
		UInt32 Len=Decode_ToUInt(Message);

		if((Len==0)||(Message.Length<Len))
			return "";
		
		Len=Len*sizeof(ushort);
				
		ExtractBuffer(Message, ref _decodeBuffer, (int)Len);
		return System.Text.Encoding.Unicode.GetString(_decodeBuffer, 0, (int)Len).Trim(new char[]{'\0'});
    }

    public static bool Decode_ToTypeData(MemoryStream Message, ref object obj)
    {
		return GetStrToTypeData(Message,ref obj);
    }

    public static void Encode_FromByte(byte Value)
    {
		int Size=sizeof(byte);
		Array.Copy(BitConverter.GetBytes(Value), 0, _encodeBuffer, 0, Size);
		_encodeStream.Write(_encodeBuffer, 0, Size);
//        _socket.Char(Value);
    }

    public static void Encode_FromUShort(ushort Value)
    {
		int Size=sizeof(ushort);
		Array.Copy(BitConverter.GetBytes(Value), 0, _encodeBuffer, 0, Size);	
		_encodeStream.Write(_encodeBuffer, 0, Size);
//        _socket.GetWordString(Value);
    }

    public static void Encode_FromUInt(uint Value)
    {
		int Size=sizeof(uint);
		Array.Copy(BitConverter.GetBytes(Value), 0, _encodeBuffer, 0, Size);		
		_encodeStream.Write(_encodeBuffer, 0, Size);
//        _socket.GetDWordString(Value);
    }

    public static void Encode_FromULong(ulong Value)
    {
		int Size=sizeof(ulong);
		Array.Copy(BitConverter.GetBytes(Value), 0, _encodeBuffer, 0, Size);	
		_encodeStream.Write(_encodeBuffer, 0, Size);
//        _socket.GetulongString(Value);
    }

	public static void Encode_FromFloat(float Value)
	{
		int Size=sizeof(float);
		Array.Copy(BitConverter.GetBytes(Value), 0, _encodeBuffer, 0, Size);	
		_encodeStream.Write(_encodeBuffer, 0, Size);
//		_socket.GetfloatString(Value);
	}

	public static void Encode_FromDouble(double Value)
	{
		int Size=sizeof(double);
		Array.Copy(BitConverter.GetBytes(Value), 0, _encodeBuffer, 0, Size);	
		_encodeStream.Write(_encodeBuffer, 0, Size);
//		_socket.GetdoubleString(Value);
	}

	public static void Encode_FromString(string Value)
    {
		System.Text.UnicodeEncoding vEncoding = new System.Text.UnicodeEncoding();
		byte[] mBuffer=vEncoding.GetBytes(Value);
		int Size=mBuffer.Length;
				
		Encode_FromUInt((uint)Value.Length);			
		_encodeStream.Write(mBuffer, 0, Size);		
    }

	public static void Encode_FromTypeData(object obj)
    {
        GetTypeString(obj);
    }

    public static void Encode_FromTypeData<T>(T Data)where T:struct
    {
		object obj=(object)Data;
        GetTypeString(obj);
    }
	
	private static bool EncodeField(object obj, FieldInfo Info)
    {
	    try
	    {
		    if (Info.FieldType.Name.Equals("Byte")){
			    Encode_FromByte((byte)Info.GetValue(obj));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Boolean")){
			    Encode_FromByte((byte)Info.GetValue(obj));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("UInt16")){
			    Encode_FromUShort((ushort)Info.GetValue(obj));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("UInt32")){			    
			    Encode_FromUInt((uint)Info.GetValue(obj));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("UInt64")){			    
			    Encode_FromULong((ulong)Info.GetValue(obj));
			    return true;
		    }
		    if (Info.FieldType.Name.Equals("String"))
		    {
			    return false;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Byte[]")){
				foreach(byte mByte in  (byte[])Info.GetValue(obj))
			    	Encode_FromByte(mByte);					
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Boolean[]")){
				foreach(bool mbool in  (bool[])Info.GetValue(obj)){
					byte mByte=0;
					if(mbool)
						mByte=1;
			    	Encode_FromByte(mByte);				
				}
				
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("ushort[]")){
				foreach(ushort mShort in  (ushort[])Info.GetValue(obj))
			    	Encode_FromUShort(mShort);					
				
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("uint[]")){
				foreach(uint mInt in  (uint[])Info.GetValue(obj))
			    	Encode_FromUInt(mInt);					
				
			    return true;
		    }
		    else
			    return false;
	    }
	    catch
		{
			return false;
		}
    }
	
	 private static bool DecodeField(MemoryStream Message, ref object obj, FieldInfo Info)
    {
	    try
	    {
		    if (obj == null)
			    return false;
			
		    if (Info.FieldType.Name.Equals("Byte")){			    
			    Info.SetValue(obj, Decode_ToByte(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Boolean")){
			    Info.SetValue(obj, Decode_ToBoolean(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("SByte")){
			    Info.SetValue(obj, Decode_ToSByte(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("UInt16")){
			    Info.SetValue(obj, Decode_ToUShort(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Int16")){
			    Info.SetValue(obj, Decode_ToShort(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("UInt32")){
			    Info.SetValue(obj, Decode_ToUInt(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Int32")){
			    Info.SetValue(obj, Decode_ToInt(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Single")){
			    Info.SetValue(obj, Decode_ToFloat(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Double")){
			    Info.SetValue(obj, Decode_ToDouble(Message));
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("UInt64")){
			    Info.SetValue(obj, Decode_ToULong(Message));
			    return true;
		    }
		    if (Info.FieldType.Name.Equals("String")){
				int strSize=0;
				
				object[] objs=Info.GetCustomAttributes(false);
				if(objs.Length>0)
					strSize=((MarshalAsAttribute)objs[0]).SizeConst;
				if(obj.GetType().StructLayoutAttribute.CharSet==CharSet.Unicode)
					strSize*=2;
				
				ExtractBuffer(Message, ref _decodeBuffer, strSize);				
			    Info.SetValue(obj, System.Text.Encoding.Unicode.GetString(_decodeBuffer, 0, strSize));			    
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Byte[]")){
			    byte[] buf = (byte[])Info.GetValue(obj);
			    for (int j = 0; j < buf.Length; j++)
				    buf[j] = Decode_ToByte(Message);
				
			    Info.SetValue(obj, buf);
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Boolean[]")){
			    bool[] buf = (bool[])Info.GetValue(obj);
			    for (int j = 0; j < buf.Length; j++)
				    buf[j] = Decode_ToBoolean(Message);
				
			    Info.SetValue(obj, buf);
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("UInt16[]")){
			    ushort[] buf = (ushort[])Info.GetValue(obj);
			    for (int j = 0; j < buf.Length; j++)
				    buf[j] = Decode_ToUShort(Message);
				
			    Info.SetValue(obj, buf);
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Int16[]")){
			    short[] buf = (short[])Info.GetValue(obj);
			    for (int j = 0; j < buf.Length; j++)
				    buf[j] = Decode_ToShort(Message);
				
			    Info.SetValue(obj, buf);
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("UInt32[]")){
			    uint[] buf = (uint[])Info.GetValue(obj);
			    for (int j = 0; j < buf.Length; j++)
				    buf[j] = Decode_ToUInt(Message);
				
			    Info.SetValue(obj, buf);
			    return true;
		    }
		    else
		    if (Info.FieldType.Name.Equals("Int32[]")){
			    int[] buf = (int[])Info.GetValue(obj);
			    for (int j = 0; j < buf.Length; j++)
				    buf[j] = Decode_ToInt(Message);
				
			    Info.SetValue(obj, buf);
			    return true;
		    }
		    else
			    return false;
	    }
	    catch
	    {
		    return false;
	    }
    }
	
	private static Array DecodeArray(MemoryStream Message, object obj, FieldInfo info)
    {
	    Array ay = (Array)info.GetValue(obj);
		
	    for (int j = 0; j < ay.Length; j++)
	    {
		    object obj2 = ay.GetValue(j);
			if(obj2==null)
				continue;
		    FieldInfo[] infos = obj2.GetType().GetFields();
		    for (int k = 0; k < infos.Length; k++)
		    {
			    if (!DecodeField(Message, ref obj2, infos[k]))
			    {
				    if (infos[k].FieldType.IsArray)
				    {
					    Array ay2 = DecodeArray(Message, obj2, infos[k]);
					    infos[k].SetValue(obj2, ay2);
				    }
				    else
				    {
					    object obj3 = infos[k].GetValue(obj2);
					    GetStrToTypeData(Message, ref obj3);
					    infos[k].SetValue(obj2, obj3);
				    }
			    }
		    }
		    ay.SetValue(obj2, j);
	    }
		
	    return ay;
    }
	
	private static bool GetStrToTypeData(MemoryStream Message, ref object obj)
    {
	    if (obj == null)
		    return false;
		
		// 囧WebPlayer無法使用 Marshal.SizeOf()
		int size = obj.GetType().StructLayoutAttribute.Size;
	    
	    FieldInfo[] infos = obj.GetType().GetFields();		
	    for (int i = 0; i < infos.Length; i++)
	    {
		    try
		    {
			    if (!DecodeField(Message, ref obj, infos[i]))
			    {
					if(infos[i]==null)
						continue;
				    if (infos[i].FieldType.IsArray)
				    {
					    Array ay = DecodeArray(Message, obj, infos[i]);
						if(ay!=null)
							continue;
					    infos[i].SetValue(obj, ay);
				    }
				    else
				    {
					    object obj2 = infos[i].GetValue(obj);
						if(obj2==null)
							continue;
					    GetStrToTypeData(Message, ref obj2);
					    infos[i].SetValue(obj, obj2);
				    }
			    }
		    }
		    catch(Exception e)
		    {			    
				CommonFunction.DebugError(e.Message);
			    return false;
		    }
	    }		
	    
	    return true;
    }
	
	public static void GetTypeString(object obj)
    {
	    if (obj == null)
		    return;
		
	    int size = obj.GetType().StructLayoutAttribute.Size;
	    FieldInfo[] infos = obj.GetType().GetFields();		
	    for (int i = 0; i < infos.Length; i++)
	    {
		    try
		    {
			    if (!EncodeField(obj, infos[i]))
			    {
				    object obj2 = infos[i].GetValue(obj);
				    GetTypeString(obj2);
			    }
		    }
		    catch
		    {
			    CommonFunction.DebugMsg(string.Format("{0} GetTypeString error", infos[i].Name));
			    return;
		    }
	    }
    }
}
