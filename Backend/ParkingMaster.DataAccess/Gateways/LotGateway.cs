//using ParkingMaster.DataAccess.Context; // giving error for some reason
using ParkingMaster.Models.Models;
using ParkingMaster.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;

namespace ParkingMaster.DataAccess
{
    public class LotGateway : IDisposable
    {
        UserContext context;

        public LotGateway()
        {
            context = new UserContext();
        }

        public LotGateway(UserContext c)
        {
            context = c;
        }

        public ResponseDTO<bool> AddLot(Lot lot, List<Spot> spotList)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Lots.Add(lot);

                    foreach (Spot spot in spotList)
                    {
                        context.Spots.Add(spot);
                    }

                    context.SaveChanges();

                    dbContextTransaction.Commit();

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = "[DATA ACCESS] Failed to add lot to data store."
                    };
                }
            }
        }

        public ResponseDTO<bool> AddMapFile(HttpPostedFile mapfile)
        {
            try
            {
                //Directory.CreateDirectory(@"C:\\MapFiles\\"); // Will do nothing if Directory already exists
                //string filepath = "C:\\MapFiles\\" + lot.MapFilePath;
                //mapfile.SaveAs(filepath);
                return new ResponseDTO<bool>()
                {
                    Data = true
                };

            }
            catch (HttpException)
            {
                return new ResponseDTO<bool>()
                {
                    Data = false,
                    Error = "[DATA ACCESS] Cannot save map file."
                };
            }
        }

        public ResponseDTO<bool> DeleteLot(Guid ownerid, string lotname)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var deletelot = (from lot in context.Lots
                               where lot.OwnerId == ownerid &&
                               lot.LotName == lotname
                               select lot).FirstOrDefault();
                    context.Lots.Remove(deletelot);
                    context.SaveChanges();
                    
                    dbContextTransaction.Commit();

                    //string filepath = "C:\\MapFiles\\" + lot.MapFilePath;
                    //File.Delete(filepath);

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = "[DATA ACCESS] Failed to delete lot from data store."
                    };
                }
            }
        }

        /*
        public ResponseDTO<Boolean> EditLotName(Guid ownerid, string oldlotname, string newlotname)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var editlot = (from lot in context.Lots
                                     where lot.OwnerId == ownerid &&
                                     lot.LotName == oldlotname
                                     select lot).FirstOrDefault();

                    editlot.LotName = newlotname;

                    context.SaveChanges();

                    dbContextTransaction.Commit();

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = "[DATA ACCESS] Failed to edit lot name."
                    };
                }
            }
        }
        */

        public ResponseDTO<Lot> GetLotByName(Guid ownerid, string lotname)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var returnlot = (from lot in context.Lots
                                   where lot.OwnerId == ownerid &&
                                   lot.LotName == lotname
                                   select lot).FirstOrDefault();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<Lot>()
                    {
                        Data = returnlot
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<Lot>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch lot."
                    };
                }
            }
        }

        public ResponseDTO<List<Lot>> GetAllLots()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var lots = (from lot in context.Lots select lot).ToList();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = lots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch lots."
                    };
                }
            }
        }

        public ResponseDTO<List<Lot>> GetAllLotsByOwner(Guid ownerid)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var lots = (from lot in context.Lots where lot.OwnerId == ownerid select lot).ToList();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = lots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Lot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch lots."
                    };
                }
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpots()
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var spots = (from spot in context.Spots select spot).ToList();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = spots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch spots."
                    };
                }
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByOwner(Guid ownerid)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<Spot> spots = new List<Spot>();

                    var lots = (from lot in context.Lots where lot.OwnerId == ownerid select lot).ToList();
                    List<Spot> lotspots = new List<Spot>();
                    foreach (Lot l in lots)
                    {
                        lotspots = (from spot in context.Spots where spot.LotId == l.LotId select spot).ToList();
                        foreach (Spot sp in lotspots)
                        {
                            spots.Add(sp);
                        }
                    }

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = spots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch spots."
                    };
                }
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByLot(Guid lotId)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<Spot> lotspots = (from spot in context.Spots where spot.LotId == lotId select spot).ToList();

                    //context.SaveChanges();

                    //dbContextTransaction.Commit();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = lotspots
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<List<Spot>>()
                    {
                        Data = null,
                        Error = "[DATA ACCESS] Could not fetch spots."
                    };
                }
            }
        }

        public ResponseDTO<bool> ReserveSpot(ReservationDTO reservation)
        {
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    var _spot = (from spot in context.Spots where spot.SpotId == reservation.SpotId select spot).FirstOrDefault();

                    // Check that spot is not currently occupied
                    if (DateTime.Now.CompareTo(_spot.ReservedUntil) != 1)
                    {
                        return new ResponseDTO<bool>()
                        {
                            Data = false,
                            Error = "[DATA ACCESS] Spot is already reserved."
                        };
                    }

                    // If spot is free, reserve spot with new information
                    _spot.ReservedUntil = DateTime.Now.AddMinutes(reservation.DurationInMinutes);
                    _spot.TakenBy = reservation.UserId;
                    _spot.VehicleVin = reservation.VehicleVin;
                    
                    context.SaveChanges();
                    dbContextTransaction.Commit();

                    return new ResponseDTO<bool>()
                    {
                        Data = true
                    };
                }
                catch (Exception)
                {
                    //dbContextTransaction.Rollback();

                    return new ResponseDTO<bool>()
                    {
                        Data = false,
                        Error = "[DATA ACCESS] Error occured while reserving spot."
                    };
                }
            }
        }

        public void ResetDatabase()
        {
            List<Spot> spots = GetAllSpots().Data;
            List<Lot> lots = GetAllLots().Data;

            foreach (Spot s in spots)
            {
                context.Spots.Remove(s);
            }
            foreach (Lot l in lots)
            {
                context.Lots.Remove(l);
            }

        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
