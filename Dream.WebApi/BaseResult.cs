using System;

public class BaseResult<T>
{
	public int code { get; set; }
	public String msg{ get; set; }
	public T data { get; set; }
	public BaseResult(int code, String msg, T data) 
	{
		this.code = code;
		this.msg = msg;
		this.data = data;
	}
	public BaseResult()
	{

	}

	public String ToJson ()
    {
		return Newtonsoft.Json.JsonConvert.SerializeObject(this);
	}
}
