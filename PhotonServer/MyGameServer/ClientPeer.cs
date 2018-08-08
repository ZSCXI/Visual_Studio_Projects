using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace MyGameServer
{
    //管理每个客户端的连接(类似客服)
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {
        
        }

        //处理哭护短断开连接的后续工作
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            
        }

        //处理客户端的请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            //通过客户端传过来的customOpCode区分请求
            switch (operationRequest.OperationCode)
            {
                case 1:
                    MyGameServer.log.Info("Receive a client Request!");
                    //获取客户端传过来的参数
                    Dictionary<byte, object> data = operationRequest.Parameters;
                    object intValue;
                    data.TryGetValue(1,out intValue);
                    object stringValue;
                    data.TryGetValue(2,out stringValue);
                    MyGameServer.log.Info("Getting Parameters is " + intValue.ToString() + " " + stringValue);//输出在日志上
                    //与请求对应的，反馈给客户端也需要区分 与请求保持一致
                    OperationResponse opResponse = new OperationResponse(1);
                    //发送数据给客户端  opResponse.SetParameters()方法
                    Dictionary<byte, object> serverData = new Dictionary<byte, object>();
                    serverData.Add(1,111111);
                    serverData.Add(2,222222);
                    opResponse.SetParameters(serverData); //将服务端的数据发送给客户端
                    //响应客户端
                    SendOperationResponse(opResponse, sendParameters);

                    //当客户端没有请求时，服务端向客户端发送事件
                    EventData ed = new EventData(1);
                    ed.Parameters = serverData;
                    //SendEvent方法可以在服务器端的任何地方进行调用
                    SendEvent(ed,new SendParameters());

                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }
}
