using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.DomainModels
{
    public class Wallet : DomainModelBridge
    {
        public string AgentId { get; private set; } // PK
        public ApplicationUser Agent { get; private set; }
        public decimal WalletLimit { get; private set; }
        public decimal ConsumedFunds { get; private set; }

        private readonly List<Reservation> _reservations = new();
        public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();

        public decimal EffectiveAvailableFunds => WalletLimit - Math.Round((ConsumedFunds + _reservations.Sum(r => r.Amount)),2);
        public decimal EffectiveUtilizedPercentage => WalletLimit == 0 ? 0 : Math.Round(((ConsumedFunds + _reservations.Sum(r => r.Amount)) / WalletLimit) * 100, 2);
        public decimal EffectiveUtilizedFunds => WalletLimit == 0 ? 0 : Math.Round(ConsumedFunds + _reservations.Sum(r => r.Amount), 2);

        public Wallet()
        {
           
            _reservations = new List<Reservation>();
        }
        public Wallet(string agentId, decimal walletLimit)
        {
            AgentId = agentId;
            WalletLimit = walletLimit;
            ConsumedFunds = 0;
        }

        public bool CanPermitCollection(decimal amount) => EffectiveAvailableFunds >= amount;

        public void SetWalletLimit(decimal newLimit)
        {
            if (newLimit < 0)
                throw new ArgumentOutOfRangeException(nameof(newLimit), "Wallet limit cannot be negative.");

            this.WalletLimit = newLimit;
        }

        public Reservation ReserveFunds(decimal amount)
        {
            if (!CanPermitCollection(amount))
                return null;
               //throw new InvalidOperationException("Insufficient available funds.");

            var reservation = new Reservation(SequentialGuid.NewGuidString(), amount);
            _reservations.Add(reservation);
            return reservation;
        }

        public void ConsumeFunds(string reservationId)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == reservationId)
                ?? throw new InvalidOperationException("Reservation not found.");

            reservation.TrackingState = TrackingState.Deleted;
            ConsumedFunds += reservation.Amount;
           // _reservations.Remove(reservation);
            this.SetAddedOrModified();
        }

        public void ReleaseFunds(decimal amount)
        {
          
            ConsumedFunds -= amount;           
            this.SetAddedOrModified();
        }

        public void CancelReservation(string reservationId)
        {
            var reservation = _reservations.FirstOrDefault(r => r.Id == reservationId)
                ?? throw new InvalidOperationException("Reservation not found.");

            _reservations.Remove(reservation);
        }
    }

    public class Reservation : IObjectWithState
    {
        public string Id { get; private set; }
        public decimal Amount { get; private set; }
        [NotMapped]
        public string Type { get; set; }
        [NotMapped]
        public TrackingState TrackingState { get; set; }

        public Reservation() { }

        public Reservation(string id, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Reservation amount must be positive.");

            Id = id;
            Amount = amount;
            TrackingState = TrackingState.Added;
        }
    }

}
