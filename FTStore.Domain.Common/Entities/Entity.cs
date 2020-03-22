using System;
using System.Collections.Generic;

using FluentValidation.Results;

namespace FTStore.Domain.Common.Entities
{
    public abstract class Entity
    {
        public ValidationResult ValidationResult { get => _validationResult; }
        protected ValidationResult _validationResult = new ValidationResult();


        public int Id { get; protected set; }

        public void DefineId(int id)
        {
            this.Id = id;
        }

        public abstract bool IsValid();

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var compareTo = obj as Entity;

            return Id.Equals(compareTo.Id);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 42) + Id.GetHashCode();
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
