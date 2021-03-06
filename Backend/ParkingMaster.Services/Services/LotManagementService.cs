using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ParkingMaster.DataAccess;
using ParkingMaster.DataAccess.Gateways;
using ParkingMaster.Models.DTO;
using ParkingMaster.Models.Models;
using System.Drawing;

namespace ParkingMaster.Services.Services
{
    public class LotManagementService : ILotManagementService
    {
        UserContext _dbcontext;
        private LotGateway _lotGateway;

        public LotManagementService()
        {
            _lotGateway = new LotGateway();
        }

        public LotManagementService(UserContext context)
        {
            _dbcontext = context;
            _lotGateway = new LotGateway(_dbcontext);
        }

        public ResponseDTO<bool> AddLot(Guid ownerid, string lotname, string address, double cost, UserAccount useraccount, HttpPostedFile spotfile, HttpPostedFile mapfile)
        { 
            try
            {
                Lot newLot = new Lot()
                {
                    LotId = Guid.NewGuid(),
                    OwnerId = ownerid,
                    LotName = lotname,
                    Address = address,
                    Cost = cost,
                    //UserAccount = useraccount
                    MapFilePath = useraccount.Username + "_" + lotname + "_" + Guid.NewGuid().ToString() + Path.GetExtension(mapfile.FileName)
                };
                newLot.Spots = ParseSpotsFromFile(newLot.LotId, newLot.LotName, spotfile);
                ResponseDTO<bool> response = _lotGateway.AddLot(newLot, newLot.Spots);
                if (response.Data == true)
                {
                    ResponseDTO<bool> saveMap = _lotGateway.AddMapFile(mapfile, newLot.MapFilePath);
                    if (saveMap.Data == false)
                    {
                        return saveMap;
                    }
                }
                return response;
            }
            catch (ArgumentException)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "[LOT MANAGEMENT SERVICE] Improperly formatted file."
                };
            }
            catch (Exception)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "[LOT MANAGEMENT SERVICE] Could not add lot."
                };
            }
        }

        public ResponseDTO<bool> DeleteLot(Guid lotId)
        {
            try
            {
                ResponseDTO<bool> response = _lotGateway.DeleteLot(lotId);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<Boolean>()
                {
                    Data = false,
                    Error = "[LOT MANAGEMENT SERVICE] Could not delete lot."
                };
            }
        }

        public ResponseDTO<Lot> GetLotByName(Guid ownerid, string name)
        {
            try
            {
                ResponseDTO<Lot> response = _lotGateway.GetLotByName(ownerid, name);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<Lot>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT SERVICE] Could not get lot by name."
                };
            }
        }

        public ResponseDTO<Lot> GetLotByLotId(Guid lotId)
        {
            return _lotGateway.GetLotByLotId(lotId);
        }

        public List<Spot> ParseSpotsFromFile(Guid lotid, string lotname, HttpPostedFile file)
        {
            List<Spot> response = new List<Spot>();
            try
            {
                //var lines = new List<string>();
                using (StreamReader reader = new StreamReader(file.InputStream))
                {
                    //string line;
                    while (reader.Peek() != -1) // while ((line = reader.ReadLine()) != null)
                    {
                        string line = reader.ReadLine();
                        string[] vars = line.Split(',');
                        Spot spot = new Spot()
                        {
                            SpotId = Guid.NewGuid(),
                            SpotName = vars[0],
                            LotId = lotid,
                            LotName = lotname,
                            IsHandicappedAccessible = (vars[1].Equals("true")) ? true : false
                        };
                        response.Add(spot);
                    }
                }
                return response;
            }
            catch (ArgumentException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ResponseDTO<Image> GetLotImage(string imagepath)
        {
            return _lotGateway.GetMapFile(imagepath);
        }

        public ResponseDTO<List<Lot>> GetAllLots()
        {
            try
            {
                ResponseDTO<List<Lot>> response = _lotGateway.GetAllLots();
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Lot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT SERVICE] Could not get lots."
                };
            }
        }

        public ResponseDTO<List<Lot>> GetAllLotsByOwner(Guid ownerid)
        {
            try
            {
                ResponseDTO<List<Lot>> response = _lotGateway.GetAllLotsByOwner(ownerid);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Lot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT SERVICE] Could not get lots."
                };
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpots()
        {
            try
            {
                ResponseDTO<List<Spot>> response = _lotGateway.GetAllSpots();
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Spot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT SERVICE] Could not get spots."
                };
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByOwner(Guid ownerid)
        {
            try
            {
                ResponseDTO<List<Spot>> response = _lotGateway.GetAllSpotsByOwner(ownerid);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Spot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT SERVICE] Could not get spots."
                };
            }
        }

        public ResponseDTO<List<Spot>> GetAllSpotsByLot(Guid lotId)
        {
            try
            {
                ResponseDTO<List<Spot>> response = _lotGateway.GetAllSpotsByLot(lotId);
                return response;
            }
            catch (Exception)
            {
                return new ResponseDTO<List<Spot>>()
                {
                    Data = null,
                    Error = "[LOT MANAGEMENT SERVICE] Could not get spots."
                };
            }
        }
    }
}
