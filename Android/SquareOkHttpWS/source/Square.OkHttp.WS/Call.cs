using System;
using System.Collections.Generic;
using Android.Runtime;
using System.Threading.Tasks;
using Square.OkIO;

namespace Square.OkHttp.WS
{
	partial class WebSocketCall
    {
        public WebSocketListener Enqueue()
        {
            var listener = new WebSocketListener(this);
            Enqueue(listener);
            return listener;
        }

        public void Enqueue(
            Action<int, string> close,
            Action<Java.IO.IOException, Response> failure,
			Action<ResponseBody> message,
            Action<IWebSocket, Response> open,
            Action<OkBuffer> pong)
        {
            Enqueue(new WebSocketListenerActions
            {
                Close = close,
                Failure = failure,
                Message = message,
                Open = open,
                Pong = pong
            });
        }
    }

    public class WebSocketListener : Java.Lang.Object, IWebSocketListener
    {
        private object sender;

        public WebSocketListener(object sender)
        {
            this.sender = sender;
        }

        void IWebSocketListener.OnClose(int code, string reason)
        {
            var handler = Close;
            if (handler != null)
            {
                handler(sender, new CloseEventArgs(code, reason));
            }
        }

        void IWebSocketListener.OnFailure(Java.IO.IOException exception, Response response)
        {
            var handler = Failure;
            if (handler != null)
            {
                handler(sender, new FailureEventArgs(exception, response));
            }
        }

		void IWebSocketListener.OnMessage(ResponseBody payload)
        {
            var handler = Message;
            if (handler != null)
            {
				handler(sender, new MessageEventArgs(payload));
            }
        }

        void IWebSocketListener.OnOpen(IWebSocket socket, Response response)
        {
            var handler = Open;
            if (handler != null)
            {
                handler(sender, new OpenEventArgs(socket, response));
            }
        }

        void IWebSocketListener.OnPong(OkBuffer buffer)
        {
            var handler = Pong;
            if (handler != null)
            {
                handler(sender, new PongEventArgs(buffer));
            }
        }

        public event EventHandler<CloseEventArgs> Close;
        public event EventHandler<FailureEventArgs> Failure;
        public event EventHandler<MessageEventArgs> Message;
        public event EventHandler<OpenEventArgs> Open;
        public event EventHandler<PongEventArgs> Pong;
    }

    internal class WebSocketListenerActions : Java.Lang.Object, IWebSocketListener
    {
        void IWebSocketListener.OnClose(int code, string reason)
        {
            var handler = Close;
            if (handler != null)
            {
                handler(code, reason);
            }
        }

        void IWebSocketListener.OnFailure(Java.IO.IOException exception, Response response)
        {
            var handler = Failure;
            if (handler != null)
            {
                handler(exception, response);
            }
        }

		void IWebSocketListener.OnMessage(ResponseBody payload)
        {
            var handler = Message;
            if (handler != null)
            {
				handler(payload);
            }
        }

        void IWebSocketListener.OnOpen(IWebSocket socket, Response response)
        {
            var handler = Open;
            if (handler != null)
            {
                handler(socket, response);
            }
        }

        void IWebSocketListener.OnPong(OkBuffer buffer)
        {
            var handler = Pong;
            if (handler != null)
            {
                handler(buffer);
            }
        }

        public Action<int, string> Close;
        public Action<Java.IO.IOException, Response> Failure;
		public Action<ResponseBody> Message;
        public Action<IWebSocket, Response> Open;
        public Action<OkBuffer> Pong;
    }
}
