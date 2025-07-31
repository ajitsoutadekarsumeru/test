using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public partial class Communication : DomainModel
    {
        public Communication() { }

        public Communication(            
            string actorId,
            string channel,
            string messageBody,
            string messageSubject,
            string recipient,
            string recipientType,
            string language,
            string status
          )
        {
            Id = SequentialGuid.NewGuidString();
            ActorId = actorId;
            Channel = channel;
            MessageSubject = messageSubject;
            MessageBody = messageBody ?? throw new ArgumentNullException(nameof(messageBody));
            Recipient = recipient ?? throw new ArgumentNullException(nameof(recipient));
            RecipientType = recipientType ?? throw new ArgumentNullException(nameof(recipientType));
            Language = language;           
            Status = status;            
        }

        #region Attributes
        [StringLength(32)]
        public string? ActorId { get; private set; }   // account/user/etc. that this comm targets
        [StringLength(50)]
        public string Channel { get; private set; }       
        public string MessageBody { get; private set; }
        [StringLength(50)]
        public string? MessageSubject { get; private set; }
        [StringLength(50)]
        public string Recipient { get; private set; }       
        [StringLength(50)]
        public string RecipientType { get; private set; } // Recipient type (Customer, Agent, etc.)
        [StringLength(50)]
        public string Language { get; private set; }

        [StringLength(50)]
        public string Status { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string? MetadataJson { get; private set; } // Optional JSON or Metadata field for channel-specific extra details
        public DateTime? DispatchedAt { get; private set; }
        public DateTime? DeliveredAt { get; private set; }
        public DateTime? ReadAt { get; private set; }
        #endregion

        #region State transitions
        public void MarkDispatched(DateTime whenUtc)
        {
            Status = CommunicationStatusEnum.Dispatched.Value;
            DispatchedAt = whenUtc;
            LastModifiedDate = DateTime.UtcNow;
        }
        public void MarkDelivered(DateTime whenUtc)
        {
            Status = CommunicationStatusEnum.Delivered.Value;
            DeliveredAt = whenUtc;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void MarkRead(DateTime whenUtc)
        {
            Status = CommunicationStatusEnum.Read.Value;
            ReadAt = whenUtc;
            LastModifiedDate = DateTime.UtcNow;
        }

        public void MarkFailed(string errorMessage, DateTime whenUtc)
        {
            Status = CommunicationStatusEnum.Failed.Value;
            ErrorMessage = errorMessage;
            DispatchedAt ??= whenUtc; // might fail before/after dispatch
            LastModifiedDate = DateTime.UtcNow;
        }
        #endregion



    }
}
