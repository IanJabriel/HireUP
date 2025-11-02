namespace HireUP.Domain.Entities.Base
{
    public abstract class EmployerRegistrableEntity
    {
        public abstract int Id { get; protected set; }
        public abstract List<Employer> EmployeesIds { get; protected set; }

        public void RegisterEmployer(Employer employer)
        {
            if (employer == null)
                throw new ArgumentNullException(nameof(employer));

            if (EmployeesIds.Any(e => e.Id == employer.Id))
                throw new InvalidOperationException($"Empregador já está inscrito neste {GetEntityTypeName()}");

            EmployeesIds.Add(employer);
        }

        public void UnregisterEmployer(int employerId)
        {
            var employer = EmployeesIds.FirstOrDefault(e => e.Id == employerId);
            
            if (employer == null)
                throw new InvalidOperationException($"Empregador não está inscrito neste {GetEntityTypeName()}");

            EmployeesIds.Remove(employer);
        }

        public bool IsEmployerRegistered(int employerId)
        {
            return EmployeesIds.Any(e => e.Id == employerId);
        }

        protected abstract string GetEntityTypeName();
    }
}
