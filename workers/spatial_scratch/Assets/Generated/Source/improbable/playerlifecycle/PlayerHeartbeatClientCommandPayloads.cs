// ===========
// DO NOT EDIT - this file is automatically regenerated.
// ===========

using System.Collections.Generic;
using Improbable.Worker.CInterop;
using Improbable.Gdk.Core;
using Improbable.Gdk.Core.Commands;

namespace Improbable.PlayerLifecycle
{
    public partial class PlayerHeartbeatClient
    {
        public partial class PlayerHeartbeat
        {
            public struct Request : ICommandRequest
            {
                public EntityId TargetEntityId;
                public global::Improbable.Common.Empty Payload;
                public uint? TimeoutMillis;
                public bool AllowShortCircuiting;
                public object Context;

                public Request(
                    EntityId targetEntityId,
                    global::Improbable.Common.Empty request,
                    uint? timeoutMillis = null,
                    bool allowShortCircuiting = false,
                    object context = null)
                {
                    TargetEntityId = targetEntityId;
                    Payload = request;
                    TimeoutMillis = timeoutMillis;
                    AllowShortCircuiting = allowShortCircuiting;
                    Context = context;
                }
            }


            public readonly struct ReceivedRequest : IReceivedCommandRequest
            {
                public readonly EntityId EntityId;
                public readonly long RequestId;
                public readonly string CallerWorkerId;
                public readonly List<string> CallerAttributeSet;
                public readonly global::Improbable.Common.Empty Payload;

                public ReceivedRequest(
                    EntityId entityId,
                    long requestId,
                    string callerWorkerId,
                    List<string> callerAttributeSet,
                    global::Improbable.Common.Empty request)
                {
                    EntityId = entityId;
                    RequestId = requestId;
                    CallerWorkerId = callerWorkerId;
                    CallerAttributeSet = callerAttributeSet;
                    Payload = request;
                }

                long IReceivedCommandRequest.GetRequestId()
                {
                    return RequestId;
                }

                EntityId IReceivedEntityMessage.GetEntityId()
                {
                    return EntityId;
                }
            }

            /// <summary>
            ///     A Response will be considered a failure if FailureMessage is not null;
            /// </summary>
            // todo Should consider making this a union of a failure and a response
            // todo consider making this readonly
            public struct Response : ICommandResponse
            {
                public long RequestId;
                public global::Improbable.Common.Empty? Payload;
                public string FailureMessage;

                public Response(long requestId, global::Improbable.Common.Empty payload)
                {
                    RequestId = requestId;
                    Payload = payload;
                    FailureMessage = null;
                }

                public Response(long requestId, string failureMessage)
                {
                    RequestId = requestId;
                    Payload = null;
                    FailureMessage = failureMessage;
                }
            }

            public readonly struct ReceivedResponse : IReceivedCommandResponse
            {
                public readonly Unity.Entities.Entity SendingEntity;
                public readonly EntityId EntityId;
                public readonly string Message;
                public readonly StatusCode StatusCode;
                public readonly global::Improbable.Common.Empty? ResponsePayload;
                public readonly global::Improbable.Common.Empty RequestPayload;
                public readonly System.Object Context;
                public readonly long RequestId;

                public ReceivedResponse(
                    Unity.Entities.Entity sendingEntity,
                    EntityId entityId,
                    string message,
                    StatusCode statusCode,
                    global::Improbable.Common.Empty? response,
                    global::Improbable.Common.Empty request,
                    System.Object context,
                    long requestId)
                {
                    SendingEntity = sendingEntity;
                    EntityId = entityId;
                    Message = message;
                    StatusCode = statusCode;
                    ResponsePayload = response;
                    RequestPayload = request;
                    Context = context;
                    RequestId = requestId;
                }

                long IReceivedCommandResponse.GetRequestId()
                {
                    return RequestId;
                }
            }

            public readonly struct RawReceivedResponse : IRawReceivedCommandResponse
            {
                public readonly EntityId EntityId;
                public readonly string Message;
                public readonly StatusCode StatusCode;
                public readonly global::Improbable.Common.Empty? ResponsePayload;
                public readonly long RequestId;

                public RawReceivedResponse(
                    EntityId entityId,
                    string message,
                    StatusCode statusCode,
                    global::Improbable.Common.Empty? response,
                    long requestId)
                {
                    EntityId = entityId;
                    Message = message;
                    StatusCode = statusCode;
                    ResponsePayload = response;
                    RequestId = requestId;
                }
            }
        }
    }
}
