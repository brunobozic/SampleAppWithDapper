using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SampleAppWithDapper.DataAccess.Repositories.Aircraft
{

    public class AircraftRepository : IRepositoryAsync<Domain.DomainModels.Aircraft.Aircraft>, IAircraftRepository
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        private DatabaseConnectionManager _context;
        public AircraftRepository(DatabaseConnectionManager dbProvider)
        {
            _connection = dbProvider.Connection;
            if (_connection.State == ConnectionState.Closed) _connection.Open();
            _transaction = dbProvider.Transaction;
            _context = dbProvider;
        }

        public IDbConnection Connection { get => _connection; }
        public IDbTransaction Transaction { get => _transaction; set { _transaction = value; _context.Transaction = value; } }

        public async Task<IEnumerable<Domain.DomainModels.Aircraft.Aircraft>> GetAll()
        {
            string sQuery = "SELECT * FROM Aircraft";
            return await _connection.QueryAsync<Domain.DomainModels.Aircraft.Aircraft>(sQuery);
        }

        public async Task<IEnumerable<Domain.DomainModels.Aircraft.Aircraft>> Get(Func<Domain.DomainModels.Aircraft.Aircraft, bool> predicate)
        {
            string sQuery = @"
                                SELECT 
                                       Id
                                      ,Manufacturer
                                      ,Model
                                      ,RegistrationNumber
                                      ,FirstClassCapacity
                                      ,RegularClassCapacity
                                      ,CrewCapacity
                                      ,ManufactureDate
                                      ,NumberOfEngines
                                      ,EmptyWeight
                                      ,MaxTakeoffWeight
                                  FROM Aircraft"
            ;

            IEnumerable<Domain.DomainModels.Aircraft.Aircraft> aircraft = await _connection.QueryAsync<Domain.DomainModels.Aircraft.Aircraft>(sQuery, null);

            return aircraft.Where(predicate);
        }

        public async Task<Domain.DomainModels.Aircraft.Aircraft> GetOne(object id)
        {
            var sQuery = @"
                                SELECT 
                                       Id
                                      ,Manufacturer
                                      ,Model
                                      ,RegistrationNumber
                                      ,FirstClassCapacity
                                      ,RegularClassCapacity
                                      ,CrewCapacity
                                      ,ManufactureDate
                                      ,NumberOfEngines
                                      ,EmptyWeight
                                      ,MaxTakeoffWeight
                                  FROM Aircraft WHERE Id = @Id"
            ;

            return await _connection.QueryFirstOrDefaultAsync<Domain.DomainModels.Aircraft.Aircraft>(sQuery, new { Id = id });
        }

        public async Task<int> Insert(Domain.DomainModels.Aircraft.Aircraft entity)
        {
            int retval = -1;
            if (entity != null)
            {
                string sQuery = @"
                                    INSERT INTO Aircraft 
                                        (Manufacturer
                                        ,Model
                                        ,RegistrationNumber
                                        ,FirstClassCapacity
                                        ,RegularClassCapacity
                                        ,CrewCapacity
                                        ,ManufactureDate
                                        ,NumberOfEngines
                                        ,EmptyWeight
                                        ,MaxTakeoffWeight)
                                    VALUES (@Manufacturer
                                        ,@Model
                                        ,@RegistrationNumber
                                        ,@FirstClassCapacity
                                        ,@RegularClassCapacity
                                        ,@CrewCapacity
                                        ,@ManufactureDate
                                        ,@NumberOfEngines
                                        ,@EmptyWeight
                                        ,@MaxTakeoffWeight)"
                ;

                sQuery += "SELECT CAST(SCOPE_IDENTITY() as int); ";
                if (_transaction == null) Transaction = _connection.BeginTransaction();
                IEnumerable<int> retvals = await _connection.QueryAsync<int>(sQuery, entity, _transaction);
                retval = retvals.Single();
            }
            return retval;
        }

        public async Task Delete(Domain.DomainModels.Aircraft.Aircraft entity)
        {
            if (entity != null)
            {
                var id = entity.Id;
                string sQuery = "DELETE FROM Aircraft"
                                + " WHERE Id = @Id";
                if (_transaction == null) Transaction = _connection.BeginTransaction();
                await _connection.ExecuteAsync(sQuery, new { Id = id }, _transaction);
            }
        }

        public async Task Delete(object id)
        {
            string sQuery = "DELETE FROM Aircraft"
                            + " WHERE Id = @Id";
            if (_transaction == null) Transaction = _connection.BeginTransaction();
            await _connection.ExecuteAsync(sQuery, new { Id = id }, _transaction);
        }

        public async Task SoftDelete(Domain.DomainModels.Aircraft.Aircraft entity)
        {
            if (entity != null)
            {
                string sQuery = @"
                                    UPDATE Aircraft 
                                    SET  
                                         DeletedUtc = @DeletedUtc
                                        ,DeletedBy = @DeletedBy
                                        ,IsDeleted = @IsDeleted 
                                    WHERE Id = @Id"
                    ;
                if (_transaction == null) Transaction = _connection.BeginTransaction();
                await _connection.QueryAsync(sQuery, entity, _transaction);

            }
        }

        public async Task SoftDelete(object id)
        {
            string sQuery = @"
                                    UPDATE Aircraft 
                                    SET  
                                         DeletedUtc = @DeletedUtc
                                        ,DeletedBy = @DeletedBy
                                        ,IsDeleted = @IsDeleted 
                                    WHERE Id = @Id"
                ;
            if (_transaction == null) Transaction = _connection.BeginTransaction();
            await _connection.ExecuteAsync(sQuery, new { Id = id }, _transaction);
        }

        public async Task UpdateAsync(object id, Domain.DomainModels.Aircraft.Aircraft entity)
        {
            if (entity != null)
            {
                string sQuery = @"
                                    UPDATE Aircraft 
                                    SET  
                                         Manufacturer = @Manufacturer
                                        ,Model = @Model
                                        ,RegistrationNumber = @RegistrationNumber 
                                        ,FirstClassCapacity = @FirstClassCapacity
                                        ,RegularClassCapacity = @RegularClassCapacity
                                        ,CrewCapacity = @CrewCapacity
                                        ,ManufactureDate = @ManufactureDate
                                        ,NumberOfEngines = @NumberOfEngines
                                        ,EmptyWeight = @EmptyWeight
                                        ,MaxTakeoffWeight = @MaxTakeoffWeight
                                    WHERE Id = @Id"
                ;
                if (_transaction == null) Transaction = _connection.BeginTransaction();
                await _connection.QueryAsync(sQuery, entity, _transaction);
            }
        }
    }
}
