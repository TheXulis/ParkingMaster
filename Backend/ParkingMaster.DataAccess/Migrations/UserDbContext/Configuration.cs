﻿using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Constants;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ParkingMaster.DataAccess.Migrations.UserDbContext
{
	class Configuration : DbMigrationsConfiguration<ParkingMaster.DataAccess.UserContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			MigrationsDirectory = @"Migrations\UserDbContext";
		}

		//Seed method
		protected override void Seed(ParkingMaster.DataAccess.UserContext context)
		{
            // Uncomment any code you wish to run

            //ResetDatabase(context);                   // Completely Resets the Database (Except for TOS)
            //ResetThenAddFunctions(context);           // Just resets functions and adds all specified functions
            //AddTOS(context);                          // Adds in a TOS (only run this once, if you already have a TOS do not rerun even after ResetDatabase())
            //CreateAdminFromExistingUser(context);     // Manually changes a user into an admin, must manually change userId for admin
        }

        private void CreateAdminFromExistingUser(ParkingMaster.DataAccess.UserContext context)
        {
            var userGateway = new UserGateway(context);
            Guid adminId = new Guid("C0651A73-D8A4-4FDB-A5B3-032E4F9442EA");
            List<Claim> claimsList = new List<Claim>();

            UserAccountDTO adminAccount = userGateway.GetUserByUserId(adminId).Data;
            claimsList.Add(new Claim("Action", "UsageDashboard"));
            claimsList.Add(new Claim("Action", "ViewLogs"));
            claimsList.Add(new Claim("User", adminAccount.Username));

            adminAccount.RoleType = Roles.ADMINISTRATOR;
            userGateway.SetRole(adminAccount);
            userGateway.ResetUserClaims(adminId, claimsList);
        }

        private void ResetThenAddFunctions(ParkingMaster.DataAccess.UserContext context)
        {
            var functionGateway = new FunctionGateway(context);
            functionGateway.ResetDatabase();

            // Add Functions For Authorizarion
            functionGateway.StoreFunction(new Function("SetRole", true));
            functionGateway.StoreFunction(new Function("AddParkingLot", true));
            functionGateway.StoreFunction(new Function("DeleteParkingLot", true));
            functionGateway.StoreFunction(new Function("ViewParkingLot", true));
            functionGateway.StoreFunction(new Function("ViewAllParkingLots", true));
            functionGateway.StoreFunction(new Function("AddVehicle", true));
            functionGateway.StoreFunction(new Function("ReserveParkingSpot", true));
            functionGateway.StoreFunction(new Function("UpdateReservation", true));
            functionGateway.StoreFunction(new Function("UsageDashboard", true));
            functionGateway.StoreFunction(new Function("ViewLogs", true));
        }

        private void AddTOS(ParkingMaster.DataAccess.UserContext context)
        {
            var tosGateway = new TosGateway(context);

            // Add TOS
            string filepath = "C:\\TOS\\default.txt";
            string text = File.ReadAllText(filepath);
            TermsOfService tos = new TermsOfService()
            {
                Content = text,
                TosName = "Initial TOS"
            };
            tosGateway.AddAndSetNewTos(tos);
        }

        private void ResetDatabase(ParkingMaster.DataAccess.UserContext context)
        {
            // Reset Database
            var userGateway = new UserGateway(context);
            userGateway.ResetDatabase();
            var functionGateway = new FunctionGateway(context);
            functionGateway.ResetDatabase();
            var lotGateway = new LotGateway(context);
            lotGateway.ResetDatabase();
            var sessionGateway = new SessionGateway(context);
            sessionGateway.ResetDatabase();
        }

        private void UnitTestSeed(ParkingMaster.DataAccess.UserContext context)
        {
            // Reset Database
            var userGateway = new UserGateway(context);
            userGateway.ResetDatabase();
            var functionGateway = new FunctionGateway(context);
            functionGateway.ResetDatabase();
            var lotGateway = new LotGateway(context);
            lotGateway.ResetDatabase();
            var sessionGateway = new SessionGateway(context);
            sessionGateway.ResetDatabase();

            UserAccount user = new UserAccount()
            {
                SsoId = new Guid("12345678-1234-1234-1234-123456789ABC"),
                Username = "pnguyen@gmail.com",
                IsActive = true,
                AcceptedTOS = false,
                RoleType = "standard"
            };
            // Creating a list of claims
            List<Claim> claims = new List<Claim>
            {
                new Claim("User", "pnguyen@gmail.com"),
                new Claim("Action", "DisabledAction"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout"),
                new Claim("Parent", "client1@yahoo.com")
            };
            userGateway.StoreIndividualUser(user, claims);


            // Do not use this user for tests because it will be deleted in UserManagementService tests
            user = new UserAccount()
            {
                SsoId = new Guid("87654321-4321-4321-4321-CBA987654321"),
                Id = new Guid("88888888-4444-3333-2222-111111111111"),
                Username = "plaurent@yahoo.com",
                IsActive = true,
                AcceptedTOS = false,
                RoleType = "standard"
            };
            claims = new List<Claim>
            {
                new Claim("User", "plaurent@yahoo.com"),
                new Claim("Action", "DisabledAction"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout"),
                new Claim("Parent", "client1@yahoo.com")
            };
            userGateway.StoreIndividualUser(user, claims);


            user = new UserAccount()
            {
                SsoId = new Guid("24682468-2468-2468-2468-CBA987654321"),
                Username = "tnguyen@gmail.com",
                IsActive = true,
                AcceptedTOS = false,
                RoleType = "standard"
            };
            claims = new List<Claim>
            {
                new Claim("User", "tnguyen@gmail.com"),
                new Claim("Action", "Logout"),
                new Claim("Action", "Client2Action"),
                new Claim("Parent", "client2@gmail.com")
            };
            userGateway.StoreIndividualUser(user, claims);


            user = new UserAccount()
            {
                SsoId = new Guid("13571357-1357-1357-1357-CBA987654321"),
                Username = "client1@yahoo.com",
                IsActive = true,
                AcceptedTOS = false,
                RoleType = "lotmanager"
            };
            claims = new List<Claim>
            {
                new Claim("User", "client1@yahoo.com"),
                new Claim("Action", "Client1Action"),
                new Claim("Action", "DisabledAction"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout")
            };
            userGateway.StoreIndividualUser(user, claims);


            user = new UserAccount()
            {
                SsoId = new Guid("13571357-1357-1357-1357-CBA987654321"),
                Username = "client2@gmail.com",
                IsActive = true,
                AcceptedTOS = false,
                RoleType = "lotmanager"
            };
            claims = new List<Claim>
            {
                new Claim("User", "client2@gmail.com"),
                new Claim("Action", "Client2Action"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout"),
                new Claim("Action", "Client2Action")
            };
            userGateway.StoreIndividualUser(user, claims);

            var user1 = new UserAccount()
            {
                SsoId = new Guid("2AE9A868-17AA-490F-9094-5907D2E64EBB"),
                Username = "user1@yahoo.com",
                IsActive = true,
                AcceptedTOS = false,
                RoleType = "standard"
            };
            claims = new List<Claim>
            {
                new Claim("User", "user1@yahoo.com"),
                new Claim("Action", "DisabledAction"),
                new Claim("Action", "CreateOtherUser"),
                new Claim("Action", "Logout"),
                new Claim("Parent", "client1@yahoo.com")
            };
            userGateway.StoreIndividualUser(user1, claims);




            functionGateway.StoreFunction(new Function("DisableAction", false));
            functionGateway.StoreFunction(new Function("CreateOtherUser", true));
            functionGateway.StoreFunction(new Function("Logout", true));
            functionGateway.StoreFunction(new Function("Client1Action", true));
            functionGateway.StoreFunction(new Function("Client2Action", true));
            functionGateway.StoreFunction(new Function("AddParkingLot", true));
            functionGateway.StoreFunction(new Function("DeleteParkingLot", true));

            functionGateway.StoreFunction(new Function("SetRole", true));

            

            sessionGateway.StoreSession(new Session(user1.Id));
            sessionGateway.StoreSession(new Session(userGateway.GetUserByUsername("pnguyen@gmail.com").Data.Id));
            ResponseDTO<Session> sessionDTO = sessionGateway.StoreSession(new Session(userGateway.GetUserByUsername("client1@yahoo.com").Data.Id));

            UserAccountDTO testUser = userGateway.GetUserByUsername("client1@yahoo.com").Data;
            Lot testLot = new Lot
            {
                LotId = new Guid("205296C2-9827-404B-9C95-2A28E00BFE0A"),
                OwnerId = testUser.Id,
                LotName = "TestLot",
                Address = "123 Testing St.",
                Cost = 20.0,
                UserAccount = sessionDTO.Data.UserAccount,
                MapFilePath = "client1_TestLot_123"
            };
            List<Spot> testSpots = new List<Spot>
            {
                new Spot
                {
                    SpotId = new Guid("ABADF4D9-7310-4E1B-9A5C-66E0055DD99D"),
                    SpotName = "1",
                    LotId = testLot.LotId,
                    LotName = testLot.LotName,
                    IsHandicappedAccessible = false,
                    Lot = testLot
                },
                new Spot
                {
                    SpotId = new Guid("D1B84BA3-21EE-4E44-A415-606E2F6F8FAA"),
                    SpotName = "2",
                    LotId = testLot.LotId,
                    LotName = testLot.LotName,
                    IsHandicappedAccessible = false,
                    Lot = testLot
                }
            };

            lotGateway.AddLot(testLot, testSpots);

            // Add Vehicles
            var vehicleGateway = new VehicleGateway(context);
            testUser = userGateway.GetUserByUsername("pnguyen@gmail.com").Data;

            Vehicle userVehicle = new Vehicle()
            {
                OwnerId = testUser.Id,
                Make = "Car Make",
                Model = "Car Model",
                Year = 2019,
                State = "CA",
                Plate = "1ABC123",
                Vin = "1ABCD12345A123456"
            };

            vehicleGateway.StoreVehicle(userVehicle);


            // Add Test Reservations
            ReservationDTO reservation = new ReservationDTO()
            {
                SpotId = new Guid("ABADF4D9-7310-4E1B-9A5C-66E0055DD99D"),
                UserId = testUser.Id,
                VehicleVin = userVehicle.Vin,
                DurationInMinutes = 2
            };
            lotGateway.ReserveSpot(reservation);
        }
    }
}
