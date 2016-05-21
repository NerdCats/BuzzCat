namespace BuzzCat.App
{
    using Microsoft.AspNet.SignalR.Hubs;
    using NLog;

    public class LoggingPipelineModule : HubPipelineModule
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected override bool OnBeforeConnect(IHub hub)
        {
            logger.Debug($"Incoming connection to {hub.GetType().Name} from connection id {hub.Context.ConnectionId}");
            return base.OnBeforeConnect(hub);
        }

        protected override void OnAfterConnect(IHub hub)
        {
            logger.Debug($"Incoming connection established {hub.GetType().Name} from connection id {hub.Context.ConnectionId}");
            base.OnAfterConnect(hub);
        }

        protected override bool OnBeforeDisconnect(IHub hub, bool stopCalled)
        {
            logger.Debug($"Connection id {hub.Context.ConnectionId} disconnecting connection from hub {hub.GetType().Name}, stopCalled: { stopCalled}");
            return base.OnBeforeDisconnect(hub, stopCalled);
        }

        protected override void OnAfterDisconnect(IHub hub, bool stopCalled)
        {
            logger.Debug($"Connection id {hub.Context.ConnectionId} disconnected connection from hub {hub.GetType().Name}, stopCalled: {stopCalled}");
            base.OnAfterDisconnect(hub, stopCalled);
        }

        protected override bool OnBeforeReconnect(IHub hub)
        {
            logger.Debug($"Connection id {hub.Context.ConnectionId} reconnecting connection to hub {hub.GetType().Name}");
            return base.OnBeforeReconnect(hub);
        }

        protected override void OnAfterReconnect(IHub hub)
        {
            logger.Debug($"Connection id {hub.Context.ConnectionId} reconnected connection to hub {hub.GetType().Name}");
            base.OnAfterReconnect(hub);
        }

        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            logger.Debug($"Method {context.MethodDescriptor.Name} getting invoked in {context.Hub.GetType().Name} hub from {context.Hub.Context.ConnectionId}");
            return base.OnBeforeIncoming(context);
        }

        protected override object OnAfterIncoming(object result, IHubIncomingInvokerContext context)
        {
            logger.Debug($"Method {context.MethodDescriptor.Name} invoked in {context.Hub.GetType().Name} hub from {context.Hub.Context.ConnectionId}");
            return base.OnAfterIncoming(result, context);
        }

        protected override bool OnBeforeOutgoing(IHubOutgoingInvokerContext context)
        {
            logger.Debug($"Method {context.Invocation.Method} invoking");
            return base.OnBeforeOutgoing(context);
        }

        protected override void OnAfterOutgoing(IHubOutgoingInvokerContext context)
        {
            logger.Debug($"Method {context.Invocation.Method} invoked");
            base.OnAfterOutgoing(context);
        }

        protected override void OnIncomingError(ExceptionContext exceptionContext, IHubIncomingInvokerContext invokerContext)
        {
            logger.Error($"Incoming Connection Error {exceptionContext.Error.Message}");
            if (exceptionContext.Error.InnerException != null)
            {
                logger.Info(exceptionContext.Error.InnerException);
            }

            base.OnIncomingError(exceptionContext, invokerContext);
        }
    }
}
