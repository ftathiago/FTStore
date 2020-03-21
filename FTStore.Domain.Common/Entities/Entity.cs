using System;
using System.Collections.Generic;

using FluentValidation.Results;

namespace FTStore.Domain.SharedKernel.Entities
{
    public abstract class Entity
    {
        protected ValidationResult _validationResult;

        public int Id { get; protected set; }

        public abstract bool IsValid();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var compareTo = obj as Entity;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 46) + Id.GetHashCode();
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;
            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{this.GetType().Name} Id[{Id.ToString()}]";
        }
    }
}
